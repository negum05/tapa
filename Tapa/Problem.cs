using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Tapa
{
	class Problem
	{
		// 問題生成をするディレクトリパス（GUIで指定）
		public static string directory_path = @"C:\Users\Amano\Desktop\rooptest_txt\";
		// 問題のファイル名
		public static string file_name = "tapa_problem.txt";
		// 問題のフルパス
		public static string file_path;
		// ぱずぷれの問題txt名
		public static string txt_path;

		private static int MIN_WHITEBOX_START_RATE = 45;
		private static int MAX_WHITEBOX_START_RATE = 55;


		// 数字マスを格納できる座標リスト
		private static List<Coordinates> can_be_number_whitebox_list = new List<Coordinates>();

		/*********************************
		 * 
		 * 初期盤面にランダムで白マスを配置する
		 *   
		 * *******************************/
		private void setRandomWhiteBox()
		{
			int whitebox_num = getRandomInt(Tapa.BOX_SUM * MIN_WHITEBOX_START_RATE / 100, Tapa.BOX_SUM * MAX_WHITEBOX_START_RATE / 100);

			// 白マス候補の未定マス
			List<Coordinates> candidate_notdeploy2whitebox_list = new List<Coordinates>(Tapa.not_deployedbox_coord_list);

			for (int i = whitebox_num; i > 0 && candidate_notdeploy2whitebox_list.Count > 0; i--) {
				// 白マス候補の未定マスからランダムにマスを選択
				Coordinates will_whitebox = candidate_notdeploy2whitebox_list[
					Problem.getRandomInt(0, candidate_notdeploy2whitebox_list.Count)];
				// 選択したマスを白色にする
				Tapa.box[will_whitebox.x][will_whitebox.y].Color = Box.WHITE;

				// 一繋がりの未定マス群リストを作成する
				Box.divideNotDeployedBoxToGroup();

				if (Tapa.isolation_notdeployedboxes_group_list.Count > 1) {
					Tapa.box[will_whitebox.x][will_whitebox.y].revision_color = Box.NOCOLOR;
					Tapa.not_deployedbox_coord_list.Add(will_whitebox);	// 今回選択したマスを未定マスリストへ追加
					i++;
				}
			}
		}


		/*********************************
		 * 
		 * 一繋がりの黒マスを生成する
		 * 始点はランダム
		 *   
		 * *******************************/
		private void makeBlackBoxRoute()
		{
			doTarjan(); // 切断点をcutpoint_listに格納
			foreach (Coordinates cp in cutpoint_list) {	// 切断点を黒マスにする
				Tapa.box[cp.x][cp.y].Color = Box.BLACK;
			}

			// 切断点が存在しなければ、未定マスからランダムに選択、それを黒マスにする。
			if (cutpoint_list.Count > 0) {
				Coordinates base_blackbox_coord
					= new Coordinates(Tapa.not_deployedbox_coord_list[
						getRandomInt(0, Tapa.not_deployedbox_coord_list.Count)]);
				Tapa.box[base_blackbox_coord.x][base_blackbox_coord.y].Color = Box.BLACK;
			}

			// 伸び代のある黒マスがなくなるまで以下を実行
			while (Tapa.edge_blackbox_coord_list.Count > 0) {
				// 伸び代のある黒マスリストからランダムに1つ選択、その黒マスから伸びることのできるマスをランダムに選択、そのマスを黒色にする。
				Coordinates new_blackbox_coord = getRandCoordAround(
					Tapa.edge_blackbox_coord_list[getRandomInt(0, Tapa.edge_blackbox_coord_list.Count)]);

				Tapa.box[new_blackbox_coord.x][new_blackbox_coord.y].Color = Box.BLACK;
				doTarjan();	// 切断点を黒マスにする
			}
			// 白マスに囲まれた未定マスが残っている時、それを白マスで埋める
			for (int i = Tapa.not_deployedbox_coord_list.Count - 1; i >= 0; i--) {
				Coordinates tmp_coord = Tapa.not_deployedbox_coord_list[i];
				Tapa.box[tmp_coord.x][tmp_coord.y].Color = Box.WHITE;
			}
		}

		/*********************************
		 * 
		 * Tarjanのアルゴリズムを用いて切断点をcutpoint_listに格納する
		 *   
		 * *******************************/

		// 自身に接している未定マスを保持
		private static Dictionary<Coordinates, List<Coordinates>> edge = new Dictionary<Coordinates, List<Coordinates>>();
		// DFSで辿り着いた番号を保持
		private static Dictionary<Coordinates, int> ord = new Dictionary<Coordinates, int>();
		// lowlink (DFSで使った有向辺を任意回、DFSで使わなかった辺を高々1回使ってたどり着ける頂点の中で最小のord値)を保持
		private static Dictionary<Coordinates, int> low = new Dictionary<Coordinates, int>();
		// 既に到達したかのフラグ true:到達済み
		private static Dictionary<Coordinates, bool> visited = new Dictionary<Coordinates, bool>();
		// DFS中の到達番号
		private static int reach_num;
		// 切断点を保持
		private static List<Coordinates> cutpoint_list = new List<Coordinates>();
		private void doTarjan()
		{
			edge.Clear();
			ord.Clear();
			low.Clear();
			cutpoint_list.Clear();

			// 任意の未定マス毎に接している未定マスを登録
			foreach (Coordinates tmp_co in Tapa.not_deployedbox_coord_list) {
				edge[tmp_co] = Box.getNoColorBoxCoordinatesAround(tmp_co);
				visited[tmp_co] = false;	// 到達済みフラグをfalseにする
			}

			reach_num = -1;
			foreach (Coordinates tmp_co in Tapa.not_deployedbox_coord_list) {
				if (!visited[tmp_co]) { doDFS(tmp_co, null); }
			}

		}

		/*********************************
		 * 
		 * DFSを行う過程で切断点をcutpoint_listに格納する
		 * 
		 * root以外	：ord[u] <= low[v]ならuは切断点
		 * root		：子が2つ以上で、子が部分木ならrootは切断点
		 * 
		 * 引数：
		 * u		：注目しているマス
		 * parent	：uの親
		 *   
		 * *******************************/
		private void doDFS(Coordinates u, Coordinates parent)
		{
			visited[u] = true;	// 到達済みフラグ
			ord[u] = low[u] = reach_num++;	// ord(とlow)の値を格納(lowは高々ord)

			int visit_children = 0;	// DFSで辿った子の数
			bool is_cutpoint = false;

			foreach (Coordinates v in edge[u]) {	// 自身の全ての子に対して
				if (!visited[v]) {
					visit_children++;
					doDFS(v, u);
					low[u] = low[u] < low[v] ? low[u] : low[v];	// 子のlowのほうが小さければ、lowを更新

					if (!u.Equals(Tapa.not_deployedbox_coord_list[0]) && ord[u] <= low[v]) {
						is_cutpoint = true;
					}
				}
				else if (!v.Equals(parent)) {	// 親以外の到達済みマスなら
					low[u] = low[u] < ord[v] ? low[u] : ord[v];	// 自身のlowと子のordのうち、小さい方でlowを更新
				}

			}
			if (u.Equals(Tapa.not_deployedbox_coord_list[0]) && visit_children >= 2) {
				is_cutpoint = true;
			}
			if (is_cutpoint) { cutpoint_list.Add(u); }
		}


		/*********************************
		 * 
		 * 白マスに入る数字を格納する
		 *   
		 * *******************************/
		private void setBoxNumber()
		{
			Box.during_make_inputbord = true;

			can_be_number_whitebox_list.Clear();
			for (int i = 1; i <= Tapa.MAX_BOARD_ROW; i++) {
				for (int j = 1; j <= Tapa.MAX_BOARD_COL; j++) {
					if (Tapa.box[i][j].Color == Box.WHITE) {
						Tapa.box[i][j].hasNum = true;
						Tapa.box[i][j].boxNum = getBoxNumber(new Coordinates(i, j));

						can_be_number_whitebox_list.Add(new Coordinates(i, j));
					}
				}
			}

			Box.during_make_inputbord = false;
		}

		/*********************************
		 * 
		 * co周りのマス色から、coに入る数字を返す。
		 * 
		 * 引数：
		 * co	: 数字を入れたいマス
		 *   
		 * *******************************/
		private int getBoxNumber(Coordinates co)
		{
			// 周囲8マスの色を取得
			List<int> around_boxcolor = new List<int> {
				Tapa.box[co.x-1][co.y-1].Color,	// 左上
				Tapa.box[co.x-1][co.y].Color,	// 上
				Tapa.box[co.x-1][co.y+1].Color,	// 右上
				Tapa.box[co.x][co.y+1].Color,	// 右
				Tapa.box[co.x+1][co.y+1].Color,	// 右下
				Tapa.box[co.x+1][co.y].Color,	// 下
				Tapa.box[co.x+1][co.y-1].Color,	// 左下
				Tapa.box[co.x][co.y-1].Color,	// 左
			};

			// 周囲8マスの連続した黒マスの数の格納用
			List<int> around_blackbox = new List<int>();
			bool is_counting = false;
			int count = 0;
			foreach (int color in around_boxcolor) {
				if (color == Box.BLACK) {
					is_counting = true;
					count++;
				}
				else if (color == Box.WHITE) {
					if (is_counting) {
						around_blackbox.Add(count);
						count = 0;
						is_counting = false;
					}
				}
			}
			if (around_boxcolor[7] == Box.BLACK) { around_blackbox.Add(count); }
			// 全てが黒マスでなく左上と左が黒マスだった場合、始めと最後に数えた黒マスの数を足して、改めて格納し直す。
			if (count != 8 && around_boxcolor[0] == Box.BLACK && around_boxcolor[7] == Box.BLACK) {
				around_blackbox[0] = around_blackbox[0] + around_blackbox[around_blackbox.Count - 1];
				around_blackbox.RemoveAt(around_blackbox.Count - 1);
			}

			around_blackbox.Sort();	// 昇順にソート
			int digit_pow = (int)Math.Pow(10, around_blackbox.Count - 1);	// 10^(桁数-1)
			int tmp_num = 0;
			foreach (int _num in around_blackbox) {
				tmp_num += _num * digit_pow;
				digit_pow /= 10;
			}
			return tmp_num;
		}

		/*********************************
		 * 
		 * 数字マスを追加して問題生成する。
		 * 引数：
		 * boxnumber_in_whitebox_coord_dict	: 数字マスの座標と数字の対応
		 *   
		 * *******************************/
		private void generateTapaProblemInAddNumBox(Dictionary<Coordinates, int> boxnumber_in_whitebox_coord_dict)
		{
			do {
				// 埋める数字マスをランダムに選択
				Coordinates adopting_boxnumber_coord = Problem.can_be_number_whitebox_list[
					   Problem.getRandomInt(0, Problem.can_be_number_whitebox_list.Count)];


				Box tmp_box = Tapa.box[adopting_boxnumber_coord.x][adopting_boxnumber_coord.y];

				// 選択した座標が既に配色済みの場合
				if (tmp_box.Color != Box.NOCOLOR) {
					Problem.can_be_number_whitebox_list.Remove(adopting_boxnumber_coord);	// 数字マスを格納できる座標リストから除外
					// boxnumber_in_whitebox_coord_dict.Remove(adopting_boxnumber_coord);		// ハッシュから除外
					continue;
				}

				Box.during_make_inputbord = true;

				tmp_box.Color = Box.WHITE;	// 選択した座標を白色にする
				tmp_box.hasNum = true;		// 数字を持ってるフラグをオンにする
				tmp_box.boxNum = boxnumber_in_whitebox_coord_dict[adopting_boxnumber_coord];	// 選択した座標に数字を格納
				Tapa.numbox_coord_list.Add(new Coordinates(adopting_boxnumber_coord));			// 数字マスリストに追加
				Tapa.not_deployedbox_coord_list.Remove(adopting_boxnumber_coord);				// 未定マスリストから除外
				PatternAroundNumBox.preparePatternArroundNumBox();								// 数字に対応したidを格納

				Problem.can_be_number_whitebox_list.Remove(adopting_boxnumber_coord);			// 数字マスを格納できる座標リストから除外
				boxnumber_in_whitebox_coord_dict.Remove(adopting_boxnumber_coord);				// ハッシュから除外

				Box.during_make_inputbord = false;

				Tapa.solveTapa(Tapa.REPEAT_NUM);


			} while (Problem.can_be_number_whitebox_list.Count > 0);

			Console.WriteLine("数字マスランダム配置後");
			Tapa.printBoard();


			while (Tapa.not_deployedbox_coord_list.Count > 0) {

				Console.WriteLine("未定マスが存在！！！！");

				// 白マス周りにある未定マスの数を格納
				Dictionary<Coordinates, int> count_whitebox_dict = new Dictionary<Coordinates, int>();

				foreach (Coordinates tmp_co in Tapa.not_deployedbox_coord_list) {
					List<Coordinates> whitebox_around_notdeployedbox_list = Box.getWhiteBoxCoordAround8(tmp_co);
					foreach (Coordinates tmp_whitebox_co in whitebox_around_notdeployedbox_list) {
						if (!count_whitebox_dict.ContainsKey(tmp_whitebox_co)) {
							count_whitebox_dict[tmp_whitebox_co] = 1;
						}
						else {
							count_whitebox_dict[tmp_whitebox_co]++;
						}
					}
				}

				int max = 0;
				Coordinates white2numbox = new Coordinates();	// 未定マスが最も多く周囲にある白マス
				foreach (KeyValuePair<Coordinates, int> pair in count_whitebox_dict) {
					if (pair.Value > max) { white2numbox = new Coordinates(pair.Key); }
				}

				Box.during_make_inputbord = true;

				Box tmp_box = Tapa.box[white2numbox.x][white2numbox.y];
				tmp_box.hasNum = true;
				tmp_box.boxNum = boxnumber_in_whitebox_coord_dict[white2numbox];	// 数字を格納 
				Tapa.numbox_coord_list.Add(new Coordinates(white2numbox));			// 数字マスリストに追加
				PatternAroundNumBox.preparePatternArroundNumBox();					// 数字に対応したidを格納

				Box.during_make_inputbord = false;

				Tapa.solveTapa(Tapa.REPEAT_NUM);

				if (Tapa.DEBUG) {
					Console.WriteLine("未定マス修正");
					Tapa.printBoard();
				}
			}

			// 盤面にある数字マス以外のマスを未定マスにする
			Tapa.numbox_coord_list.Clear();							// 数字マスの座標リスト
			Tapa.not_deployedbox_coord_list.Clear();				// 未定マスの座標リスト
			Tapa.isolation_notdeployedboxes_group_list.Clear();		// 一繋がりの未定マス群の座標リスト
			Tapa.edge_blackbox_coord_list.Clear();					// 伸び代のある黒マスの座標リスト
			Tapa.isolation_blackboxes_group_list.Clear();			// 一繋がりの黒マス群の座標リスト
			for (int i = 1; i <= Tapa.MAX_BOARD_ROW; i++) {
				for (int j = 1; j <= Tapa.MAX_BOARD_COL; j++) {
					Box tmp = Tapa.box[i][j];
					if (!tmp.hasNum) {
						tmp.revision_color = Box.NOCOLOR;
						Tapa.not_deployedbox_coord_list.Add(new Coordinates(tmp.coord));
					}
					else {
						Tapa.numbox_coord_list.Add(new Coordinates(tmp.coord));
					}
				}
			}
		}

		/*********************************
		 * 
		 * 数字マスを削除して問題生成する。
		 * 引数：
		 * boxnumber_in_whitebox_coord_dict	: 数字マスの座標と数字の対応
		 *   
		 * *******************************/
		private void generateTapaProblemInDeleteNumBox()
		{
			// 未削除の数字マスリストを現在残っている数字マスリストで更新
			Problem.can_be_number_whitebox_list = new List<Coordinates>(Tapa.numbox_coord_list);
			PatternAroundNumBox.preparePatternArroundNumBox();

			if (Tapa.DEBUG) {
				Console.WriteLine("ソート前");
				Tapa.printCoordList(Problem.can_be_number_whitebox_list);
			}

			// idの数で降順にソート
			// idが同数だったらヒント数で昇順にソート
			//Problem.can_be_number_whitebox_list.Sort(
			//	delegate(Coordinates co1, Coordinates co2) {
			//		int num1 = Tapa.box[co1.x][co1.y].id_list.Count;
			//		int num2 = Tapa.box[co2.x][co2.y].id_list.Count;

			//		if (num1 < num2) { return 1; }
			//		else if (num1 > num2) { return -1; }
			//		else {
			//			int hint1 = Tapa.box[co1.x][co1.y].min_hint;
			//			int hint2 = Tapa.box[co2.x][co2.y].min_hint;
			//			if (hint1 < hint2) { return -1; }
			//			else if (hint1 > hint2) { return 1; }
			//			else return 0;
			//		}
			//	});
			// ヒント数で昇順にソート
			// ヒント数が同数だったらidで降順にソート
			Problem.can_be_number_whitebox_list.Sort(
				delegate(Coordinates co1, Coordinates co2) {
					int hint1 = Tapa.box[co1.x][co1.y].min_hint;
					int hint2 = Tapa.box[co2.x][co2.y].min_hint;
					if (hint1 < hint2) { return -1; }
					else if (hint1 > hint2) { return 1; }
					else {
						int num1 = Tapa.box[co1.x][co1.y].id_list.Count;
						int num2 = Tapa.box[co2.x][co2.y].id_list.Count;
						if (num1 < num2) { return -1; }
						else if (num1 > num2) { return 1; }
						else return 0;
					}
				});
			Tapa.numbox_coord_list.Sort(
				delegate(Coordinates co1, Coordinates co2) {
					int hint1 = Tapa.box[co1.x][co1.y].min_hint;
					int hint2 = Tapa.box[co2.x][co2.y].min_hint;
					if (hint1 < hint2) { return 1; }
					else if (hint1 > hint2) { return -1; }
					else {
						int num1 = Tapa.box[co1.x][co1.y].id_list.Count;
						int num2 = Tapa.box[co2.x][co2.y].id_list.Count;
						if (num1 < num2) { return 1; }
						else if (num1 > num2) { return -1; }
						else return 0;
					}
				}
				);

			if (Tapa.DEBUG) {
				Console.WriteLine("ソート後");
				Tapa.printCoordList(Problem.can_be_number_whitebox_list);
			}

			// 数字マスを削除する前の盤面にある数字マスの数
			int first_numbox_num = Tapa.numbox_coord_list.Count;
			do {
				// 時間計測開始
				System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();

				// 現在の状態を保存
				StateSave save_point = new StateSave();
				StateSave.saveNowState(save_point);

				// 削除する数字マスをランダムに選択
				//Coordinates deleting_numbox_coord = Problem.can_be_number_whitebox_list[
				//	Problem.getRandomInt(0, Problem.can_be_number_whitebox_list.Count)];

				// idの数でソートされた数字マスリストの先頭を選択（削除する数字マス）
				Coordinates deleting_numbox_coord = Problem.can_be_number_whitebox_list[0];

				// 選択された数字マスを、未削除の数字マス座標リストから除外する。
				Problem.can_be_number_whitebox_list.Remove(deleting_numbox_coord);
				Box deleting_box = Tapa.box[deleting_numbox_coord.x][deleting_numbox_coord.y];	// 浅いコピー

				// 選択した数字マスを未定マスにする
				deleting_box.revision_color = Box.NOCOLOR;	// 選択した座標を未定マスにする
				deleting_box.hasNum = false;				// 数字を持ってるフラグをオフにする
				Tapa.numbox_coord_list.Remove(deleting_numbox_coord);							// 数字マスリストから除外
				Tapa.not_deployedbox_coord_list.Add(new Coordinates(deleting_numbox_coord));	// 未定マスリストに追加

				// 問題を解く
				Tapa.solveTapa(Tapa.REPEAT_NUM);

				// 解ならば復元後に、選択した数字マスをそのまま未定マスにする。
				if (Tapa.isCorrectAnswer()) {
					StateSave.loadSavedState(save_point);
					// 選択した数字マスを未定マスにする
					Tapa.box[deleting_numbox_coord.x][deleting_numbox_coord.y].revision_color = Box.NOCOLOR;	// 選択した座標を未定マスにする
					Tapa.box[deleting_numbox_coord.x][deleting_numbox_coord.y].hasNum = false;				// 数字を持ってるフラグをオフにする
					Tapa.numbox_coord_list.Remove(deleting_numbox_coord);							// 数字マスリストから除外
					Tapa.not_deployedbox_coord_list.Add(new Coordinates(deleting_numbox_coord));	// 未定マスリストに追加
				}
				else {
					// 解でないなら、盤面を復元するのみ。
					StateSave.loadSavedState(save_point);
				}

				//時間計測終了
				sw.Stop();
				deleting_numbox_coord.printCoordinates();
				Console.WriteLine("の削除可否判断にかかった時間 >> " + sw.Elapsed);

			} while (Problem.can_be_number_whitebox_list.Count > 0);

		}

		/*********************************
		 * 
		 * 問題を生成するプログラムを呼び出す。
		 * 引数
		 * pattern	:	呼び出すプログラムのid
		 * 1 >> 数字を追加して問題生成する
		 * 2 >> 数字を削除して問題生成する
		 *   
		 * *******************************/
		private void generateTapaPrblem(int pattern)
		{
			// 数字マスの座標とその数字を関連付けたハッシュ
			Dictionary<Coordinates, int> boxnumber_in_whitebox_coord_dict = new Dictionary<Coordinates, int>();
			foreach (Coordinates tmp_co in Problem.can_be_number_whitebox_list) {
				boxnumber_in_whitebox_coord_dict[tmp_co] = Tapa.box[tmp_co.x][tmp_co.y].boxNum;
			}

			// （黒マスListなども含む）盤面の初期化
			Tapa.resetBoard();

			switch (pattern) {
				case 0:
					generateTapaProblemInAddNumBox(boxnumber_in_whitebox_coord_dict);
					break;
				case 1:
					// 数字マスを全て配置する
					Box.during_make_inputbord = true;
					foreach (KeyValuePair<Coordinates, int> pair in boxnumber_in_whitebox_coord_dict) {
						Box tmp_box = Tapa.box[pair.Key.x][pair.Key.y];

						tmp_box.hasNum = true;		// 数字を持ってるフラグをオンにする
						tmp_box.boxNum = boxnumber_in_whitebox_coord_dict[pair.Key];	// 選択した座標に数字を格納
						Tapa.numbox_coord_list.Add(new Coordinates(pair.Key));			// 数字マスリストに追加
						Tapa.not_deployedbox_coord_list.Remove(pair.Key);				// 未定マスリストから除外
						PatternAroundNumBox.preparePatternArroundNumBox();				// 数字に対応したidを格納
					}
					Box.during_make_inputbord = false;

					generateTapaProblemInDeleteNumBox();
					break;
				case 2:
					generateTapaProblemInAddNumBox(boxnumber_in_whitebox_coord_dict);
					generateTapaProblemInDeleteNumBox();
					break;
			}

			

		}

		/*********************************
		 * 
		 * base_coordの上下左右の未定マスの座標をランダムで1つ返す
		 * 
		 * 引数
		 * base_coord	: 中心座標
		 *   
		 * *******************************/
		private static Coordinates getRandCoordAround(Coordinates base_coord)
		{
			List<Coordinates> extendable_coord_list = Box.getNoColorBoxCoordinatesAround(base_coord);
			if (extendable_coord_list.Count == 0) { return null; }
			return extendable_coord_list[getRandomInt(0, extendable_coord_list.Count)];
		}

		/*********************************
		 * 
		 * int型の乱数を返す
		 * 引数
		 * min	: 乱数の下限
		 * max	: 乱数の上限
		 *   
		 * *******************************/
		// TickCount:PCを最後に起動してからの経過時間（ミリ秒）
		static int seed = Environment.TickCount;		// 乱数の種
		public static int getRandomInt(int min, int max)
		{
			return new Random(seed++).Next(min, max);
		}

		/*********************************
		 * 
		 * ぱずぷれで作成したtxtファイルの読み込み
		 *   
		 * *******************************/
		private void readTapaTxt()
		{

			Problem.txt_path = @"C:\Users\Amano\OneDrive\zemi\dot_data\madoka\madoka.txt";

			List<string> line_list = new List<string>();
			using (StreamReader sr = new StreamReader(
				Problem.txt_path, Encoding.GetEncoding("Shift_JIS"))) {

				string line = "";

				while ((line = sr.ReadLine()) != null) {
					line_list.Add(line);
				}
			}

			Tapa.MAX_BOARD_ROW = Convert.ToInt32(line_list[2]);	// 3行目に行数
			Tapa.MAX_BOARD_COL = Convert.ToInt32(line_list[3]);	// 4行目に列数

			// 盤面生成
			Tapa.resetBoard();

			for (int i = 1; i <= Tapa.MAX_BOARD_ROW; i++) {
				string wk_str = line_list[i + 3];
				for (int j = 1; j <= Tapa.MAX_BOARD_COL; j++) {
					int pt = wk_str.IndexOf(' ');

					if (wk_str.Substring(0, pt) == "#") { Tapa.box[i][j].Color = Box.BLACK; }
					else { Tapa.box[i][j].Color = Box.WHITE; }

					wk_str = wk_str.Substring(pt + 1);
					
				}
			}

		}

		/*********************************
		 * 
		 * ぱずぷれv3用のtxtファイルを出力する
		 *   
		 * *******************************/
		private static void generateTapaProblemText()
		{
			// file_path = directory_path + Tapa.file_name;
			// file_path = MyCSVManagement.working_directory + Tapa.file_name;	// csv生成時
			file_path = @"C:\Users\Amano\OneDrive\zemi\dot_data\madoka\madoka_prob.txt";


			Console.WriteLine("file_path >> " + file_path);
			using (StreamWriter w = new StreamWriter(file_path)) {
				w.WriteLine("pzprv3\ntapa\n{0}\n{1}", Tapa.MAX_BOARD_ROW, Tapa.MAX_BOARD_COL);

				for (int i = 1; i <= Tapa.MAX_BOARD_ROW; i++) {
					String st = "";
					for (int j = 1; j <= Tapa.MAX_BOARD_COL; j++) {
						Box tmp_box = Tapa.box[i][j];
						if (tmp_box.hasNum) {
							// リストの作成
							List<int> tmp_box_num_list = new List<int>();
							do {              // 数字を桁毎にリストに追加
								tmp_box_num_list.Insert(0, tmp_box.boxNum % 10);
								tmp_box.boxNum /= 10;
							} while (tmp_box.boxNum > 0);  // do-whileは0の場合を許可するため

							foreach (int tmp_num in tmp_box_num_list) {
								st += tmp_num.ToString() + ",";
							}
							st = st.Remove(st.Length - 1);
						}
						else {
							st += ".";
						}
						st += " ";
					}
					w.WriteLine(st);
				}
			}
		}

		public static void manageMakingProblem()
		{
			Problem p; 

			do {
				Tapa.resetBoard();
				p = new Problem();

				p.setRandomWhiteBox();
				p.makeBlackBoxRoute();
			} while (!Tapa.isCorrectAnswer());

			

			p.readTapaTxt();

			p.setBoxNumber();

			

			p.generateTapaPrblem(1);

			Tapa.processnum_numbox = Tapa.numbox_coord_list.Count;

			generateTapaProblemText();

			
		}
	}
}

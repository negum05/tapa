using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tapa
{
	class Problem
	{
		int MIN_WHITEBOX_START_RATE = 40;
		int MAX_WHITEBOX_START_RATE = 60;

		/*********************************
		 * 
		 * 問題生成のための初期盤面を生成
		 * （外枠：白マス　内側：未定マス）
		 *   
		 * *******************************/
		private void makeBasicBoard()
		{
			// 色を塗る以外の処理をColorプロパティ内で行わないため
			Box.during_make_inputbord = true;

			for (int i = 1; i <= Tapa.MAX_BOARD_ROW; i++) {
				List<Box> tmp_box_list = new List<Box>();
				for (int j = 1; j <= Tapa.MAX_BOARD_COL; j++) {
					Box tmp_box = new Box();
					tmp_box.coord = new Coordinates(i, j);
					tmp_box.Color = Box.NOCOLOR;
					tmp_box_list.Add(tmp_box);
					Tapa.not_deployedbox_coord_list.Add(tmp_box.coord);		// 未定マスの座標Listに追加
				}
				Tapa.box.Add(tmp_box_list);
			}
			Tapa.makeOuterBox(Tapa.MAX_BOARD_ROW, Tapa.MAX_BOARD_COL);
			
			Box.during_make_inputbord = false;


			Tapa.printBoard();
		}

		/*********************************
		 * 
		 * 初期盤面にランダムで白マスを配置する
		 *   
		 * *******************************/
		private void setRandomWhiteBox()
		{
			int whitebox_num = getRandomInt(Tapa.BOX_SUM * MIN_WHITEBOX_START_RATE / 100, Tapa.BOX_SUM * MAX_WHITEBOX_START_RATE / 100);
			Console.WriteLine("start_whitebox_num >> " + whitebox_num);

			for (int i = whitebox_num; i > 0; i--) {
				// 未定マスからランダムにマスを選択
				Coordinates will_whitebox = Tapa.not_deployedbox_coord_list[
					Problem.getRandomInt(0, Tapa.not_deployedbox_coord_list.Count)];
				// 選択したマスを白色にする
				Tapa.box[will_whitebox.x][will_whitebox.y].Color = Box.WHITE;

				// 一繋がりの未定マス群リストを作成する
				Box.divideNotDeployedBoxToGroup();
				
				if (Tapa.isolation_notdeployedboxes_group_list.Count > 1) {
					Tapa.box[will_whitebox.x][will_whitebox.y].revision_color = Box.NOCOLOR;
					Tapa.not_deployedbox_coord_list.Add(will_whitebox);	// 今回選択したマスを未定マスリストへ追加
					i++;
				}

				Tapa.printBoard();
				Console.WriteLine();

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

				Tapa.printBoard();
				Console.WriteLine();
			}
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
			foreach(Coordinates tmp_co in Tapa.not_deployedbox_coord_list) {
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
			// 白マスリストの作成
			List<Coordinates> whitebox_list = new List<Coordinates>();
			for (int i = 1; i <= Tapa.MAX_BOARD_ROW; i++) {
				for (int j = 1; j <= Tapa.MAX_BOARD_COL; j++ ) {
					if (Tapa.box[i][j].Color == Box.WHITE) {
						whitebox_list.Add(new Coordinates(i, j));
					}
				}
			}

			Box.during_make_inputbord = true;
			foreach (Coordinates tmp_co in whitebox_list) {
				Tapa.box[tmp_co.x][tmp_co.y].hasNum = true;
				Tapa.box[tmp_co.x][tmp_co.y].box_num = getBoxNumber(tmp_co);
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

			co.printCoordinates();
			Console.Write("周りの色 >> ");
			foreach (int tmp in around_boxcolor) { Console.Write(" " + tmp); }
			Console.WriteLine();

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

			Console.Write("list内1 >> ");
			foreach (int tmp in around_blackbox) { Console.Write(" " + tmp); }
			Console.WriteLine();
			
			// 全てが黒マスでなく左上と左が黒マスだった場合、始めと最後に数えた黒マスの数を足して、改めて格納する。
			if (count != 8 && around_boxcolor[0] == Box.BLACK && around_boxcolor[7] == Box.BLACK) {
				around_blackbox[0] = around_blackbox[0] + around_blackbox[around_blackbox.Count - 1];
				around_blackbox.RemoveAt(around_blackbox.Count - 1);
			}

			around_blackbox.Sort();	// 昇順にソート

			Console.Write("list内2 >> ");
			foreach (int tmp in around_blackbox) { Console.Write(" " + tmp); }
			Console.WriteLine();

			int digit_pow = (int)Math.Pow(10, around_blackbox.Count - 1);	// 10^(桁数-1)
			int tmp_num = 0;
			foreach (int _num in around_blackbox) {
				tmp_num += _num * digit_pow;
				digit_pow /= 10;
			}

			Console.WriteLine("数字 >> " + tmp_num + "\n");

			return tmp_num;			
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

		public static void manageMakingProblem()
		{
			Problem p = new Problem();
			p.makeBasicBoard();
			p.setRandomWhiteBox();
			p.makeBlackBoxRoute();
			p.setBoxNumber();
			Tapa.printBoard();
		}
	}
}

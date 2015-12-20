using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Tapa
{
	class Box
	{
		public static readonly int NOCOLOR = 0;    // 色未定
		public static readonly int WHITE = -1;       // 白色
		public static readonly int BLACK = 1;       // 黒色

		public static bool during_clone = false;	// true:クローン処理中
		public static bool during_make_inputbord = false;	// true:外周の冗長なマスを作成中

		public Coordinates coord;					// 座標、黒マス群の識別名
		public int changed_count_in_search_confirm_box { get; set; }		// 異なる色に変更された回数
		private bool has_num = false;				// 数字を持っているか
		public bool hasNum
		{
			get { return this.has_num; }
			set
			{
				if (value) { this.Color = Box.WHITE; }			// 数字マスは白色
				this.has_num = value;
			}
		}
		public int box_num;							// マスの数字
		public List<byte> id_list;                  // id(数字マス周りのパターンの識別子)のリスト
		private int color;							// マスの色
		public int Color
		{
			get { return this.color; }
			set
			{
				// 異なる色に変更されそうになればインクリメント (id_listから色の確定するマスを探す際に使用)
				if (this.color != value) { changed_count_in_search_confirm_box++; }
				if (this.color == Box.NOCOLOR) {
					this.color = value;
					Tapa.was_change_board = true;
					if (!during_clone && !Box.during_make_inputbord) {	// (クローン処理中 or 盤面入力中)はマスをリストに追加しない
						// 塗る色が黒かつ伸び代があれば、伸び代のある黒マスリストに追加
						if (this.color == Box.BLACK) {
							// 塗られたマス周りで黒マスの団子ができないよう白マスを配置
							avoidDumpling(Tapa.box[this.coord.x][this.coord.y].coord);
							//this.coord.printCoordinates();
							//Console.Write(" Color: " + this.color + " value:" + value);
							//Console.Write("\n");
							if (canExtendBlackBox(this.coord)) {
								this.can_extend_blackbox = true;
								Tapa.edge_blackbox_coord_list.Add(this.coord);
							}
							// 接している一繋がりの黒マス群に追加/結合
							Box.divideBlackBoxToGroup(this.coord);
						}
						// 色の塗られたマスの上下左右の黒マスの伸び代をチェック/変更する
						resetExtendableBlackBoxAround(this.coord);
						Tapa.not_deployedbox_coord_list.Remove(this.coord);	// 未定マスリストから除外
						if (Tapa.DEBUG_PRINT_PROCESS) {
							this.coord.printCoordinates();
							Console.Write(" : ");
							Tapa.printNowStateProcess();
							Console.WriteLine();
						}
					}
				}
			}
		}
		public bool can_extend_blackbox { get; set; }			// true:伸び代のある黒マス
		public int id_not_deployedbox_group { get; set; }	// 一繋がりの未定マス群のid

		public Box()
		{
			this.coord = new Coordinates();
			changed_count_in_search_confirm_box = 0;
			hasNum = false;
			this.box_num = -1;
			this.id_list = new List<byte>();
			this.color = Box.NOCOLOR;
			this.can_extend_blackbox = false;
			this.id_not_deployedbox_group = 0;
		}

		public Box(Box origin_box)
		{
			this.coord = new Coordinates(origin_box.coord);
			this.changed_count_in_search_confirm_box = origin_box.changed_count_in_search_confirm_box;
			this.hasNum = origin_box.has_num;
			this.box_num = origin_box.box_num;
			this.id_list = new List<byte>();
			if (origin_box.id_list.Count > 0) {
				foreach (byte tmp_id in origin_box.id_list) {
					this.id_list.Add(tmp_id);
				}
			}
			this.color = origin_box.color;
			this.can_extend_blackbox = origin_box.can_extend_blackbox;
			this.id_not_deployedbox_group = origin_box.id_not_deployedbox_group;
		}

		/*********************************
		 * 
		 * マスの状態を初期化
		 * 初期値
		 * co		: -100, -100
		 * changed_count_in_search_confirm_box	: 0
		 * has_num	: 0
		 * box_num	: -1
		 * id_list	: null
		 * color	: Box.NOCOLOR
		 * can_extend_blackbox	: false
		 * id_not_deployedbox_group	: 0
		 *   
		 * *******************************/
		public void clear()
		{
			this.coord = new Coordinates();
			this.changed_count_in_search_confirm_box = 0;
			this.hasNum = false;
			this.box_num = -1;
			this.id_list.Clear();
			this.Color = Box.NOCOLOR;
			this.can_extend_blackbox = false;
			this.id_not_deployedbox_group = 0;
		}

		/*********************************
		 * 
		 * 数字マスの数字の表示
		 *   
		 * *******************************/
		static string wb = "□";
		static string bb = "■";
		static string non = "－";
		public void printBoxNum()
		{
			if (!this.has_num) {
				if (this.color == Box.WHITE) { Console.Write(wb + wb + "　"); }
				else if (this.color == Box.BLACK) { Console.Write(bb + bb + "　"); }
				else { Console.Write(non + non + "　"); }
			}
			else {
				int space = this.box_num.ToString().Length;
				int rest = 4 - space;
				Console.Write(this.box_num);
				// while (space-- > 0) { Console.Write(" "); }
				while (rest-- > 0) { Console.Write(" "); }
				Console.Write("　");
			}
		}

		/*********************************
		* 
		* 数字マスのid_listの表示
		*   
		* *******************************/
		public void printIdList()
		{
			if (!this.has_num) {
				Console.WriteLine("Error: id_listのないマスからid_listを出力しようとしています。");
				Application.Exit();
			}
			else {
				foreach (byte tmp_id in id_list) {
					Console.Write(tmp_id.ToString() + " ");
				}
			}
		}

		/*********************************
		* 
		* co(新しく白マスになった座標)周りの黒マスを見て、
		* coが白マスになったことで伸び代がなくなった黒マスの伸び代フラグをオフにし、
		* 伸び代のある黒マスリストから除外する。
		* その黒マスが所属する孤立した黒マス群リストを、伸び代のある黒マスが前にくるようソートする。
		* 
		* 引数
		* co	: 白マスの座標
		*   
		* *******************************/
		private static void resetExtendableBlackBoxAround(Coordinates co)
		{
			// [浅いコピー]	上下左右のBoxのリスト
			List<Box> around_box = new List<Box> {
				Tapa.box[co.x-1][co.y],		// 上
				Tapa.box[co.x][co.y+1],		// 右
				Tapa.box[co.x+1][co.y],		// 下
				Tapa.box[co.x][co.y-1]		// 左
			};

			foreach (Box tmp_box in around_box) {
				// 伸び代のflagがオン、かつ四方のマスが配色済みなら、
				// 伸び代のflagをオフにし、伸び代のある黒マスリストから除外する。
				if (tmp_box.can_extend_blackbox && !canExtendBlackBox(tmp_box.coord)) {
					tmp_box.can_extend_blackbox = false;
					Tapa.edge_blackbox_coord_list.Remove(tmp_box.coord);
					//// 伸び代のなくなった黒マスが所属していた、孤立した黒マス群をソートする。
					//for (int ite_iso_list = Tapa.isolation_blackboxes_group_list.Count - 1; ite_iso_list >= 0; ite_iso_list--) {
					//	if (Tapa.isolation_blackboxes_group_list[ite_iso_list].Contains(tmp_box.coord)) {
					//		List<Coordinates> sorted_coord_list = sortFrontExtendableList(Tapa.isolation_blackboxes_group_list[ite_iso_list]);
					//		Tapa.isolation_blackboxes_group_list[ite_iso_list].RemoveRange(0, sorted_coord_list.Count);	// ソート前のリストを削除
					//		Tapa.isolation_blackboxes_group_list[ite_iso_list].InsertRange(0, sorted_coord_list);		// 空になったリストにソート後のリストを挿入
					//	}
					//}
				}
			}

		}

		/*********************************
		* 
		* co(新しく黒マスになった座標)周りの孤立した黒マス群を1つの黒マス群に結合する。
		* 引数
		* co	: 黒マスの座標
		*   
		* *******************************/
		private static void divideBlackBoxToGroup(Coordinates co)
		{
			// [浅いコピー]	上下左右のBoxのリスト	
			List<Box> around_box = new List<Box> {
				Tapa.box[co.x-1][co.y],		// 上
				Tapa.box[co.x][co.y+1],		// 右
				Tapa.box[co.x+1][co.y],		// 下
				Tapa.box[co.x][co.y-1]		// 左
			};

			// 上下左右にある黒マスの座標リスト
			List<Coordinates> around_box_coord = new List<Coordinates>();
			foreach (Box tmp_box in around_box) {
				if (tmp_box.Color == Box.BLACK) {
					around_box_coord.Add(tmp_box.coord);
				}
			}

			//Console.Write("上下左右の黒マス >> ");
			//Tapa.printCoordList(around_box_coord);

			int count_arround_box_coord = around_box_coord.Count;
			// 上下左右に黒マスがない場合、自身を新しい孤立した黒マス群としてリストに追加
			if (count_arround_box_coord == 0) {
				Tapa.isolation_blackboxes_group_list.Add(new List<Coordinates>() { co });

				//Console.Write("divide(孤立): " + count_arround_box_coord);
				//co.printCoordinates();
				//Console.Write("\n");
			}
			else {
				//Console.Write("divide(結合):" + count_arround_box_coord);
				//co.printCoordinates();
				//Console.Write("\n");

				List<Coordinates> merged_group_list = new List<Coordinates>() { co };	// 結合したリストの保存用
				// true:（上下左右の黒マスの）添字番目の黒マスのリストを結合済み（添字がarround_box_coordと対応）
				bool[] was_checked_co = Enumerable.Repeat<bool>(false, count_arround_box_coord).ToArray();

				for (int ite_iso_group = Tapa.isolation_blackboxes_group_list.Count - 1; ite_iso_group >= 0; ite_iso_group--) {
					for (int ite_arround_box = count_arround_box_coord - 1; ite_arround_box >= 0; ite_arround_box--) {

						//Console.Write("[1]");
						//around_box_coord[ite_arround_box].printCoordinates();

						if (!was_checked_co[ite_arround_box]
							&& Tapa.isolation_blackboxes_group_list[ite_iso_group].Contains(around_box_coord[ite_arround_box])) {

							//Console.Write("[2]");
							//around_box_coord[ite_arround_box].printCoordinates();

							// 新たに結合されるリストに孤立していた黒マス群を追加
							merged_group_list.AddRange(new List<Coordinates>(Tapa.isolation_blackboxes_group_list[ite_iso_group]));
							// 直前でmerged_group_listに追加したリストの元を孤立する黒マス群から削除
							Tapa.isolation_blackboxes_group_list.RemoveAt(ite_iso_group);
							// ite_arround_box番目の黒マスのリストを結合したのでtrueにする。
							was_checked_co[ite_arround_box] = true;
							break;
						}
					}
				}
				// 今回の結合で伸び代の無くなった黒マスを伸び代のある黒マスリストから除外
				// resetExtendableBlackBoxAround(co);
				for (int i = Tapa.edge_blackbox_coord_list.Count - 1; i >= 0; i--) {
					Coordinates tmp_co = Tapa.edge_blackbox_coord_list[i];
					if (!Box.canExtendBlackBox(tmp_co)) {
						Tapa.box[tmp_co.x][tmp_co.y].can_extend_blackbox = false;
						Tapa.edge_blackbox_coord_list.RemoveAt(i);
					}
				}
				// 今回新たに結合された孤立した黒マスリストを、孤立した黒マス群リストに追加。
				Tapa.isolation_blackboxes_group_list.Add(new List<Coordinates>(merged_group_list));
			}
		}

		/*********************************
		 * 
		 * 引数で受け取った座標リストのうち、
		 * 伸び代のある黒マスを前側にソートし、そのリストを返す。
		 * 引数
		 * origin_list	: （一繋がりの黒マス群の各）マスの座標リスト
		 *   
		 * *******************************/
		//private static List<Coordinates> sortFrontExtendableList(List<Coordinates> origin_list)
		//{
		//	List<Coordinates> sorted_list = new List<Coordinates>(origin_list.Count);

		//	foreach (Coordinates tmp_coord in origin_list) {
		//		if (Tapa.box[tmp_coord.x][tmp_coord.y].can_extend_blackbox) {
		//			sorted_list.Insert(0, tmp_coord);
		//		}
		//		else {
		//			sorted_list.Add(tmp_coord);
		//		}
		//	}

		//	return sorted_list;
		//}

		/*********************************
		 * 
		 * 座標coのマスの上下左右にある黒マスの数を返す。
		 * 引数
		 * co	: マスの座標
		 *   
		 * *******************************/
		//private static int countBlackBoxAround(Coordinates co)
		//{
		//	List<int> around_colors = new List<int> {
		//		Tapa.box[co.x-1][co.y].Color,
		//		Tapa.box[co.x][co.y+1].Color,
		//		Tapa.box[co.x+1][co.y].Color,
		//		Tapa.box[co.x][co.y-1].Color
		//	};

		//	int count = 0;
		//	// [色と定数の関係] Box.WHITE : 0	Box.BLACK : 1	Box.NOCOLOR : -1
		//	foreach (int tmp_color in around_colors) {
		//		if (tmp_color == Box.BLACK) {
		//			count++;
		//		}
		//	}
		//	return count;
		//}

		/*********************************
		* 
		* 座標coのマスの上下左右にある未定マスの数を返す。
		* 引数
		* co	: マスの座標
		*   
		* *******************************/
		private static int countNoColorBoxAround(Coordinates co)
		{
			List<int> around_colors = new List<int> {
				Tapa.box[co.x-1][co.y].Color,
				Tapa.box[co.x][co.y+1].Color,
				Tapa.box[co.x+1][co.y].Color,
				Tapa.box[co.x][co.y-1].Color
			};

			int count = 0;
			// [色と定数の関係] Box.WHITE : -1	Box.BLACK : 1	Box.NOCOLOR : 0
			foreach (int tmp_color in around_colors) {
				if (tmp_color == Box.NOCOLOR) {
					count++;
				}
			}
			return count;
		}

		/*********************************
		* 
		* 座標coのマスの上下左右にある未定マスの座標をリストで返す。
		* 引数
		* co	: マスの座標
		*   
		* *******************************/
		private static List<Coordinates> getNoColorBoxCoordinatesAround(Coordinates co)
		{
			List<Coordinates> tmp_coord_list = new List<Coordinates>();

			List<Box> around_box = new List<Box> {
				Tapa.box[co.x-1][co.y],
				Tapa.box[co.x][co.y+1],
				Tapa.box[co.x+1][co.y],
				Tapa.box[co.x][co.y-1]
			};

			// [色と定数の関係] Box.WHITE : -1	Box.BLACK : 1	Box.NOCOLOR : 0
			foreach (Box tmp_box in around_box) {
				if (tmp_box.Color == Box.NOCOLOR) {
					tmp_coord_list.Add(new Coordinates(tmp_box.coord));
				}
			}
			return tmp_coord_list;
		}

		/*********************************
		 * 
		 * 座標coのマスの上下左右に１つでも未定マスがあればtrueを返す。
		 * 引数
		 * co	: 黒マスの座標
		 *   
		 * *******************************/
		private static bool canExtendBlackBox(Coordinates co)
		{
			if (Tapa.box[co.x][co.y].Color != Box.BLACK) {
				Console.WriteLine("Error: canExtendBlackBoxに黒マス以外の座標を引数として渡しています。({0},{1})", co.x, co.y);
				Application.Exit();
			}

			if (Tapa.box[co.x - 1][co.y].Color != Box.NOCOLOR		// 上のマス色
				&& Tapa.box[co.x][co.y + 1].Color != Box.NOCOLOR	// 右のマス色
				&& Tapa.box[co.x + 1][co.y].Color != Box.NOCOLOR	// 下のマス色
				&& Tapa.box[co.x][co.y - 1].Color != Box.NOCOLOR) {	// 左のマス色
				return false;
			}
			return true;
		}

		/*********************************
		 * 
		 * 座標coのマスの上下左右に１つでも黒マスがあればtrueを返す。
		 * 引数
		 * co	: 黒マスの座標
		 *   
		 * *******************************/
		public static bool existBlackBoxAround(Coordinates co)
		{
			if (Tapa.box[co.x - 1][co.y].Color != Box.BLACK		// 上のマス色
				&& Tapa.box[co.x][co.y + 1].Color != Box.BLACK	// 右のマス色
				&& Tapa.box[co.x + 1][co.y].Color != Box.BLACK	// 下のマス色
				&& Tapa.box[co.x][co.y - 1].Color != Box.BLACK) {	// 左のマス色
				return false;
			}
			return true;
		}

		/*********************************
		 * 
		 * 黒マスの上下左右で3つが白マスかつ1つが未定マスならそれを黒にし、
		 * 伸び代のある黒マスリスト（edge_blackbox_coord_list）から除外する。
		 * 引数
		 * ite_coord	: 伸び代のある黒マスリストの要素番号
		 *   
		 * *******************************/
		//private static void extendBlackBox(int ite_coord)
		//{
		//	Coordinates tmp_coord = new Coordinates(Tapa.edge_blackbox_coord_list[ite_coord]);

		//	Box T = new Box(Tapa.box[tmp_coord.x - 1][tmp_coord.y]);	// TOP
		//	Box R = new Box(Tapa.box[tmp_coord.x][tmp_coord.y + 1]);	// RIGHT
		//	Box B = new Box(Tapa.box[tmp_coord.x + 1][tmp_coord.y]);	// BOTTOM
		//	Box L = new Box(Tapa.box[tmp_coord.x][tmp_coord.y - 1]);	// LEFT

		//	// 伸び代のある黒マスが予期せぬ動きをしたら見てみるといいかも
		//	//Console.WriteLine("(" + tmp_coord.x + "," + tmp_coord.y + ')');
		//	//Console.WriteLine("T >> " + "(" + T.coord.x + "," + T.coord.y + ") " + T.color.ToString() + " " + T.hasNum.ToString());
		//	//Console.WriteLine("R >> " + "(" + R.coord.x + "," + R.coord.y + ") " + R.color.ToString() + " " + R.hasNum.ToString());
		//	//Console.WriteLine("B >> " + "(" + B.coord.x + "," + B.coord.y + ") " + B.color.ToString() + " " + B.hasNum.ToString());
		//	//Console.WriteLine("L >> " + "(" + L.coord.x + "," + L.coord.y + ") " + L.color.ToString() + " " + L.hasNum.ToString());
		//	//Console.WriteLine();

		//	if (T.Color == Box.NOCOLOR && R.Color == Box.WHITE && B.Color == Box.WHITE && L.Color == Box.WHITE) {
		//		Tapa.box[tmp_coord.x - 1][tmp_coord.y].Color = Box.BLACK;	// 上のマスを黒に
		//		Tapa.edge_blackbox_coord_list.RemoveAt(ite_coord);
		//	}
		//	else if (T.Color == Box.WHITE && R.Color == Box.NOCOLOR && B.Color == Box.WHITE && L.Color == Box.WHITE) {
		//		Tapa.box[tmp_coord.x][tmp_coord.y + 1].Color = Box.BLACK;	// 右のマスを黒に
		//		Tapa.edge_blackbox_coord_list.RemoveAt(ite_coord);
		//	}
		//	else if (T.Color == Box.WHITE && R.Color == Box.WHITE && B.Color == Box.NOCOLOR && L.Color == Box.WHITE) {
		//		Tapa.box[tmp_coord.x + 1][tmp_coord.y].Color = Box.BLACK;	// 下のマスを黒に
		//		Tapa.edge_blackbox_coord_list.RemoveAt(ite_coord);
		//	}
		//	else if (T.Color == Box.WHITE && R.Color == Box.WHITE && B.Color == Box.WHITE && L.Color == Box.NOCOLOR) {
		//		Tapa.box[tmp_coord.x][tmp_coord.y - 1].Color = Box.BLACK;	// 左のマスを黒に
		//		Tapa.edge_blackbox_coord_list.RemoveAt(ite_coord);
		//	}
		//}

		/*********************************
		 * 
		 * 黒マスが2*2の四角（団子、Dumpling）にならないように白マスを配置するメソッド
		 * 、
		 * 黒マスの周囲8マスを見てその未定マスが黒になると団子になってしまう場合、
		 * そのマスを白マスにする。
		 * 引数
		 * co	: (黒)マスの座標
		 *   
		 * *******************************/
		private static void avoidDumpling(Coordinates co)
		{
			// ### 浅いコピー
			Box TL = Tapa.box[co.x - 1][co.y - 1];	// (左上)	Top-Left
			Box TC = Tapa.box[co.x - 1][co.y];		// (上)		Top-Center
			Box TR = Tapa.box[co.x - 1][co.y + 1];	// (右上)	Top-Right
			Box ML = Tapa.box[co.x][co.y - 1];		// (左)		Middle-Left
			Box MC = Tapa.box[co.x][co.y];			// (真ん中)	Middle-Center
			Box MR = Tapa.box[co.x][co.y + 1];		// (右)		Middle-Right
			Box BR = Tapa.box[co.x + 1][co.y + 1];	// (右下)	Bottom-Right
			Box BC = Tapa.box[co.x + 1][co.y];		// (下)		Bottom-Center
			Box BL = Tapa.box[co.x + 1][co.y - 1];	// (左下)	Bottom-Left

			if (TL.Color + TC.Color + ML.Color + MC.Color == 3) {			// 左上
				if (TL.Color == Box.NOCOLOR) { TL.Color = Box.WHITE; }
				else if (TC.Color == Box.NOCOLOR) { TC.Color = Box.WHITE; }
				else if (ML.Color == Box.NOCOLOR) { ML.Color = Box.WHITE; }
				else if (MC.Color == Box.NOCOLOR) { MC.Color = Box.WHITE; }
			}
			else if (TC.Color + TR.Color + MC.Color + MR.Color == 3) {		// 右上
				if (TC.Color == Box.NOCOLOR) { TC.Color = Box.WHITE; }
				else if (TR.Color == Box.NOCOLOR) { TR.Color = Box.WHITE; }
				else if (MC.Color == Box.NOCOLOR) { MC.Color = Box.WHITE; }
				else if (MR.Color == Box.NOCOLOR) { MR.Color = Box.WHITE; }
			}
			else if (ML.Color + MC.Color + BL.Color + BC.Color == 3) {		// 左下
				if (ML.Color == Box.NOCOLOR) { ML.Color = Box.WHITE; }
				else if (MC.Color == Box.NOCOLOR) { MC.Color = Box.WHITE; }
				else if (BL.Color == Box.NOCOLOR) { BL.Color = Box.WHITE; }
				else if (BC.Color == Box.NOCOLOR) { BC.Color = Box.WHITE; }
			}
			else if (MC.Color + MR.Color + BC.Color + BR.Color == 3) {		// 右下
				if (MC.Color == Box.NOCOLOR) { MC.Color = Box.WHITE; }
				else if (MR.Color == Box.NOCOLOR) { MR.Color = Box.WHITE; }
				else if (BC.Color == Box.NOCOLOR) { BC.Color = Box.WHITE; }
				else if (BR.Color == Box.NOCOLOR) { BR.Color = Box.WHITE; }
			}

			//if (TC.Color == Box.NOCOLOR) {	// 上マスが黒になると団子になるか
			//	if ((ML.Color == Box.BLACK && TL.Color == Box.BLACK)
			//		|| (TR.Color == Box.BLACK && MR.Color == Box.BLACK)) {
			//		TC.Color = Box.WHITE;
			//	}
			//}
			//else if (MR.Color == Box.NOCOLOR) {	// 右マスが黒になると団子になるか
			//	if ((TC.Color == Box.BLACK && TR.Color == Box.BLACK)
			//		|| (BR.Color == Box.BLACK && BC.Color == Box.BLACK)) {
			//		MR.Color = Box.WHITE;
			//	}
			//}
			//else if (BC.Color == Box.NOCOLOR) {	// 下マスが黒になると団子になるか
			//	if ((MR.Color == Box.BLACK && BR.Color == Box.BLACK)
			//		|| (BL.Color == Box.BLACK && ML.Color == Box.BLACK)) {
			//		BC.Color = Box.WHITE;
			//	}
			//}
			//else if (ML.Color == Box.NOCOLOR) {	// 左マスが黒になると団子になるか
			//	if ((BC.Color == Box.BLACK && BL.Color == Box.BLACK)
			//		|| (TL.Color == Box.BLACK && TC.Color == Box.BLACK)) {
			//		ML.Color = Box.WHITE;
			//	}
			//}
		}

		/*********************************
		 * 
		 * 孤立した黒マス群の伸び代が1マスだけの時、そのマスを黒に塗る。
		 * 全ての黒マス群をチェックし、もし上の処理をすれば、また一から黒マス群を見直す（リストが再構成されるため）。
		 * 先頭の黒マス群から最後の黒マス群までチェックして、1度も変化しなければ終了。
		 *   
		 * *******************************/
		private static void extendIsolationBlackBoxGroup()
		{
			for (int ite_iso_group_list = 0; ite_iso_group_list < Tapa.isolation_blackboxes_group_list.Count; ite_iso_group_list++) {
				List<Coordinates> tmp_iso_group = Tapa.isolation_blackboxes_group_list[ite_iso_group_list];
				int count_extendable_blackbox = 0;						// 孤立した黒マス群にある伸び代のある黒マスの数
				Coordinates last_extendable_coord = new Coordinates();	// 孤立した黒マス群にある伸び代のある黒マスの内、リストの最も後ろにある座標。
				foreach (Coordinates tmp_coord in tmp_iso_group) {
					if (Tapa.box[tmp_coord.x][tmp_coord.y].can_extend_blackbox) {
						count_extendable_blackbox++;
						last_extendable_coord = new Coordinates(tmp_coord);
					}
				}
				// 孤立した黒マス群の内、伸び代のある黒マスが1つ
				// かつその黒マスの周りに未定のマスが1つだけの場合、その未定マスを黒に塗る。
				if (count_extendable_blackbox == 1 && Box.countNoColorBoxAround(last_extendable_coord) == 1) {
					// 未定マスの座標を取得
					List<Coordinates> around_undeployed_coord = Box.getNoColorBoxCoordinatesAround(last_extendable_coord);
					Tapa.box[around_undeployed_coord[0].x][around_undeployed_coord[0].y].Color = Box.BLACK;
					extendIsolationBlackBoxGroup();
				}
			}
		}

		/*********************************
		 * 
		 * 盤面に孤立した黒マス群がないか調べる
		 * true		: ない
		 * false	: ある
		 *   
		 * *******************************/
		public static bool checkNotIsolationBlackBoxGroup()
		{
			for (int ite_iso_group_list = Tapa.isolation_blackboxes_group_list.Count - 1; ite_iso_group_list >= 0; ite_iso_group_list--) {
				bool is_not_iso = false;
				// List<Coordinates> tmp_iso_group = Tapa.isolation_blackboxes_group_list[ite_iso_group_list];
				foreach (Coordinates tmp_co in Tapa.isolation_blackboxes_group_list[ite_iso_group_list]) {
					// 伸び代のある黒マスがあれば次の黒マス群を見に行く
					// Console.Write(" >> " + Tapa.box[tmp_co.x][tmp_co.y].can_extend_blackbox + "\n");
					if (Tapa.box[tmp_co.x][tmp_co.y].can_extend_blackbox) {
						is_not_iso = true;
						break;
					}
				}
				if (!is_not_iso) { return false; }
			}
			return true;
		}

		/*********************************
		 * 
		 * 盤面に黒マスの団子がないか調べる
		 * true		: ない
		 * false	: ある
		 *   
		 * *******************************/
		public static bool checkNotDumplingBlackBox()
		{
			foreach (List<Coordinates> tmp_coord_list in Tapa.isolation_blackboxes_group_list) {
				foreach (Coordinates tmp_coord in tmp_coord_list) {
					if(Tapa.box[tmp_coord.x-1][tmp_coord.y-1].Color
						+ Tapa.box[tmp_coord.x-1][tmp_coord.y].Color
						+ Tapa.box[tmp_coord.x][tmp_coord.y].Color
						+ Tapa.box[tmp_coord.x][tmp_coord.y - 1].Color == 4) { return false; }
				}
			}
			return true;
		}

		 /*********************************
		 * 
		 * 隣り合った未定マスをリストにして返す。
		 * 引数
		 * notdeployed_id	: 未定マス群毎に振り分けられたid
		 * co	: 未定マスの座標
		 * remaining_not_deployedbox_list	: 一繋がりの未定マス群に登録されていない未定マスリスト
		 *   
		 * *******************************/
		private static List<Coordinates> uniteAdjacentNotDeployedBox(int notdeployed_id, Coordinates co, List<Coordinates> remaining_not_deployedbox_list)
		{
			// 未定マス群のidを登録
			Tapa.box[co.x][co.y].id_not_deployedbox_group = notdeployed_id;
			// 自身の入ったリスト
			List<Coordinates> tmp_coord_list = new List<Coordinates> { co };
			// 一繋がりの未定マス群に登録されていない未定マスリストから自身を除外
			remaining_not_deployedbox_list.Remove(co);

			// 上下左右のマス
			List<Box> adjacent_box_list = new List<Box> {
				Tapa.box[co.x - 1][co.y],
				Tapa.box[co.x][co.y + 1],
				Tapa.box[co.x + 1][co.y],
				Tapa.box[co.x][co.y - 1]
			};

			foreach (Box tmp_box in adjacent_box_list) {
				if (tmp_box.Color == Box.NOCOLOR && remaining_not_deployedbox_list.Contains(tmp_box.coord)) {
					tmp_coord_list.AddRange(
						uniteAdjacentNotDeployedBox(notdeployed_id, tmp_box.coord, remaining_not_deployedbox_list));
				}
			}

			return tmp_coord_list;
		}

		/*********************************
		* 
		* 一繋がりの未定マス群のリストを作成する
		*   
		* *******************************/
		public static void divideNotDeployedBoxToGroup()
		{
			// 未定マス群リストをリセット
			Tapa.isolation_notdeployedboxes_group_list.Clear();
			// 未定マスのリストをコピー
			List<Coordinates> remaining_not_deployedbox_list = new List<Coordinates>(Tapa.not_deployedbox_coord_list);

			for (int i = 0; i < remaining_not_deployedbox_list.Count; i++) {
				Tapa.isolation_notdeployedboxes_group_list.Add(
					uniteAdjacentNotDeployedBox(i, remaining_not_deployedbox_list[i], remaining_not_deployedbox_list));
			}
		}

		/*********************************
		 * 
		 * 黒マス群のうち、ある未定マス群に接している黒マスが1つ
		 * かつ接している辺が1つのみのとき、接している未定マスを黒マスにする。
		 * 
		 * *******************************/
		private static void extendBlackBoxOnlyOneAdjacentIsolationNotDeployedBoxGroup()
		{
			// 一繋がりの未定マス群（not_deployedbox_group）の数
			int ndbg_size = Tapa.isolation_notdeployedboxes_group_list.Count;

			for (int i = 0; i < Tapa.isolation_blackboxes_group_list.Count; i++ ) {
				List<Coordinates> tmp_bb_coord_list = new List<Coordinates>(Tapa.isolation_blackboxes_group_list[i]);
				// 今回調べている一繋がりの黒マス群と接している未定マスを保存するリスト
				List<Coordinates> local_adjacent_not_deployedbox_list = new List<Coordinates>();
				int[] count_adjacent_group = Enumerable.Repeat<int>(0, ndbg_size + 1).ToArray();	// 未定マス領域の種類の長さをもった配列
				for (int j = 0; j < tmp_bb_coord_list.Count; j++ ) {
					Coordinates tmp_bb_coord = tmp_bb_coord_list[j];
					if (Tapa.box[tmp_bb_coord.x][tmp_bb_coord.y].can_extend_blackbox) {
						// 上下左右にある未定マスをリストで取得
						List<Coordinates> around_not_deployedcoord_list = Box.getNoColorBoxCoordinatesAround(tmp_bb_coord);
						// 接している未定マスを追加
						local_adjacent_not_deployedbox_list.AddRange(new List<Coordinates>(around_not_deployedcoord_list));
						// 黒マスに接している未定マスの領域数を（重複も含め）数える
						foreach (Coordinates tmp_around_not_coord in around_not_deployedcoord_list) {
							count_adjacent_group[
								Tapa.box[tmp_around_not_coord.x][tmp_around_not_coord.y].id_not_deployedbox_group]++;
						}
					}
				}
				// 接している種類別の未定マス群の数を見る
				for (int k = 0; k < count_adjacent_group.Count(); k++) {
					if (count_adjacent_group[k] == 1) {
						foreach (Coordinates tmp_co in local_adjacent_not_deployedbox_list) {
							if (Tapa.box[tmp_co.x][tmp_co.y].id_not_deployedbox_group == k) {
								Tapa.box[tmp_co.x][tmp_co.y].Color = Box.BLACK;
								// 一箇所黒色になったら、このメソッドの処理を初めから行う
								extendBlackBoxOnlyOneAdjacentIsolationNotDeployedBoxGroup();
							}
						}
					}
				}
			}
		}

		/*********************************
		 * 
		 * 黒マス周りの処理
		 *   
		 * *******************************/
		public static void manageBlackBox()
		{
			Tapa.NOW_STATE_PROCESS = Tapa.STATE_AVOID_DUMPLING_AROUND_BLACK_BOX;

			//for (int ite_coord = 0; ite_coord < Tapa.edge_blackbox_coord_list.Count; ite_coord++) {
			//	Coordinates tmp_co = Tapa.edge_blackbox_coord_list[ite_coord];	// ###浅いコピー
			//	if (Tapa.box[tmp_co.x][tmp_co.y].Color != Box.BLACK) {
			//		Console.WriteLine("Error: 黒マスでないマスがedge_blackbox_coord_listに入っています ({0},{1})", tmp_co.x, tmp_co.y);
			//	}
			//	avoidDumpling(tmp_co);
			//}

			// 孤立した黒マス群のリストを見て、伸び代が1つしかない黒マス群があればそこを黒に塗る。
			Tapa.NOW_STATE_PROCESS = Tapa.STATE_ISOLATION_BLACK_BOXES_ONLY_EXTENDABLE;
			extendIsolationBlackBoxGroup();

			// 数字周り、黒マス周りの処理で一度もマス色が変化しなかった場合
			if (!Tapa.was_change_board) {
				// 一繋がりの未定マス群リストを作成する
				divideNotDeployedBoxToGroup();
				// 黒マス群がある未定マス群に一箇所のみ接していた場合、そこに黒マスを伸ばす
				extendBlackBoxOnlyOneAdjacentIsolationNotDeployedBoxGroup();
			}
		}

		// 1000 ~ 9999 の乱数を返す
		// 黒マス群のリストにidを設定するために作ったけどいらないかも
		public static int getRandomName()
		{
			int seed = Environment.TickCount;		// 乱数の種

			return new Random(seed++).Next(1000, 9999);
		}
	}
}

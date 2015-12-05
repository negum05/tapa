﻿using System;
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
		public static readonly int NOCOLOR = -1;    // 色未定
		public static readonly int WHITE = 0;       // 白色
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
				if (value == true) { this.Color = Box.WHITE; }			// 数字マスは白色
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
					if (!during_clone) {	// クローン処理中はマスをリストに追加しない
						// 白マスが塗られたことで伸び代がなくなった黒マスの伸び代フラグをオフにする。
						if (!Box.during_make_inputbord && value == Box.WHITE) {
							resetExtendableBlackBoxAroundWhiteBox(this.coord);
						}
						// 塗る色が黒かつ伸び代があれば、伸び代のある黒マスリストに追加
						else if (value == Box.BLACK) {
							//Console.Write("Color:");
							//this.coord.printCoordinates();
							//Console.Write("\n");
							Box.divideBlackBoxToGroup(this.coord);	// 接している孤立した黒マス群に追加
							if (canExtendBlackBox(this.coord)) {
								this.can_extend_blackbox = true;
								Tapa.edge_blackbox_coord_list.Add(this.coord);
							}
							Tapa.not_deployedbox_coord_list.Remove(this.coord);
						}
					}
				}
			}
		}
		public bool can_extend_blackbox { get; set; }			// true:伸び代のある黒マス

		public Box()
		{
			this.coord = new Coordinates();
			changed_count_in_search_confirm_box = 0;
			hasNum = false;
			this.box_num = -1;
			this.id_list = new List<byte>();
			this.color = Box.NOCOLOR;
			this.can_extend_blackbox = false;
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
		}

		/*********************************
		 * 
		 * 数字マスの数字の表示
		 *   
		 * *******************************/
		public void printBoxNum()
		{
			if (!this.has_num) {
				if (this.color == Box.WHITE) { Console.Write("==== "); }
				else if (this.color == Box.BLACK) { Console.Write("**** "); }
				else { Console.Write("---- "); }
			}
			else {
				int rest = 5 - this.box_num.ToString().Length;
				Console.Write(this.box_num);
				while (rest-- > 0) { Console.Write(" "); }
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
				Console.WriteLine("Error: id_listの内マスからid_listを出力しようとしています。");
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
		private static void resetExtendableBlackBoxAroundWhiteBox(Coordinates co)
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
			List<Coordinates> arround_box_coord = new List<Coordinates>();
			foreach (Box tmp_box in around_box) {
				if (tmp_box.Color == Box.BLACK) {
					arround_box_coord.Add(tmp_box.coord);
				}
			}

			int count_arround_box_coord = arround_box_coord.Count;
			// 上下左右に黒マスがない場合、自身を新しい孤立した黒マス群としてリストに追加
			if (count_arround_box_coord == 0) {
				Tapa.isolation_blackboxes_group_list.Add(new List<Coordinates>(){ co });

				Console.Write("divide(孤立):");
				co.printCoordinates();
				Console.Write("\n");
			}
			else {
				Console.Write("divide(結合):");
				co.printCoordinates();
				Console.Write("\n");

				List<Coordinates> merged_group_list = new List<Coordinates>() { co };	// 結合したリストの保存用
				// true:添字番目の黒マスのリストを結合済み（添字がarround_box_coordと対応）
				bool[] was_checked_co = Enumerable.Repeat<bool>(false, count_arround_box_coord).ToArray();
				
				for (int ite_iso_group = Tapa.isolation_blackboxes_group_list.Count-1; ite_iso_group >= 0; ite_iso_group--) {
					for (int ite_arround_box = count_arround_box_coord - 1; ite_arround_box >= 0; ite_arround_box--) {
						if (!was_checked_co[ite_arround_box]
							&& Tapa.isolation_blackboxes_group_list[ite_iso_group].Contains(arround_box_coord[ite_arround_box])) {
							// 新たに結合されるリストに孤立していた黒マス群を追加
							merged_group_list.AddRange(new List<Coordinates>(Tapa.isolation_blackboxes_group_list[ite_iso_group]));
							// 直前でmerged_group_listに追加した元のリストを孤立する黒マス群から削除
							Tapa.isolation_blackboxes_group_list.RemoveAt(ite_iso_group);
							// ite_arround_box番目の黒マスのリストを結合したのでtrueにする。
							was_checked_co[ite_arround_box] = true;							
							break;
						}
					}
				}
				// 今回の結合で伸び代の無くなった黒マスを伸び代のある黒マスリストから除外
				for (int i = Tapa.edge_blackbox_coord_list.Count - 1; i >= 0; i--) {
					Coordinates tmp_co = Tapa.edge_blackbox_coord_list[i];
					if (!Box.canExtendBlackBox(tmp_co)) {
						Tapa.box[tmp_co.x][tmp_co.y].can_extend_blackbox = false;
						Tapa.edge_blackbox_coord_list.RemoveAt(i);
					}
				}
				// 今回新たに結合された孤立した黒マスリストを、孤立した黒マス群リストに追加。
				Tapa.isolation_blackboxes_group_list.Add(merged_group_list);	
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
			// [色と定数の関係] Box.WHITE : 0	Box.BLACK : 1	Box.NOCOLOR : -1
			foreach (int tmp_color in around_colors) {
				if (tmp_color == Box.NOCOLOR) {
					count++;
				}
			}
			return count;
		}

		/*********************************
		* 
		* 座標coのマスの上下左右にある未定マスの座標を返す。
		* （上右下左の順に先に見つけた未定マスを返す。）
		* 引数
		* co	: マスの座標
		*   
		* *******************************/
		private static Coordinates getNoColorBoxCoordinatesAround(Coordinates co)
		{
			List<Box> around_box = new List<Box> {
				Tapa.box[co.x-1][co.y],
				Tapa.box[co.x][co.y+1],
				Tapa.box[co.x+1][co.y],
				Tapa.box[co.x][co.y-1]
			};

			// [色と定数の関係] Box.WHITE : 0	Box.BLACK : 1	Box.NOCOLOR : -1
			foreach (Box tmp_box in around_box) {
				if (tmp_box.Color == Box.NOCOLOR) {
					return tmp_box.coord;
				}
			}
			Console.WriteLine("Error: getNoColorBoxCoordinatesAroundの引数の上下左右に未定マスが存在しません。");
			Application.Exit();
			return null;
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
		 * 
		 * 黒マスの上下左右の未定マスに注目し、
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
			Box MR = Tapa.box[co.x][co.y + 1];		// (右)		Middle-Right
			Box BR = Tapa.box[co.x + 1][co.y + 1];	// (右下)	Bottom-Right
			Box BC = Tapa.box[co.x + 1][co.y];		// (下)		Bottom-Center
			Box BL = Tapa.box[co.x + 1][co.y - 1];	// (左下)	Bottom-Left
			Box ML = Tapa.box[co.x][co.y - 1];		// (左)		Middle-Left

			if (TC.Color == Box.NOCOLOR) {	// 上マスが黒になると団子になるか
				if( (ML.Color == Box.BLACK && TL.Color == Box.BLACK)
					|| (TR.Color == Box.BLACK && MR.Color == Box.BLACK)) {
						TC.Color = Box.WHITE;
				}
			}
			else if (MR.Color == Box.NOCOLOR) {	// 右マスが黒になると団子になるか
				if ((TC.Color == Box.BLACK && TR.Color == Box.BLACK)
					|| (BR.Color == Box.BLACK && BC.Color == Box.BLACK)) {
					MR.Color = Box.WHITE;
				}
			}
			else if (BC.Color == Box.NOCOLOR) {	// 下マスが黒になると団子になるか
				if ((MR.Color == Box.BLACK && BR.Color == Box.BLACK)
					|| (BL.Color == Box.BLACK && ML.Color == Box.BLACK)) {
					BC.Color = Box.WHITE;
				}
			}
			else if (ML.Color == Box.NOCOLOR) {	// 左マスが黒になると団子になるか
				if ((BC.Color == Box.BLACK && BL.Color == Box.BLACK)
					|| (TL.Color == Box.BLACK && TC.Color == Box.BLACK)) {
					ML.Color = Box.WHITE;
				}
			}
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
			for (int ite_iso_group_list = Tapa.isolation_blackboxes_group_list.Count - 1; ite_iso_group_list >= 0; ite_iso_group_list--) {
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
					Coordinates undeployed_coord = Box.getNoColorBoxCoordinatesAround(last_extendable_coord);
					Tapa.box[undeployed_coord.x][undeployed_coord.y].Color = Box.BLACK;
				}
			}
		}

		public static void manageBlackBox()
		{
			for (int ite_coord = Tapa.edge_blackbox_coord_list.Count - 1; ite_coord >= 0; ite_coord--) {
				Coordinates tmp_co = Tapa.edge_blackbox_coord_list[ite_coord];	// ###浅いコピー
				if (Tapa.box[tmp_co.x][tmp_co.y].Color != Box.BLACK) {
					Console.WriteLine("Error: 黒マスでないマスがedge_blackbox_coord_listに入っています ({0},{1})", tmp_co.x, tmp_co.y);
				}
				avoidDumpling(tmp_co);
				// extendBlackBox(ite_coord);
				// 伸び代がなければ、その座標をリストから除外
				//if (!canExtendBlackBox(Tapa.edge_blackbox_coord_list[ite_coord])) {
				//	Tapa.edge_blackbox_coord_list.RemoveAt(ite_coord);
				//}
			}
			// 孤立した黒マス群のリストを見て、伸び代が1つしかない黒マス群があればそこを黒に塗る。
			extendIsolationBlackBoxGroup();
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

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
		public static readonly int NOCOLOR = 0;    // 色未定
		public static readonly int WHITE = -1;     // 白色
		public static readonly int BLACK = 1;      // 黒色

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
				if (value) { this.color = Box.WHITE; }			// 数字マスは白色
				this.has_num = value;
			}
		}
		public int min_hint;						// 数字マス周りのマスが確定するための最少ヒント数
		private int box_num;
		public int boxNum
		{
			get { return this.box_num; }
			set
			{
				this.box_num = value;
				if (this.has_num) {
					this.min_hint = PatternAroundNumBox.getMinHintAroundNumBox(value);	// 最少ヒント数
				}
			}
		}

		// マスの数字
		public List<byte> id_list;                  // id(数字マス周りのパターンの識別子)のリスト
		private int color;							// マスの色
		public int Color							// 解答中の色塗り
		{
			get { return this.color; }
			set
			{
				// 異なる色に変更されそうになればインクリメント (id_listから色の確定するマスを探す際に使用)
				if (this.color != value) { changed_count_in_search_confirm_box++; }
				if (this.color == Box.NOCOLOR) {
					this.color = value;
					if (!during_clone && !Box.during_make_inputbord) {	// (クローン処理中 or 盤面入力中)はマスをリストに追加しない
						Tapa.was_change_board = true;	// 今回の処理で変化したのでフラグを立てる
						// 塗る色が黒かつ伸び代があれば、伸び代のある黒マスリストに追加
						if (this.color == Box.BLACK) {
							// 塗られたマス周りで黒マスの団子ができないよう白マスを配置
							avoidDumpling(Tapa.box[this.coord.x][this.coord.y].coord);
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
					}
				}
			}
		}
		public int revision_color
		{
			set { this.color = value; }
		}
		public int connecting_color					// dot画から黒マス孤立をなくす際使用（白マスを生成しない）
		{
			set
			{
				if (this.color == Box.NOCOLOR) {
					this.color = value;
					// 塗る色が黒かつ伸び代があれば、伸び代のある黒マスリストに追加
					if (this.color == Box.BLACK) {
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
			this.color = Box.NOCOLOR;
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
			foreach (byte tmp_id in id_list) {
				Console.Write(tmp_id.ToString() + " ");
			}
			Console.WriteLine();
		}

		/*********************************
		* 
		* co(新しく白マスになった座標)周りの黒マスを見て、
		* coが白マスになったことで伸び代がなくなった黒マスの伸び代フラグをオフにし、
		* 伸び代のある黒マスリストから除外する。
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

			// 上下左右に黒マスがない場合、自身を新しい孤立した黒マス群としてリストに追加
			if (around_box_coord.Count == 0) {
				Tapa.isolation_blackboxes_group_list.Add(new List<Coordinates>() { co });
			}
			else {

				List<Coordinates> merged_group_list = new List<Coordinates>() { co };	// 結合したリストの保存用
				for (int ite_iso_group = Tapa.isolation_blackboxes_group_list.Count - 1; ite_iso_group >= 0; ite_iso_group--) {
					// 周囲の黒い数字マスで処理の終わった黒マスのインデックス保存用
					List<int> del_index = new List<int>();
					for (int ite_around_box = around_box_coord.Count - 1; ite_around_box >= 0; ite_around_box--) {
						if (Tapa.isolation_blackboxes_group_list[ite_iso_group].Contains(around_box_coord[ite_around_box])) {
							del_index.Add(ite_around_box);
						}
					}
					if (del_index.Count > 0) {
						// 新たに結合されるリストに孤立していた黒マス群を追加
						merged_group_list.AddRange(new List<Coordinates>(Tapa.isolation_blackboxes_group_list[ite_iso_group]));
						// 直前でmerged_group_listに追加したリストの元を孤立する黒マス群から削除
						Tapa.isolation_blackboxes_group_list.RemoveAt(ite_iso_group);
						// 今回追加する黒マス群に含まれる周りの黒マスをリストから除外
						foreach (int i in del_index) {
							around_box_coord.RemoveAt(i);
						}
					}
					if (around_box_coord.Count == 0) { break; }
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
		* 座標coのマスの上下左右にある指定色マスの座標をリストで返す。
		* 引数
		* co	: マスの座標
		* color	: 調べたい色
		*   
		* *******************************/
		public static List<Coordinates> getWhatColorBoxCoordListAround(Coordinates co, int color)
		{
			List<Coordinates> tmp_coord_list = new List<Coordinates>();

			List<Box> around_box = new List<Box> {
				Tapa.box[co.x-1][co.y],
				Tapa.box[co.x][co.y+1],
				Tapa.box[co.x+1][co.y],
				Tapa.box[co.x][co.y-1]
			};

			foreach (Box tmp_box in around_box) {
				if (tmp_box.Color == color) {
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
		public static bool canExtendBlackBox(Coordinates co)
		{
			if (Tapa.box[co.x][co.y].Color != Box.BLACK) {
				Console.WriteLine("Error: canExtendBlackBoxに黒マス以外の座標を引数として渡しています。({0},{1})", co.x, co.y);
				Application.Exit();
			}

			if (Tapa.box[co.x - 1][co.y].Color == Box.NOCOLOR		// 上のマス色
				|| Tapa.box[co.x][co.y + 1].Color == Box.NOCOLOR	// 右のマス色
				|| Tapa.box[co.x + 1][co.y].Color == Box.NOCOLOR	// 下のマス色
				|| Tapa.box[co.x][co.y - 1].Color == Box.NOCOLOR) {	// 左のマス色
				return true;
			}
			return false;
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
		 * 座標co周り8マスにある白マスの座標をリストで返す。(外周を除く)
		 *
		 * 引数
		 * co	: 注目座標
		 *   
		 * *******************************/
		public static List<Coordinates> getWhiteBoxCoordAround8(Coordinates co)
		{
			List<Coordinates> around_whitebox_list = new List<Coordinates>();

			for (int i = co.x - 1; i <= co.x + 1; i++) {
				for (int j = co.y - 1; j <= co.y + 1; j++) {
					Coordinates around_co = new Coordinates(i, j);
					if ((i == co.x && j == co.y) || !Box.checkNotOuterBox(around_co)) { continue; }
					if (Tapa.box[i][j].Color == Box.WHITE) {
						around_whitebox_list.Add(around_co);
					}
				}
			}

			return around_whitebox_list;
		}

		/*********************************
		 * 
		 * 座標co周り8マスに未定マスが存在するか。
		 * true:	存在する
		 *
		 * 引数
		 * co	: 注目座標
		 *   
		 * *******************************/
		public static bool existWhatColorBoxAround8(Coordinates co, int color)
		{
			for (int i = co.x - 1; i <= co.x + 1; i++) {
				for (int j = co.y - 1; j <= co.y + 1; j++) {
					Coordinates around_co = new Coordinates(i, j);
					if ((i == co.x && j == co.y) || !Box.checkNotOuterBox(around_co)) { continue; }
					if (Tapa.box[i][j].Color == color) { return true; }
				}
			}
			return false;
		}

		/*********************************
		 * 
		 * 座標co周り5*5マスにある黒マスの属する黒マス群のインデックスをリストで返す。
		 *
		 * 引数
		 * co	: 注目座標
		 *   
		 * *******************************/
		public static List<int> getIndexBlackBoxGroupListAround55(Coordinates co)
		{
			// co周り5*5マス内の黒マスを取得
			List<Coordinates> bb_co_around55 = new List<Coordinates>();
			for (int i = co.x - 2; i <= co.x + 2; i++) {
				for (int j = co.y - 2; j <= co.y + 2; j++) {
					// 外周より外側
					if (i <= 0 || Tapa.MAX_BOARD_ROW < i || j <= 0 || Tapa.MAX_BOARD_COL < j) { continue; }
					else if (Tapa.box[i][j].Color == Box.BLACK) { bb_co_around55.Add(Tapa.box[i][j].coord); }
				}
			}

			List<int> bbgroup_index_list = new List<int>();	// 黒マス群リストのインデックス保存用
			for (int ite_iso_group = Tapa.isolation_blackboxes_group_list.Count - 1; ite_iso_group >= 0; ite_iso_group--) {
				// 周囲の黒い数字マスで処理の終わった黒マスのインデックス保存用
				List<int> del_index = new List<int>();
				for (int ite_around_box = bb_co_around55.Count - 1; ite_around_box >= 0; ite_around_box--) {
					if (Tapa.isolation_blackboxes_group_list[ite_iso_group].Contains(bb_co_around55[ite_around_box])) {
						del_index.Add(ite_around_box);
					}
				}
				if (del_index.Count > 0) {
					// 黒マス群のインデックスを追加
					bbgroup_index_list.Add(ite_iso_group);
					// 今回追加する黒マス群に含まれる周りの黒マスをリストから除外
					foreach (int i in del_index) {
						bb_co_around55.RemoveAt(i);
					}
				}
				if (bb_co_around55.Count == 0) { break; }
			}
			return bbgroup_index_list;
		}


		/*********************************
		 * 
		 * 黒マス群のインデックスsの座標からtの座標までの直線の周囲2マス以内にある
		 * 数字マスをret_numbox_index_listに格納する。
		 *
		 * 引数
		 * s	: 直線の始点の黒マス群インデックス
		 * t	: 直線の終点の黒マス群インデックス
		 * d	: dfsがたどってきた直線の向き
		 * ret_numbox_index_list	: 黒マス群周りにある数字マス格納用
		 *   
		 * *******************************/
		private static void getNumBoxIndexAroundLine(
			int s, int t, Coordinates d, ref List<Coordinates> ret_numbox_coord_list)
		{
			Coordinates tl;	// 長方形の左上の座標
			int y_right, x_bottom;	// 長方形の右辺のy座標、底辺のx座標
			// 長方形の左上の座標と右辺y、底辺xの座標を取得
			if (d.Equals(Coordinates.RIGHT)) {
				tl = new Coordinates(Box.bb_group[s].x - 2, Box.bb_group[s].y - 2);
				y_right = Box.bb_group[t].y - Box.bb_group[s].y + 4 + tl.y;
				x_bottom = 4 + tl.x;
			}
			else if (d.Equals(Coordinates.LEFT)) {
				tl = new Coordinates(Box.bb_group[t].x - 2, Box.bb_group[t].y - 2);
				y_right = Box.bb_group[s].y - Box.bb_group[t].y + 4 + tl.y;
				x_bottom = 4 + tl.x;
			}
			else if (d.Equals(Coordinates.UP)) {
				tl = new Coordinates(Box.bb_group[t].x - 2, Box.bb_group[t].y - 2);
				y_right = 4 + tl.y;
				x_bottom = Box.bb_group[s].x - Box.bb_group[t].x + 4 + tl.x;
			}
			else if (d.Equals(Coordinates.DOWN)) {
				tl = new Coordinates(Box.bb_group[s].x - 2, Box.bb_group[s].y - 2);
				y_right = 4 + tl.y;
				x_bottom = Box.bb_group[t].x - Box.bb_group[s].x + 4 + tl.x;
			}
			else if (d.Equals(Coordinates.ZERO)) {
				Coordinates tmp = Box.bb_group[0];
				tl = new Coordinates(tmp.x - 2, tmp.y - 2);
				y_right = 4 + tl.y;
				x_bottom = tl.x + 4;
			}
			else {
				Console.WriteLine("【Box.getNumBoxIndexAroundLine】向きがおかしい");
				return;
			}
			// 長方形内の数字マスを格納
			for (int i = tl.x; i <= x_bottom; i++) {
				for (int j = tl.y; j <= y_right; j++) {
					Coordinates tmp_coord = new Coordinates(i, j);
					if (!Box.checkNotOuterBox(tmp_coord)) { continue; }
					if (Tapa.box[i][j].hasNum
						&& !ret_numbox_coord_list.Contains(tmp_coord)
						&& Tapa.numbox_coord_list.Contains(tmp_coord)) {
						ret_numbox_coord_list.Add(tmp_coord);
					}
				}
			}
		}

		/*********************************
		 * 
		 * 黒マス群に対してdfsを行い、黒マス群周りの数字マスの数字マスリストにおけるインデックスを
		 * ret_numbox_index_listに格納する。
		 *
		 * 引数
		 * u	: 親の黒マス群インデックス
		 * s	: 現在の直線の始点の黒マス群インデックス
		 * t	: 現在の直線の端（始点とは反対側）の黒マス群インデックス
		 * d	: dfsがたどっている直線の向き
		 * ret_numbox_index_list	: 黒マス群周りにある数字マス座標格納用
		 *   
		 * *******************************/
		private static void doDFStoSortBlackBoxGroup(
			int u, int s, int t, Coordinates d, ref List<Coordinates> ret_numbox_coord_list)
		{
			if (visited[u]) { return; }
			visited[u] = true;
			t = u;	// 突き当りを更新

			foreach (int v in edge[u]) {
				Coordinates next_d = bb_group[v] - bb_group[u];
				if (s == t) { d = next_d; }	// 突き当りと始点が同じ ＝ 曲がったばかり
				else {
					if (d != next_d) {
						getNumBoxIndexAroundLine(s, t, d, ref ret_numbox_coord_list);
						s = t;	// 始点を今の位置に
					}
				}
				doDFStoSortBlackBoxGroup(v, s, t, d, ref ret_numbox_coord_list);
			}
		}

		/*********************************
		 * 
		 * 黒マス群の周囲2マス以内にある数字マスの座標リストを返す
		 *
		 * 引数
		 * bbgroup_index_list	:	対象の黒マス群のインデックスのリスト
		 *   
		 * *******************************/

		static Dictionary<int, List<int>> edge;
		static List<Coordinates> bb_group;	// dfsを行う黒マス群の一時保存用
		static bool[] visited;	// dfsで到着済みかを判定する
		public static List<Coordinates> getCoordListAroundBlackBoxGroup(List<int> bbgroup_index_list)
		{

			// 黒マス周りの数字マスの、数字マスリストでのインデックス格納用
			List<Coordinates> ret_numbox_coord_list = new List<Coordinates>();
			for (int bb_index = bbgroup_index_list.Count - 1; bb_index >= 0; bb_index--) {
				bb_group = Tapa.isolation_blackboxes_group_list[bbgroup_index_list[bb_index]];

				// 黒マス群が黒マス単体の時
				if (bb_group.Count == 1) {
					getNumBoxIndexAroundLine(0, 0, Coordinates.ZERO, ref ret_numbox_coord_list);
				}
				else {
					// 今回ソートする黒マス群の大きさ確保　falseで初期化
					visited = new bool[bb_group.Count];
					for (int i = visited.Length - 1; i >= 0; i--) { visited[i] = false; }

					// xを昇順、yを昇順でソート（左上の黒マスが先頭）
					bb_group.Sort(
						delegate(Coordinates co1, Coordinates co2) {
							if (co1.x < co2.x) { return -1; }
							else if (co1.x > co2.x) { return 1; }
							else {
								if (co1.y < co2.y) { return -1; }
								else if (co1.y > co2.y) { return 1; }
								else return 0;
							}
						});

					// 辺を設定
					edge = new Dictionary<int, List<int>>();
					for (int i = bb_group.Count - 1; i >= 0; i--) {
						List<Coordinates> adjacent_coord = Box.getWhatColorBoxCoordListAround(bb_group[i], Box.BLACK);
						List<int> index = new List<int>();
						for (int j = adjacent_coord.Count - 1; j >= 0; j--) {
							if (!bb_group.Contains(adjacent_coord[j])) { adjacent_coord.RemoveAt(j); }
							else { index.Add(bb_group.IndexOf(adjacent_coord[j])); }

						}
						edge[i] = index;
					}
					// 黒マス群周りの数字をdfs中に取得する
					// 引数：親、（現在の直線の）始点、　（現在の直線の）終点、（現在の直線の）向き、数字マス保存用
					doDFStoSortBlackBoxGroup(0, 0, 0, new Coordinates(0, 0), ref ret_numbox_coord_list);
				}

			}

			return ret_numbox_coord_list;

		}

		/*********************************
		 * 
		 * 座標coが外周の座標かどうか返す
		 * true		: 外周の座標ではない
		 * false	: 外周の座標である
		 * 
		 * 引数
		 * co	: 注目座標
		 *   
		 * *******************************/
		public static bool checkNotOuterBox(Coordinates co)
		{
			if (co.x <= 0 || Tapa.MAX_BOARD_ROW < co.x
				|| co.y <= 0 || Tapa.MAX_BOARD_COL < co.y) { return false; }
			return true;
		}
		public static bool checkNotOuterBox(int x, int y)
		{
			if (x <= 0 || Tapa.MAX_BOARD_ROW < x
				|| y <= 0 || Tapa.MAX_BOARD_COL < y) { return false; }
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
			if (TC.Color + TR.Color + MC.Color + MR.Color == 3) {		// 右上
				if (TC.Color == Box.NOCOLOR) { TC.Color = Box.WHITE; }
				else if (TR.Color == Box.NOCOLOR) { TR.Color = Box.WHITE; }
				else if (MC.Color == Box.NOCOLOR) { MC.Color = Box.WHITE; }
				else if (MR.Color == Box.NOCOLOR) { MR.Color = Box.WHITE; }
			}
			if (ML.Color + MC.Color + BL.Color + BC.Color == 3) {		// 左下
				if (ML.Color == Box.NOCOLOR) { ML.Color = Box.WHITE; }
				else if (MC.Color == Box.NOCOLOR) { MC.Color = Box.WHITE; }
				else if (BL.Color == Box.NOCOLOR) { BL.Color = Box.WHITE; }
				else if (BC.Color == Box.NOCOLOR) { BC.Color = Box.WHITE; }
			}
			if (MC.Color + MR.Color + BC.Color + BR.Color == 3) {		// 右下
				if (MC.Color == Box.NOCOLOR) { MC.Color = Box.WHITE; }
				else if (MR.Color == Box.NOCOLOR) { MR.Color = Box.WHITE; }
				else if (BC.Color == Box.NOCOLOR) { BC.Color = Box.WHITE; }
				else if (BR.Color == Box.NOCOLOR) { BR.Color = Box.WHITE; }
			}
		}

		/*********************************
		 * 
		 * 孤立した黒マス群の伸び代が1マスだけの時、そのマスを黒に塗る。
		 * 全ての黒マス群をチェックし、もし上の処理をすれば、また一から黒マス群を見直す（リストが再構成されるため）。
		 * 先頭の黒マス群から最後の黒マス群までチェックして、1度も変化しなければ終了。
		 * 引数
		 * solve_num	:	ヒントを作るマス数の上限
		 *   
		 * *******************************/
		private static void extendIsolationBlackBoxGroup(int solve_limit = -1)
		{
			// 黒マス群が一つしかなければこの処理をしない（解が一意にならないため）
			if (Tapa.isolation_blackboxes_group_list.Count == 1) { return; }

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
					List<Coordinates> around_undeployed_coord = Box.getWhatColorBoxCoordListAround(last_extendable_coord, Box.NOCOLOR);
					Tapa.box[around_undeployed_coord[0].x][around_undeployed_coord[0].y].Color = Box.BLACK;

					if (solve_limit >= Tapa.not_deployedbox_coord_list.Count) {
						Tapa.is_over_solve_num = true;
						return;
					}

					if (Tapa.DEBUG) {
						Console.Write("黒マス延長");
						around_undeployed_coord[0].printCoordinates();
						Console.WriteLine();
						Tapa.printBoard();
					}

					// 問題生成で一意の問題を作れなくなるためコメントアウト
					// extendIsolationBlackBoxGroup();
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
		public static bool checkNotIsolationBlackBoxGroup(List<List<Coordinates>> bb_multi_list = null)
		{
			List<List<Coordinates>> ml = new List<List<Coordinates>>();
			if (bb_multi_list == null) { ml = Tapa.isolation_blackboxes_group_list; }
			else { ml = bb_multi_list; }

			for (int ite_iso_group_list = ml.Count - 1; ite_iso_group_list >= 0; ite_iso_group_list--) {
				bool is_not_iso = false;
				// List<Coordinates> tmp_iso_group = Tapa.isolation_blackboxes_group_list[ite_iso_group_list];
				foreach (Coordinates tmp_co in ml[ite_iso_group_list]) {
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
		 * 【Box.BLACK の値は１】
		 * 盤面に黒マスの団子がないか調べる
		 * true		: ない
		 * false	: ある
		 *   
		 * *******************************/
		public static bool checkNotDumplingBlackBox()
		{
			foreach (List<Coordinates> tmp_coord_list in Tapa.isolation_blackboxes_group_list) {
				foreach (Coordinates tmp_coord in tmp_coord_list) {
					if (Tapa.box[tmp_coord.x - 1][tmp_coord.y - 1].Color
						+ Tapa.box[tmp_coord.x - 1][tmp_coord.y].Color
						+ Tapa.box[tmp_coord.x][tmp_coord.y].Color
						+ Tapa.box[tmp_coord.x][tmp_coord.y - 1].Color == 4) { return false; }
				}
			}
			return true;
		}

		/*********************************
		 * 
		 * 盤面にある団子マスの座標リストを返す
		 *   
		 * *******************************/
		public static List<Coordinates> getDumpCoord()
		{
			List<Coordinates> dump_list = new List<Coordinates>();
			foreach (List<Coordinates> tmp_coord_list in Tapa.isolation_blackboxes_group_list) {
				foreach (Coordinates tmp_coord in tmp_coord_list) {
					if (Tapa.box[tmp_coord.x - 1][tmp_coord.y - 1].Color
						+ Tapa.box[tmp_coord.x - 1][tmp_coord.y].Color
						+ Tapa.box[tmp_coord.x][tmp_coord.y].Color
						+ Tapa.box[tmp_coord.x][tmp_coord.y - 1].Color == 4) {	// 団子マスだった場合
						dump_list.Add(new Coordinates(tmp_coord.x - 1, tmp_coord.y - 1));
						dump_list.Add(new Coordinates(tmp_coord.x - 1, tmp_coord.y));
						dump_list.Add(new Coordinates(tmp_coord.x, tmp_coord.y));
						dump_list.Add(new Coordinates(tmp_coord.x, tmp_coord.y - 1));	// 団子マスの座標を保存;r
						return dump_list;
					}
				}
			}
			return null;
		}

		/*********************************
		 * 
		 * co座標の周囲3*3マスに黒マスの団子がないか調べる
		 * true		: ない
		 * false	: ある
		 *   
		 * *******************************/
		public static bool checkNotDumplingBlackBoxAround(Coordinates co)
		{
			int TL = Tapa.box[co.x - 1][co.y - 1].Color;	// 左上
			int TC = Tapa.box[co.x - 1][co.y].Color;		// 上
			int TR = Tapa.box[co.x - 1][co.y + 1].Color;	// 右上
			int ML = Tapa.box[co.x][co.y - 1].Color;		// 左
			int MC = Tapa.box[co.x][co.y].Color;			// 真ん中
			int MR = Tapa.box[co.x][co.y + 1].Color;		// 右
			int BL = Tapa.box[co.x + 1][co.y - 1].Color;	// 左下
			int BC = Tapa.box[co.x + 1][co.y].Color;		// 下
			int BR = Tapa.box[co.x + 1][co.y + 1].Color;	// 右下

			if (TL + TC + ML + MC == 4) { return false; }
			if (TC + TR + MC + MR == 4) { return false; }
			if (ML + MC + BL + BC == 4) { return false; }
			if (MC + MR + BC + BR == 4) { return false; }
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
			if (!remaining_not_deployedbox_list.Contains(co)) { return null; }

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
		 * 【coが黒マス群リストの末尾に属していること前提】
		 * co座標から最も近くにある別黒マス群に属する黒マスの座標を取得 
		 * co座標の黒マスは黒マス群の末尾のリストに属する(for文)
		 * 引数
		 * co			:	注目座標
		 * belong_list	:	coの属する黒マスリスト
		 * 
		 * *******************************/
		public static Coordinates getCloseBlackBoxCoord(Coordinates co, List<Coordinates> belong_list)
		{
			Coordinates close_coord = new Coordinates();
			int distance = int.MaxValue;
			int tmp_distance = int.MaxValue;
			// ### 近い黒マスを黒マスリストから探す
			for (int i = 0; i < Tapa.isolation_blackboxes_group_list.Count - 1; i++) {
				List<Coordinates> list = Tapa.isolation_blackboxes_group_list[i];
				if (list.Equals(belong_list)) { continue; }
				for (int j = 0; j < list.Count; j++) {
					Coordinates tmp_co = list[j];
					tmp_distance = Box.getDistance(co, tmp_co);
					if (tmp_distance < distance) {
						distance = tmp_distance;
						close_coord = tmp_co;
					}
				}
			}

			return close_coord;
		}

		/*********************************
		 * 
		 * co1とco2の距離を計算して返す
		 * 引数
		 * co1,co2			:	距離を求めたい座標
		 *  
		 * *******************************/
		public static int getDistance(Coordinates co1, Coordinates co2)
		{
			return Math.Abs(co1.x - co2.x) + Math.Abs(co1.y - co2.y);
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

			for (int i = 0; i < Tapa.not_deployedbox_coord_list.Count; i++) {
				List<Coordinates> tmp_coord_list = uniteAdjacentNotDeployedBox(i, Tapa.not_deployedbox_coord_list[i], remaining_not_deployedbox_list);
				if (tmp_coord_list != null) {
					Tapa.isolation_notdeployedboxes_group_list.Add(tmp_coord_list);
				}

			}
		}

		/*********************************
		* 
		* 一繋がりの黒マス群のうち、最少の黒マス群の参照を返す
		*   
		* *******************************/
		public static List<Coordinates> getMinIsoBlackBoxListRef()
		{
			int min = int.MaxValue;
			int min_index = 0;
			for (int i = Tapa.isolation_blackboxes_group_list.Count - 1; i >= 0; i--) {
				if (Tapa.isolation_blackboxes_group_list[i].Count < min) {
					min = Tapa.isolation_blackboxes_group_list[i].Count;
					min_index = i;
				}
			}
			return Tapa.isolation_blackboxes_group_list[min_index];
		}


		/*********************************
		 * 
		 * 黒マス群のうち、ある未定マス群に接している黒マスが1つ
		 * かつ接している辺が1つのみのとき、接している未定マスを黒マスにする。
		 * 【手法が限定的だったため処理の必要なし】
		 * 
		 * *******************************/
		//private static void extendBlackBoxOnlyOneAdjacentIsolationNotDeployedBoxGroup()
		//{
		//	// 一繋がりの未定マス群（not_deployedbox_group）の数
		//	int ndbg_size = Tapa.isolation_notdeployedboxes_group_list.Count;

		//	for (int i = 0; i < Tapa.isolation_blackboxes_group_list.Count; i++) {
		//		List<Coordinates> tmp_bb_coord_list = new List<Coordinates>(Tapa.isolation_blackboxes_group_list[i]);
		//		// 今回調べている一繋がりの黒マス群と接している未定マスを保存するリスト
		//		List<Coordinates> local_adjacent_not_deployedbox_list = new List<Coordinates>();
		//		int[] count_adjacent_group = Enumerable.Repeat<int>(0, ndbg_size).ToArray();	// 未定マス領域の種類の長さをもった配列
		//		for (int j = 0; j < tmp_bb_coord_list.Count; j++) {
		//			Coordinates tmp_bb_coord = tmp_bb_coord_list[j];
		//			if (Tapa.box[tmp_bb_coord.x][tmp_bb_coord.y].can_extend_blackbox) {
		//				// 上下左右にある未定マスをリストで取得
		//				List<Coordinates> around_not_deployedcoord_list = Box.getNoColorBoxCoordinatesAround(tmp_bb_coord);
		//				// 接している未定マスを追加
		//				local_adjacent_not_deployedbox_list.AddRange(new List<Coordinates>(around_not_deployedcoord_list));
		//				// 黒マスに接している未定マスの領域数を（重複も含め）数える
		//				foreach (Coordinates tmp_around_not_coord in around_not_deployedcoord_list) {
		//					count_adjacent_group[
		//						Tapa.box[tmp_around_not_coord.x][tmp_around_not_coord.y].id_not_deployedbox_group]++;
		//				}
		//			}
		//		}
		//		// 接している種類別の未定マス群の数を見る
		//		for (int k = 0; k < count_adjacent_group.Count(); k++) {
		//			if (count_adjacent_group[k] == 1) {
		//				foreach (Coordinates tmp_co in local_adjacent_not_deployedbox_list) {
		//					if (Tapa.box[tmp_co.x][tmp_co.y].id_not_deployedbox_group == k) {
		//						Tapa.box[tmp_co.x][tmp_co.y].Color = Box.BLACK;
		//						// 一繋がりの未定マス群リストを作成する
		//						divideNotDeployedBoxToGroup();
		//						// 一箇所黒色になったら、このメソッドの処理を初めから行う
		//						extendBlackBoxOnlyOneAdjacentIsolationNotDeployedBoxGroup();
		//					}
		//				}
		//			}
		//		}
		//	}
		//}

		/*********************************
		 * 
		 * 黒マス周りの処理
		 * 引数
		 * solve_limit	:	ヒント生成後の盤面にあってほしい未定マスの数
		 *   
		 * *******************************/
		public static bool is_count_bb = false;
		public static void manageBlackBox(int solve_limit = -1)
		{
			int first_count = Tapa.not_deployedbox_coord_list.Count;
			Tapa.sw_csv.Restart();	// CSV
			// 孤立した黒マス群のリストを見て、伸び代が1つしかない黒マス群があればそこを黒に塗る。
			extendIsolationBlackBoxGroup(solve_limit);
			Tapa.sum_times_kuromasu += Tapa.sw_csv.ElapsedMilliseconds;	// CSV
			Tapa.visittimes_kuromasu++;

			/////////////
			if (Tapa.is_count) {
				Tapa.processnum_kuromasu += first_count - Tapa.not_deployedbox_coord_list.Count;
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

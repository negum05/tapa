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
		public static readonly int NOCOLOR = -1;    // 色未定
		public static readonly int WHITE = 0;       // 白色
		public static readonly int BLACK = 1;       // 黒色

		public static bool during_clone = false;	// true:クローン処理中

		public Coordinates coord;					// 座標
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
						// 塗る色が黒かつ伸び代があれば、伸び代のある黒マスリストに追加
						if ((this.color == Box.BLACK) && canExtendBlackBox(this.coord)) {
							Tapa.edge_blackbox_coord_list.Add(this.coord);
						}
						Tapa.not_deployedbox_coord_list.Remove(this.coord);
					}
				}
			}
		}
		public bool can_extend_blackbox;			// true:伸び代のある黒マス

		public Box()
		{
			this.coord = new Coordinates();
			changed_count_in_search_confirm_box = 0;
			hasNum = false;
			this.box_num = -1;
			this.id_list = new List<byte>();
			this.color = Box.NOCOLOR;
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
		private static void extendBlackBox(int ite_coord)
		{
			Coordinates tmp_coord = new Coordinates(Tapa.edge_blackbox_coord_list[ite_coord]);

			Box T = new Box(Tapa.box[tmp_coord.x - 1][tmp_coord.y]);	// TOP
			Box R = new Box(Tapa.box[tmp_coord.x][tmp_coord.y + 1]);	// RIGHT
			Box B = new Box(Tapa.box[tmp_coord.x + 1][tmp_coord.y]);	// BOTTOM
			Box L = new Box(Tapa.box[tmp_coord.x][tmp_coord.y - 1]);	// LEFT

			// 伸び代のある黒マスが予期せぬ動きをしたら見てみるといいかも
			//Console.WriteLine("(" + tmp_coord.x + "," + tmp_coord.y + ')');
			//Console.WriteLine("T >> " + "(" + T.coord.x + "," + T.coord.y + ") " + T.color.ToString() + " " + T.hasNum.ToString());
			//Console.WriteLine("R >> " + "(" + R.coord.x + "," + R.coord.y + ") " + R.color.ToString() + " " + R.hasNum.ToString());
			//Console.WriteLine("B >> " + "(" + B.coord.x + "," + B.coord.y + ") " + B.color.ToString() + " " + B.hasNum.ToString());
			//Console.WriteLine("L >> " + "(" + L.coord.x + "," + L.coord.y + ") " + L.color.ToString() + " " + L.hasNum.ToString());
			//Console.WriteLine();

			if (T.Color == Box.NOCOLOR && R.Color == Box.WHITE && B.Color == Box.WHITE && L.Color == Box.WHITE) {
				Tapa.box[tmp_coord.x - 1][tmp_coord.y].Color = Box.BLACK;	// 上のマスを黒に
				Tapa.edge_blackbox_coord_list.RemoveAt(ite_coord);
			}
			else if (T.Color == Box.WHITE && R.Color == Box.NOCOLOR && B.Color == Box.WHITE && L.Color == Box.WHITE) {
				Tapa.box[tmp_coord.x][tmp_coord.y + 1].Color = Box.BLACK;	// 右のマスを黒に
				Tapa.edge_blackbox_coord_list.RemoveAt(ite_coord);
			}
			else if (T.Color == Box.WHITE && R.Color == Box.WHITE && B.Color == Box.NOCOLOR && L.Color == Box.WHITE) {
				Tapa.box[tmp_coord.x + 1][tmp_coord.y].Color = Box.BLACK;	// 下のマスを黒に
				Tapa.edge_blackbox_coord_list.RemoveAt(ite_coord);
			}
			else if (T.Color == Box.WHITE && R.Color == Box.WHITE && B.Color == Box.WHITE && L.Color == Box.NOCOLOR) {
				Tapa.box[tmp_coord.x][tmp_coord.y - 1].Color = Box.BLACK;	// 左のマスを黒に
				Tapa.edge_blackbox_coord_list.RemoveAt(ite_coord);
			}
		}

		/*********************************
		 * 
		 * 黒マスが2*2の四角（団子、Dumpling）にならないように白マスを配置するメソッド
		 * 
		 * 黒マスの上下左右の未定マスに注目し、
		 * 黒マスの周囲8マスを見てその未定マスが黒になると団子になってしまう場合、
		 * そのマスを白マスにする。
		 *   
		 * *******************************/
		private static void avoidDumpling(int ite_coord)
		{
			Coordinates tmp_coord = new Coordinates(Tapa.edge_blackbox_coord_list[ite_coord]);
			if (Tapa.box[tmp_coord.x][tmp_coord.y].Color != Box.BLACK) {
				Console.WriteLine("Error: 黒マスでないマスがedge_blackbox_coord_listに入っています ({0},{1})", tmp_coord.x, tmp_coord.y);
			}

			// ### 浅いコピー
			Box TL = Tapa.box[tmp_coord.x - 1][tmp_coord.y - 1];	// (左上)	Top-Left
			Box TC = Tapa.box[tmp_coord.x - 1][tmp_coord.y];		// (上)		Top-Center
			Box TR = Tapa.box[tmp_coord.x - 1][tmp_coord.y + 1];	// (右上)	Top-Right
			Box MR = Tapa.box[tmp_coord.x][tmp_coord.y + 1];		// (右)		Middle-Right
			Box BR = Tapa.box[tmp_coord.x + 1][tmp_coord.y + 1];	// (右下)	Bottom-Right
			Box BC = Tapa.box[tmp_coord.x + 1][tmp_coord.y];		// (下)		Bottom-Center
			Box BL = Tapa.box[tmp_coord.x + 1][tmp_coord.y - 1];	// (左下)	Bottom-Left
			Box ML = Tapa.box[tmp_coord.x][tmp_coord.y - 1];		// (左)		Middle-Left

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

		public static void manageBlackBox()
		{
			for (int ite_coord = Tapa.edge_blackbox_coord_list.Count - 1; ite_coord >= 0; ite_coord--) {
				avoidDumpling(ite_coord);
				extendBlackBox(ite_coord);
				// 伸び代がなければ、その座標をリストから除外
				if (!canExtendBlackBox(Tapa.edge_blackbox_coord_list[ite_coord])) {
					Tapa.edge_blackbox_coord_list.RemoveAt(ite_coord);
					continue;
				}
			}
		}
	}
}

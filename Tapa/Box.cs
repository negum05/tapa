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
				// 数字マスのid_listを見て、数字マス周りで色が確定するマスを探す間、異なる色に変更されそうになればインクリメント
				if (this.color != value) { changed_count_in_search_confirm_box++; }	
				if (this.color == Box.NOCOLOR) {
					// 塗る色が黒であれば、伸び代のある黒マスリストに追加
					if (value == Box.BLACK) { Tapa.edge_blackbox_coord_list.Add(this.coord); }
					this.color = value;
					Tapa.not_deployedbox_coord_list.Remove(this.coord);
				}
			}
		}

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

        public void clear()
        {
			this.coord = new Coordinates();
			this.changed_count_in_search_confirm_box = 0;
            this.has_num = false;
            this.box_num = -1;
			this.id_list.Clear();
            this.color = Box.NOCOLOR;
        }

		// 数字マスの数字の表示
        public void printBoxNum()
        {
            if (!this.has_num) {
                if (this.color == Box.WHITE) { Console.Write("==== "); }
                else if(this.color == Box.BLACK) { Console.Write("**** "); }
                else { Console.Write("---- "); }
            }
            else {
                int rest = 5 - this.box_num.ToString().Length;
                Console.Write(this.box_num);
                while (rest-- > 0) { Console.Write(" "); }
            }
        }

		// 数字マスのid_listの表示
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
		 * 伸び代のある黒マスリストから、
		 * 黒マスの上下左右で3つが白マス、1つが未定マスならそれを黒にし、リストから除外する。
		 *   
		 * *******************************/
		public static void extendBlackBox()
		{
			for(int ite_coord = Tapa.edge_blackbox_coord_list.Count - 1; ite_coord >= 0; ite_coord--) {

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
					Tapa.box[tmp_coord.x][tmp_coord.y - 1].Color = Box.BLACK;	// 上のマスを黒に
					Tapa.edge_blackbox_coord_list.RemoveAt(ite_coord);
				}
			}
		}
    }
}

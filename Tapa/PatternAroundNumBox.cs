using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tapa
{
	class PatternAroundNumBox
	{
		// 数字ごとの黒マスパターンの先頭id
		public static readonly byte HEAD_BN_0 = 0;
		public static readonly byte HEAD_BN_1 = 1;
		public static readonly byte HEAD_BN_2 = 9;
		public static readonly byte HEAD_BN_3 = 17;
		public static readonly byte HEAD_BN_4 = 25;
		public static readonly byte HEAD_BN_5 = 33;
		public static readonly byte HEAD_BN_6 = 41;
		public static readonly byte HEAD_BN_7 = 49;
		public static readonly byte HEAD_BN_8 = 57;
		public static readonly byte HEAD_BN_11 = 58;
		public static readonly byte HEAD_BN_12 = 78;
		public static readonly byte HEAD_BN_13 = 110;
		public static readonly byte HEAD_BN_14 = 134;
		public static readonly byte HEAD_BN_15 = 150;
		public static readonly byte HEAD_BN_22 = 158;
		public static readonly byte HEAD_BN_23 = 170;
		public static readonly byte HEAD_BN_24 = 186;
		public static readonly byte HEAD_BN_33 = 194;
		public static readonly byte HEAD_BN_111 = 198;
		public static readonly byte HEAD_BN_112 = 214;
		public static readonly byte HEAD_BN_113 = 238;
		public static readonly byte HEAD_BN_122 = 246;
		public static readonly byte HEAD_BN_1111 = 254;
		public static readonly byte MIN_ID = 0;
		public static readonly byte MAX_ID = 255;

		// idとマスの数字の連想配列
		public static Dictionary<byte, int> id_num_dict
			= new Dictionary<byte, int>() {
                {HEAD_BN_0, 0},
                {HEAD_BN_1, 1},
                {HEAD_BN_2, 2},
                {HEAD_BN_3, 3},
                {HEAD_BN_4, 4},
                {HEAD_BN_5, 5},
                {HEAD_BN_6, 6},
                {HEAD_BN_7, 7},
                {HEAD_BN_8, 8},
                {HEAD_BN_11, 11},
                {HEAD_BN_12, 12},
                {HEAD_BN_13, 13},
                {HEAD_BN_14, 14},
                {HEAD_BN_15, 15},
                {HEAD_BN_22, 22},
                {HEAD_BN_23, 23},
                {HEAD_BN_24, 24},
                {HEAD_BN_33, 33},
                {HEAD_BN_111, 111},
                {HEAD_BN_112, 112},
                {HEAD_BN_113, 113},
                {HEAD_BN_122, 122},
                {HEAD_BN_1111, 1111},
            };

		/*********************************
		 * 
		 * 数字マスの数字から、数字マス周りで配置可能なパターンの全idを返す。
		 * id : 各パターンを識別するための数値(型はbyte)
		 * 引数
		 * box_num : 数字マスの値（[1 2]なら12）
		 *   
		 * *******************************/
		static List<byte> getPatternAroundNumBoxList(int box_num)
		{
			List<byte> byte_list = new List<byte>();
			byte head_id = 0;
			byte tail_id = 0;
			// idの振り分け
			if (box_num == id_num_dict[HEAD_BN_0]) {
				head_id = (byte)HEAD_BN_0;
				tail_id = (byte)(HEAD_BN_1 - 1);
			}
			else if (id_num_dict[HEAD_BN_1] == box_num) {	// [1]
				head_id = (byte)HEAD_BN_1;
				tail_id = (byte)(HEAD_BN_2 - 1);
			}
			else if (id_num_dict[HEAD_BN_2] == box_num) {	// [2]
				head_id = (byte)HEAD_BN_2;
				tail_id = (byte)(HEAD_BN_3 - 1);
			}
			else if (id_num_dict[HEAD_BN_3] == box_num) {	// [3]
				head_id = (byte)HEAD_BN_3;
				tail_id = (byte)(HEAD_BN_4 - 1);
			}
			else if (id_num_dict[HEAD_BN_4] == box_num) {	// [4]
				head_id = (byte)HEAD_BN_4;
				tail_id = (byte)(HEAD_BN_5 - 1);
			}
			else if (id_num_dict[HEAD_BN_5] == box_num) {	// [5]
				head_id = (byte)HEAD_BN_5;
				tail_id = (byte)(HEAD_BN_6 - 1);
			}
			else if (id_num_dict[HEAD_BN_6] == box_num) {	// [6]
				head_id = (byte)HEAD_BN_6;
				tail_id = (byte)(HEAD_BN_7 - 1);
			}
			else if (id_num_dict[HEAD_BN_7] == box_num) {	// [7]
				head_id = (byte)HEAD_BN_7;
				tail_id = (byte)(HEAD_BN_8 - 1);
			}
			else if (id_num_dict[HEAD_BN_8] == box_num) {	// [8]
				head_id = (byte)HEAD_BN_8;
				tail_id = (byte)(HEAD_BN_11 - 1);
			}
			else if (id_num_dict[HEAD_BN_11] == box_num) {	// [11]
				head_id = (byte)HEAD_BN_11;
				tail_id = (byte)(HEAD_BN_12 - 1);
			}
			else if (id_num_dict[HEAD_BN_12] == box_num) {	// [12]
				head_id = (byte)HEAD_BN_12;
				tail_id = (byte)(HEAD_BN_13 - 1);
			}
			else if (id_num_dict[HEAD_BN_13] == box_num) {	// [13]
				head_id = (byte)HEAD_BN_13;
				tail_id = (byte)(HEAD_BN_14 - 1);
			}
			else if (id_num_dict[HEAD_BN_14] == box_num) {	// [14]
				head_id = (byte)HEAD_BN_14;
				tail_id = (byte)(HEAD_BN_15 - 1);
			}
			else if (id_num_dict[HEAD_BN_15] == box_num) {	// [15]
				head_id = (byte)HEAD_BN_15;
				tail_id = (byte)(HEAD_BN_22 - 1);
			}
			else if (id_num_dict[HEAD_BN_22] == box_num) {	// [22]
				head_id = (byte)HEAD_BN_22;
				tail_id = (byte)(HEAD_BN_23 - 1);
			}
			else if (id_num_dict[HEAD_BN_23] == box_num) {	// [23]
				head_id = (byte)HEAD_BN_23;
				tail_id = (byte)(HEAD_BN_24 - 1);
			}
			else if (id_num_dict[HEAD_BN_24] == box_num) {	// [24]
				head_id = (byte)HEAD_BN_24;
				tail_id = (byte)(HEAD_BN_33 - 1);
			}
			else if (id_num_dict[HEAD_BN_33] == box_num) {	// [33]
				head_id = (byte)HEAD_BN_33;
				tail_id = (byte)(HEAD_BN_111 - 1);
			}
			else if (id_num_dict[HEAD_BN_111] == box_num) {	// [111]
				head_id = (byte)HEAD_BN_111;
				tail_id = (byte)(HEAD_BN_112 - 1);
			}
			else if (id_num_dict[HEAD_BN_112] == box_num) {	// [112]
				head_id = (byte)HEAD_BN_112;
				tail_id = (byte)(HEAD_BN_113 - 1);
			}
			else if (id_num_dict[HEAD_BN_113] == box_num) {	// [113]
				head_id = (byte)HEAD_BN_113;
				tail_id = (byte)(HEAD_BN_122 - 1);
			}
			else if (id_num_dict[HEAD_BN_122] == box_num) {	// [122]
				head_id = (byte)HEAD_BN_122;
				tail_id = (byte)(HEAD_BN_1111 - 1);
			}
			else if (id_num_dict[HEAD_BN_1111] == box_num) {	// [1111]
				head_id = (byte)HEAD_BN_1111;
				tail_id = (byte)(MAX_ID);
			}
			else {
				Console.WriteLine("Error: 数字マスのid振り分けでエラー[box_num >> {0}]", box_num);
				Application.Exit();
			}

			// idをリストにする
			for (byte i = head_id; i <= tail_id; i++) {
				byte_list.Add(i);
				if (i == MAX_ID) { break; }
			}
			return byte_list;
		}

		/*********************************
		 * 
		 * 座標とidから座標周りでidの配置方法が可能か判定する。
		 * 引数
		 * co		: マスの座標
		 * id       : 各パターンを識別するための数値(型はbyte)
		 *   
		 * *******************************/
		static private bool checkPatternAroundNumBox(Coordinates co, byte id)
		{
			int TL = Tapa.box[co.x - 1][co.y - 1].Color;    // 左上(Top-Left)
			int TC = Tapa.box[co.x - 1][co.y].Color;      // 中上(Top-Center)
			int TR = Tapa.box[co.x - 1][co.y + 1].Color;    // 右上(Top-Right)
			int ML = Tapa.box[co.x][co.y - 1].Color;      // 左中(Middle-Left)
			int MR = Tapa.box[co.x][co.y + 1].Color;      // 右中(Middle-Right)
			int BL = Tapa.box[co.x + 1][co.y - 1].Color;    // 左下(Bottom-Left)
			int BC = Tapa.box[co.x + 1][co.y].Color;      // 中下(Bottom-Center)
			int BR = Tapa.box[co.x + 1][co.y + 1].Color;    // 右下(Bottom-Right)

			// [0] id=0
			if (id == HEAD_BN_0) {
				if (TL != Box.BLACK && TC != Box.BLACK && TR != Box.BLACK
					&& ML != Box.BLACK/*				*/&& MR != Box.BLACK
					&& BL != Box.BLACK && BC != Box.BLACK && BR != Box.BLACK) { return true; }
				else { return false; }
			}
			// [1] id=1~8
			else if (HEAD_BN_1 <= id && id < HEAD_BN_2) {
				switch (id) {
					case (byte)1:
						if (TL != Box.WHITE && TC != Box.BLACK && TR != Box.BLACK
							&& ML != Box.BLACK /*				*/&& MR != Box.BLACK
							&& BL != Box.BLACK && BC != Box.BLACK && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)2:
						if (TL != Box.BLACK && TC != Box.WHITE && TR != Box.BLACK
							&& ML != Box.BLACK /*				*/&& MR != Box.BLACK
							&& BL != Box.BLACK && BC != Box.BLACK && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)3:
						if (TL != Box.BLACK && TC != Box.BLACK && TR != Box.WHITE
							&& ML != Box.BLACK /*				*/&& MR != Box.BLACK
							&& BL != Box.BLACK && BC != Box.BLACK && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)4:
						if (TL != Box.BLACK && TC != Box.BLACK && TR != Box.BLACK
							&& ML != Box.BLACK /*				*/&& MR != Box.WHITE
							&& BL != Box.BLACK && BC != Box.BLACK && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)5:
						if (TL != Box.BLACK && TC != Box.BLACK && TR != Box.BLACK
							&& ML != Box.BLACK /*				*/&& MR != Box.BLACK
							&& BL != Box.BLACK && BC != Box.BLACK && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)6:
						if (TL != Box.BLACK && TC != Box.BLACK && TR != Box.BLACK
							&& ML != Box.BLACK /*				*/&& MR != Box.BLACK
							&& BL != Box.BLACK && BC != Box.WHITE && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)7:
						if (TL != Box.BLACK && TC != Box.BLACK && TR != Box.BLACK
							&& ML != Box.BLACK /*				*/&& MR != Box.BLACK
							&& BL != Box.WHITE && BC != Box.BLACK && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)8:
						if (TL != Box.BLACK && TC != Box.BLACK && TR != Box.BLACK
							&& ML != Box.WHITE /*				*/&& MR != Box.BLACK
							&& BL != Box.BLACK && BC != Box.BLACK && BR != Box.BLACK) { return true; }
						else { return false; }
					default:
						Console.WriteLine("Error: 数字マス[1]周りのパターンチェック");
						Application.Exit();
						return false;
				}
			}
			// [2] id=9~16
			else if (HEAD_BN_2 <= id && id < HEAD_BN_3) {
				switch (id) {
					case (byte)9:
						if (TL != Box.WHITE && TC != Box.WHITE && TR != Box.BLACK
							&& ML != Box.BLACK && MR != Box.BLACK
							&& BL != Box.BLACK && BC != Box.BLACK && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)10:
						if (TL != Box.BLACK && TC != Box.WHITE && TR != Box.WHITE
							&& ML != Box.BLACK && MR != Box.BLACK
							&& BL != Box.BLACK && BC != Box.BLACK && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)11:
						if (TL != Box.BLACK && TC != Box.BLACK && TR != Box.WHITE
							&& ML != Box.BLACK && MR != Box.WHITE
							&& BL != Box.BLACK && BC != Box.BLACK && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)12:
						if (TL != Box.BLACK && TC != Box.BLACK && TR != Box.BLACK
							&& ML != Box.BLACK && MR != Box.WHITE
							&& BL != Box.BLACK && BC != Box.BLACK && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)13:
						if (TL != Box.BLACK && TC != Box.BLACK && TR != Box.BLACK
							&& ML != Box.BLACK && MR != Box.BLACK
							&& BL != Box.BLACK && BC != Box.WHITE && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)14:
						if (TL != Box.BLACK && TC != Box.BLACK && TR != Box.BLACK
							&& ML != Box.BLACK && MR != Box.BLACK
							&& BL != Box.WHITE && BC != Box.WHITE && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)15:
						if (TL != Box.BLACK && TC != Box.BLACK && TR != Box.BLACK
							&& ML != Box.WHITE && MR != Box.BLACK
							&& BL != Box.WHITE && BC != Box.BLACK && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)16:
						if (TL != Box.WHITE && TC != Box.BLACK && TR != Box.BLACK
							&& ML != Box.WHITE && MR != Box.BLACK
							&& BL != Box.BLACK && BC != Box.BLACK && BR != Box.BLACK) { return true; }
						else { return false; }
					default:
						Console.WriteLine("Error: 数字マス[2]周りのパターンチェック");
						Application.Exit();
						return false;
				}
			}
			// [3] id=17~24
			else if (HEAD_BN_3 <= id && id < HEAD_BN_4) {
				switch (id) {
					case (byte)17:
						if (TL != Box.WHITE && TC != Box.WHITE && TR != Box.WHITE
							&& ML != Box.BLACK && MR != Box.BLACK
							&& BL != Box.BLACK && BC != Box.BLACK && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)18:
						if (TL != Box.BLACK && TC != Box.WHITE && TR != Box.WHITE
							&& ML != Box.BLACK && MR != Box.WHITE
							&& BL != Box.BLACK && BC != Box.BLACK && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)19:
						if (TL != Box.BLACK && TC != Box.BLACK && TR != Box.WHITE
							&& ML != Box.BLACK && MR != Box.WHITE
							&& BL != Box.BLACK && BC != Box.BLACK && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)20:
						if (TL != Box.BLACK && TC != Box.BLACK && TR != Box.BLACK
							&& ML != Box.BLACK && MR != Box.WHITE
							&& BL != Box.BLACK && BC != Box.WHITE && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)21:
						if (TL != Box.BLACK && TC != Box.BLACK && TR != Box.BLACK
							&& ML != Box.BLACK && MR != Box.BLACK
							&& BL != Box.WHITE && BC != Box.WHITE && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)22:
						if (TL != Box.BLACK && TC != Box.BLACK && TR != Box.BLACK
							&& ML != Box.WHITE && MR != Box.BLACK
							&& BL != Box.WHITE && BC != Box.WHITE && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)23:
						if (TL != Box.WHITE && TC != Box.BLACK && TR != Box.BLACK
							&& ML != Box.WHITE && MR != Box.BLACK
							&& BL != Box.WHITE && BC != Box.BLACK && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)24:
						if (TL != Box.WHITE && TC != Box.WHITE && TR != Box.BLACK
							&& ML != Box.WHITE && MR != Box.BLACK
							&& BL != Box.BLACK && BC != Box.BLACK && BR != Box.BLACK) { return true; }
						else { return false; }
					default:
						Console.WriteLine("Error: 数字マス[3]周りのパターンチェック");
						Application.Exit();
						return false;
				}
			}
			// [4] id=25~32
			else if (HEAD_BN_4 <= id && id < HEAD_BN_5) {
				switch (id) {
					case (byte)25:
						if (TL != Box.WHITE && TC != Box.WHITE && TR != Box.WHITE
							&& ML != Box.BLACK && MR != Box.WHITE
							&& BL != Box.BLACK && BC != Box.BLACK && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)26:
						if (TL != Box.BLACK && TC != Box.WHITE && TR != Box.WHITE
							&& ML != Box.BLACK && MR != Box.WHITE
							&& BL != Box.BLACK && BC != Box.BLACK && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)27:
						if (TL != Box.BLACK && TC != Box.BLACK && TR != Box.WHITE
							&& ML != Box.BLACK && MR != Box.WHITE
							&& BL != Box.BLACK && BC != Box.WHITE && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)28:
						if (TL != Box.BLACK && TC != Box.BLACK && TR != Box.BLACK
							&& ML != Box.BLACK && MR != Box.WHITE
							&& BL != Box.WHITE && BC != Box.WHITE && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)29:
						if (TL != Box.BLACK && TC != Box.BLACK && TR != Box.BLACK
							&& ML != Box.WHITE && MR != Box.BLACK
							&& BL != Box.WHITE && BC != Box.WHITE && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)30:
						if (TL != Box.WHITE && TC != Box.BLACK && TR != Box.BLACK
							&& ML != Box.WHITE && MR != Box.BLACK
							&& BL != Box.WHITE && BC != Box.WHITE && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)31:
						if (TL != Box.WHITE && TC != Box.WHITE && TR != Box.BLACK
							&& ML != Box.WHITE && MR != Box.BLACK
							&& BL != Box.WHITE && BC != Box.BLACK && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)32:
						if (TL != Box.WHITE && TC != Box.WHITE && TR != Box.WHITE
							&& ML != Box.WHITE && MR != Box.BLACK
							&& BL != Box.BLACK && BC != Box.BLACK && BR != Box.BLACK) { return true; }
						else { return false; }
					default:
						Console.WriteLine("Error: 数字マス[4]周りのパターンチェック");
						Application.Exit();
						return false;
				}
			}
			// [5] id=33~40
			else if (HEAD_BN_5 <= id && id < HEAD_BN_6) {
				switch (id) {
					case (byte)33:
						if (TL != Box.WHITE && TC != Box.WHITE && TR != Box.WHITE
							&& ML != Box.BLACK && MR != Box.WHITE
							&& BL != Box.BLACK && BC != Box.BLACK && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)34:
						if (TL != Box.BLACK && TC != Box.WHITE && TR != Box.WHITE
							&& ML != Box.BLACK && MR != Box.WHITE
							&& BL != Box.BLACK && BC != Box.WHITE && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)35:
						if (TL != Box.BLACK && TC != Box.BLACK && TR != Box.WHITE
							&& ML != Box.BLACK && MR != Box.WHITE
							&& BL != Box.WHITE && BC != Box.WHITE && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)36:
						if (TL != Box.BLACK && TC != Box.BLACK && TR != Box.BLACK
							&& ML != Box.WHITE && MR != Box.WHITE
							&& BL != Box.WHITE && BC != Box.WHITE && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)37:
						if (TL != Box.WHITE && TC != Box.BLACK && TR != Box.BLACK
							&& ML != Box.WHITE && MR != Box.BLACK
							&& BL != Box.WHITE && BC != Box.WHITE && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)38:
						if (TL != Box.WHITE && TC != Box.WHITE && TR != Box.BLACK
							&& ML != Box.WHITE && MR != Box.BLACK
							&& BL != Box.WHITE && BC != Box.WHITE && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)39:
						if (TL != Box.WHITE && TC != Box.WHITE && TR != Box.WHITE
							&& ML != Box.WHITE && MR != Box.BLACK
							&& BL != Box.WHITE && BC != Box.BLACK && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)40:
						if (TL != Box.WHITE && TC != Box.WHITE && TR != Box.WHITE
							&& ML != Box.WHITE && MR != Box.WHITE
							&& BL != Box.BLACK && BC != Box.BLACK && BR != Box.BLACK) { return true; }
						else { return false; }
					default:
						Console.WriteLine("Error: 数字マス[5]周りのパターンチェック");
						Application.Exit();
						return false;
				}
			}
			// [6] id=41~48
			else if (HEAD_BN_6 <= id && id < HEAD_BN_7) {
				switch (id) {
					case (byte)41:
						if (TL != Box.WHITE && TC != Box.WHITE && TR != Box.WHITE
							&& ML != Box.BLACK && MR != Box.WHITE
							&& BL != Box.BLACK && BC != Box.WHITE && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)42:
						if (TL != Box.BLACK && TC != Box.WHITE && TR != Box.WHITE
							&& ML != Box.BLACK && MR != Box.WHITE
							&& BL != Box.WHITE && BC != Box.WHITE && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)43:
						if (TL != Box.BLACK && TC != Box.BLACK && TR != Box.WHITE
							&& ML != Box.WHITE && MR != Box.WHITE
							&& BL != Box.WHITE && BC != Box.WHITE && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)44:
						if (TL != Box.WHITE && TC != Box.BLACK && TR != Box.BLACK
							&& ML != Box.WHITE && MR != Box.WHITE
							&& BL != Box.WHITE && BC != Box.WHITE && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)45:
						if (TL != Box.WHITE && TC != Box.WHITE && TR != Box.BLACK
							&& ML != Box.WHITE && MR != Box.BLACK
							&& BL != Box.WHITE && BC != Box.WHITE && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)46:
						if (TL != Box.WHITE && TC != Box.WHITE && TR != Box.WHITE
							&& ML != Box.WHITE && MR != Box.BLACK
							&& BL != Box.WHITE && BC != Box.WHITE && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)47:
						if (TL != Box.WHITE && TC != Box.WHITE && TR != Box.WHITE
							&& ML != Box.WHITE && MR != Box.WHITE
							&& BL != Box.WHITE && BC != Box.BLACK && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)48:
						if (TL != Box.WHITE && TC != Box.WHITE && TR != Box.WHITE
							&& ML != Box.WHITE && MR != Box.WHITE
							&& BL != Box.BLACK && BC != Box.BLACK && BR != Box.WHITE) { return true; }
						else { return false; }
					default:
						Console.WriteLine("Error: 数字マス[6]周りのパターンチェック");
						Application.Exit();
						return false;
				}
			}
			// [7] id=49~56
			else if (HEAD_BN_7 <= id && id < HEAD_BN_8) {
				switch (id) {
					case (byte)49:
						if (TL != Box.WHITE && TC != Box.WHITE && TR != Box.WHITE
							&& ML != Box.BLACK && MR != Box.WHITE
							&& BL != Box.WHITE && BC != Box.WHITE && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)50:
						if (TL != Box.BLACK && TC != Box.WHITE && TR != Box.WHITE
							&& ML != Box.WHITE && MR != Box.WHITE
							&& BL != Box.WHITE && BC != Box.WHITE && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)51:
						if (TL != Box.WHITE && TC != Box.BLACK && TR != Box.WHITE
							&& ML != Box.WHITE && MR != Box.WHITE
							&& BL != Box.WHITE && BC != Box.WHITE && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)52:
						if (TL != Box.WHITE && TC != Box.WHITE && TR != Box.BLACK
							&& ML != Box.WHITE && MR != Box.WHITE
							&& BL != Box.WHITE && BC != Box.WHITE && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)53:
						if (TL != Box.WHITE && TC != Box.WHITE && TR != Box.WHITE
							&& ML != Box.WHITE && MR != Box.BLACK
							&& BL != Box.WHITE && BC != Box.WHITE && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)54:
						if (TL != Box.WHITE && TC != Box.WHITE && TR != Box.WHITE
							&& ML != Box.WHITE && MR != Box.WHITE
							&& BL != Box.WHITE && BC != Box.WHITE && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)55:
						if (TL != Box.WHITE && TC != Box.WHITE && TR != Box.WHITE
							&& ML != Box.WHITE && MR != Box.WHITE
							&& BL != Box.WHITE && BC != Box.BLACK && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)56:
						if (TL != Box.WHITE && TC != Box.WHITE && TR != Box.WHITE
							&& ML != Box.WHITE && MR != Box.WHITE
							&& BL != Box.BLACK && BC != Box.WHITE && BR != Box.WHITE) { return true; }
						else { return false; }
					default:
						Console.WriteLine("Error: 数字マス[7]周りのパターンチェック");
						Application.Exit();
						return false;
				}
			}
			// [8] id=57
			else if (id == HEAD_BN_8) {
				if (TL != Box.WHITE && TC != Box.WHITE && TR != Box.WHITE
					&& ML != Box.WHITE && MR != Box.WHITE
					&& BL != Box.WHITE && BC != Box.WHITE && BR != Box.WHITE) { return true; }
				else { return false; }
			}
			// [11] id=58~77
			else if (HEAD_BN_11 <= id && id < HEAD_BN_12) {
				switch (id) {
					case (byte)58:
						if (TL != Box.WHITE && TC != Box.BLACK && TR != Box.WHITE
							&& ML != Box.BLACK && MR != Box.BLACK
							&& BL != Box.BLACK && BC != Box.BLACK && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)59:
						if (TL != Box.WHITE && TC != Box.BLACK && TR != Box.BLACK
							&& ML != Box.BLACK && MR != Box.WHITE
							&& BL != Box.BLACK && BC != Box.BLACK && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)60:
						if (TL != Box.WHITE && TC != Box.BLACK && TR != Box.BLACK
							&& ML != Box.BLACK && MR != Box.BLACK
							&& BL != Box.BLACK && BC != Box.BLACK && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)61:
						if (TL != Box.WHITE && TC != Box.BLACK && TR != Box.BLACK
							&& ML != Box.BLACK && MR != Box.BLACK
							&& BL != Box.BLACK && BC != Box.WHITE && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)62:
						if (TL != Box.WHITE && TC != Box.BLACK && TR != Box.BLACK
							&& ML != Box.BLACK && MR != Box.BLACK
							&& BL != Box.WHITE && BC != Box.BLACK && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)63:
						if (TL != Box.BLACK && TC != Box.WHITE && TR != Box.BLACK
							&& ML != Box.BLACK && MR != Box.WHITE
							&& BL != Box.BLACK && BC != Box.BLACK && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)64:
						if (TL != Box.BLACK && TC != Box.WHITE && TR != Box.BLACK
							&& ML != Box.BLACK && MR != Box.BLACK
							&& BL != Box.BLACK && BC != Box.BLACK && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)65:
						if (TL != Box.BLACK && TC != Box.WHITE && TR != Box.BLACK
							&& ML != Box.BLACK && MR != Box.BLACK
							&& BL != Box.BLACK && BC != Box.WHITE && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)66:
						if (TL != Box.BLACK && TC != Box.WHITE && TR != Box.BLACK
							&& ML != Box.BLACK && MR != Box.BLACK
							&& BL != Box.WHITE && BC != Box.BLACK && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)67:
						if (TL != Box.BLACK && TC != Box.WHITE && TR != Box.BLACK
							&& ML != Box.WHITE && MR != Box.BLACK
							&& BL != Box.BLACK && BC != Box.BLACK && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)68:
						if (TL != Box.BLACK && TC != Box.BLACK && TR != Box.WHITE
							&& ML != Box.BLACK && MR != Box.BLACK
							&& BL != Box.BLACK && BC != Box.BLACK && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)69:
						if (TL != Box.BLACK && TC != Box.BLACK && TR != Box.WHITE
							&& ML != Box.BLACK && MR != Box.BLACK
							&& BL != Box.BLACK && BC != Box.WHITE && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)70:
						if (TL != Box.BLACK && TC != Box.BLACK && TR != Box.WHITE
							&& ML != Box.BLACK && MR != Box.BLACK
							&& BL != Box.WHITE && BC != Box.BLACK && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)71:
						if (TL != Box.BLACK && TC != Box.BLACK && TR != Box.WHITE
							&& ML != Box.WHITE && MR != Box.BLACK
							&& BL != Box.BLACK && BC != Box.BLACK && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)72:
						if (TL != Box.BLACK && TC != Box.BLACK && TR != Box.BLACK
							&& ML != Box.BLACK && MR != Box.WHITE
							&& BL != Box.BLACK && BC != Box.WHITE && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)73:
						if (TL != Box.BLACK && TC != Box.BLACK && TR != Box.BLACK
							&& ML != Box.BLACK && MR != Box.WHITE
							&& BL != Box.WHITE && BC != Box.BLACK && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)74:
						if (TL != Box.BLACK && TC != Box.BLACK && TR != Box.BLACK
							&& ML != Box.WHITE && MR != Box.WHITE
							&& BL != Box.BLACK && BC != Box.BLACK && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)75:
						if (TL != Box.BLACK && TC != Box.BLACK && TR != Box.BLACK
							&& ML != Box.BLACK && MR != Box.BLACK
							&& BL != Box.WHITE && BC != Box.BLACK && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)76:
						if (TL != Box.BLACK && TC != Box.BLACK && TR != Box.BLACK
							&& ML != Box.WHITE && MR != Box.BLACK
							&& BL != Box.BLACK && BC != Box.BLACK && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)77:
						if (TL != Box.BLACK && TC != Box.BLACK && TR != Box.BLACK
							&& ML != Box.WHITE && MR != Box.BLACK
							&& BL != Box.BLACK && BC != Box.WHITE && BR != Box.BLACK) { return true; }
						else { return false; }
					default:
						Console.WriteLine("Error: 数字マス[11]周りのパターンチェック");
						Application.Exit();
						return false;
				}
			}
			// [12] id=78~109
			else if (HEAD_BN_12 <= id && id < HEAD_BN_13) {
				switch (id) {
					case (byte)78:
						if (TL != Box.WHITE && TC != Box.BLACK && TR != Box.WHITE
							&& ML != Box.BLACK && MR != Box.WHITE
							&& BL != Box.BLACK && BC != Box.BLACK && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)79:
						if (TL != Box.WHITE && TC != Box.BLACK && TR != Box.BLACK
							&& ML != Box.BLACK && MR != Box.WHITE
							&& BL != Box.BLACK && BC != Box.BLACK && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)80:
						if (TL != Box.WHITE && TC != Box.BLACK && TR != Box.BLACK
							&& ML != Box.BLACK && MR != Box.BLACK
							&& BL != Box.BLACK && BC != Box.WHITE && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)81:
						if (TL != Box.WHITE && TC != Box.BLACK && TR != Box.BLACK
							&& ML != Box.BLACK && MR != Box.BLACK
							&& BL != Box.WHITE && BC != Box.WHITE && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)82:
						if (TL != Box.BLACK && TC != Box.WHITE && TR != Box.BLACK
							&& ML != Box.BLACK && MR != Box.WHITE
							&& BL != Box.BLACK && BC != Box.BLACK && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)83:
						if (TL != Box.BLACK && TC != Box.WHITE && TR != Box.BLACK
							&& ML != Box.BLACK && MR != Box.BLACK
							&& BL != Box.BLACK && BC != Box.WHITE && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)84:
						if (TL != Box.BLACK && TC != Box.WHITE && TR != Box.BLACK
							&& ML != Box.BLACK && MR != Box.BLACK
							&& BL != Box.WHITE && BC != Box.WHITE && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)85:
						if (TL != Box.BLACK && TC != Box.WHITE && TR != Box.BLACK
							&& ML != Box.WHITE && MR != Box.BLACK
							&& BL != Box.WHITE && BC != Box.BLACK && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)86:
						if (TL != Box.BLACK && TC != Box.BLACK && TR != Box.WHITE
							&& ML != Box.BLACK && MR != Box.BLACK
							&& BL != Box.BLACK && BC != Box.WHITE && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)87:
						if (TL != Box.BLACK && TC != Box.BLACK && TR != Box.WHITE
							&& ML != Box.BLACK && MR != Box.BLACK
							&& BL != Box.WHITE && BC != Box.WHITE && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)88:
						if (TL != Box.BLACK && TC != Box.BLACK && TR != Box.WHITE
							&& ML != Box.WHITE && MR != Box.BLACK
							&& BL != Box.WHITE && BC != Box.BLACK && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)89:
						if (TL != Box.WHITE && TC != Box.BLACK && TR != Box.WHITE
							&& ML != Box.WHITE && MR != Box.BLACK
							&& BL != Box.BLACK && BC != Box.BLACK && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)90:
						if (TL != Box.BLACK && TC != Box.BLACK && TR != Box.BLACK
							&& ML != Box.BLACK && MR != Box.WHITE
							&& BL != Box.WHITE && BC != Box.WHITE && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)91:
						if (TL != Box.BLACK && TC != Box.BLACK && TR != Box.BLACK
							&& ML != Box.WHITE && MR != Box.WHITE
							&& BL != Box.WHITE && BC != Box.BLACK && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)92:
						if (TL != Box.WHITE && TC != Box.BLACK && TR != Box.BLACK
							&& ML != Box.WHITE && MR != Box.WHITE
							&& BL != Box.BLACK && BC != Box.BLACK && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)93:
						if (TL != Box.WHITE && TC != Box.WHITE && TR != Box.BLACK
							&& ML != Box.BLACK && MR != Box.WHITE
							&& BL != Box.BLACK && BC != Box.BLACK && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)94:
						if (TL != Box.BLACK && TC != Box.BLACK && TR != Box.BLACK
							&& ML != Box.WHITE && MR != Box.BLACK
							&& BL != Box.WHITE && BC != Box.BLACK && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)95:
						if (TL != Box.WHITE && TC != Box.BLACK && TR != Box.BLACK
							&& ML != Box.WHITE && MR != Box.BLACK
							&& BL != Box.BLACK && BC != Box.BLACK && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)96:
						if (TL != Box.WHITE && TC != Box.WHITE && TR != Box.BLACK
							&& ML != Box.BLACK && MR != Box.BLACK
							&& BL != Box.BLACK && BC != Box.BLACK && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)97:
						if (TL != Box.BLACK && TC != Box.WHITE && TR != Box.WHITE
							&& ML != Box.BLACK && MR != Box.BLACK
							&& BL != Box.BLACK && BC != Box.BLACK && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)98:
						if (TL != Box.WHITE && TC != Box.BLACK && TR != Box.BLACK
							&& ML != Box.WHITE && MR != Box.BLACK
							&& BL != Box.BLACK && BC != Box.WHITE && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)99:
						if (TL != Box.WHITE && TC != Box.WHITE && TR != Box.BLACK
							&& ML != Box.BLACK && MR != Box.BLACK
							&& BL != Box.BLACK && BC != Box.WHITE && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)100:
						if (TL != Box.BLACK && TC != Box.WHITE && TR != Box.WHITE
							&& ML != Box.BLACK && MR != Box.BLACK
							&& BL != Box.BLACK && BC != Box.WHITE && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)101:
						if (TL != Box.BLACK && TC != Box.BLACK && TR != Box.WHITE
							&& ML != Box.BLACK && MR != Box.WHITE
							&& BL != Box.BLACK && BC != Box.WHITE && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)102:
						if (TL != Box.WHITE && TC != Box.WHITE && TR != Box.BLACK
							&& ML != Box.BLACK && MR != Box.BLACK
							&& BL != Box.WHITE && BC != Box.BLACK && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)103:
						if (TL != Box.BLACK && TC != Box.WHITE && TR != Box.WHITE
							&& ML != Box.BLACK && MR != Box.BLACK
							&& BL != Box.WHITE && BC != Box.BLACK && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)104:
						if (TL != Box.BLACK && TC != Box.BLACK && TR != Box.WHITE
							&& ML != Box.BLACK && MR != Box.WHITE
							&& BL != Box.WHITE && BC != Box.BLACK && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)105:
						if (TL != Box.BLACK && TC != Box.BLACK && TR != Box.BLACK
							&& ML != Box.BLACK && MR != Box.WHITE
							&& BL != Box.WHITE && BC != Box.BLACK && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)106:
						if (TL != Box.BLACK && TC != Box.WHITE && TR != Box.WHITE
							&& ML != Box.WHITE && MR != Box.BLACK
							&& BL != Box.BLACK && BC != Box.BLACK && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)107:
						if (TL != Box.BLACK && TC != Box.BLACK && TR != Box.WHITE
							&& ML != Box.WHITE && MR != Box.WHITE
							&& BL != Box.BLACK && BC != Box.BLACK && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)108:
						if (TL != Box.BLACK && TC != Box.BLACK && TR != Box.BLACK
							&& ML != Box.WHITE && MR != Box.WHITE
							&& BL != Box.BLACK && BC != Box.BLACK && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)109:
						if (TL != Box.BLACK && TC != Box.BLACK && TR != Box.BLACK
							&& ML != Box.WHITE && MR != Box.BLACK
							&& BL != Box.BLACK && BC != Box.WHITE && BR != Box.WHITE) { return true; }
						else { return false; }
					default:
						Console.WriteLine("Error: 数字マス[12]周りのパターンチェック");
						Application.Exit();
						return false;
				}
			}
			// [13] id=110~133
			else if (HEAD_BN_13 <= id && id < HEAD_BN_14) {
				switch (id) {
					case (byte)110:
						if (TL != Box.WHITE && TC != Box.BLACK && TR != Box.WHITE
							&& ML != Box.BLACK && MR != Box.WHITE
							&& BL != Box.BLACK && BC != Box.BLACK && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)111:
						if (TL != Box.WHITE && TC != Box.BLACK && TR != Box.BLACK
							&& ML != Box.BLACK && MR != Box.WHITE
							&& BL != Box.BLACK && BC != Box.WHITE && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)112:
						if (TL != Box.WHITE && TC != Box.BLACK && TR != Box.BLACK
							&& ML != Box.BLACK && MR != Box.BLACK
							&& BL != Box.WHITE && BC != Box.WHITE && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)113:
						if (TL != Box.BLACK && TC != Box.WHITE && TR != Box.BLACK
							&& ML != Box.BLACK/*              */ && MR != Box.WHITE
							&& BL != Box.BLACK && BC != Box.WHITE && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)114:
						if (TL != Box.BLACK && TC != Box.WHITE && TR != Box.BLACK
							&& ML != Box.BLACK && MR != Box.BLACK
							&& BL != Box.WHITE && BC != Box.WHITE && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)115:
						if (TL != Box.BLACK && TC != Box.WHITE && TR != Box.BLACK
							&& ML != Box.WHITE && MR != Box.BLACK
							&& BL != Box.WHITE && BC != Box.WHITE && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)116:
						if (TL != Box.BLACK && TC != Box.BLACK && TR != Box.WHITE
							&& ML != Box.BLACK && MR != Box.BLACK
							&& BL != Box.WHITE && BC != Box.WHITE && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)117:
						if (TL != Box.BLACK && TC != Box.BLACK && TR != Box.WHITE
							&& ML != Box.WHITE && MR != Box.BLACK
							&& BL != Box.WHITE && BC != Box.WHITE && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)118:
						if (TL != Box.WHITE && TC != Box.BLACK && TR != Box.WHITE
							&& ML != Box.WHITE && MR != Box.BLACK
							&& BL != Box.WHITE && BC != Box.BLACK && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)119:
						if (TL != Box.BLACK && TC != Box.BLACK && TR != Box.BLACK
							&& ML != Box.WHITE && MR != Box.WHITE
							&& BL != Box.WHITE && BC != Box.WHITE && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)120:
						if (TL != Box.WHITE && TC != Box.BLACK && TR != Box.BLACK
							&& ML != Box.WHITE && MR != Box.WHITE
							&& BL != Box.WHITE && BC != Box.BLACK && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)121:
						if (TL != Box.WHITE && TC != Box.WHITE && TR != Box.BLACK
							&& ML != Box.WHITE && MR != Box.WHITE
							&& BL != Box.BLACK && BC != Box.BLACK && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)122:
						if (TL != Box.WHITE && TC != Box.BLACK && TR != Box.BLACK
							&& ML != Box.WHITE && MR != Box.BLACK
							&& BL != Box.WHITE && BC != Box.BLACK && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)123:
						if (TL != Box.WHITE && TC != Box.WHITE && TR != Box.BLACK
							&& ML != Box.WHITE && MR != Box.BLACK
							&& BL != Box.BLACK && BC != Box.BLACK && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)124:
						if (TL != Box.WHITE && TC != Box.WHITE && TR != Box.WHITE
							&& ML != Box.BLACK && MR != Box.BLACK
							&& BL != Box.BLACK && BC != Box.BLACK && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)125:
						if (TL != Box.WHITE && TC != Box.WHITE && TR != Box.BLACK
							&& ML != Box.WHITE && MR != Box.BLACK
							&& BL != Box.BLACK && BC != Box.WHITE && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)126:
						if (TL != Box.WHITE && TC != Box.WHITE && TR != Box.WHITE
							&& ML != Box.BLACK && MR != Box.BLACK
							&& BL != Box.BLACK && BC != Box.WHITE && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)127:
						if (TL != Box.BLACK && TC != Box.WHITE && TR != Box.WHITE
							&& ML != Box.BLACK && MR != Box.WHITE
							&& BL != Box.BLACK && BC != Box.WHITE && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)128:
						if (TL != Box.WHITE && TC != Box.WHITE && TR != Box.WHITE
							&& ML != Box.BLACK && MR != Box.BLACK
							&& BL != Box.WHITE && BC != Box.BLACK && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)129:
						if (TL != Box.BLACK && TC != Box.WHITE && TR != Box.WHITE
							&& ML != Box.BLACK && MR != Box.WHITE
							&& BL != Box.WHITE && BC != Box.BLACK && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)130:
						if (TL != Box.BLACK && TC != Box.BLACK && TR != Box.WHITE
							&& ML != Box.BLACK && MR != Box.WHITE
							&& BL != Box.WHITE && BC != Box.BLACK && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)131:
						if (TL != Box.BLACK && TC != Box.WHITE && TR != Box.WHITE
							&& ML != Box.WHITE && MR != Box.WHITE
							&& BL != Box.BLACK && BC != Box.BLACK && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)132:
						if (TL != Box.BLACK && TC != Box.BLACK && TR != Box.WHITE
							&& ML != Box.WHITE && MR != Box.WHITE
							&& BL != Box.BLACK && BC != Box.BLACK && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)133:
						if (TL != Box.BLACK && TC != Box.BLACK && TR != Box.BLACK
							&& ML != Box.WHITE && MR != Box.WHITE
							&& BL != Box.BLACK && BC != Box.WHITE && BR != Box.WHITE) { return true; }
						else { return false; }
					default:
						Console.WriteLine("Error: 数字マス[13]周りのパターンチェック");
						Application.Exit();
						return false;
				}
			}
			// [14] id=134~149
			else if (HEAD_BN_14 <= id && id < HEAD_BN_15) {
				switch (id) {
					case (byte)134:
						if (TL != Box.WHITE && TC != Box.BLACK && TR != Box.WHITE
							&& ML != Box.BLACK && MR != Box.WHITE
							&& BL != Box.BLACK && BC != Box.WHITE && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)135:
						if (TL != Box.WHITE && TC != Box.BLACK && TR != Box.BLACK
							&& ML != Box.BLACK && MR != Box.WHITE
							&& BL != Box.WHITE && BC != Box.WHITE && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)136:
						if (TL != Box.BLACK && TC != Box.WHITE && TR != Box.BLACK
							&& ML != Box.BLACK && MR != Box.WHITE
							&& BL != Box.WHITE && BC != Box.WHITE && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)137:
						if (TL != Box.BLACK && TC != Box.WHITE && TR != Box.BLACK
							&& ML != Box.WHITE && MR != Box.BLACK
							&& BL != Box.WHITE && BC != Box.WHITE && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)138:
						if (TL != Box.BLACK && TC != Box.BLACK && TR != Box.WHITE
							&& ML != Box.WHITE && MR != Box.BLACK
							&& BL != Box.WHITE && BC != Box.WHITE && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)139:
						if (TL != Box.WHITE && TC != Box.BLACK && TR != Box.WHITE
							&& ML != Box.WHITE && MR != Box.BLACK
							&& BL != Box.WHITE && BC != Box.WHITE && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)140:
						if (TL != Box.WHITE && TC != Box.BLACK && TR != Box.BLACK
							&& ML != Box.WHITE && MR != Box.WHITE
							&& BL != Box.WHITE && BC != Box.WHITE && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)141:
						if (TL != Box.WHITE && TC != Box.WHITE && TR != Box.BLACK
							&& ML != Box.WHITE && MR != Box.WHITE
							&& BL != Box.WHITE && BC != Box.BLACK && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)142:
						if (TL != Box.WHITE && TC != Box.WHITE && TR != Box.BLACK
							&& ML != Box.WHITE && MR != Box.BLACK
							&& BL != Box.WHITE && BC != Box.BLACK && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)143:
						if (TL != Box.WHITE && TC != Box.WHITE && TR != Box.WHITE
							&& ML != Box.WHITE && MR != Box.BLACK
							&& BL != Box.BLACK && BC != Box.BLACK && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)144:
						if (TL != Box.WHITE && TC != Box.WHITE && TR != Box.WHITE
							&& ML != Box.WHITE && MR != Box.BLACK
							&& BL != Box.BLACK && BC != Box.WHITE && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)145:
						if (TL != Box.WHITE && TC != Box.WHITE && TR != Box.WHITE
							&& ML != Box.BLACK && MR != Box.WHITE
							&& BL != Box.BLACK && BC != Box.WHITE && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)146:
						if (TL != Box.WHITE && TC != Box.WHITE && TR != Box.WHITE
							&& ML != Box.BLACK && MR != Box.WHITE
							&& BL != Box.WHITE && BC != Box.BLACK && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)147:
						if (TL != Box.BLACK && TC != Box.WHITE && TR != Box.WHITE
							&& ML != Box.BLACK && MR != Box.WHITE
							&& BL != Box.WHITE && BC != Box.BLACK && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)148:
						if (TL != Box.BLACK && TC != Box.WHITE && TR != Box.WHITE
							&& ML != Box.WHITE && MR != Box.WHITE
							&& BL != Box.BLACK && BC != Box.BLACK && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)149:
						if (TL != Box.BLACK && TC != Box.BLACK && TR != Box.WHITE
							&& ML != Box.WHITE && MR != Box.WHITE
							&& BL != Box.BLACK && BC != Box.WHITE && BR != Box.WHITE) { return true; }
						else { return false; }
					default:
						Console.WriteLine("Error: 数字マス[14]周りのパターンチェック");
						Application.Exit();
						return false;
				}
			}
			// [15] id=150~157
			else if (HEAD_BN_15 <= id && id < HEAD_BN_22) {
				switch (id) {
					case (byte)150:
						if (TL != Box.WHITE && TC != Box.BLACK && TR != Box.WHITE
							&& ML != Box.BLACK && MR != Box.WHITE
							&& BL != Box.WHITE && BC != Box.WHITE && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)151:
						if (TL != Box.BLACK && TC != Box.WHITE && TR != Box.BLACK
							&& ML != Box.WHITE && MR != Box.WHITE
							&& BL != Box.WHITE && BC != Box.WHITE && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)152:
						if (TL != Box.WHITE && TC != Box.BLACK && TR != Box.WHITE
							&& ML != Box.WHITE && MR != Box.BLACK
							&& BL != Box.WHITE && BC != Box.WHITE && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)153:
						if (TL != Box.WHITE && TC != Box.WHITE && TR != Box.BLACK
							&& ML != Box.WHITE && MR != Box.WHITE
							&& BL != Box.WHITE && BC != Box.WHITE && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)154:
						if (TL != Box.WHITE && TC != Box.WHITE && TR != Box.WHITE
							&& ML != Box.WHITE && MR != Box.BLACK
							&& BL != Box.WHITE && BC != Box.BLACK && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)155:
						if (TL != Box.WHITE && TC != Box.WHITE && TR != Box.WHITE
							&& ML != Box.WHITE && MR != Box.WHITE
							&& BL != Box.BLACK && BC != Box.WHITE && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)156:
						if (TL != Box.WHITE && TC != Box.WHITE && TR != Box.WHITE
							&& ML != Box.BLACK && MR != Box.WHITE
							&& BL != Box.WHITE && BC != Box.BLACK && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)157:
						if (TL != Box.BLACK && TC != Box.WHITE && TR != Box.WHITE
							&& ML != Box.WHITE && MR != Box.WHITE
							&& BL != Box.BLACK && BC != Box.WHITE && BR != Box.WHITE) { return true; }
						else { return false; }
					default:
						Console.WriteLine("Error: 数字マス[14]周りのパターンチェック");
						Application.Exit();
						return false;
				}
			}
			// [22] id=158~169
			else if (HEAD_BN_22 <= id && id < HEAD_BN_23) {
				switch (id) {
					case (byte)158:
						if (TL != Box.WHITE && TC != Box.WHITE && TR != Box.BLACK
							&& ML != Box.BLACK && MR != Box.WHITE
							&& BL != Box.BLACK && BC != Box.BLACK && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)159:
						if (TL != Box.WHITE && TC != Box.WHITE && TR != Box.BLACK
							&& ML != Box.BLACK && MR != Box.BLACK
							&& BL != Box.BLACK && BC != Box.WHITE && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)160:
						if (TL != Box.WHITE && TC != Box.WHITE && TR != Box.BLACK
							&& ML != Box.BLACK && MR != Box.BLACK
							&& BL != Box.WHITE && BC != Box.WHITE && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)161:
						if (TL != Box.BLACK && TC != Box.WHITE && TR != Box.WHITE
							&& ML != Box.BLACK && MR != Box.BLACK
							&& BL != Box.BLACK && BC != Box.WHITE && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)162:
						if (TL != Box.BLACK && TC != Box.WHITE && TR != Box.WHITE
							&& ML != Box.BLACK && MR != Box.BLACK
							&& BL != Box.WHITE && BC != Box.WHITE && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)163:
						if (TL != Box.BLACK && TC != Box.WHITE && TR != Box.WHITE
							&& ML != Box.WHITE && MR != Box.BLACK
							&& BL != Box.WHITE && BC != Box.BLACK && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)164:
						if (TL != Box.BLACK && TC != Box.BLACK && TR != Box.WHITE
							&& ML != Box.BLACK && MR != Box.WHITE
							&& BL != Box.WHITE && BC != Box.WHITE && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)165:
						if (TL != Box.BLACK && TC != Box.BLACK && TR != Box.WHITE
							&& ML != Box.WHITE && MR != Box.WHITE
							&& BL != Box.WHITE && BC != Box.BLACK && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)166:
						if (TL != Box.WHITE && TC != Box.BLACK && TR != Box.WHITE
							&& ML != Box.WHITE && MR != Box.WHITE
							&& BL != Box.BLACK && BC != Box.BLACK && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)167:
						if (TL != Box.BLACK && TC != Box.BLACK && TR != Box.BLACK
							&& ML != Box.WHITE && MR != Box.WHITE
							&& BL != Box.WHITE && BC != Box.BLACK && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)168:
						if (TL != Box.WHITE && TC != Box.BLACK && TR != Box.BLACK
							&& ML != Box.WHITE && MR != Box.WHITE
							&& BL != Box.BLACK && BC != Box.BLACK && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)169:
						if (TL != Box.WHITE && TC != Box.BLACK && TR != Box.BLACK
							&& ML != Box.WHITE && MR != Box.BLACK
							&& BL != Box.BLACK && BC != Box.WHITE && BR != Box.WHITE) { return true; }
						else { return false; }
					default:
						Console.WriteLine("Error: 数字マス[22]周りのパターンチェック");
						Application.Exit();
						return false;
				}
			}
			// [23] id=170~185
			else if (HEAD_BN_23 <= id && id < HEAD_BN_24) {
				switch (id) {
					case (byte)170:
						if (TL != Box.WHITE && TC != Box.WHITE && TR != Box.BLACK
							&& ML != Box.BLACK && MR != Box.WHITE
							&& BL != Box.BLACK && BC != Box.WHITE && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)171:
						if (TL != Box.WHITE && TC != Box.WHITE && TR != Box.BLACK
							&& ML != Box.BLACK && MR != Box.BLACK
							&& BL != Box.WHITE && BC != Box.WHITE && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)172:
						if (TL != Box.BLACK && TC != Box.WHITE && TR != Box.WHITE
							&& ML != Box.BLACK && MR != Box.BLACK
							&& BL != Box.WHITE && BC != Box.WHITE && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)173:
						if (TL != Box.BLACK && TC != Box.WHITE && TR != Box.WHITE
							&& ML != Box.WHITE && MR != Box.BLACK
							&& BL != Box.WHITE && BC != Box.WHITE && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)174:
						if (TL != Box.BLACK && TC != Box.BLACK && TR != Box.WHITE
							&& ML != Box.WHITE && MR != Box.WHITE
							&& BL != Box.WHITE && BC != Box.WHITE && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)175:
						if (TL != Box.WHITE && TC != Box.BLACK && TR != Box.WHITE
							&& ML != Box.WHITE && MR != Box.WHITE
							&& BL != Box.WHITE && BC != Box.BLACK && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)176:
						if (TL != Box.WHITE && TC != Box.BLACK && TR != Box.BLACK
							&& ML != Box.WHITE && MR != Box.WHITE
							&& BL != Box.WHITE && BC != Box.BLACK && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)177:
						if (TL != Box.WHITE && TC != Box.WHITE && TR != Box.BLACK
							&& ML != Box.WHITE && MR != Box.WHITE
							&& BL != Box.BLACK && BC != Box.BLACK && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)178:
						if (TL != Box.WHITE && TC != Box.WHITE && TR != Box.BLACK
							&& ML != Box.WHITE && MR != Box.BLACK
							&& BL != Box.BLACK && BC != Box.WHITE && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)179:
						if (TL != Box.WHITE && TC != Box.WHITE && TR != Box.WHITE
							&& ML != Box.BLACK && MR != Box.BLACK
							&& BL != Box.BLACK && BC != Box.WHITE && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)180:
						if (TL != Box.WHITE && TC != Box.WHITE && TR != Box.WHITE
							&& ML != Box.BLACK && MR != Box.BLACK
							&& BL != Box.WHITE && BC != Box.WHITE && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)181:
						if (TL != Box.BLACK && TC != Box.WHITE && TR != Box.WHITE
							&& ML != Box.BLACK && MR != Box.WHITE
							&& BL != Box.WHITE && BC != Box.WHITE && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)182:
						if (TL != Box.BLACK && TC != Box.WHITE && TR != Box.WHITE
							&& ML != Box.WHITE && MR != Box.WHITE
							&& BL != Box.WHITE && BC != Box.BLACK && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)183:
						if (TL != Box.BLACK && TC != Box.BLACK && TR != Box.WHITE
							&& ML != Box.WHITE && MR != Box.WHITE
							&& BL != Box.WHITE && BC != Box.BLACK && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)184:
						if (TL != Box.WHITE && TC != Box.BLACK && TR != Box.WHITE
							&& ML != Box.WHITE && MR != Box.WHITE
							&& BL != Box.BLACK && BC != Box.BLACK && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)185:
						if (TL != Box.WHITE && TC != Box.BLACK && TR != Box.BLACK
							&& ML != Box.WHITE && MR != Box.WHITE
							&& BL != Box.BLACK && BC != Box.WHITE && BR != Box.WHITE) { return true; }
						else { return false; }
					default:
						Console.WriteLine("Error: 数字マス[23]周りのパターンチェック");
						Application.Exit();
						return false;
				}
			}
			// [24] id=186~193
			else if (HEAD_BN_24 <= id && id < HEAD_BN_33) {
				switch (id) {
					case (byte)186:
						if (TL != Box.WHITE && TC != Box.WHITE && TR != Box.BLACK
							&& ML != Box.BLACK && MR != Box.WHITE
							&& BL != Box.WHITE && BC != Box.WHITE && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)187:
						if (TL != Box.BLACK && TC != Box.WHITE && TR != Box.WHITE
							&& ML != Box.WHITE && MR != Box.BLACK
							&& BL != Box.WHITE && BC != Box.WHITE && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)188:
						if (TL != Box.WHITE && TC != Box.BLACK && TR != Box.WHITE
							&& ML != Box.WHITE && MR != Box.WHITE
							&& BL != Box.WHITE && BC != Box.WHITE && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)189:
						if (TL != Box.WHITE && TC != Box.WHITE && TR != Box.BLACK
							&& ML != Box.WHITE && MR != Box.WHITE
							&& BL != Box.WHITE && BC != Box.BLACK && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)190:
						if (TL != Box.WHITE && TC != Box.WHITE && TR != Box.WHITE
							&& ML != Box.WHITE && MR != Box.BLACK
							&& BL != Box.BLACK && BC != Box.WHITE && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)191:
						if (TL != Box.WHITE && TC != Box.WHITE && TR != Box.WHITE
							&& ML != Box.BLACK && MR != Box.WHITE
							&& BL != Box.WHITE && BC != Box.WHITE && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)192:
						if (TL != Box.BLACK && TC != Box.WHITE && TR != Box.WHITE
							&& ML != Box.WHITE && MR != Box.WHITE
							&& BL != Box.WHITE && BC != Box.BLACK && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)193:
						if (TL != Box.WHITE && TC != Box.BLACK && TR != Box.WHITE
							&& ML != Box.WHITE && MR != Box.WHITE
							&& BL != Box.BLACK && BC != Box.WHITE && BR != Box.WHITE) { return true; }
						else { return false; }
					default:
						Console.WriteLine("Error: 数字マス[24]周りのパターンチェック");
						Application.Exit();
						return false;
				}
			}
			// [33] id=194~197
			else if (HEAD_BN_33 <= id && id < HEAD_BN_111) {
				switch (id) {
					case (byte)194:
						if (TL != Box.WHITE && TC != Box.WHITE && TR != Box.WHITE
							&& ML != Box.BLACK && MR != Box.BLACK
							&& BL != Box.WHITE && BC != Box.WHITE && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)195:
						if (TL != Box.BLACK && TC != Box.WHITE && TR != Box.WHITE
							&& ML != Box.WHITE && MR != Box.WHITE
							&& BL != Box.WHITE && BC != Box.WHITE && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)196:
						if (TL != Box.WHITE && TC != Box.BLACK && TR != Box.WHITE
							&& ML != Box.WHITE && MR != Box.WHITE
							&& BL != Box.WHITE && BC != Box.BLACK && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)197:
						if (TL != Box.WHITE && TC != Box.WHITE && TR != Box.BLACK
							&& ML != Box.WHITE && MR != Box.WHITE
							&& BL != Box.BLACK && BC != Box.WHITE && BR != Box.WHITE) { return true; }
						else { return false; }
					default:
						Console.WriteLine("Error: 数字マス[33]周りのパターンチェック");
						Application.Exit();
						return false;
				}
			}
			// [111] id=198~213			
			else if (HEAD_BN_111 <= id && id < HEAD_BN_112) {
				switch (id) {
					case (byte)198:
						if (TL != Box.WHITE && TC != Box.BLACK && TR != Box.WHITE
							&& ML != Box.BLACK && MR != Box.BLACK
							&& BL != Box.BLACK && BC != Box.BLACK && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)199:
						if (TL != Box.WHITE && TC != Box.BLACK && TR != Box.WHITE
							&& ML != Box.BLACK && MR != Box.BLACK
							&& BL != Box.BLACK && BC != Box.WHITE && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)200:
						if (TL != Box.WHITE && TC != Box.BLACK && TR != Box.WHITE
							&& ML != Box.BLACK && MR != Box.BLACK
							&& BL != Box.WHITE && BC != Box.BLACK && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)201:
						if (TL != Box.WHITE && TC != Box.BLACK && TR != Box.BLACK
							&& ML != Box.BLACK && MR != Box.WHITE
							&& BL != Box.BLACK && BC != Box.WHITE && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)202:
						if (TL != Box.WHITE && TC != Box.BLACK && TR != Box.BLACK
							&& ML != Box.BLACK && MR != Box.WHITE
							&& BL != Box.WHITE && BC != Box.BLACK && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)203:
						if (TL != Box.WHITE && TC != Box.BLACK && TR != Box.BLACK
							&& ML != Box.BLACK && MR != Box.BLACK
							&& BL != Box.WHITE && BC != Box.BLACK && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)204:
						if (TL != Box.BLACK && TC != Box.WHITE && TR != Box.BLACK
							&& ML != Box.BLACK && MR != Box.WHITE
							&& BL != Box.BLACK && BC != Box.WHITE && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)205:
						if (TL != Box.BLACK && TC != Box.WHITE && TR != Box.BLACK
							&& ML != Box.BLACK && MR != Box.WHITE
							&& BL != Box.WHITE && BC != Box.BLACK && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)206:
						if (TL != Box.BLACK && TC != Box.WHITE && TR != Box.BLACK
							&& ML != Box.WHITE && MR != Box.WHITE
							&& BL != Box.BLACK && BC != Box.BLACK && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)207:
						if (TL != Box.BLACK && TC != Box.WHITE && TR != Box.BLACK
							&& ML != Box.BLACK && MR != Box.BLACK
							&& BL != Box.WHITE && BC != Box.BLACK && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)208:
						if (TL != Box.BLACK && TC != Box.WHITE && TR != Box.BLACK
							&& ML != Box.WHITE && MR != Box.BLACK
							&& BL != Box.BLACK && BC != Box.BLACK && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)209:
						if (TL != Box.BLACK && TC != Box.WHITE && TR != Box.BLACK
							&& ML != Box.WHITE && MR != Box.BLACK
							&& BL != Box.BLACK && BC != Box.WHITE && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)210:
						if (TL != Box.BLACK && TC != Box.BLACK && TR != Box.WHITE
							&& ML != Box.BLACK && MR != Box.BLACK
							&& BL != Box.WHITE && BC != Box.BLACK && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)211:
						if (TL != Box.BLACK && TC != Box.BLACK && TR != Box.WHITE
							&& ML != Box.WHITE && MR != Box.BLACK
							&& BL != Box.BLACK && BC != Box.BLACK && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)212:
						if (TL != Box.BLACK && TC != Box.BLACK && TR != Box.WHITE
							&& ML != Box.WHITE && MR != Box.BLACK
							&& BL != Box.BLACK && BC != Box.WHITE && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)213:
						if (TL != Box.BLACK && TC != Box.BLACK && TR != Box.BLACK
							&& ML != Box.WHITE && MR != Box.WHITE
							&& BL != Box.BLACK && BC != Box.WHITE && BR != Box.BLACK) { return true; }
						else { return false; }
					default:
						Console.WriteLine("Error: 数字マス[111]周りのパターンチェック");
						Application.Exit();
						return false;
				}
			}
			// [112] id=214~237
			else if (HEAD_BN_112 <= id && id < HEAD_BN_113) {
				switch (id) {
					case (byte)214:
						if (TL != Box.WHITE && TC != Box.WHITE && TR != Box.BLACK
							&& ML != Box.BLACK && MR != Box.WHITE
							&& BL != Box.BLACK && BC != Box.WHITE && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)215:
						if (TL != Box.WHITE && TC != Box.WHITE && TR != Box.BLACK
							&& ML != Box.BLACK && MR != Box.WHITE
							&& BL != Box.WHITE && BC != Box.BLACK && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)216:
						if (TL != Box.WHITE && TC != Box.WHITE && TR != Box.BLACK
							&& ML != Box.BLACK && MR != Box.BLACK
							&& BL != Box.WHITE && BC != Box.BLACK && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)217:
						if (TL != Box.BLACK && TC != Box.WHITE && TR != Box.WHITE
							&& ML != Box.BLACK && MR != Box.BLACK
							&& BL != Box.WHITE && BC != Box.BLACK && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)218:
						if (TL != Box.BLACK && TC != Box.WHITE && TR != Box.WHITE
							&& ML != Box.WHITE && MR != Box.BLACK
							&& BL != Box.BLACK && BC != Box.BLACK && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)219:
						if (TL != Box.BLACK && TC != Box.WHITE && TR != Box.WHITE
							&& ML != Box.WHITE && MR != Box.BLACK
							&& BL != Box.BLACK && BC != Box.WHITE && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)220:
						if (TL != Box.BLACK && TC != Box.BLACK && TR != Box.WHITE
							&& ML != Box.WHITE && MR != Box.WHITE
							&& BL != Box.BLACK && BC != Box.WHITE && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)221:
						if (TL != Box.WHITE && TC != Box.BLACK && TR != Box.WHITE
							&& ML != Box.BLACK && MR != Box.WHITE
							&& BL != Box.BLACK && BC != Box.WHITE && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)222:
						if (TL != Box.WHITE && TC != Box.BLACK && TR != Box.WHITE
							&& ML != Box.BLACK && MR != Box.WHITE
							&& BL != Box.WHITE && BC != Box.BLACK && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)223:
						if (TL != Box.WHITE && TC != Box.BLACK && TR != Box.BLACK
							&& ML != Box.BLACK && MR != Box.WHITE
							&& BL != Box.WHITE && BC != Box.BLACK && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)224:
						if (TL != Box.BLACK && TC != Box.WHITE && TR != Box.BLACK
							&& ML != Box.BLACK && MR != Box.WHITE
							&& BL != Box.WHITE && BC != Box.BLACK && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)225:
						if (TL != Box.BLACK && TC != Box.WHITE && TR != Box.BLACK
							&& ML != Box.WHITE && MR != Box.WHITE
							&& BL != Box.BLACK && BC != Box.BLACK && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)226:
						if (TL != Box.BLACK && TC != Box.WHITE && TR != Box.BLACK
							&& ML != Box.WHITE && MR != Box.BLACK
							&& BL != Box.BLACK && BC != Box.WHITE && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)227:
						if (TL != Box.BLACK && TC != Box.BLACK && TR != Box.WHITE
							&& ML != Box.WHITE && MR != Box.BLACK
							&& BL != Box.BLACK && BC != Box.WHITE && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)228:
						if (TL != Box.WHITE && TC != Box.BLACK && TR != Box.WHITE
							&& ML != Box.BLACK && MR != Box.BLACK
							&& BL != Box.BLACK && BC != Box.WHITE && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)229:
						if (TL != Box.WHITE && TC != Box.BLACK && TR != Box.WHITE
							&& ML != Box.BLACK && MR != Box.BLACK
							&& BL != Box.WHITE && BC != Box.WHITE && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)230:
						if (TL != Box.WHITE && TC != Box.BLACK && TR != Box.BLACK
							&& ML != Box.BLACK && MR != Box.WHITE
							&& BL != Box.WHITE && BC != Box.WHITE && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)231:
						if (TL != Box.BLACK && TC != Box.WHITE && TR != Box.BLACK
							&& ML != Box.BLACK && MR != Box.WHITE
							&& BL != Box.WHITE && BC != Box.WHITE && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)232:
						if (TL != Box.BLACK && TC != Box.WHITE && TR != Box.BLACK
							&& ML != Box.WHITE && MR != Box.WHITE
							&& BL != Box.WHITE && BC != Box.BLACK && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)233:
						if (TL != Box.BLACK && TC != Box.WHITE && TR != Box.BLACK
							&& ML != Box.WHITE && MR != Box.BLACK
							&& BL != Box.WHITE && BC != Box.BLACK && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)234:
						if (TL != Box.BLACK && TC != Box.BLACK && TR != Box.WHITE
							&& ML != Box.WHITE && MR != Box.BLACK
							&& BL != Box.WHITE && BC != Box.BLACK && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)235:
						if (TL != Box.WHITE && TC != Box.BLACK && TR != Box.WHITE
							&& ML != Box.WHITE && MR != Box.BLACK
							&& BL != Box.BLACK && BC != Box.BLACK && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)236:
						if (TL != Box.WHITE && TC != Box.BLACK && TR != Box.WHITE
							&& ML != Box.WHITE && MR != Box.BLACK
							&& BL != Box.BLACK && BC != Box.WHITE && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)237:
						if (TL != Box.WHITE && TC != Box.BLACK && TR != Box.BLACK
							&& ML != Box.WHITE && MR != Box.WHITE
							&& BL != Box.BLACK && BC != Box.WHITE && BR != Box.BLACK) { return true; }
						else { return false; }
					default:
						Console.WriteLine("Error: 数字マス[112]周りのパターンチェック");
						Application.Exit();
						return false;
				}
			}
			// [113] id=238~245
			else if (HEAD_BN_113 <= id && id < HEAD_BN_122) {
				switch (id) {
					case (byte)238:
						if (TL != Box.WHITE && TC != Box.WHITE && TR != Box.WHITE
							&& ML != Box.BLACK && MR != Box.BLACK
							&& BL != Box.WHITE && BC != Box.BLACK && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)239:
						if (TL != Box.BLACK && TC != Box.WHITE && TR != Box.WHITE
							&& ML != Box.WHITE && MR != Box.WHITE
							&& BL != Box.BLACK && BC != Box.WHITE && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)240:
						if (TL != Box.WHITE && TC != Box.BLACK && TR != Box.WHITE
							&& ML != Box.BLACK && MR != Box.WHITE
							&& BL != Box.WHITE && BC != Box.BLACK && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)241:
						if (TL != Box.BLACK && TC != Box.WHITE && TR != Box.BLACK
							&& ML != Box.WHITE && MR != Box.WHITE
							&& BL != Box.BLACK && BC != Box.WHITE && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)242:
						if (TL != Box.WHITE && TC != Box.BLACK && TR != Box.WHITE
							&& ML != Box.BLACK && MR != Box.BLACK
							&& BL != Box.WHITE && BC != Box.WHITE && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)243:
						if (TL != Box.BLACK && TC != Box.WHITE && TR != Box.BLACK
							&& ML != Box.WHITE && MR != Box.WHITE
							&& BL != Box.WHITE && BC != Box.WHITE && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)244:
						if (TL != Box.WHITE && TC != Box.BLACK && TR != Box.WHITE
							&& ML != Box.WHITE && MR != Box.BLACK
							&& BL != Box.WHITE && BC != Box.BLACK && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)245:
						if (TL != Box.WHITE && TC != Box.WHITE && TR != Box.BLACK
							&& ML != Box.WHITE && MR != Box.WHITE
							&& BL != Box.BLACK && BC != Box.WHITE && BR != Box.BLACK) { return true; }
						else { return false; }
					default:
						Console.WriteLine("Error: 数字マス[113]周りのパターンチェック");
						Application.Exit();
						return false;
				}
			}
			// [122] id=246~253
			else if (HEAD_BN_122 <= id && id < HEAD_BN_1111) {
				switch (id) {
					case (byte)246:
						if (TL != Box.WHITE && TC != Box.BLACK && TR != Box.WHITE
							&& ML != Box.BLACK && MR != Box.WHITE
							&& BL != Box.WHITE && BC != Box.WHITE && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)247:
						if (TL != Box.BLACK && TC != Box.BLACK && TR != Box.BLACK
							&& ML != Box.WHITE && MR != Box.WHITE
							&& BL != Box.WHITE && BC != Box.BLACK && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)248:
						if (TL != Box.WHITE && TC != Box.BLACK && TR != Box.WHITE
							&& ML != Box.WHITE && MR != Box.BLACK
							&& BL != Box.BLACK && BC != Box.WHITE && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)249:
						if (TL != Box.WHITE && TC != Box.WHITE && TR != Box.BLACK
							&& ML != Box.BLACK && MR != Box.WHITE
							&& BL != Box.WHITE && BC != Box.WHITE && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)250:
						if (TL != Box.BLACK && TC != Box.WHITE && TR != Box.WHITE
							&& ML != Box.WHITE && MR != Box.BLACK
							&& BL != Box.WHITE && BC != Box.BLACK && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)251:
						if (TL != Box.WHITE && TC != Box.BLACK && TR != Box.WHITE
							&& ML != Box.WHITE && MR != Box.WHITE
							&& BL != Box.BLACK && BC != Box.WHITE && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)252:
						if (TL != Box.WHITE && TC != Box.WHITE && TR != Box.BLACK
							&& ML != Box.BLACK && MR != Box.WHITE
							&& BL != Box.WHITE && BC != Box.BLACK && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)253:
						if (TL != Box.BLACK && TC != Box.WHITE && TR != Box.WHITE
							&& ML != Box.WHITE && MR != Box.BLACK
							&& BL != Box.BLACK && BC != Box.WHITE && BR != Box.WHITE) { return true; }
						else { return false; }
					default:
						Console.WriteLine("Error: 数字マス[122]周りのパターンチェック");
						Application.Exit();
						return false;
				}
			}
			// [1111] id=254~255
			else if (HEAD_BN_1111 <= id && id <= MAX_ID) {
				switch (id) {
					case (byte)254:
						if (TL != Box.WHITE && TC != Box.BLACK && TR != Box.WHITE
							&& ML != Box.BLACK && MR != Box.BLACK
							&& BL != Box.WHITE && BC != Box.BLACK && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)255:
						if (TL != Box.BLACK && TC != Box.WHITE && TR != Box.BLACK
							&& ML != Box.WHITE && MR != Box.WHITE
							&& BL != Box.BLACK && BC != Box.WHITE && BR != Box.BLACK) { return true; }
						else { return false; }
					default:
						Console.WriteLine("Error: 数字マス[1111]周りのパターンチェック");
						Application.Exit();
						return false;
				}
			}
			else {
				Console.WriteLine("Error: 数字マスのid振り分けでエラー");
				Application.Exit();
				return false;
			}
		}

		/*********************************
		 * 
		 * 座標とidから座標周りでidの通り黒マスを配置する。
		 * 引数
		 * co		: マスの座標
		 * id       : 各パターンを識別するための数値(型はbyte)
		 * clone_box_arround_numbox_list [default : null]
		 *			: 数字マス周りの8マスのリスト
		 *   
		 * *******************************/
		static private void setPatternAroundNumBox(Coordinates co, byte id
			, List<Box> clone_box_around_numbox_list = null)
		{
			Box TL = new Box();		// 左上(Top-Left)
			Box TC = new Box();		// 中上(Top-Center)
			Box TR = new Box();		// 右上(Top-Right)
			Box ML = new Box();		// 左中(Middle-Left)
			Box MR = new Box();		// 右中(Middle-Right)
			Box BL = new Box();	    // 左下(Bottom-Left)
			Box BC = new Box();		// 中下(Bottom-Center)
			Box BR = new Box();	    // 右下(Bottom-Right)

			if (clone_box_around_numbox_list == null) {
				// 盤面の数字マス周りのマスを取得（盤面本体）
				TL = Tapa.box[co.x - 1][co.y - 1];		// 左上(Top-Left)
				TC = Tapa.box[co.x - 1][co.y];			// 中上(Top-Center)
				TR = Tapa.box[co.x - 1][co.y + 1];		// 右上(Top-Right)
				ML = Tapa.box[co.x][co.y - 1];			// 左中(Middle-Left)
				MR = Tapa.box[co.x][co.y + 1];			// 右中(Middle-Right)
				BL = Tapa.box[co.x + 1][co.y - 1];	    // 左下(Bottom-Left)
				BC = Tapa.box[co.x + 1][co.y];			// 中下(Bottom-Center)
				BR = Tapa.box[co.x + 1][co.y + 1];	    // 右下(Bottom-Right)
			}
			else {
				// 盤面の数字マス周りのマスを取得（盤面のコピー）
				TL = clone_box_around_numbox_list[0];		// 左上(Top-Left)
				TC = clone_box_around_numbox_list[1];      // 中上(Top-Center)
				TR = clone_box_around_numbox_list[2];		// 右上(Top-Right)
				ML = clone_box_around_numbox_list[3];		// 左中(Middle-Left)
				MR = clone_box_around_numbox_list[4];      // 右中(Middle-Right)
				BL = clone_box_around_numbox_list[5];		// 左下(Bottom-Left)
				BC = clone_box_around_numbox_list[6];      // 中下(Bottom-Center)
				BR = clone_box_around_numbox_list[7];		// 右下(Bottom-Right)
			}

			// idから数字マス周りに黒マスを配置
			// [0] id=0
			if (id == HEAD_BN_0) {
				TL.Color = Box.WHITE; TC.Color = Box.WHITE; TR.Color = Box.WHITE;
				ML.Color = Box.WHITE; /*				  */MR.Color = Box.WHITE;
				BL.Color = Box.WHITE; BC.Color = Box.WHITE; BR.Color = Box.WHITE;
			}
			// [1] id=1~8
			else if (HEAD_BN_1 <= id && id < HEAD_BN_2) {
				switch (id) {
					case (byte)1:
						TL.Color = Box.BLACK; TC.Color = Box.WHITE; TR.Color = Box.WHITE;
						ML.Color = Box.WHITE; /*				  */MR.Color = Box.WHITE;
						BL.Color = Box.WHITE; BC.Color = Box.WHITE; BR.Color = Box.WHITE;
						break;
					case (byte)2:
						TL.Color = Box.WHITE; TC.Color = Box.BLACK; TR.Color = Box.WHITE;
						ML.Color = Box.WHITE; /*				  */MR.Color = Box.WHITE;
						BL.Color = Box.WHITE; BC.Color = Box.WHITE; BR.Color = Box.WHITE;
						break;
					case (byte)3:
						TL.Color = Box.WHITE; TC.Color = Box.WHITE; TR.Color = Box.BLACK;
						ML.Color = Box.WHITE; /*				  */MR.Color = Box.WHITE;
						BL.Color = Box.WHITE; BC.Color = Box.WHITE; BR.Color = Box.WHITE;
						break;
					case (byte)4:
						TL.Color = Box.WHITE; TC.Color = Box.WHITE; TR.Color = Box.WHITE;
						ML.Color = Box.WHITE; /*				  */MR.Color = Box.BLACK;
						BL.Color = Box.WHITE; BC.Color = Box.WHITE; BR.Color = Box.WHITE;
						break;
					case (byte)5:
						TL.Color = Box.WHITE; TC.Color = Box.WHITE; TR.Color = Box.WHITE;
						ML.Color = Box.WHITE; /*				  */MR.Color = Box.WHITE;
						BL.Color = Box.WHITE; BC.Color = Box.WHITE; BR.Color = Box.BLACK;
						break;
					case (byte)6:
						TL.Color = Box.WHITE; TC.Color = Box.WHITE; TR.Color = Box.WHITE;
						ML.Color = Box.WHITE; /*				  */MR.Color = Box.WHITE;
						BL.Color = Box.WHITE; BC.Color = Box.BLACK; BR.Color = Box.WHITE;
						break;
					case (byte)7:
						TL.Color = Box.WHITE; TC.Color = Box.WHITE; TR.Color = Box.WHITE;
						ML.Color = Box.WHITE; /*				  */MR.Color = Box.WHITE;
						BL.Color = Box.BLACK; BC.Color = Box.WHITE; BR.Color = Box.WHITE;
						break;
					case (byte)8:
						TL.Color = Box.WHITE; TC.Color = Box.WHITE; TR.Color = Box.WHITE;
						ML.Color = Box.BLACK; /*				  */MR.Color = Box.WHITE;
						BL.Color = Box.WHITE; BC.Color = Box.WHITE; BR.Color = Box.WHITE;
						break;
					default:
						Console.WriteLine("Error: 数字マス[1]周りの黒マス配置中にエラー");
						Application.Exit();
						break;
				}
			}
			// [2] id=9~16
			else if (HEAD_BN_2 <= id && id < HEAD_BN_3) {
				switch (id) {
					case (byte)9:
						TL.Color = Box.BLACK; TC.Color = Box.BLACK; TR.Color = Box.WHITE;
						ML.Color = Box.WHITE; MR.Color = Box.WHITE;
						BL.Color = Box.WHITE; BC.Color = Box.WHITE; BR.Color = Box.WHITE;
						break;
					case (byte)10:
						TL.Color = Box.WHITE; TC.Color = Box.BLACK; TR.Color = Box.BLACK;
						ML.Color = Box.WHITE; MR.Color = Box.WHITE;
						BL.Color = Box.WHITE; BC.Color = Box.WHITE; BR.Color = Box.WHITE;
						break;
					case (byte)11:
						TL.Color = Box.WHITE; TC.Color = Box.WHITE; TR.Color = Box.BLACK;
						ML.Color = Box.WHITE; MR.Color = Box.BLACK;
						BL.Color = Box.WHITE; BC.Color = Box.WHITE; BR.Color = Box.WHITE;
						break;
					case (byte)12:
						TL.Color = Box.WHITE; TC.Color = Box.WHITE; TR.Color = Box.WHITE;
						ML.Color = Box.WHITE; MR.Color = Box.BLACK;
						BL.Color = Box.WHITE; BC.Color = Box.WHITE; BR.Color = Box.BLACK;
						break;
					case (byte)13:
						TL.Color = Box.WHITE; TC.Color = Box.WHITE; TR.Color = Box.WHITE;
						ML.Color = Box.WHITE; MR.Color = Box.WHITE;
						BL.Color = Box.WHITE; BC.Color = Box.BLACK; BR.Color = Box.BLACK;
						break;
					case (byte)14:
						TL.Color = Box.WHITE; TC.Color = Box.WHITE; TR.Color = Box.WHITE;
						ML.Color = Box.WHITE; MR.Color = Box.WHITE;
						BL.Color = Box.BLACK; BC.Color = Box.BLACK; BR.Color = Box.WHITE;
						break;
					case (byte)15:
						TL.Color = Box.WHITE; TC.Color = Box.WHITE; TR.Color = Box.WHITE;
						ML.Color = Box.BLACK; MR.Color = Box.WHITE;
						BL.Color = Box.BLACK; BC.Color = Box.WHITE; BR.Color = Box.WHITE;
						break;
					case (byte)16:
						TL.Color = Box.BLACK; TC.Color = Box.WHITE; TR.Color = Box.WHITE;
						ML.Color = Box.BLACK; MR.Color = Box.WHITE;
						BL.Color = Box.WHITE; BC.Color = Box.WHITE; BR.Color = Box.WHITE;
						break;
					default:
						Console.WriteLine("Error: 数字マス[2]周りの黒マス配置中にエラー");
						Application.Exit();
						break;
				}
			}
			// [3] id=17~24
			else if (HEAD_BN_3 <= id && id < HEAD_BN_4) {
				switch (id) {
					case (byte)17:
						TL.Color = Box.BLACK; TC.Color = Box.BLACK; TR.Color = Box.BLACK;
						ML.Color = Box.WHITE; MR.Color = Box.WHITE;
						BL.Color = Box.WHITE; BC.Color = Box.WHITE; BR.Color = Box.WHITE;
						break;
					case (byte)18:
						TL.Color = Box.WHITE; TC.Color = Box.BLACK; TR.Color = Box.BLACK;
						ML.Color = Box.WHITE; MR.Color = Box.BLACK;
						BL.Color = Box.WHITE; BC.Color = Box.WHITE; BR.Color = Box.WHITE;
						break;
					case (byte)19:
						TL.Color = Box.WHITE; TC.Color = Box.WHITE; TR.Color = Box.BLACK;
						ML.Color = Box.WHITE; MR.Color = Box.BLACK;
						BL.Color = Box.WHITE; BC.Color = Box.WHITE; BR.Color = Box.BLACK;
						break;
					case (byte)20:
						TL.Color = Box.WHITE; TC.Color = Box.WHITE; TR.Color = Box.WHITE;
						ML.Color = Box.WHITE; MR.Color = Box.BLACK;
						BL.Color = Box.WHITE; BC.Color = Box.BLACK; BR.Color = Box.BLACK;
						break;
					case (byte)21:
						TL.Color = Box.WHITE; TC.Color = Box.WHITE; TR.Color = Box.WHITE;
						ML.Color = Box.WHITE; MR.Color = Box.WHITE;
						BL.Color = Box.BLACK; BC.Color = Box.BLACK; BR.Color = Box.BLACK;
						break;
					case (byte)22:
						TL.Color = Box.WHITE; TC.Color = Box.WHITE; TR.Color = Box.WHITE;
						ML.Color = Box.BLACK; MR.Color = Box.WHITE;
						BL.Color = Box.BLACK; BC.Color = Box.BLACK; BR.Color = Box.WHITE;
						break;
					case (byte)23:
						TL.Color = Box.BLACK; TC.Color = Box.WHITE; TR.Color = Box.WHITE;
						ML.Color = Box.BLACK; MR.Color = Box.WHITE;
						BL.Color = Box.BLACK; BC.Color = Box.WHITE; BR.Color = Box.WHITE;
						break;
					case (byte)24:
						TL.Color = Box.BLACK; TC.Color = Box.BLACK; TR.Color = Box.WHITE;
						ML.Color = Box.BLACK; MR.Color = Box.WHITE;
						BL.Color = Box.WHITE; BC.Color = Box.WHITE; BR.Color = Box.WHITE;
						break;
					default:
						Console.WriteLine("Error: 数字マス[3]周りの黒マス配置中にエラー");
						Application.Exit();
						break;
				}
			}
			// [4] id=25~32
			else if (HEAD_BN_4 <= id && id < HEAD_BN_5) {
				switch (id) {
					case (byte)25:
						TL.Color = Box.BLACK; TC.Color = Box.BLACK; TR.Color = Box.BLACK;
						ML.Color = Box.WHITE; MR.Color = Box.BLACK;
						BL.Color = Box.WHITE; BC.Color = Box.WHITE; BR.Color = Box.WHITE;
						break;
					case (byte)26:
						TL.Color = Box.WHITE; TC.Color = Box.BLACK; TR.Color = Box.BLACK;
						ML.Color = Box.WHITE; MR.Color = Box.BLACK;
						BL.Color = Box.WHITE; BC.Color = Box.WHITE; BR.Color = Box.BLACK;
						break;
					case (byte)27:
						TL.Color = Box.WHITE; TC.Color = Box.WHITE; TR.Color = Box.BLACK;
						ML.Color = Box.WHITE; MR.Color = Box.BLACK;
						BL.Color = Box.WHITE; BC.Color = Box.BLACK; BR.Color = Box.BLACK;
						break;
					case (byte)28:
						TL.Color = Box.WHITE; TC.Color = Box.WHITE; TR.Color = Box.WHITE;
						ML.Color = Box.WHITE; MR.Color = Box.BLACK;
						BL.Color = Box.BLACK; BC.Color = Box.BLACK; BR.Color = Box.BLACK;
						break;
					case (byte)29:
						TL.Color = Box.WHITE; TC.Color = Box.WHITE; TR.Color = Box.WHITE;
						ML.Color = Box.BLACK; MR.Color = Box.WHITE;
						BL.Color = Box.BLACK; BC.Color = Box.BLACK; BR.Color = Box.BLACK;
						break;
					case (byte)30:
						TL.Color = Box.BLACK; TC.Color = Box.WHITE; TR.Color = Box.WHITE;
						ML.Color = Box.BLACK; MR.Color = Box.WHITE;
						BL.Color = Box.BLACK; BC.Color = Box.BLACK; BR.Color = Box.WHITE;
						break;
					case (byte)31:
						TL.Color = Box.BLACK; TC.Color = Box.BLACK; TR.Color = Box.WHITE;
						ML.Color = Box.BLACK; MR.Color = Box.WHITE;
						BL.Color = Box.BLACK; BC.Color = Box.WHITE; BR.Color = Box.WHITE;
						break;
					case (byte)32:
						TL.Color = Box.BLACK; TC.Color = Box.BLACK; TR.Color = Box.BLACK;
						ML.Color = Box.BLACK; MR.Color = Box.WHITE;
						BL.Color = Box.WHITE; BC.Color = Box.WHITE; BR.Color = Box.WHITE;
						break;
					default:
						Console.WriteLine("Error: 数字マス[4]周りの黒マス配置中にエラー");
						Application.Exit();
						break;
				}
			}
			// [5] id=33~40
			else if (HEAD_BN_5 <= id && id < HEAD_BN_6) {
				switch (id) {
					case (byte)33:
						TL.Color = Box.BLACK; TC.Color = Box.BLACK; TR.Color = Box.BLACK;
						ML.Color = Box.WHITE; MR.Color = Box.BLACK;
						BL.Color = Box.WHITE; BC.Color = Box.WHITE; BR.Color = Box.BLACK;
						break;
					case (byte)34:
						TL.Color = Box.WHITE; TC.Color = Box.BLACK; TR.Color = Box.BLACK;
						ML.Color = Box.WHITE; MR.Color = Box.BLACK;
						BL.Color = Box.WHITE; BC.Color = Box.BLACK; BR.Color = Box.BLACK;
						break;
					case (byte)35:
						TL.Color = Box.WHITE; TC.Color = Box.WHITE; TR.Color = Box.BLACK;
						ML.Color = Box.WHITE; MR.Color = Box.BLACK;
						BL.Color = Box.BLACK; BC.Color = Box.BLACK; BR.Color = Box.BLACK;
						break;
					case (byte)36:
						TL.Color = Box.WHITE; TC.Color = Box.WHITE; TR.Color = Box.WHITE;
						ML.Color = Box.BLACK; MR.Color = Box.BLACK;
						BL.Color = Box.BLACK; BC.Color = Box.BLACK; BR.Color = Box.BLACK;
						break;
					case (byte)37:
						TL.Color = Box.BLACK; TC.Color = Box.WHITE; TR.Color = Box.WHITE;
						ML.Color = Box.BLACK; MR.Color = Box.WHITE;
						BL.Color = Box.BLACK; BC.Color = Box.BLACK; BR.Color = Box.BLACK;
						break;
					case (byte)38:
						TL.Color = Box.BLACK; TC.Color = Box.BLACK; TR.Color = Box.WHITE;
						ML.Color = Box.BLACK; MR.Color = Box.WHITE;
						BL.Color = Box.BLACK; BC.Color = Box.BLACK; BR.Color = Box.WHITE;
						break;
					case (byte)39:
						TL.Color = Box.BLACK; TC.Color = Box.BLACK; TR.Color = Box.BLACK;
						ML.Color = Box.BLACK; MR.Color = Box.WHITE;
						BL.Color = Box.BLACK; BC.Color = Box.WHITE; BR.Color = Box.WHITE;
						break;
					case (byte)40:
						TL.Color = Box.BLACK; TC.Color = Box.BLACK; TR.Color = Box.BLACK;
						ML.Color = Box.BLACK; MR.Color = Box.BLACK;
						BL.Color = Box.WHITE; BC.Color = Box.WHITE; BR.Color = Box.WHITE;
						break;
					default:
						Console.WriteLine("Error: 数字マス[5]周りの黒マス配置中にエラー");
						Application.Exit();
						break;
				}
			}
			// [6] id=41~48
			else if (HEAD_BN_6 <= id && id < HEAD_BN_7) {
				switch (id) {
					case (byte)41:
						TL.Color = Box.BLACK; TC.Color = Box.BLACK; TR.Color = Box.BLACK;
						ML.Color = Box.WHITE; MR.Color = Box.BLACK;
						BL.Color = Box.WHITE; BC.Color = Box.BLACK; BR.Color = Box.BLACK;
						break;
					case (byte)42:
						TL.Color = Box.WHITE; TC.Color = Box.BLACK; TR.Color = Box.BLACK;
						ML.Color = Box.WHITE; MR.Color = Box.BLACK;
						BL.Color = Box.BLACK; BC.Color = Box.BLACK; BR.Color = Box.BLACK;
						break;
					case (byte)43:
						TL.Color = Box.WHITE; TC.Color = Box.WHITE; TR.Color = Box.BLACK;
						ML.Color = Box.BLACK; MR.Color = Box.BLACK;
						BL.Color = Box.BLACK; BC.Color = Box.BLACK; BR.Color = Box.BLACK;
						break;
					case (byte)44:
						TL.Color = Box.BLACK; TC.Color = Box.WHITE; TR.Color = Box.WHITE;
						ML.Color = Box.BLACK; MR.Color = Box.BLACK;
						BL.Color = Box.BLACK; BC.Color = Box.BLACK; BR.Color = Box.BLACK;
						break;
					case (byte)45:
						TL.Color = Box.BLACK; TC.Color = Box.BLACK; TR.Color = Box.WHITE;
						ML.Color = Box.BLACK; MR.Color = Box.WHITE;
						BL.Color = Box.BLACK; BC.Color = Box.BLACK; BR.Color = Box.BLACK;
						break;
					case (byte)46:
						TL.Color = Box.BLACK; TC.Color = Box.BLACK; TR.Color = Box.BLACK;
						ML.Color = Box.BLACK; MR.Color = Box.WHITE;
						BL.Color = Box.BLACK; BC.Color = Box.BLACK; BR.Color = Box.WHITE;
						break;
					case (byte)47:
						TL.Color = Box.BLACK; TC.Color = Box.BLACK; TR.Color = Box.BLACK;
						ML.Color = Box.BLACK; MR.Color = Box.BLACK;
						BL.Color = Box.BLACK; BC.Color = Box.WHITE; BR.Color = Box.WHITE;
						break;
					case (byte)48:
						TL.Color = Box.BLACK; TC.Color = Box.BLACK; TR.Color = Box.BLACK;
						ML.Color = Box.BLACK; MR.Color = Box.BLACK;
						BL.Color = Box.WHITE; BC.Color = Box.WHITE; BR.Color = Box.BLACK;
						break;
					default:
						Console.WriteLine("Error: 数字マス[6]周りの黒マス配置中にエラー");
						Application.Exit();
						break;
				}
			}
			// [7] id=49~56
			else if (HEAD_BN_7 <= id && id < HEAD_BN_8) {
				switch (id) {
					case (byte)49:
						TL.Color = Box.BLACK; TC.Color = Box.BLACK; TR.Color = Box.BLACK;
						ML.Color = Box.WHITE; MR.Color = Box.BLACK;
						BL.Color = Box.BLACK; BC.Color = Box.BLACK; BR.Color = Box.BLACK;
						break;
					case (byte)50:
						TL.Color = Box.WHITE; TC.Color = Box.BLACK; TR.Color = Box.BLACK;
						ML.Color = Box.BLACK; MR.Color = Box.BLACK;
						BL.Color = Box.BLACK; BC.Color = Box.BLACK; BR.Color = Box.BLACK;
						break;
					case (byte)51:
						TL.Color = Box.BLACK; TC.Color = Box.WHITE; TR.Color = Box.BLACK;
						ML.Color = Box.BLACK; MR.Color = Box.BLACK;
						BL.Color = Box.BLACK; BC.Color = Box.BLACK; BR.Color = Box.BLACK;
						break;
					case (byte)52:
						TL.Color = Box.BLACK; TC.Color = Box.BLACK; TR.Color = Box.WHITE;
						ML.Color = Box.BLACK; MR.Color = Box.BLACK;
						BL.Color = Box.BLACK; BC.Color = Box.BLACK; BR.Color = Box.BLACK;
						break;
					case (byte)53:
						TL.Color = Box.BLACK; TC.Color = Box.BLACK; TR.Color = Box.BLACK;
						ML.Color = Box.BLACK; MR.Color = Box.WHITE;
						BL.Color = Box.BLACK; BC.Color = Box.BLACK; BR.Color = Box.BLACK;
						break;
					case (byte)54:
						TL.Color = Box.BLACK; TC.Color = Box.BLACK; TR.Color = Box.BLACK;
						ML.Color = Box.BLACK; MR.Color = Box.BLACK;
						BL.Color = Box.BLACK; BC.Color = Box.BLACK; BR.Color = Box.WHITE;
						break;
					case (byte)55:
						TL.Color = Box.BLACK; TC.Color = Box.BLACK; TR.Color = Box.BLACK;
						ML.Color = Box.BLACK; MR.Color = Box.BLACK;
						BL.Color = Box.BLACK; BC.Color = Box.WHITE; BR.Color = Box.BLACK;
						break;
					case (byte)56:
						TL.Color = Box.BLACK; TC.Color = Box.BLACK; TR.Color = Box.BLACK;
						ML.Color = Box.BLACK; MR.Color = Box.BLACK;
						BL.Color = Box.WHITE; BC.Color = Box.BLACK; BR.Color = Box.BLACK;
						break;
					default:
						Console.WriteLine("Error: 数字マス[7]周りの黒マス配置中にエラー");
						Application.Exit();
						break;
				}
			}
			// [8] id=57
			else if (id == HEAD_BN_8) {
				TL.Color = Box.BLACK; TC.Color = Box.BLACK; TR.Color = Box.BLACK;
				ML.Color = Box.BLACK; MR.Color = Box.BLACK;
				BL.Color = Box.BLACK; BC.Color = Box.BLACK; BR.Color = Box.BLACK;
			}
			// [11] id=58~77
			else if (HEAD_BN_11 <= id && id < HEAD_BN_12) {
				switch (id) {
					case (byte)58:
						TL.Color = Box.BLACK; TC.Color = Box.WHITE; TR.Color = Box.BLACK;
						ML.Color = Box.WHITE; MR.Color = Box.WHITE;
						BL.Color = Box.WHITE; BC.Color = Box.WHITE; BR.Color = Box.WHITE;
						break;
					case (byte)59:
						TL.Color = Box.BLACK; TC.Color = Box.WHITE; TR.Color = Box.WHITE;
						ML.Color = Box.WHITE; MR.Color = Box.BLACK;
						BL.Color = Box.WHITE; BC.Color = Box.WHITE; BR.Color = Box.WHITE;
						break;
					case (byte)60:
						TL.Color = Box.BLACK; TC.Color = Box.WHITE; TR.Color = Box.WHITE;
						ML.Color = Box.WHITE; MR.Color = Box.WHITE;
						BL.Color = Box.WHITE; BC.Color = Box.WHITE; BR.Color = Box.BLACK;
						break;
					case (byte)61:
						TL.Color = Box.BLACK; TC.Color = Box.WHITE; TR.Color = Box.WHITE;
						ML.Color = Box.WHITE; MR.Color = Box.WHITE;
						BL.Color = Box.WHITE; BC.Color = Box.BLACK; BR.Color = Box.WHITE;
						break;
					case (byte)62:
						TL.Color = Box.BLACK; TC.Color = Box.WHITE; TR.Color = Box.WHITE;
						ML.Color = Box.WHITE; MR.Color = Box.WHITE;
						BL.Color = Box.BLACK; BC.Color = Box.WHITE; BR.Color = Box.WHITE;
						break;
					case (byte)63:
						TL.Color = Box.WHITE; TC.Color = Box.BLACK; TR.Color = Box.WHITE;
						ML.Color = Box.WHITE; MR.Color = Box.BLACK;
						BL.Color = Box.WHITE; BC.Color = Box.WHITE; BR.Color = Box.WHITE;
						break;
					case (byte)64:
						TL.Color = Box.WHITE; TC.Color = Box.BLACK; TR.Color = Box.WHITE;
						ML.Color = Box.WHITE; MR.Color = Box.WHITE;
						BL.Color = Box.WHITE; BC.Color = Box.WHITE; BR.Color = Box.BLACK;
						break;
					case (byte)65:
						TL.Color = Box.WHITE; TC.Color = Box.BLACK; TR.Color = Box.WHITE;
						ML.Color = Box.WHITE; MR.Color = Box.WHITE;
						BL.Color = Box.WHITE; BC.Color = Box.BLACK; BR.Color = Box.WHITE;
						break;
					case (byte)66:
						TL.Color = Box.WHITE; TC.Color = Box.BLACK; TR.Color = Box.WHITE;
						ML.Color = Box.WHITE; MR.Color = Box.WHITE;
						BL.Color = Box.BLACK; BC.Color = Box.WHITE; BR.Color = Box.WHITE;
						break;
					case (byte)67:
						TL.Color = Box.WHITE; TC.Color = Box.BLACK; TR.Color = Box.WHITE;
						ML.Color = Box.BLACK; MR.Color = Box.WHITE;
						BL.Color = Box.WHITE; BC.Color = Box.WHITE; BR.Color = Box.WHITE;
						break;
					case (byte)68:
						TL.Color = Box.WHITE; TC.Color = Box.WHITE; TR.Color = Box.BLACK;
						ML.Color = Box.WHITE; MR.Color = Box.WHITE;
						BL.Color = Box.WHITE; BC.Color = Box.WHITE; BR.Color = Box.BLACK;
						break;
					case (byte)69:
						TL.Color = Box.WHITE; TC.Color = Box.WHITE; TR.Color = Box.BLACK;
						ML.Color = Box.WHITE; MR.Color = Box.WHITE;
						BL.Color = Box.WHITE; BC.Color = Box.BLACK; BR.Color = Box.WHITE;
						break;
					case (byte)70:
						TL.Color = Box.WHITE; TC.Color = Box.WHITE; TR.Color = Box.BLACK;
						ML.Color = Box.WHITE; MR.Color = Box.WHITE;
						BL.Color = Box.BLACK; BC.Color = Box.WHITE; BR.Color = Box.WHITE;
						break;
					case (byte)71:
						TL.Color = Box.WHITE; TC.Color = Box.WHITE; TR.Color = Box.BLACK;
						ML.Color = Box.BLACK; MR.Color = Box.WHITE;
						BL.Color = Box.WHITE; BC.Color = Box.WHITE; BR.Color = Box.WHITE;
						break;
					case (byte)72:
						TL.Color = Box.WHITE; TC.Color = Box.WHITE; TR.Color = Box.WHITE;
						ML.Color = Box.WHITE; MR.Color = Box.BLACK;
						BL.Color = Box.WHITE; BC.Color = Box.BLACK; BR.Color = Box.WHITE;
						break;
					case (byte)73:
						TL.Color = Box.WHITE; TC.Color = Box.WHITE; TR.Color = Box.WHITE;
						ML.Color = Box.WHITE; MR.Color = Box.BLACK;
						BL.Color = Box.BLACK; BC.Color = Box.WHITE; BR.Color = Box.WHITE;
						break;
					case (byte)74:
						TL.Color = Box.WHITE; TC.Color = Box.WHITE; TR.Color = Box.WHITE;
						ML.Color = Box.BLACK; MR.Color = Box.BLACK;
						BL.Color = Box.WHITE; BC.Color = Box.WHITE; BR.Color = Box.WHITE;
						break;
					case (byte)75:
						TL.Color = Box.WHITE; TC.Color = Box.WHITE; TR.Color = Box.WHITE;
						ML.Color = Box.WHITE; MR.Color = Box.WHITE;
						BL.Color = Box.BLACK; BC.Color = Box.WHITE; BR.Color = Box.BLACK;
						break;
					case (byte)76:
						TL.Color = Box.WHITE; TC.Color = Box.WHITE; TR.Color = Box.WHITE;
						ML.Color = Box.BLACK; MR.Color = Box.WHITE;
						BL.Color = Box.WHITE; BC.Color = Box.WHITE; BR.Color = Box.BLACK;
						break;
					case (byte)77:
						TL.Color = Box.WHITE; TC.Color = Box.WHITE; TR.Color = Box.WHITE;
						ML.Color = Box.BLACK; MR.Color = Box.WHITE;
						BL.Color = Box.WHITE; BC.Color = Box.BLACK; BR.Color = Box.WHITE;
						break;
					default:
						Console.WriteLine("Error: 数字マス[11]周りの黒マス配置中にエラー");
						Application.Exit();
						break;
				}
			}
			// [12] id=78~109
			else if (HEAD_BN_12 <= id && id < HEAD_BN_13) {
				switch (id) {
					case (byte)78:
						TL.Color = Box.BLACK; TC.Color = Box.WHITE; TR.Color = Box.BLACK;
						ML.Color = Box.WHITE; MR.Color = Box.BLACK;
						BL.Color = Box.WHITE; BC.Color = Box.WHITE; BR.Color = Box.WHITE;
						break;
					case (byte)79:
						TL.Color = Box.BLACK; TC.Color = Box.WHITE; TR.Color = Box.WHITE;
						ML.Color = Box.WHITE; MR.Color = Box.BLACK;
						BL.Color = Box.WHITE; BC.Color = Box.WHITE; BR.Color = Box.BLACK;
						break;
					case (byte)80:
						TL.Color = Box.BLACK; TC.Color = Box.WHITE; TR.Color = Box.WHITE;
						ML.Color = Box.WHITE; MR.Color = Box.WHITE;
						BL.Color = Box.WHITE; BC.Color = Box.BLACK; BR.Color = Box.BLACK;
						break;
					case (byte)81:
						TL.Color = Box.BLACK; TC.Color = Box.WHITE; TR.Color = Box.WHITE;
						ML.Color = Box.WHITE; MR.Color = Box.WHITE;
						BL.Color = Box.BLACK; BC.Color = Box.BLACK; BR.Color = Box.WHITE;
						break;
					case (byte)82:
						TL.Color = Box.WHITE; TC.Color = Box.BLACK; TR.Color = Box.WHITE;
						ML.Color = Box.WHITE; MR.Color = Box.WHITE;
						BL.Color = Box.BLACK; BC.Color = Box.WHITE; BR.Color = Box.WHITE;
						break;
					case (byte)83:
						TL.Color = Box.WHITE; TC.Color = Box.BLACK; TR.Color = Box.WHITE;
						ML.Color = Box.WHITE; MR.Color = Box.WHITE;
						BL.Color = Box.WHITE; BC.Color = Box.BLACK; BR.Color = Box.BLACK;
						break;
					case (byte)84:
						TL.Color = Box.WHITE; TC.Color = Box.BLACK; TR.Color = Box.WHITE;
						ML.Color = Box.WHITE; MR.Color = Box.WHITE;
						BL.Color = Box.BLACK; BC.Color = Box.BLACK; BR.Color = Box.WHITE;
						break;
					case (byte)85:
						TL.Color = Box.WHITE; TC.Color = Box.BLACK; TR.Color = Box.WHITE;
						ML.Color = Box.BLACK; MR.Color = Box.WHITE;
						BL.Color = Box.BLACK; BC.Color = Box.WHITE; BR.Color = Box.WHITE;
						break;
					case (byte)86:
						TL.Color = Box.WHITE; TC.Color = Box.WHITE; TR.Color = Box.BLACK;
						ML.Color = Box.WHITE; MR.Color = Box.WHITE;
						BL.Color = Box.WHITE; BC.Color = Box.BLACK; BR.Color = Box.BLACK;
						break;
					case (byte)87:
						TL.Color = Box.WHITE; TC.Color = Box.WHITE; TR.Color = Box.BLACK;
						ML.Color = Box.WHITE; MR.Color = Box.WHITE;
						BL.Color = Box.BLACK; BC.Color = Box.BLACK; BR.Color = Box.WHITE;
						break;
					case (byte)88:
						TL.Color = Box.WHITE; TC.Color = Box.WHITE; TR.Color = Box.BLACK;
						ML.Color = Box.BLACK; MR.Color = Box.WHITE;
						BL.Color = Box.BLACK; BC.Color = Box.WHITE; BR.Color = Box.WHITE;
						break;
					case (byte)89:
						TL.Color = Box.BLACK; TC.Color = Box.WHITE; TR.Color = Box.BLACK;
						ML.Color = Box.BLACK; MR.Color = Box.WHITE;
						BL.Color = Box.WHITE; BC.Color = Box.WHITE; BR.Color = Box.WHITE;
						break;
					case (byte)90:
						TL.Color = Box.WHITE; TC.Color = Box.WHITE; TR.Color = Box.WHITE;
						ML.Color = Box.WHITE; MR.Color = Box.BLACK;
						BL.Color = Box.BLACK; BC.Color = Box.BLACK; BR.Color = Box.WHITE;
						break;
					case (byte)91:
						TL.Color = Box.WHITE; TC.Color = Box.WHITE; TR.Color = Box.WHITE;
						ML.Color = Box.BLACK; MR.Color = Box.BLACK;
						BL.Color = Box.BLACK; BC.Color = Box.WHITE; BR.Color = Box.WHITE;
						break;
					case (byte)92:
						TL.Color = Box.BLACK; TC.Color = Box.WHITE; TR.Color = Box.WHITE;
						ML.Color = Box.BLACK; MR.Color = Box.BLACK;
						BL.Color = Box.WHITE; BC.Color = Box.WHITE; BR.Color = Box.WHITE;
						break;
					case (byte)93:
						TL.Color = Box.BLACK; TC.Color = Box.BLACK; TR.Color = Box.WHITE;
						ML.Color = Box.WHITE; MR.Color = Box.BLACK;
						BL.Color = Box.WHITE; BC.Color = Box.WHITE; BR.Color = Box.WHITE;
						break;
					case (byte)94:
						TL.Color = Box.WHITE; TC.Color = Box.WHITE; TR.Color = Box.WHITE;
						ML.Color = Box.BLACK; MR.Color = Box.WHITE;
						BL.Color = Box.BLACK; BC.Color = Box.WHITE; BR.Color = Box.BLACK;
						break;
					case (byte)95:
						TL.Color = Box.BLACK; TC.Color = Box.WHITE; TR.Color = Box.WHITE;
						ML.Color = Box.BLACK; MR.Color = Box.WHITE;
						BL.Color = Box.WHITE; BC.Color = Box.WHITE; BR.Color = Box.BLACK;
						break;
					case (byte)96:
						TL.Color = Box.BLACK; TC.Color = Box.BLACK; TR.Color = Box.WHITE;
						ML.Color = Box.WHITE; MR.Color = Box.WHITE;
						BL.Color = Box.WHITE; BC.Color = Box.WHITE; BR.Color = Box.BLACK;
						break;
					case (byte)97:
						TL.Color = Box.WHITE; TC.Color = Box.BLACK; TR.Color = Box.BLACK;
						ML.Color = Box.WHITE; MR.Color = Box.WHITE;
						BL.Color = Box.WHITE; BC.Color = Box.WHITE; BR.Color = Box.BLACK;
						break;
					case (byte)98:
						TL.Color = Box.BLACK; TC.Color = Box.WHITE; TR.Color = Box.WHITE;
						ML.Color = Box.BLACK; MR.Color = Box.WHITE;
						BL.Color = Box.WHITE; BC.Color = Box.BLACK; BR.Color = Box.WHITE;
						break;
					case (byte)99:
						TL.Color = Box.BLACK; TC.Color = Box.BLACK; TR.Color = Box.WHITE;
						ML.Color = Box.WHITE; MR.Color = Box.WHITE;
						BL.Color = Box.WHITE; BC.Color = Box.BLACK; BR.Color = Box.WHITE;
						break;
					case (byte)100:
						TL.Color = Box.WHITE; TC.Color = Box.BLACK; TR.Color = Box.BLACK;
						ML.Color = Box.WHITE; MR.Color = Box.WHITE;
						BL.Color = Box.WHITE; BC.Color = Box.BLACK; BR.Color = Box.WHITE;
						break;
					case (byte)101:
						TL.Color = Box.WHITE; TC.Color = Box.WHITE; TR.Color = Box.BLACK;
						ML.Color = Box.WHITE; MR.Color = Box.BLACK;
						BL.Color = Box.WHITE; BC.Color = Box.BLACK; BR.Color = Box.WHITE;
						break;
					case (byte)102:
						TL.Color = Box.BLACK; TC.Color = Box.BLACK; TR.Color = Box.WHITE;
						ML.Color = Box.WHITE; MR.Color = Box.WHITE;
						BL.Color = Box.BLACK; BC.Color = Box.WHITE; BR.Color = Box.WHITE;
						break;
					case (byte)103:
						TL.Color = Box.WHITE; TC.Color = Box.BLACK; TR.Color = Box.BLACK;
						ML.Color = Box.WHITE; MR.Color = Box.WHITE;
						BL.Color = Box.BLACK; BC.Color = Box.WHITE; BR.Color = Box.WHITE;
						break;
					case (byte)104:
						TL.Color = Box.WHITE; TC.Color = Box.WHITE; TR.Color = Box.BLACK;
						ML.Color = Box.WHITE; MR.Color = Box.BLACK;
						BL.Color = Box.BLACK; BC.Color = Box.WHITE; BR.Color = Box.WHITE;
						break;
					case (byte)105:
						TL.Color = Box.WHITE; TC.Color = Box.WHITE; TR.Color = Box.WHITE;
						ML.Color = Box.WHITE; MR.Color = Box.BLACK;
						BL.Color = Box.BLACK; BC.Color = Box.WHITE; BR.Color = Box.BLACK;
						break;
					case (byte)106:
						TL.Color = Box.WHITE; TC.Color = Box.BLACK; TR.Color = Box.BLACK;
						ML.Color = Box.BLACK; MR.Color = Box.WHITE;
						BL.Color = Box.WHITE; BC.Color = Box.WHITE; BR.Color = Box.WHITE;
						break;
					case (byte)107:
						TL.Color = Box.WHITE; TC.Color = Box.WHITE; TR.Color = Box.BLACK;
						ML.Color = Box.BLACK; MR.Color = Box.BLACK;
						BL.Color = Box.WHITE; BC.Color = Box.WHITE; BR.Color = Box.WHITE;
						break;
					case (byte)108:
						TL.Color = Box.WHITE; TC.Color = Box.WHITE; TR.Color = Box.WHITE;
						ML.Color = Box.BLACK; MR.Color = Box.BLACK;
						BL.Color = Box.WHITE; BC.Color = Box.WHITE; BR.Color = Box.BLACK;
						break;
					case (byte)109:
						TL.Color = Box.WHITE; TC.Color = Box.WHITE; TR.Color = Box.WHITE;
						ML.Color = Box.BLACK; MR.Color = Box.WHITE;
						BL.Color = Box.WHITE; BC.Color = Box.BLACK; BR.Color = Box.BLACK;
						break;
					default:
						Console.WriteLine("Error: 数字マス[12]周りの黒マス配置中にエラー");
						Application.Exit();
						break;
				}
			}
			// [13] id=110~133
			else if (HEAD_BN_13 <= id && id < HEAD_BN_14) {
				switch (id) {
					case (byte)110:
						TL.Color = Box.BLACK; TC.Color = Box.WHITE; TR.Color = Box.BLACK;
						ML.Color = Box.WHITE; MR.Color = Box.BLACK;
						BL.Color = Box.WHITE; BC.Color = Box.WHITE; BR.Color = Box.BLACK;
						break;
					case (byte)111:
						TL.Color = Box.BLACK; TC.Color = Box.WHITE; TR.Color = Box.WHITE;
						ML.Color = Box.WHITE; MR.Color = Box.BLACK;
						BL.Color = Box.WHITE; BC.Color = Box.BLACK; BR.Color = Box.BLACK;
						break;
					case (byte)112:
						TL.Color = Box.BLACK; TC.Color = Box.WHITE; TR.Color = Box.WHITE;
						ML.Color = Box.WHITE; MR.Color = Box.WHITE;
						BL.Color = Box.BLACK; BC.Color = Box.BLACK; BR.Color = Box.BLACK;
						break;
					case (byte)113:
						TL.Color = Box.WHITE; TC.Color = Box.BLACK; TR.Color = Box.WHITE;
						ML.Color = Box.WHITE; MR.Color = Box.BLACK;
						BL.Color = Box.WHITE; BC.Color = Box.BLACK; BR.Color = Box.BLACK;
						break;
					case (byte)114:
						TL.Color = Box.WHITE; TC.Color = Box.BLACK; TR.Color = Box.WHITE;
						ML.Color = Box.WHITE; MR.Color = Box.WHITE;
						BL.Color = Box.BLACK; BC.Color = Box.BLACK; BR.Color = Box.BLACK;
						break;
					case (byte)115:
						TL.Color = Box.WHITE; TC.Color = Box.BLACK; TR.Color = Box.WHITE;
						ML.Color = Box.BLACK; MR.Color = Box.WHITE;
						BL.Color = Box.BLACK; BC.Color = Box.BLACK; BR.Color = Box.WHITE;
						break;
					case (byte)116:
						TL.Color = Box.WHITE; TC.Color = Box.WHITE; TR.Color = Box.BLACK;
						ML.Color = Box.WHITE; MR.Color = Box.WHITE;
						BL.Color = Box.BLACK; BC.Color = Box.BLACK; BR.Color = Box.BLACK;
						break;
					case (byte)117:
						TL.Color = Box.WHITE; TC.Color = Box.WHITE; TR.Color = Box.BLACK;
						ML.Color = Box.BLACK; MR.Color = Box.WHITE;
						BL.Color = Box.BLACK; BC.Color = Box.BLACK; BR.Color = Box.WHITE;
						break;
					case (byte)118:
						TL.Color = Box.BLACK; TC.Color = Box.WHITE; TR.Color = Box.BLACK;
						ML.Color = Box.BLACK; MR.Color = Box.WHITE;
						BL.Color = Box.BLACK; BC.Color = Box.WHITE; BR.Color = Box.WHITE;
						break;
					case (byte)119:
						TL.Color = Box.WHITE; TC.Color = Box.WHITE; TR.Color = Box.WHITE;
						ML.Color = Box.BLACK; MR.Color = Box.BLACK;
						BL.Color = Box.BLACK; BC.Color = Box.BLACK; BR.Color = Box.WHITE;
						break;
					case (byte)120:
						TL.Color = Box.BLACK; TC.Color = Box.WHITE; TR.Color = Box.WHITE;
						ML.Color = Box.BLACK; MR.Color = Box.BLACK;
						BL.Color = Box.BLACK; BC.Color = Box.WHITE; BR.Color = Box.WHITE;
						break;
					case (byte)121:
						TL.Color = Box.BLACK; TC.Color = Box.BLACK; TR.Color = Box.WHITE;
						ML.Color = Box.BLACK; MR.Color = Box.BLACK;
						BL.Color = Box.WHITE; BC.Color = Box.WHITE; BR.Color = Box.WHITE;
						break;
					case (byte)122:
						TL.Color = Box.BLACK; TC.Color = Box.WHITE; TR.Color = Box.WHITE;
						ML.Color = Box.BLACK; MR.Color = Box.WHITE;
						BL.Color = Box.BLACK; BC.Color = Box.WHITE; BR.Color = Box.BLACK;
						break;
					case (byte)123:
						TL.Color = Box.BLACK; TC.Color = Box.BLACK; TR.Color = Box.WHITE;
						ML.Color = Box.BLACK; MR.Color = Box.WHITE;
						BL.Color = Box.WHITE; BC.Color = Box.WHITE; BR.Color = Box.BLACK;
						break;
					case (byte)124:
						TL.Color = Box.BLACK; TC.Color = Box.BLACK; TR.Color = Box.BLACK;
						ML.Color = Box.WHITE; MR.Color = Box.WHITE;
						BL.Color = Box.WHITE; BC.Color = Box.WHITE; BR.Color = Box.BLACK;
						break;
					case (byte)125:
						TL.Color = Box.BLACK; TC.Color = Box.BLACK; TR.Color = Box.WHITE;
						ML.Color = Box.BLACK; MR.Color = Box.WHITE;
						BL.Color = Box.WHITE; BC.Color = Box.BLACK; BR.Color = Box.WHITE;
						break;
					case (byte)126:
						TL.Color = Box.BLACK; TC.Color = Box.BLACK; TR.Color = Box.BLACK;
						ML.Color = Box.WHITE; MR.Color = Box.WHITE;
						BL.Color = Box.WHITE; BC.Color = Box.BLACK; BR.Color = Box.WHITE;
						break;
					case (byte)127:
						TL.Color = Box.WHITE; TC.Color = Box.BLACK; TR.Color = Box.BLACK;
						ML.Color = Box.WHITE; MR.Color = Box.BLACK;
						BL.Color = Box.WHITE; BC.Color = Box.BLACK; BR.Color = Box.WHITE;
						break;
					case (byte)128:
						TL.Color = Box.BLACK; TC.Color = Box.BLACK; TR.Color = Box.BLACK;
						ML.Color = Box.WHITE; MR.Color = Box.WHITE;
						BL.Color = Box.BLACK; BC.Color = Box.WHITE; BR.Color = Box.WHITE;
						break;
					case (byte)129:
						TL.Color = Box.WHITE; TC.Color = Box.BLACK; TR.Color = Box.BLACK;
						ML.Color = Box.WHITE; MR.Color = Box.WHITE;
						BL.Color = Box.BLACK; BC.Color = Box.WHITE; BR.Color = Box.WHITE;
						break;
					case (byte)130:
						TL.Color = Box.WHITE; TC.Color = Box.WHITE; TR.Color = Box.BLACK;
						ML.Color = Box.WHITE; MR.Color = Box.BLACK;
						BL.Color = Box.BLACK; BC.Color = Box.WHITE; BR.Color = Box.BLACK;
						break;
					case (byte)131:
						TL.Color = Box.WHITE; TC.Color = Box.BLACK; TR.Color = Box.BLACK;
						ML.Color = Box.BLACK; MR.Color = Box.BLACK;
						BL.Color = Box.WHITE; BC.Color = Box.WHITE; BR.Color = Box.WHITE;
						break;
					case (byte)132:
						TL.Color = Box.WHITE; TC.Color = Box.WHITE; TR.Color = Box.BLACK;
						ML.Color = Box.BLACK; MR.Color = Box.BLACK;
						BL.Color = Box.WHITE; BC.Color = Box.WHITE; BR.Color = Box.BLACK;
						break;
					case (byte)133:
						TL.Color = Box.WHITE; TC.Color = Box.WHITE; TR.Color = Box.WHITE;
						ML.Color = Box.BLACK; MR.Color = Box.BLACK;
						BL.Color = Box.WHITE; BC.Color = Box.BLACK; BR.Color = Box.BLACK;
						break;
					default:
						Console.WriteLine("Error: 数字マス[13]周りの黒マス配置中にエラー");
						Application.Exit();
						break;
				}
			}
			// [14] id=134~149
			else if (HEAD_BN_14 <= id && id < HEAD_BN_15) {
				switch (id) {
					case (byte)134:
						TL.Color = Box.BLACK; TC.Color = Box.WHITE; TR.Color = Box.BLACK;
						ML.Color = Box.WHITE; MR.Color = Box.BLACK;
						BL.Color = Box.WHITE; BC.Color = Box.BLACK; BR.Color = Box.BLACK;
						break;
					case (byte)135:
						TL.Color = Box.BLACK; TC.Color = Box.WHITE; TR.Color = Box.WHITE;
						ML.Color = Box.WHITE; MR.Color = Box.BLACK;
						BL.Color = Box.BLACK; BC.Color = Box.BLACK; BR.Color = Box.BLACK;
						break;
					case (byte)136:
						TL.Color = Box.WHITE; TC.Color = Box.BLACK; TR.Color = Box.WHITE;
						ML.Color = Box.WHITE; MR.Color = Box.BLACK;
						BL.Color = Box.BLACK; BC.Color = Box.BLACK; BR.Color = Box.BLACK;
						break;
					case (byte)137:
						TL.Color = Box.WHITE; TC.Color = Box.BLACK; TR.Color = Box.WHITE;
						ML.Color = Box.BLACK; MR.Color = Box.WHITE;
						BL.Color = Box.BLACK; BC.Color = Box.BLACK; BR.Color = Box.BLACK;
						break;
					case (byte)138:
						TL.Color = Box.WHITE; TC.Color = Box.WHITE; TR.Color = Box.BLACK;
						ML.Color = Box.BLACK; MR.Color = Box.WHITE;
						BL.Color = Box.BLACK; BC.Color = Box.BLACK; BR.Color = Box.BLACK;
						break;
					case (byte)139:
						TL.Color = Box.BLACK; TC.Color = Box.WHITE; TR.Color = Box.BLACK;
						ML.Color = Box.BLACK; MR.Color = Box.WHITE;
						BL.Color = Box.BLACK; BC.Color = Box.BLACK; BR.Color = Box.WHITE;
						break;
					case (byte)140:
						TL.Color = Box.BLACK; TC.Color = Box.WHITE; TR.Color = Box.WHITE;
						ML.Color = Box.BLACK; MR.Color = Box.BLACK;
						BL.Color = Box.BLACK; BC.Color = Box.BLACK; BR.Color = Box.WHITE;
						break;
					case (byte)141:
						TL.Color = Box.BLACK; TC.Color = Box.BLACK; TR.Color = Box.WHITE;
						ML.Color = Box.BLACK; MR.Color = Box.BLACK;
						BL.Color = Box.BLACK; BC.Color = Box.WHITE; BR.Color = Box.WHITE;
						break;
					case (byte)142:
						TL.Color = Box.BLACK; TC.Color = Box.BLACK; TR.Color = Box.WHITE;
						ML.Color = Box.BLACK; MR.Color = Box.WHITE;
						BL.Color = Box.BLACK; BC.Color = Box.WHITE; BR.Color = Box.BLACK;
						break;
					case (byte)143:
						TL.Color = Box.BLACK; TC.Color = Box.BLACK; TR.Color = Box.BLACK;
						ML.Color = Box.BLACK; MR.Color = Box.WHITE;
						BL.Color = Box.WHITE; BC.Color = Box.WHITE; BR.Color = Box.BLACK;
						break;
					case (byte)144:
						TL.Color = Box.BLACK; TC.Color = Box.BLACK; TR.Color = Box.BLACK;
						ML.Color = Box.BLACK; MR.Color = Box.WHITE;
						BL.Color = Box.WHITE; BC.Color = Box.BLACK; BR.Color = Box.WHITE;
						break;
					case (byte)145:
						TL.Color = Box.BLACK; TC.Color = Box.BLACK; TR.Color = Box.BLACK;
						ML.Color = Box.WHITE; MR.Color = Box.BLACK;
						BL.Color = Box.WHITE; BC.Color = Box.BLACK; BR.Color = Box.WHITE;
						break;
					case (byte)146:
						TL.Color = Box.BLACK; TC.Color = Box.BLACK; TR.Color = Box.BLACK;
						ML.Color = Box.WHITE; MR.Color = Box.BLACK;
						BL.Color = Box.BLACK; BC.Color = Box.WHITE; BR.Color = Box.WHITE;
						break;
					case (byte)147:
						TL.Color = Box.WHITE; TC.Color = Box.BLACK; TR.Color = Box.BLACK;
						ML.Color = Box.WHITE; MR.Color = Box.BLACK;
						BL.Color = Box.BLACK; BC.Color = Box.WHITE; BR.Color = Box.BLACK;
						break;
					case (byte)148:
						TL.Color = Box.WHITE; TC.Color = Box.BLACK; TR.Color = Box.BLACK;
						ML.Color = Box.BLACK; MR.Color = Box.BLACK;
						BL.Color = Box.WHITE; BC.Color = Box.WHITE; BR.Color = Box.BLACK;
						break;
					case (byte)149:
						TL.Color = Box.WHITE; TC.Color = Box.WHITE; TR.Color = Box.BLACK;
						ML.Color = Box.BLACK; MR.Color = Box.BLACK;
						BL.Color = Box.WHITE; BC.Color = Box.BLACK; BR.Color = Box.BLACK;
						break;
					default:
						Console.WriteLine("Error: 数字マス[14]周りの黒マス配置中にエラー");
						Application.Exit();
						break;
				}
			}
			// [15] id=150~157
			else if (HEAD_BN_15 <= id && id < HEAD_BN_22) {
				switch (id) {
					case (byte)150:
						TL.Color = Box.BLACK; TC.Color = Box.WHITE; TR.Color = Box.BLACK;
						ML.Color = Box.WHITE; MR.Color = Box.BLACK;
						BL.Color = Box.BLACK; BC.Color = Box.BLACK; BR.Color = Box.BLACK;
						break;
					case (byte)151:
						TL.Color = Box.WHITE; TC.Color = Box.BLACK; TR.Color = Box.WHITE;
						ML.Color = Box.BLACK; MR.Color = Box.BLACK;
						BL.Color = Box.BLACK; BC.Color = Box.BLACK; BR.Color = Box.BLACK;
						break;
					case (byte)152:
						TL.Color = Box.BLACK; TC.Color = Box.WHITE; TR.Color = Box.BLACK;
						ML.Color = Box.BLACK; MR.Color = Box.WHITE;
						BL.Color = Box.BLACK; BC.Color = Box.BLACK; BR.Color = Box.BLACK;
						break;
					case (byte)153:
						TL.Color = Box.BLACK; TC.Color = Box.BLACK; TR.Color = Box.WHITE;
						ML.Color = Box.BLACK; MR.Color = Box.BLACK;
						BL.Color = Box.BLACK; BC.Color = Box.BLACK; BR.Color = Box.WHITE;
						break;
					case (byte)154:
						TL.Color = Box.BLACK; TC.Color = Box.BLACK; TR.Color = Box.BLACK;
						ML.Color = Box.BLACK; MR.Color = Box.WHITE;
						BL.Color = Box.BLACK; BC.Color = Box.WHITE; BR.Color = Box.BLACK;
						break;
					case (byte)155:
						TL.Color = Box.BLACK; TC.Color = Box.BLACK; TR.Color = Box.BLACK;
						ML.Color = Box.BLACK; MR.Color = Box.BLACK;
						BL.Color = Box.WHITE; BC.Color = Box.BLACK; BR.Color = Box.WHITE;
						break;
					case (byte)156:
						TL.Color = Box.BLACK; TC.Color = Box.BLACK; TR.Color = Box.BLACK;
						ML.Color = Box.WHITE; MR.Color = Box.BLACK;
						BL.Color = Box.BLACK; BC.Color = Box.WHITE; BR.Color = Box.BLACK;
						break;
					case (byte)157:
						TL.Color = Box.WHITE; TC.Color = Box.BLACK; TR.Color = Box.BLACK;
						ML.Color = Box.BLACK; MR.Color = Box.BLACK;
						BL.Color = Box.WHITE; BC.Color = Box.BLACK; BR.Color = Box.BLACK;
						break;
					default:
						Console.WriteLine("Error: 数字マス[15]周りの黒マス配置中にエラー");
						Application.Exit();
						break;
				}
			}
			// [22] id=158~169
			else if (HEAD_BN_22 <= id && id < HEAD_BN_23) {
				switch (id) {
					case (byte)158:
						TL.Color = Box.BLACK; TC.Color = Box.BLACK; TR.Color = Box.WHITE;
						ML.Color = Box.WHITE; MR.Color = Box.BLACK;
						BL.Color = Box.WHITE; BC.Color = Box.WHITE; BR.Color = Box.BLACK;
						break;
					case (byte)159:
						TL.Color = Box.BLACK; TC.Color = Box.BLACK; TR.Color = Box.WHITE;
						ML.Color = Box.WHITE; MR.Color = Box.WHITE;
						BL.Color = Box.WHITE; BC.Color = Box.BLACK; BR.Color = Box.BLACK;
						break;
					case (byte)160:
						TL.Color = Box.BLACK; TC.Color = Box.BLACK; TR.Color = Box.WHITE;
						ML.Color = Box.WHITE; MR.Color = Box.WHITE;
						BL.Color = Box.BLACK; BC.Color = Box.BLACK; BR.Color = Box.WHITE;
						break;
					case (byte)161:
						TL.Color = Box.WHITE; TC.Color = Box.BLACK; TR.Color = Box.BLACK;
						ML.Color = Box.WHITE; MR.Color = Box.WHITE;
						BL.Color = Box.WHITE; BC.Color = Box.BLACK; BR.Color = Box.BLACK;
						break;
					case (byte)162:
						TL.Color = Box.WHITE; TC.Color = Box.BLACK; TR.Color = Box.BLACK;
						ML.Color = Box.WHITE; MR.Color = Box.WHITE;
						BL.Color = Box.BLACK; BC.Color = Box.BLACK; BR.Color = Box.WHITE;
						break;
					case (byte)163:
						TL.Color = Box.WHITE; TC.Color = Box.BLACK; TR.Color = Box.BLACK;
						ML.Color = Box.BLACK; MR.Color = Box.WHITE;
						BL.Color = Box.BLACK; BC.Color = Box.WHITE; BR.Color = Box.WHITE;
						break;
					case (byte)164:
						TL.Color = Box.WHITE; TC.Color = Box.WHITE; TR.Color = Box.BLACK;
						ML.Color = Box.WHITE; MR.Color = Box.BLACK;
						BL.Color = Box.BLACK; BC.Color = Box.BLACK; BR.Color = Box.WHITE;
						break;
					case (byte)165:
						TL.Color = Box.WHITE; TC.Color = Box.WHITE; TR.Color = Box.BLACK;
						ML.Color = Box.BLACK; MR.Color = Box.BLACK;
						BL.Color = Box.BLACK; BC.Color = Box.WHITE; BR.Color = Box.WHITE;
						break;
					case (byte)166:
						TL.Color = Box.BLACK; TC.Color = Box.WHITE; TR.Color = Box.BLACK;
						ML.Color = Box.BLACK; MR.Color = Box.BLACK;
						BL.Color = Box.WHITE; BC.Color = Box.WHITE; BR.Color = Box.WHITE;
						break;
					case (byte)167:
						TL.Color = Box.WHITE; TC.Color = Box.WHITE; TR.Color = Box.WHITE;
						ML.Color = Box.BLACK; MR.Color = Box.BLACK;
						BL.Color = Box.BLACK; BC.Color = Box.WHITE; BR.Color = Box.BLACK;
						break;
					case (byte)168:
						TL.Color = Box.BLACK; TC.Color = Box.WHITE; TR.Color = Box.WHITE;
						ML.Color = Box.BLACK; MR.Color = Box.BLACK;
						BL.Color = Box.WHITE; BC.Color = Box.WHITE; BR.Color = Box.BLACK;
						break;
					case (byte)169:
						TL.Color = Box.BLACK; TC.Color = Box.WHITE; TR.Color = Box.WHITE;
						ML.Color = Box.BLACK; MR.Color = Box.WHITE;
						BL.Color = Box.WHITE; BC.Color = Box.BLACK; BR.Color = Box.BLACK;
						break;
					default:
						Console.WriteLine("Error: 数字マス[22]周りの黒マス配置中にエラー");
						Application.Exit();
						break;
				}
			}
			// [23] id=170~185
			else if (HEAD_BN_23 <= id && id < HEAD_BN_24) {
				switch (id) {
					case (byte)170:
						TL.Color = Box.BLACK; TC.Color = Box.BLACK; TR.Color = Box.WHITE;
						ML.Color = Box.WHITE; MR.Color = Box.BLACK;
						BL.Color = Box.WHITE; BC.Color = Box.BLACK; BR.Color = Box.BLACK;
						break;
					case (byte)171:
						TL.Color = Box.BLACK; TC.Color = Box.BLACK; TR.Color = Box.WHITE;
						ML.Color = Box.WHITE; MR.Color = Box.WHITE;
						BL.Color = Box.BLACK; BC.Color = Box.BLACK; BR.Color = Box.BLACK;
						break;
					case (byte)172:
						TL.Color = Box.WHITE; TC.Color = Box.BLACK; TR.Color = Box.BLACK;
						ML.Color = Box.WHITE; MR.Color = Box.WHITE;
						BL.Color = Box.BLACK; BC.Color = Box.BLACK; BR.Color = Box.BLACK;
						break;
					case (byte)173:
						TL.Color = Box.WHITE; TC.Color = Box.BLACK; TR.Color = Box.BLACK;
						ML.Color = Box.BLACK; MR.Color = Box.WHITE;
						BL.Color = Box.BLACK; BC.Color = Box.BLACK; BR.Color = Box.WHITE;
						break;
					case (byte)174:
						TL.Color = Box.WHITE; TC.Color = Box.WHITE; TR.Color = Box.BLACK;
						ML.Color = Box.BLACK; MR.Color = Box.BLACK;
						BL.Color = Box.BLACK; BC.Color = Box.BLACK; BR.Color = Box.WHITE;
						break;
					case (byte)175:
						TL.Color = Box.BLACK; TC.Color = Box.WHITE; TR.Color = Box.BLACK;
						ML.Color = Box.BLACK; MR.Color = Box.BLACK;
						BL.Color = Box.BLACK; BC.Color = Box.WHITE; BR.Color = Box.WHITE;
						break;
					case (byte)176:
						TL.Color = Box.BLACK; TC.Color = Box.WHITE; TR.Color = Box.WHITE;
						ML.Color = Box.BLACK; MR.Color = Box.BLACK;
						BL.Color = Box.BLACK; BC.Color = Box.WHITE; BR.Color = Box.BLACK;
						break;
					case (byte)177:
						TL.Color = Box.BLACK; TC.Color = Box.BLACK; TR.Color = Box.WHITE;
						ML.Color = Box.BLACK; MR.Color = Box.BLACK;
						BL.Color = Box.WHITE; BC.Color = Box.WHITE; BR.Color = Box.BLACK;
						break;
					case (byte)178:
						TL.Color = Box.BLACK; TC.Color = Box.BLACK; TR.Color = Box.WHITE;
						ML.Color = Box.BLACK; MR.Color = Box.WHITE;
						BL.Color = Box.WHITE; BC.Color = Box.BLACK; BR.Color = Box.BLACK;
						break;
					case (byte)179:
						TL.Color = Box.BLACK; TC.Color = Box.BLACK; TR.Color = Box.BLACK;
						ML.Color = Box.WHITE; MR.Color = Box.WHITE;
						BL.Color = Box.WHITE; BC.Color = Box.BLACK; BR.Color = Box.BLACK;
						break;
					case (byte)180:
						TL.Color = Box.BLACK; TC.Color = Box.BLACK; TR.Color = Box.BLACK;
						ML.Color = Box.WHITE; MR.Color = Box.WHITE;
						BL.Color = Box.BLACK; BC.Color = Box.BLACK; BR.Color = Box.WHITE;
						break;
					case (byte)181:
						TL.Color = Box.WHITE; TC.Color = Box.BLACK; TR.Color = Box.BLACK;
						ML.Color = Box.WHITE; MR.Color = Box.BLACK;
						BL.Color = Box.BLACK; BC.Color = Box.BLACK; BR.Color = Box.WHITE;
						break;
					case (byte)182:
						TL.Color = Box.WHITE; TC.Color = Box.BLACK; TR.Color = Box.BLACK;
						ML.Color = Box.BLACK; MR.Color = Box.BLACK;
						BL.Color = Box.BLACK; BC.Color = Box.WHITE; BR.Color = Box.WHITE;
						break;
					case (byte)183:
						TL.Color = Box.WHITE; TC.Color = Box.WHITE; TR.Color = Box.BLACK;
						ML.Color = Box.BLACK; MR.Color = Box.BLACK;
						BL.Color = Box.BLACK; BC.Color = Box.WHITE; BR.Color = Box.BLACK;
						break;
					case (byte)184:
						TL.Color = Box.BLACK; TC.Color = Box.WHITE; TR.Color = Box.BLACK;
						ML.Color = Box.BLACK; MR.Color = Box.BLACK;
						BL.Color = Box.WHITE; BC.Color = Box.WHITE; BR.Color = Box.BLACK;
						break;
					case (byte)185:
						TL.Color = Box.BLACK; TC.Color = Box.WHITE; TR.Color = Box.WHITE;
						ML.Color = Box.BLACK; MR.Color = Box.BLACK;
						BL.Color = Box.WHITE; BC.Color = Box.BLACK; BR.Color = Box.BLACK;
						break;
					default:
						Console.WriteLine("Error: 数字マス[23]周りの黒マス配置中にエラー");
						Application.Exit();
						break;
				}
			}
			// [24] id=186~193
			else if (HEAD_BN_24 <= id && id < HEAD_BN_33) {
				switch (id) {
					case (byte)186:
						TL.Color = Box.BLACK; TC.Color = Box.BLACK; TR.Color = Box.WHITE;
						ML.Color = Box.WHITE; MR.Color = Box.BLACK;
						BL.Color = Box.BLACK; BC.Color = Box.BLACK; BR.Color = Box.BLACK;
						break;
					case (byte)187:
						TL.Color = Box.WHITE; TC.Color = Box.BLACK; TR.Color = Box.BLACK;
						ML.Color = Box.BLACK; MR.Color = Box.WHITE;
						BL.Color = Box.BLACK; BC.Color = Box.BLACK; BR.Color = Box.BLACK;
						break;
					case (byte)188:
						TL.Color = Box.BLACK; TC.Color = Box.WHITE; TR.Color = Box.BLACK;
						ML.Color = Box.BLACK; MR.Color = Box.BLACK;
						BL.Color = Box.BLACK; BC.Color = Box.BLACK; BR.Color = Box.WHITE;
						break;
					case (byte)189:
						TL.Color = Box.BLACK; TC.Color = Box.BLACK; TR.Color = Box.WHITE;
						ML.Color = Box.BLACK; MR.Color = Box.BLACK;
						BL.Color = Box.BLACK; BC.Color = Box.WHITE; BR.Color = Box.BLACK;
						break;
					case (byte)190:
						TL.Color = Box.BLACK; TC.Color = Box.BLACK; TR.Color = Box.BLACK;
						ML.Color = Box.BLACK; MR.Color = Box.WHITE;
						BL.Color = Box.WHITE; BC.Color = Box.BLACK; BR.Color = Box.BLACK;
						break;
					case (byte)191:
						TL.Color = Box.BLACK; TC.Color = Box.BLACK; TR.Color = Box.BLACK;
						ML.Color = Box.WHITE; MR.Color = Box.BLACK;
						BL.Color = Box.BLACK; BC.Color = Box.BLACK; BR.Color = Box.WHITE;
						break;
					case (byte)192:
						TL.Color = Box.WHITE; TC.Color = Box.BLACK; TR.Color = Box.BLACK;
						ML.Color = Box.BLACK; MR.Color = Box.BLACK;
						BL.Color = Box.BLACK; BC.Color = Box.WHITE; BR.Color = Box.BLACK;
						break;
					case (byte)193:
						TL.Color = Box.BLACK; TC.Color = Box.WHITE; TR.Color = Box.BLACK;
						ML.Color = Box.BLACK; MR.Color = Box.BLACK;
						BL.Color = Box.WHITE; BC.Color = Box.BLACK; BR.Color = Box.BLACK;
						break;
					default:
						Console.WriteLine("Error: 数字マス[24]周りの黒マス配置中にエラー");
						Application.Exit();
						break;
				}
			}
			// [33] id=194~197
			else if (HEAD_BN_33 <= id && id < HEAD_BN_111) {
				switch (id) {
					case (byte)194:
						TL.Color = Box.BLACK; TC.Color = Box.BLACK; TR.Color = Box.BLACK;
						ML.Color = Box.WHITE; MR.Color = Box.WHITE;
						BL.Color = Box.BLACK; BC.Color = Box.BLACK; BR.Color = Box.BLACK;
						break;
					case (byte)195:
						TL.Color = Box.WHITE; TC.Color = Box.BLACK; TR.Color = Box.BLACK;
						ML.Color = Box.BLACK; MR.Color = Box.BLACK;
						BL.Color = Box.BLACK; BC.Color = Box.BLACK; BR.Color = Box.WHITE;
						break;
					case (byte)196:
						TL.Color = Box.BLACK; TC.Color = Box.WHITE; TR.Color = Box.BLACK;
						ML.Color = Box.BLACK; MR.Color = Box.BLACK;
						BL.Color = Box.BLACK; BC.Color = Box.WHITE; BR.Color = Box.BLACK;
						break;
					case (byte)197:
						TL.Color = Box.BLACK; TC.Color = Box.BLACK; TR.Color = Box.WHITE;
						ML.Color = Box.BLACK; MR.Color = Box.BLACK;
						BL.Color = Box.WHITE; BC.Color = Box.BLACK; BR.Color = Box.BLACK;
						break;
					default:
						Console.WriteLine("Error: 数字マス[33]周りの黒マス配置中にエラー");
						Application.Exit();
						break;
				}
			}
			// [111] id=198~213			
			else if (HEAD_BN_111 <= id && id < HEAD_BN_112) {
				switch (id) {
					case (byte)198:
						TL.Color = Box.BLACK; TC.Color = Box.WHITE; TR.Color = Box.BLACK;
						ML.Color = Box.WHITE; MR.Color = Box.WHITE;
						BL.Color = Box.WHITE; BC.Color = Box.WHITE; BR.Color = Box.BLACK;
						break;
					case (byte)199:
						TL.Color = Box.BLACK; TC.Color = Box.WHITE; TR.Color = Box.BLACK;
						ML.Color = Box.WHITE; MR.Color = Box.WHITE;
						BL.Color = Box.WHITE; BC.Color = Box.BLACK; BR.Color = Box.WHITE;
						break;
					case (byte)200:
						TL.Color = Box.BLACK; TC.Color = Box.WHITE; TR.Color = Box.BLACK;
						ML.Color = Box.WHITE; MR.Color = Box.WHITE;
						BL.Color = Box.BLACK; BC.Color = Box.WHITE; BR.Color = Box.WHITE;
						break;
					case (byte)201:
						TL.Color = Box.BLACK; TC.Color = Box.WHITE; TR.Color = Box.WHITE;
						ML.Color = Box.WHITE; MR.Color = Box.BLACK;
						BL.Color = Box.WHITE; BC.Color = Box.BLACK; BR.Color = Box.WHITE;
						break;
					case (byte)202:
						TL.Color = Box.BLACK; TC.Color = Box.WHITE; TR.Color = Box.WHITE;
						ML.Color = Box.WHITE; MR.Color = Box.BLACK;
						BL.Color = Box.BLACK; BC.Color = Box.WHITE; BR.Color = Box.WHITE;
						break;
					case (byte)203:
						TL.Color = Box.BLACK; TC.Color = Box.WHITE; TR.Color = Box.WHITE;
						ML.Color = Box.WHITE; MR.Color = Box.WHITE;
						BL.Color = Box.BLACK; BC.Color = Box.WHITE; BR.Color = Box.BLACK;
						break;
					case (byte)204:
						TL.Color = Box.WHITE; TC.Color = Box.BLACK; TR.Color = Box.WHITE;
						ML.Color = Box.WHITE; MR.Color = Box.BLACK;
						BL.Color = Box.WHITE; BC.Color = Box.BLACK; BR.Color = Box.WHITE;
						break;
					case (byte)205:
						TL.Color = Box.WHITE; TC.Color = Box.BLACK; TR.Color = Box.WHITE;
						ML.Color = Box.WHITE; MR.Color = Box.BLACK;
						BL.Color = Box.BLACK; BC.Color = Box.WHITE; BR.Color = Box.WHITE;
						break;
					case (byte)206:
						TL.Color = Box.WHITE; TC.Color = Box.BLACK; TR.Color = Box.WHITE;
						ML.Color = Box.BLACK; MR.Color = Box.BLACK;
						BL.Color = Box.WHITE; BC.Color = Box.WHITE; BR.Color = Box.WHITE;
						break;
					case (byte)207:
						TL.Color = Box.WHITE; TC.Color = Box.BLACK; TR.Color = Box.WHITE;
						ML.Color = Box.WHITE; MR.Color = Box.WHITE;
						BL.Color = Box.BLACK; BC.Color = Box.WHITE; BR.Color = Box.BLACK;
						break;
					case (byte)208:
						TL.Color = Box.WHITE; TC.Color = Box.BLACK; TR.Color = Box.WHITE;
						ML.Color = Box.BLACK; MR.Color = Box.WHITE;
						BL.Color = Box.WHITE; BC.Color = Box.WHITE; BR.Color = Box.BLACK;
						break;
					case (byte)209:
						TL.Color = Box.WHITE; TC.Color = Box.BLACK; TR.Color = Box.WHITE;
						ML.Color = Box.BLACK; MR.Color = Box.WHITE;
						BL.Color = Box.WHITE; BC.Color = Box.BLACK; BR.Color = Box.WHITE;
						break;
					case (byte)210:
						TL.Color = Box.WHITE; TC.Color = Box.WHITE; TR.Color = Box.BLACK;
						ML.Color = Box.WHITE; MR.Color = Box.WHITE;
						BL.Color = Box.BLACK; BC.Color = Box.WHITE; BR.Color = Box.BLACK;
						break;
					case (byte)211:
						TL.Color = Box.WHITE; TC.Color = Box.WHITE; TR.Color = Box.BLACK;
						ML.Color = Box.BLACK; MR.Color = Box.WHITE;
						BL.Color = Box.WHITE; BC.Color = Box.WHITE; BR.Color = Box.BLACK;
						break;
					case (byte)212:
						TL.Color = Box.WHITE; TC.Color = Box.WHITE; TR.Color = Box.BLACK;
						ML.Color = Box.BLACK; MR.Color = Box.WHITE;
						BL.Color = Box.WHITE; BC.Color = Box.BLACK; BR.Color = Box.WHITE;
						break;
					case (byte)213:
						TL.Color = Box.WHITE; TC.Color = Box.WHITE; TR.Color = Box.WHITE;
						ML.Color = Box.BLACK; MR.Color = Box.BLACK;
						BL.Color = Box.WHITE; BC.Color = Box.BLACK; BR.Color = Box.WHITE;
						break;
					default:
						Console.WriteLine("Error: 数字マス[111]周りの黒マス配置中にエラー");
						Application.Exit();
						break;
				}
			}
			// [112] id=214~237
			else if (HEAD_BN_112 <= id && id < HEAD_BN_113) {
				switch (id) {
					case (byte)214:
						TL.Color = Box.BLACK; TC.Color = Box.BLACK; TR.Color = Box.WHITE;
						ML.Color = Box.WHITE;/*					  */MR.Color = Box.BLACK;
						BL.Color = Box.WHITE; BC.Color = Box.BLACK; BR.Color = Box.WHITE;
						break;
					case (byte)215:
						TL.Color = Box.BLACK; TC.Color = Box.BLACK; TR.Color = Box.WHITE;
						ML.Color = Box.WHITE;/*					  */MR.Color = Box.BLACK;
						BL.Color = Box.BLACK; BC.Color = Box.WHITE; BR.Color = Box.WHITE;
						break;
					case (byte)216:
						TL.Color = Box.BLACK; TC.Color = Box.BLACK; TR.Color = Box.WHITE;
						ML.Color = Box.WHITE;/*					  */MR.Color = Box.WHITE;
						BL.Color = Box.BLACK; BC.Color = Box.WHITE; BR.Color = Box.BLACK;
						break;
					case (byte)217:
						TL.Color = Box.WHITE; TC.Color = Box.BLACK; TR.Color = Box.BLACK;
						ML.Color = Box.WHITE;/*					  */MR.Color = Box.WHITE;
						BL.Color = Box.BLACK; BC.Color = Box.WHITE; BR.Color = Box.BLACK;
						break;
					case (byte)218:
						TL.Color = Box.WHITE; TC.Color = Box.BLACK; TR.Color = Box.BLACK;
						ML.Color = Box.BLACK;/*					  */MR.Color = Box.WHITE;
						BL.Color = Box.WHITE; BC.Color = Box.WHITE; BR.Color = Box.BLACK;
						break;
					case (byte)219:
						TL.Color = Box.WHITE; TC.Color = Box.BLACK; TR.Color = Box.BLACK;
						ML.Color = Box.BLACK;/*					  */MR.Color = Box.WHITE;
						BL.Color = Box.WHITE; BC.Color = Box.BLACK; BR.Color = Box.WHITE;
						break;
					case (byte)220:
						TL.Color = Box.WHITE; TC.Color = Box.WHITE; TR.Color = Box.BLACK;
						ML.Color = Box.BLACK;/*					  */MR.Color = Box.BLACK;
						BL.Color = Box.WHITE; BC.Color = Box.BLACK; BR.Color = Box.WHITE;
						break;
					case (byte)221:
						TL.Color = Box.BLACK; TC.Color = Box.WHITE; TR.Color = Box.BLACK;
						ML.Color = Box.WHITE;/*					  */MR.Color = Box.BLACK;
						BL.Color = Box.WHITE; BC.Color = Box.BLACK; BR.Color = Box.WHITE;
						break;
					case (byte)222:
						TL.Color = Box.BLACK; TC.Color = Box.WHITE; TR.Color = Box.BLACK;
						ML.Color = Box.WHITE;/*					  */MR.Color = Box.BLACK;
						BL.Color = Box.BLACK; BC.Color = Box.WHITE; BR.Color = Box.WHITE;
						break;
					case (byte)223:
						TL.Color = Box.BLACK; TC.Color = Box.WHITE; TR.Color = Box.WHITE;
						ML.Color = Box.WHITE;/*					  */MR.Color = Box.BLACK;
						BL.Color = Box.BLACK; BC.Color = Box.WHITE; BR.Color = Box.BLACK;
						break;
					case (byte)224:
						TL.Color = Box.WHITE; TC.Color = Box.BLACK; TR.Color = Box.WHITE;
						ML.Color = Box.WHITE;/*					  */MR.Color = Box.BLACK;
						BL.Color = Box.BLACK; BC.Color = Box.WHITE; BR.Color = Box.BLACK;
						break;
					case (byte)225:
						TL.Color = Box.WHITE; TC.Color = Box.BLACK; TR.Color = Box.WHITE;
						ML.Color = Box.BLACK;/*					  */MR.Color = Box.BLACK;
						BL.Color = Box.WHITE; BC.Color = Box.WHITE; BR.Color = Box.BLACK;
						break;
					case (byte)226:
						TL.Color = Box.WHITE; TC.Color = Box.BLACK; TR.Color = Box.WHITE;
						ML.Color = Box.BLACK;/*					  */MR.Color = Box.WHITE;
						BL.Color = Box.WHITE; BC.Color = Box.BLACK; BR.Color = Box.BLACK;
						break;
					case (byte)227:
						TL.Color = Box.WHITE; TC.Color = Box.WHITE; TR.Color = Box.BLACK;
						ML.Color = Box.BLACK;/*					  */MR.Color = Box.WHITE;
						BL.Color = Box.WHITE; BC.Color = Box.BLACK; BR.Color = Box.BLACK;
						break;
					case (byte)228:
						TL.Color = Box.BLACK; TC.Color = Box.WHITE; TR.Color = Box.BLACK;
						ML.Color = Box.WHITE;/*					  */MR.Color = Box.WHITE;
						BL.Color = Box.WHITE; BC.Color = Box.BLACK; BR.Color = Box.BLACK;
						break;
					case (byte)229:
						TL.Color = Box.BLACK; TC.Color = Box.WHITE; TR.Color = Box.BLACK;
						ML.Color = Box.WHITE;/*					  */MR.Color = Box.WHITE;
						BL.Color = Box.BLACK; BC.Color = Box.BLACK; BR.Color = Box.WHITE;
						break;
					case (byte)230:
						TL.Color = Box.BLACK; TC.Color = Box.WHITE; TR.Color = Box.WHITE;
						ML.Color = Box.WHITE;/*					  */MR.Color = Box.BLACK;
						BL.Color = Box.BLACK; BC.Color = Box.BLACK; BR.Color = Box.WHITE;
						break;
					case (byte)231:
						TL.Color = Box.WHITE; TC.Color = Box.BLACK; TR.Color = Box.WHITE;
						ML.Color = Box.WHITE;/*					  */MR.Color = Box.BLACK;
						BL.Color = Box.BLACK; BC.Color = Box.BLACK; BR.Color = Box.WHITE;
						break;
					case (byte)232:
						TL.Color = Box.WHITE; TC.Color = Box.BLACK; TR.Color = Box.WHITE;
						ML.Color = Box.BLACK;/*					  */MR.Color = Box.BLACK;
						BL.Color = Box.BLACK; BC.Color = Box.WHITE; BR.Color = Box.WHITE;
						break;
					case (byte)233:
						TL.Color = Box.WHITE; TC.Color = Box.BLACK; TR.Color = Box.WHITE;
						ML.Color = Box.BLACK;/*					  */MR.Color = Box.WHITE;
						BL.Color = Box.BLACK; BC.Color = Box.WHITE; BR.Color = Box.BLACK;
						break;
					case (byte)234:
						TL.Color = Box.WHITE; TC.Color = Box.WHITE; TR.Color = Box.BLACK;
						ML.Color = Box.BLACK;/*					  */MR.Color = Box.WHITE;
						BL.Color = Box.BLACK; BC.Color = Box.WHITE; BR.Color = Box.BLACK;
						break;
					case (byte)235:
						TL.Color = Box.BLACK; TC.Color = Box.WHITE; TR.Color = Box.BLACK;
						ML.Color = Box.BLACK;/*					  */MR.Color = Box.WHITE;
						BL.Color = Box.WHITE; BC.Color = Box.WHITE; BR.Color = Box.BLACK;
						break;
					case (byte)236:
						TL.Color = Box.BLACK; TC.Color = Box.WHITE; TR.Color = Box.BLACK;
						ML.Color = Box.BLACK;/*					  */MR.Color = Box.WHITE;
						BL.Color = Box.WHITE; BC.Color = Box.BLACK; BR.Color = Box.WHITE;
						break;
					case (byte)237:
						TL.Color = Box.BLACK; TC.Color = Box.WHITE; TR.Color = Box.WHITE;
						ML.Color = Box.BLACK;/*					  */MR.Color = Box.BLACK;
						BL.Color = Box.WHITE; BC.Color = Box.BLACK; BR.Color = Box.WHITE;
						break;
					default:
						Console.WriteLine("Error: 数字マス[112]周りの黒マス配置中にエラー");
						Application.Exit();
						break;
				}
			}
			// [113] id=238~245
			else if (HEAD_BN_113 <= id && id < HEAD_BN_122) {
				switch (id) {
					case (byte)238:
						TL.Color = Box.BLACK; TC.Color = Box.BLACK; TR.Color = Box.BLACK;
						ML.Color = Box.WHITE;/*					  */MR.Color = Box.WHITE;
						BL.Color = Box.BLACK; BC.Color = Box.WHITE; BR.Color = Box.BLACK;
						break;
					case (byte)239:
						TL.Color = Box.WHITE; TC.Color = Box.BLACK; TR.Color = Box.BLACK;
						ML.Color = Box.BLACK;/*					  */MR.Color = Box.BLACK;
						BL.Color = Box.WHITE; BC.Color = Box.BLACK; BR.Color = Box.WHITE;
						break;
					case (byte)240:
						TL.Color = Box.BLACK; TC.Color = Box.WHITE; TR.Color = Box.BLACK;
						ML.Color = Box.WHITE;/*					  */MR.Color = Box.BLACK;
						BL.Color = Box.BLACK; BC.Color = Box.WHITE; BR.Color = Box.BLACK;
						break;
					case (byte)241:
						TL.Color = Box.WHITE; TC.Color = Box.BLACK; TR.Color = Box.WHITE;
						ML.Color = Box.BLACK;/*					  */MR.Color = Box.BLACK;
						BL.Color = Box.WHITE; BC.Color = Box.BLACK; BR.Color = Box.BLACK;
						break;
					case (byte)242:
						TL.Color = Box.BLACK; TC.Color = Box.WHITE; TR.Color = Box.BLACK;
						ML.Color = Box.WHITE;/*					  */MR.Color = Box.WHITE;
						BL.Color = Box.BLACK; BC.Color = Box.BLACK; BR.Color = Box.BLACK;
						break;
					case (byte)243:
						TL.Color = Box.WHITE; TC.Color = Box.BLACK; TR.Color = Box.WHITE;
						ML.Color = Box.BLACK;/*					  */MR.Color = Box.BLACK;
						BL.Color = Box.BLACK; BC.Color = Box.BLACK; BR.Color = Box.WHITE;
						break;
					case (byte)244:
						TL.Color = Box.BLACK; TC.Color = Box.WHITE; TR.Color = Box.BLACK;
						ML.Color = Box.BLACK;/*					  */MR.Color = Box.WHITE;
						BL.Color = Box.BLACK; BC.Color = Box.WHITE; BR.Color = Box.BLACK;
						break;
					case (byte)245:
						TL.Color = Box.BLACK; TC.Color = Box.BLACK; TR.Color = Box.WHITE;
						ML.Color = Box.BLACK;/*					  */MR.Color = Box.BLACK;
						BL.Color = Box.WHITE; BC.Color = Box.BLACK; BR.Color = Box.WHITE;
						break;
					default:
						Console.WriteLine("Error: 数字マス[113]周りの黒マス配置中にエラー");
						Application.Exit();
						break;
				}
			}
			// [122] id=246~253
			else if (HEAD_BN_122 <= id && id < HEAD_BN_1111) {
				switch (id) {
					case (byte)246:
						TL.Color = Box.BLACK; TC.Color = Box.WHITE; TR.Color = Box.BLACK;
						ML.Color = Box.WHITE;/*					  */MR.Color = Box.BLACK;
						BL.Color = Box.BLACK; BC.Color = Box.BLACK; BR.Color = Box.WHITE;
						break;
					case (byte)247:
						TL.Color = Box.WHITE; TC.Color = Box.BLACK; TR.Color = Box.WHITE;
						ML.Color = Box.BLACK;/*					  */MR.Color = Box.BLACK;
						BL.Color = Box.BLACK; BC.Color = Box.WHITE; BR.Color = Box.BLACK;
						break;
					case (byte)248:
						TL.Color = Box.BLACK; TC.Color = Box.WHITE; TR.Color = Box.BLACK;
						ML.Color = Box.BLACK;/*					  */MR.Color = Box.WHITE;
						BL.Color = Box.WHITE; BC.Color = Box.BLACK; BR.Color = Box.BLACK;
						break;
					case (byte)249:
						TL.Color = Box.BLACK; TC.Color = Box.BLACK; TR.Color = Box.WHITE;
						ML.Color = Box.WHITE;/*					  */MR.Color = Box.BLACK;
						BL.Color = Box.BLACK; BC.Color = Box.BLACK; BR.Color = Box.WHITE;
						break;
					case (byte)250:
						TL.Color = Box.WHITE; TC.Color = Box.BLACK; TR.Color = Box.BLACK;
						ML.Color = Box.BLACK;/*					  */MR.Color = Box.WHITE;
						BL.Color = Box.BLACK; BC.Color = Box.WHITE; BR.Color = Box.BLACK;
						break;
					case (byte)251:
						TL.Color = Box.BLACK; TC.Color = Box.WHITE; TR.Color = Box.BLACK;
						ML.Color = Box.BLACK;/*					  */MR.Color = Box.BLACK;
						BL.Color = Box.WHITE; BC.Color = Box.BLACK; BR.Color = Box.WHITE;
						break;
					case (byte)252:
						TL.Color = Box.BLACK; TC.Color = Box.BLACK; TR.Color = Box.WHITE;
						ML.Color = Box.WHITE;/*					  */MR.Color = Box.BLACK;
						BL.Color = Box.BLACK; BC.Color = Box.WHITE; BR.Color = Box.BLACK;
						break;
					case (byte)253:
						TL.Color = Box.WHITE; TC.Color = Box.BLACK; TR.Color = Box.BLACK;
						ML.Color = Box.BLACK;/*					  */MR.Color = Box.WHITE;
						BL.Color = Box.WHITE; BC.Color = Box.BLACK; BR.Color = Box.BLACK;
						break;
					default:
						Console.WriteLine("Error: 数字マス[122]周りの黒マス配置中にエラー");
						Application.Exit();
						break;
				}
			}
			// [1111] id=254~255
			else if (HEAD_BN_1111 <= id && id <= MAX_ID) {
				switch (id) {
					case (byte)254:
						TL.Color = Box.BLACK; TC.Color = Box.WHITE; TR.Color = Box.BLACK;
						ML.Color = Box.WHITE;/*					  */MR.Color = Box.WHITE;
						BL.Color = Box.BLACK; BC.Color = Box.WHITE; BR.Color = Box.BLACK;
						break;
					case (byte)255:
						TL.Color = Box.WHITE; TC.Color = Box.BLACK; TR.Color = Box.WHITE;
						ML.Color = Box.BLACK;/*					  */MR.Color = Box.BLACK;
						BL.Color = Box.WHITE; BC.Color = Box.BLACK; BR.Color = Box.WHITE;
						break;
					default:
						Console.WriteLine("Error: 数字マス[1111]周りの黒マス配置中にエラー");
						Application.Exit();
						break;
				}
			}
			else {
				Console.WriteLine("Error: idから数字マス周りの黒マスを配置中にエラー");
				Application.Exit();
			}
		}

		/*********************************
		 * 
		 * color_board内でcenterを中心とした3*3マスの座標の内、黒マスの団子があるか調べる。
		 * true		: 黒マスの団子がない
		 * false	: 黒マスの団子がある
		 * 
		 * 引数
		 * center		: 3*3の中心座標
		 * color_board	: color情報を持った配列
		 * 
		 * *******************************/
		static private bool checkNotDumpling(Coordinates center, int[,] color_board)
		{
			// 3*3の中心が白なら黒マスの団子はない
			if (color_board[center.x, center.y] == Box.WHITE) { return true; }

			int TL = color_board[center.x - 1, center.y - 1];	// (左上)	Top-Left
			int TC = color_board[center.x - 1, center.y];		// (上)		Top-Center
			int TR = color_board[center.x - 1, center.y + 1];	// (右上)	Top-Right
			int MR = color_board[center.x, center.y + 1];		// (右)		Middle-Right
			int BR = color_board[center.x + 1, center.y + 1];	// (右下)	Bottom-Right
			int BC = color_board[center.x + 1, center.y];		// (下)		Bottom-Center
			int BL = color_board[center.x + 1, center.y - 1];	// (左下)	Bottom-Left
			int ML = color_board[center.x, center.y - 1];		// (左)		Middle-Left

			if (TC == Box.BLACK) {	// 上マスが黒色の場合
				if ((ML == Box.BLACK && TL == Box.BLACK)		// 左上
					|| (TR == Box.BLACK && MR == Box.BLACK)) {	// 右上
					return false;
				}
			}
			else if (BC == Box.BLACK) {	// 下マスが黒色の場合
				if ((MR == Box.BLACK && BR == Box.BLACK)		// 左下
					|| (BL == Box.BLACK && ML == Box.BLACK)) {	// 右下
					return false;
				}
			}
			return true;
		}

		/*********************************
		 * 
		 * idの通り設置したら黒マスの団子ができないか調べる。
		 * true		: 団子ができない
		 * false	: 団子ができる
		 * 
		 * 引数
		 * co		: (数字)マスの座標
		 * id		: 各パターンを識別するための数値（型はbyte）
		 *  
		 * 
		 * 【無駄】同じ数字マスに対してidの数分、周囲の同じマスを作り直してる
		 * *******************************/
		static private bool checkNotDumplingId(Coordinates co, byte id)
		{
			int max_box_col = Tapa.box[0].Count - 1;	// 列の添字の最大値
			int max_box_row = Tapa.box.Count;			// 行の添字の最大値
			const int MAX_CLONE_ROW = 5;
			const int MAX_CLONE_COL = 5;
			Coordinates center = new Coordinates(2, 2);

			int[,] clone_color_55 = new int[MAX_CLONE_ROW, MAX_CLONE_COL];	// 数字マス周りの5*5マスのcolorを格納する配列

			// 数字マス周り5*5マスの色を取得
			for (int i_box = co.x - 2, i_color = 0; i_color < MAX_CLONE_ROW; i_box++, i_color++) {
				for (int j_box = co.y - 2, j_color = 0; j_color < MAX_CLONE_COL; j_box++, j_color++) {
					// 外周かそれより外側の要素番号の場合、白色とする。
					if (i_box <= 0 || max_box_row <= i_box || j_box <= 0 || max_box_col <= j_box) {
						clone_color_55[i_color, j_color] = Box.WHITE;
					}
					else {
						clone_color_55[i_color, j_color] = Tapa.box[i_box][j_box].Color;
					}
				}
			}

			// 数字マス周りのクローンを作成
			List<Box> clonebox_around_numbox_list = new List<Box> {	
					new Box(Tapa.box[co.x-1][co.y-1]),	// 左上
					new Box(Tapa.box[co.x-1][co.y]),	// 上
					new Box(Tapa.box[co.x-1][co.y+1]),	// 右上
					new Box(Tapa.box[co.x][co.y-1]),	// 左
					new Box(Tapa.box[co.x][co.y+1]),	// 右
					new Box(Tapa.box[co.x+1][co.y-1]),	// 左下
					new Box(Tapa.box[co.x+1][co.y]),	// 下
					new Box(Tapa.box[co.x+1][co.y+1])	// 右下
				};

			// ######## <begin> クローン処理中はリスト関係の処理をしない
			Box.during_clone = true;
			// クローンに色を塗る
			PatternAroundNumBox.setPatternAroundNumBox(co, id, clonebox_around_numbox_list);
			Box.during_clone = false;
			// ######## <end> クローン処理中はリスト関係の処理をしない

			// 数字マス周りにある黒色のマスを格納
			List<Coordinates> blackbox_coord_list = new List<Coordinates>();
			// 数字マス周り8マスをidで埋めた場合の色で塗る。
			int ite_clonebox_list = 0;
			for (int i = center.x - 1; i <= center.x + 1; i++) {
				for (int j = center.y - 1; j <= center.y + 1; j++) {
					if (i == center.x && j == center.y) { continue; } // clonebox_around_numbox_listに中心の数字マスが含まれないため
					// idで塗られた色が黒だった場合リストに追加
					else if ((clone_color_55[i, j] = clonebox_around_numbox_list[ite_clonebox_list++].Color) == Box.BLACK) {
						blackbox_coord_list.Add(new Coordinates(i, j));
					}
				}
			}
			// 数字マス周りの黒マス周りで団子ができないか調べる。
			foreach (Coordinates tmp_co in blackbox_coord_list) {
				if (!checkNotDumpling(tmp_co, clone_color_55)) {
					return false;
				}
			}
			return true;
		}

		/*********************************
		 * 
		 * 数字マスリストの各数字マスにidをセットする。
		 *   
		 * *******************************/
		static public void preparePatternArroundNumBox()
		{
			foreach (Coordinates tmp_co in Tapa.numbox_coord_list) {
				Tapa.box[tmp_co.x][tmp_co.y].id_list
					= new List<byte>(PatternAroundNumBox.getPatternAroundNumBoxList(
						Tapa.box[tmp_co.x][tmp_co.y].box_num));
			}
		}

		/*********************************
		 * 
		 * 数字マスのid_listを見て、数字マス周りで色が確定するマスを埋める。
		 * 引数
		 * co	: 数字マスの座標
		 *   
		 * *******************************/
		static private void setConfirmBoxArroundNumBox(Coordinates co)
		{
			// 数字マス周りのクローンを作成
			List<Box> clonebox_around_numbox_list = new List<Box> {	
					new Box(Tapa.box[co.x-1][co.y-1]),	// 左上
					new Box(Tapa.box[co.x-1][co.y]),	// 上
					new Box(Tapa.box[co.x-1][co.y+1]),	// 右上
					new Box(Tapa.box[co.x][co.y-1]),	// 左
					new Box(Tapa.box[co.x][co.y+1]),	// 右
					new Box(Tapa.box[co.x+1][co.y-1]),	// 左下					
					new Box(Tapa.box[co.x+1][co.y]),	// 下
					new Box(Tapa.box[co.x+1][co.y+1])	// 右下				
				};

			// クローンのマス色変更回数を0にする。
			//foreach (Box tmp_box in clonebox_arround_numbox_list) {
			//	tmp_box.changed_count_in_search_confirm_box = 0;
			//	//Console.Write("({0},{1})\n", tmp_box.coord.x, tmp_box.coord.y);
			//	//tmp_box.printBoxNum();
			//	//Console.Write("\n");
			//}

			// ######## <begin> クローン処理中はリスト関係の処理をしない
			Box.during_clone = true;
			// クローンのマス色が何回変化するか調べる。
			foreach (byte tmp_id in Tapa.box[co.x][co.y].id_list) {	// id_list(配置可能なパターン)
				PatternAroundNumBox.setPatternAroundNumBox(co, tmp_id, clonebox_around_numbox_list);
			}
			Box.during_clone = false;
			// ######## <end> クローン処理中はリスト関係の処理をしない

			// クローンのマス色変更回数が1回なら、そのマスをその色で埋める。
			foreach (Box tmp_box in clonebox_around_numbox_list) {
				if (tmp_box.changed_count_in_search_confirm_box == 1) {
					Tapa.box[tmp_box.coord.x][tmp_box.coord.y].Color = tmp_box.Color;
				}
			}
		}

		/*********************************
		 * 
		 * 1. 呼ばれた時の状態を保存する。
		 * 2. co座標のid_listからidを取り出し、配置する。
		 * 3. 黒マスの孤立を調べ、孤立していればidを除外する。
		 * 4. 状態を元に戻す。
		 * 5. 2-4をid_list内のid全てに対して行う。
		 * 
		 * 引数
		 * co		: 数字マスの座標
		 * id_list	: idのリスト
		 *   
		 * *******************************/
		static private void excludeIdToMakeIsolationBlackBoxGroup(Coordinates co, List<byte> id_list)
		{
			StateSave save_point = new StateSave();
			// 現在の状態を保存
			StateSave.saveNowState(save_point);

			//Console.Write("数字座標 >> ");
			//co.printCoordinates();
			//Console.WriteLine("############################");
			//Console.WriteLine("調査開始時の黒マス群リスト >> ");
			//Tapa.printIsolationBlackBoxGroup();
			//Console.WriteLine();
			//Console.WriteLine("調査開始時の伸び代リスト >> ");
			//Tapa.printCoordList(Tapa.edge_blackbox_coord_list);
			//Console.WriteLine();
			//Console.Write("残りid >> ");
			//Tapa.box[co.x][co.y].printIdList();
			//Console.WriteLine();
			//Tapa.printBoard();
			//Console.WriteLine();

			// 孤立するidのid_listでの要素番号を保存するリスト
			List<int> iso_id_ite_list = new List<int>();
			for (int i = id_list.Count - 1; i >= 0; i--) {
				PatternAroundNumBox.setPatternAroundNumBox(co, id_list[i]);

				//Console.WriteLine("臨時id設置後の黒マス群リスト >> ");
				//Tapa.printIsolationBlackBoxGroup();
				//Console.WriteLine();
				//Console.WriteLine("調査対象id >> " + id_list[i] + "\n");
				//Console.WriteLine();
				//Tapa.printBoard();
				//Console.WriteLine();

				// Tapa.printIsolationBlackBoxGroup();
				// Console.WriteLine();
				// Tapa.printBoard();
				if (!Box.checkNotIsolationBlackBoxGroup()) {	// 盤面に孤立した黒マス群がないか調べる

					//Console.WriteLine("除外するid >> " + id_list[i]);
					//Console.WriteLine();

					iso_id_ite_list.Add(i);
				}
				StateSave.setSavedState(save_point);

				//Console.WriteLine("元の黒マス群リストになっててほしい >> ");
				//Tapa.printIsolationBlackBoxGroup();
				//Console.WriteLine();
				//Console.WriteLine("（idチェック後）元の伸び代リストになっててほしい >> ");
				//Tapa.printCoordList(Tapa.edge_blackbox_coord_list);
				//Console.WriteLine();
				//Tapa.printBoard();
				//Console.WriteLine();
			}

			// 孤立したidをid_listから削除
			foreach (int tmp_ite in iso_id_ite_list) {
				// id_list.RemoveAt(tmp_ite);
				Tapa.box[co.x][co.y].id_list.RemoveAt(tmp_ite);
			}
			//Console.Write("残りid >> ");
			//Tapa.box[co.x][co.y].printIdList();
			//Console.WriteLine();
			//Console.WriteLine("############################\n");
		}

		/*********************************
		 * 
		 * 1. 呼ばれた時の状態を保存する。
		 * 2. co座標のid_listからidを取り出し、配置する。
		 * 3. 2.の状態で他の数字マスのidのチェック→除外を行う。
		 * 4. どれか１つでも数字マスのid_listの大きさが0になった場合、
		 *	　そのidのid_listでの添字を記録する。
		 * 5. id_listの次のidに対して、2-5を繰り返す。
		 * 6. id_listのid全ての調査が終わったら、4.で記録したidをid_listから除外する。
		 * 
		 * 引数
		 * co		: 数字マスの座標
		 * id_list	: idのリスト
		 *   
		 * *******************************/
		static private void excludeIdToKillOtherNameBoxAllId(Coordinates co, List<byte> id_list)
		{
			StateSave save_point = new StateSave();
			// 現在の状態を保存
			StateSave.saveNowState(save_point);

			// 除外するidのid_listでの要素番号を保存するリスト
			List<int> kill_id_ite_list = new List<int>();
			for (int i = id_list.Count - 1; i >= 0; i--) {
				// idの試し塗り
				PatternAroundNumBox.setPatternAroundNumBox(co, id_list[i]);
				// 数字マスのリストから今回試し塗りしたidの数字マスを除外
				Tapa.numbox_coord_list.Remove(co);

				for (int ite_coord = Tapa.numbox_coord_list.Count - 1; ite_coord >= 0; ite_coord--) {	// 数字マスのリスト
					Coordinates tmp_co = new Coordinates(Tapa.numbox_coord_list[ite_coord]);
					for (int ite_id = Tapa.box[tmp_co.x][tmp_co.y].id_list.Count - 1; ite_id >= 0; ite_id--) {	// id_list
						byte tmp_id = Tapa.box[tmp_co.x][tmp_co.y].id_list[ite_id];
						// 条件に一致したidをid_listから除外
						if (!PatternAroundNumBox.checkPatternAroundNumBox(tmp_co, tmp_id)	// idのパターンが配置できない
							|| !PatternAroundNumBox.checkNotDumplingId(tmp_co, tmp_id)) {	// またはidの通り配置したら黒マスの団子ができてしまう。
							Tapa.box[tmp_co.x][tmp_co.y].id_list.RemoveAt(ite_id);
						}
					}
					// id_listのうち、孤立する黒マス群を作るidを除外（id_listごとに処理したほうが効率的）
					excludeIdToMakeIsolationBlackBoxGroup(tmp_co, Tapa.box[tmp_co.x][tmp_co.y].id_list);

					// id_listの大きさが0なら今回試し塗りしたidの添字を、除外するid_listに追加し、次のidを見に行く。
					if (Tapa.box[tmp_co.x][tmp_co.y].id_list.Count == 0) {
						kill_id_ite_list.Add(i);
						break;
					}
				}
				// 保存した状態をロード
				StateSave.setSavedState(save_point);
			}

			// 除外対象のidをid_listから除外
			foreach (int tmp_ite in kill_id_ite_list) {
				// id_list.RemoveAt(tmp_ite);
				Tapa.box[co.x][co.y].id_list.RemoveAt(tmp_ite);
			}
		}

		/*********************************
		 * 
		 * 数字マス周りのパターンを管理
		 * 配置可能パターンが一意になった場合、その通りに配置する。
		 *   
		 * *******************************/
		static public void managePatternAroundNumBox()
		{
			for (int ite_coord = Tapa.numbox_coord_list.Count - 1; ite_coord >= 0; ite_coord--) {	// 数字マスのリスト
				Coordinates tmp_co = new Coordinates(Tapa.numbox_coord_list[ite_coord]);
				for (int ite_id = Tapa.box[tmp_co.x][tmp_co.y].id_list.Count - 1; ite_id >= 0; ite_id--) {	// id_list
					byte tmp_id = Tapa.box[tmp_co.x][tmp_co.y].id_list[ite_id];
					// 条件に一致したidをid_listから除外
					if (!PatternAroundNumBox.checkPatternAroundNumBox(tmp_co, tmp_id)	// idのパターンが配置できない
						|| !PatternAroundNumBox.checkNotDumplingId(tmp_co, tmp_id)) {	// またはidの通り配置したら黒マスの団子ができてしまう。
						if (Tapa.DEBUG) {
							tmp_co.printCoordinates();
							Console.Write(" " + Tapa.box[tmp_co.x][tmp_co.y].id_list[ite_id].ToString() + "\n");
						}
						Tapa.box[tmp_co.x][tmp_co.y].id_list.RemoveAt(ite_id);
					}
				}
				
				// id_listが一意ならそれを配置して数字マスリストから除外
				Tapa.NOW_STATE_PROCESS = Tapa.STATE_ID_LIST_ONLY_ONE;
				if (Tapa.box[tmp_co.x][tmp_co.y].id_list.Count == 1) {
					PatternAroundNumBox.setPatternAroundNumBox(tmp_co, Tapa.box[tmp_co.x][tmp_co.y].id_list[0]);
					Tapa.numbox_coord_list.RemoveAt(ite_coord);
				}
				// id_listの大きさが0ならエラー
				else if (Tapa.box[tmp_co.x][tmp_co.y].id_list.Count == 0) {
					Console.Write("Error: id_listの長さが0になってしまいました。");
					tmp_co.printCoordinates();
					Console.WriteLine();
					Application.Exit();
				}
				// tmp_coのid_listを見て数字周りで色が確定しているマスを埋める。
				Tapa.NOW_STATE_PROCESS = Tapa.STATE_CONFIRM_BOX_COLOR_FROM_ID_LIST;
				setConfirmBoxArroundNumBox(tmp_co);

				// id_listのうち、孤立する黒マス群を作るidを除外（id_listごとに処理したほうが効率的）
				excludeIdToMakeIsolationBlackBoxGroup(tmp_co, Tapa.box[tmp_co.x][tmp_co.y].id_list);
				// id_listのうち、idを配置して別の数字マスのid_listの大きさが0になるようなidを除外する。
				excludeIdToKillOtherNameBoxAllId(tmp_co, Tapa.box[tmp_co.x][tmp_co.y].id_list);
			}
		}
	}
}

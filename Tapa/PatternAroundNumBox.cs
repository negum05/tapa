﻿using System;
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
		static public List<byte> getPatternAroundNumBoxList(int box_num)
		{
			List<byte> byte_list = new List<byte>();
			byte head_id = 0;
			byte tail_id = 0;
			// idの振り分け
			if (box_num == id_num_dict[HEAD_BN_0]) { byte_list.Add((byte)HEAD_BN_0); }
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
				Console.WriteLine("Error: 数字マスのid振り分けでエラー");
				Application.Exit();
			}

			// idをリストにする
			for (byte i = head_id; i < tail_id; i++) {
				byte_list.Add(i);
			}
			return byte_list;
		}

		/*********************************
		 * 
		 * 座標とidから座標周りでidの配置方法が可能か判定する。
		 * 引数
		 * x        : 数字マスのx座標
		 * y        : 数字マスのy座標
		 * id       : 各パターンを識別するための数値(型はbyte)
		 *   
		 * *******************************/


		static public bool checkPatternAroundNumBox(int x, int y, byte id)
		{
			int TL = Tapa.box[x - 1][y - 1].color;    // 左上(Top-Left)
			int TC = Tapa.box[x][y - 1].color;      // 中上(Top-Center)
			int TR = Tapa.box[x - 1][y + 1].color;    // 右上(Top-Right)
			int ML = Tapa.box[x][y - 1].color;      // 左中(Middle-Left)
			int MR = Tapa.box[x][y + 1].color;      // 右中(Middle-Right)
			int BL = Tapa.box[x + 1][y - 1].color;    // 左下(Bottom-Left)
			int BC = Tapa.box[x + 1][y].color;      // 中下(Bottom-Center)
			int BR = Tapa.box[x - 1][y - 1].color;    // 右下(Bottom-Right)

			// [0] id=0
			if (id == HEAD_BN_0) {
				if (TL != Box.BLACK && TC != Box.BLACK && TR != Box.BLACK
					&& ML != Box.BLACK && MR != Box.BLACK
					&& BL != Box.BLACK && BC != Box.BLACK && BR != Box.BLACK) { return true; }
				else { return false; }
			}
			// [1] id=1~8
			else if (HEAD_BN_1 <= id && id < HEAD_BN_2) {
				switch (id) {
					case (byte)1:
						if (TL != Box.WHITE && TC != Box.BLACK && TR != Box.BLACK
							&& ML != Box.BLACK && MR != Box.BLACK
							&& BL != Box.BLACK && BC != Box.BLACK && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)2:
						if (TL != Box.BLACK && TC != Box.WHITE && TR != Box.BLACK
							&& ML != Box.BLACK && MR != Box.BLACK
							&& BL != Box.BLACK && BC != Box.BLACK && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)3:
						if (TL != Box.BLACK && TC != Box.BLACK && TR != Box.WHITE
							&& ML != Box.BLACK && MR != Box.BLACK
							&& BL != Box.BLACK && BC != Box.BLACK && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)4:
						if (TL != Box.BLACK && TC != Box.BLACK && TR != Box.BLACK
							&& ML != Box.BLACK && MR != Box.WHITE
							&& BL != Box.BLACK && BC != Box.BLACK && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)5:
						if (TL != Box.BLACK && TC != Box.BLACK && TR != Box.BLACK
							&& ML != Box.BLACK && MR != Box.BLACK
							&& BL != Box.BLACK && BC != Box.BLACK && BR != Box.WHITE) { return true; }
						else { return false; }
					case (byte)6:
						if (TL != Box.BLACK && TC != Box.BLACK && TR != Box.BLACK
							&& ML != Box.BLACK && MR != Box.BLACK
							&& BL != Box.BLACK && BC != Box.WHITE && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)7:
						if (TL != Box.BLACK && TC != Box.BLACK && TR != Box.BLACK
							&& ML != Box.BLACK && MR != Box.BLACK
							&& BL != Box.WHITE && BC != Box.BLACK && BR != Box.BLACK) { return true; }
						else { return false; }
					case (byte)8:
						if (TL != Box.BLACK && TC != Box.BLACK && TR != Box.BLACK
							&& ML != Box.WHITE && MR != Box.BLACK
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
							&& ML != Box.BLACK&& MR != Box.BLACK
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
							&& ML != Box.BLACK && MR != Box.WHITE
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
	}
}

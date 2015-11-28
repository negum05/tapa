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
        public static const byte HEAD_BN_0 = 0;
        public static const byte HEAD_BN_1 = 1;
        public static const byte HEAD_BN_2 = 9;
        public static const byte HEAD_BN_3 = 17;
        public static const byte HEAD_BN_4 = 25;
        public static const byte HEAD_BN_5 = 33;
        public static const byte HEAD_BN_6 = 41;
        public static const byte HEAD_BN_7 = 49;
        public static const byte HEAD_BN_8 = 57;
        public static const byte HEAD_BN_11 = 58;
        public static const byte HEAD_BN_12 = 78;
        public static const byte HEAD_BN_13 = 110;
        public static const byte HEAD_BN_14 = 134;
        public static const byte HEAD_BN_15 = 150;
        public static const byte HEAD_BN_22 = 158;
        public static const byte HEAD_BN_23 = 170;
        public static const byte HEAD_BN_24 = 186;
        public static const byte HEAD_BN_33 = 194;
        public static const byte HEAD_BN_111 = 198;
        public static const byte HEAD_BN_112 = 214;
        public static const byte HEAD_BN_113 = 238;
        public static const byte HEAD_BN_122 = 246;
        public static const byte HEAD_BN_1111 = 254;
        public static const byte MIN_ID = 0;
        public static const byte MAX_ID = 255;

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
            else if (id_num_dict[HEAD_BN_1] == box_num) {
                head_id = (byte)HEAD_BN_1;
                tail_id = (byte)(HEAD_BN_2 - 1);
            }
            else if (id_num_dict[HEAD_BN_2] == box_num) {
                head_id = (byte)HEAD_BN_2;
                tail_id = (byte)(HEAD_BN_3 - 1);
            }
            else if (id_num_dict[HEAD_BN_3] == box_num) {
                head_id = (byte)HEAD_BN_3;
                tail_id = (byte)(HEAD_BN_4 - 1);
            }
            else if (id_num_dict[HEAD_BN_4] == box_num) {
                head_id = (byte)HEAD_BN_4;
                tail_id = (byte)(HEAD_BN_5 - 1);
            }
            else if (id_num_dict[HEAD_BN_5] == box_num) {
                head_id = (byte)HEAD_BN_5;
                tail_id = (byte)(HEAD_BN_6 - 1);
            }
            else if (id_num_dict[HEAD_BN_6] == box_num) {
                head_id = (byte)HEAD_BN_6;
                tail_id = (byte)(HEAD_BN_7 - 1);
            }
            else if (id_num_dict[HEAD_BN_7] == box_num) {
                head_id = (byte)HEAD_BN_7;
                tail_id = (byte)(HEAD_BN_8 - 1);
            }
            else if (id_num_dict[HEAD_BN_8] == box_num) {
                head_id = (byte)HEAD_BN_8;
                tail_id = (byte)(HEAD_BN_11 - 1);
            }
            else if (id_num_dict[HEAD_BN_11] == box_num) {
                head_id = (byte)HEAD_BN_11;
                tail_id = (byte)(HEAD_BN_12 - 1);
            }
            else if (id_num_dict[HEAD_BN_12] == box_num) {
                head_id = (byte)HEAD_BN_12;
                tail_id = (byte)(HEAD_BN_13 - 1);
            }
            else if (id_num_dict[HEAD_BN_14] == box_num) {
                head_id = (byte)HEAD_BN_14;
                tail_id = (byte)(HEAD_BN_15 - 1);
            }
            else if (id_num_dict[HEAD_BN_15] == box_num) {
                head_id = (byte)HEAD_BN_15;
                tail_id = (byte)(HEAD_BN_22 - 1);
            }
            else if (id_num_dict[HEAD_BN_22] == box_num) {
                head_id = (byte)HEAD_BN_22;
                tail_id = (byte)(HEAD_BN_23 - 1);
            }
            else if (id_num_dict[HEAD_BN_23] == box_num) {
                head_id = (byte)HEAD_BN_23;
                tail_id = (byte)(HEAD_BN_24 - 1);
            }
            else if (id_num_dict[HEAD_BN_33] == box_num) {
                head_id = (byte)HEAD_BN_33;
                tail_id = (byte)(HEAD_BN_111 - 1);
            }
            else if (id_num_dict[HEAD_BN_111] == box_num) {
                head_id = (byte)HEAD_BN_111;
                tail_id = (byte)(HEAD_BN_112 - 1);
            }
            else if (id_num_dict[HEAD_BN_112] == box_num) {
                head_id = (byte)HEAD_BN_112;
                tail_id = (byte)(HEAD_BN_113 - 1);
            }
            else if (id_num_dict[HEAD_BN_113] == box_num) {
                head_id = (byte)HEAD_BN_113;
                tail_id = (byte)(HEAD_BN_122 - 1);
            }
            else if (id_num_dict[HEAD_BN_122] == box_num) {
                head_id = (byte)HEAD_BN_122;
                tail_id = (byte)(HEAD_BN_1111 - 1);
            }
            else if (id_num_dict[HEAD_BN_1111] == box_num) {
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
            int TL = Tapa.box[x-1][y-1].color;    // 左上(Top-Left)
            int TC = Tapa.box[x][y-1].color;      // 中上(Top-Center)
            int TR = Tapa.box[x-1][y+1].color;    // 右上(Top-Right)
            int ML = Tapa.box[x][y-1].color;      // 左中(Middle-Left)
            int MC = Tapa.box[x][y].color;        // 中中(Middle-Center)
            int MR = Tapa.box[x][y+1].color;      // 中右(Middle-Right)
            int BL = Tapa.box[x+1][y-1].color;    // 左下(Bottom-Left)
            int BC = Tapa.box[x+1][y].color;      // 中下(Bottom-Center)
            int BR = Tapa.box[x-1][y-1].color;    // 右下(Bottom-Right)

			if (id == HEAD_BN_0) {
                if(TL)
            }
            else if (HEAD_BN_1 <= box_num && box_num < HEAD_BN_2) {
                head_byte = (byte)HEAD_BN_1;
                tail_byte = (byte)(HEAD_BN_2 - 1);
            }
            else if (HEAD_BN_2 <= box_num && box_num < HEAD_BN_3) {
                head_byte = (byte)HEAD_BN_2;
                tail_byte = (byte)(HEAD_BN_3 - 1);
            }
            else if (HEAD_BN_3 <= box_num && box_num < HEAD_BN_4) {
                head_byte = (byte)HEAD_BN_3;
                tail_byte = (byte)(HEAD_BN_4 - 1);
            }
            else if (HEAD_BN_4 <= box_num && box_num < HEAD_BN_5) {
                head_byte = (byte)HEAD_BN_4;
                tail_byte = (byte)(HEAD_BN_5 - 1);
            }
            else if (HEAD_BN_5 <= box_num && box_num < HEAD_BN_6) {
                head_byte = (byte)HEAD_BN_5;
                tail_byte = (byte)(HEAD_BN_6 - 1);
            }
            else if (HEAD_BN_6 <= box_num && box_num < HEAD_BN_7) {
                head_byte = (byte)HEAD_BN_6;
                tail_byte = (byte)(HEAD_BN_7 - 1);
            }
            else if (HEAD_BN_7 <= box_num && box_num < HEAD_BN_8) {
                head_byte = (byte)HEAD_BN_7;
                tail_byte = (byte)(HEAD_BN_8 - 1);
            }
            else if (HEAD_BN_8 <= box_num && box_num < HEAD_BN_11) {
                head_byte = (byte)HEAD_BN_8;
                tail_byte = (byte)(HEAD_BN_11 - 1);
            }
            else if (HEAD_BN_11 <= box_num && box_num < HEAD_BN_12) {
                head_byte = (byte)HEAD_BN_11;
                tail_byte = (byte)(HEAD_BN_12 - 1);
            }
            else if (HEAD_BN_12 <= box_num && box_num < HEAD_BN_13) {
                head_byte = (byte)HEAD_BN_12;
                tail_byte = (byte)(HEAD_BN_13 - 1);
            }
            else if (HEAD_BN_14 <= box_num && box_num < HEAD_BN_15) {
                head_byte = (byte)HEAD_BN_14;
                tail_byte = (byte)(HEAD_BN_15 - 1);
            }
            else if (HEAD_BN_22 <= box_num && box_num < HEAD_BN_23) {
                head_byte = (byte)HEAD_BN_22;
                tail_byte = (byte)(HEAD_BN_23 - 1);
            }
            else if (HEAD_BN_23 <= box_num && box_num < HEAD_BN_24) {
                head_byte = (byte)HEAD_BN_23;
                tail_byte = (byte)(HEAD_BN_24 - 1);
            }
            else if (HEAD_BN_33 <= box_num && box_num < HEAD_BN_111) {
                head_byte = (byte)HEAD_BN_33;
                tail_byte = (byte)(HEAD_BN_111 - 1);
            }
            else if (HEAD_BN_111 <= box_num && box_num < HEAD_BN_112) {
                head_byte = (byte)HEAD_BN_111;
                tail_byte = (byte)(HEAD_BN_112 - 1);
            }
            else if (HEAD_BN_112 <= box_num && box_num < HEAD_BN_113) {
                head_byte = (byte)HEAD_BN_112;
                tail_byte = (byte)(HEAD_BN_113 - 1);
            }
            else if (HEAD_BN_113 <= box_num && box_num < HEAD_BN_122) {
                head_byte = (byte)HEAD_BN_113;
                tail_byte = (byte)(HEAD_BN_122 - 1);
            }
            else if (HEAD_BN_122 <= box_num && box_num < HEAD_BN_1111) {
                head_byte = (byte)HEAD_BN_122;
                tail_byte = (byte)(HEAD_BN_1111 - 1);
            }
            else if (HEAD_BN_1111 <= box_num && box_num <= MAX_ID) {
                head_byte = (byte)HEAD_BN_1111;
                tail_byte = (byte)(MAX_ID);
            }
            else {
                Console.WriteLine("Error: 数字マスのid振り分けでエラー");
                Application.Exit();
            }
        }
    }
}

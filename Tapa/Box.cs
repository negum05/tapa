using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Tapa
{

    class Box
    {
        public static const int WHITE = 0;      // 白色
        public static const int BLACK = 1;      // 黒色
        public static const int NOCOLOR = -1;   // 色未定
        public int x { get; set; }              // x座標
        public int y { get; set; }              // y座標
        public bool has_num { get; set; }       // 数字を持っているか
        public List<int> box_num_list;          // マスの数字
        public List<byte> id_list;              // id(数字マス周りのパターンの識別子)のリスト
        public int color { get; set; }          // マスの色 -1:未定 0:白 1:黒


        public Box()
        {
            color = Box.NOCOLOR;
            box_num_list = new List<int>();
            id_list = new List<byte>();
        }

        public Box(Box origin_box)
        {
            this.x = origin_box.x;
            this.y = origin_box.y;
            this.has_num = origin_box.has_num;
            this.box_num_list = new List<int>();
            if (origin_box.box_num_list.Count > 0) {
                foreach (int tmp_num in origin_box.box_num_list) {
                    this.box_num_list.Add(tmp_num);
                }
            }
            this.box_num_list = new List<int>();
            if (origin_box.id_list.Count > 0) {
                foreach (byte tmp_id in origin_box.id_list) {
                    this.id_list.Add(tmp_id);
                }
            }            
            this.color = origin_box.color;
        }

        public void clear()
        {
            this.has_num = false;
            this.box_num_list.Clear();
            this.color = Box.NOCOLOR;
        }

        public void printBoxNum()
        {
            if (!this.has_num) {
                if (this.color == Box.WHITE) { Console.Write("==== "); }
                else if(this.color == Box.BLACK) { Console.Write("**** "); }
                else { Console.Write("---- "); }
            }
            else {
                int count = 5;
                foreach (int tmp_num in this.box_num_list) {
                    Console.Write(tmp_num);
                    count--;
                }
                while (count-- > 0) { Console.Write(" "); }
            }
        }

        /*
         * Boxの深いコピー用に作成したけど
         * コンストラクタで似たような実装をしたからいらない疑惑...
         * 
        public Box clone()
        {
            Box tmp = new Box();
            tmp.has_num = this.has_num;
            if (this.box_num_list.Count > 0) {
                foreach (int tmp_num in this.box_num_list) {
                    tmp.box_num_list.Add(tmp_num);
                }
            }
            tmp.color = this.color;

            return tmp;
        }
         */

    }
}

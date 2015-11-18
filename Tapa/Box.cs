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
        public bool has_num { get; set; }        // 数字を持っているか
        public List<int> box_num { get; set; }   // マスの数字
        public int my_color { get; set; }        // マスの色 -1:未定 0:白 1:黒
        
        public Box()
        {
            my_color = -1;
            box_num = new List<int>();
        }

        public void clear()
        {
            this.has_num = false;
            this.box_num.Clear();
            this.my_color = -1;
        }

        public Box clone()
        {
            Box tmp = new Box();
            tmp.has_num = this.has_num;
            foreach (int tmp_num in this.box_num) {
                tmp.box_num.Add(tmp_num);
            }
            tmp.my_color = this.my_color;

            return tmp;
        }

    }
}

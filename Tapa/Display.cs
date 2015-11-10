using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tapa
{
    // parical:1つのクラスを複数クラスに分離する
    // 今回はForm1.Designer.csとこれが分離した
    // Designer.csはWindowsフォームデザイナーで編集した内容のみが反映されるため
    // 直接コードを書き込んではいけない
    // 部分クラスの内どれか一つでも基底クラスとアクセス修飾子があれば
    // 残りの部分クラスではそれを省略できる
    public partial class Display : Form
    {
        public Display()
        {
            InitializeComponent();
        }
    }
}

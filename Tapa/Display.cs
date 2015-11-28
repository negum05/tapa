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
    // 今回はDisplay.Designer.csとこれが分離した
    // Designer.csはWindowsフォームデザイナーで編集した内容のみが反映されるため
    // 直接コードを書き込んではいけない
    // 部分クラスの内どれか一つでも基底クラスとアクセス修飾子があれば
    // 残りの部分クラスではそれを省略できる

    public partial class Display : Form
    {
        Panel panel1;
        Button button1;

        public Display()
        {
            InitializeComponent();

            this.panel1 = new Panel();
            this.panel1.Location = new Point(20, 10);
            this.panel1.Size = new Size(50, 50);
            this.panel1.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(this.panel1);


            this.button1 = new Button();
            this.button1.Location = new Point(10, 10);
            this.button1.Size = new Size(170, 30);
            this.button1.Text = "ここを押して";
            

            this.Controls.Add(this.button1);
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tapa
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            // ボタンをWindows風のスタイルにしてくれる
            Application.EnableVisualStyles();
            // falseにすることでパフォーマンスを優先する
            Application.SetCompatibleTextRenderingDefault(false);
            // Form1()が停止しない間常に動作
            Application.Run(new Display());
        }
    }
}

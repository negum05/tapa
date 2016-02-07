using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CsvHelper;
using System.Security.AccessControl;
using System.Security.Principal;

namespace Tapa
{
	class MyCSVManagement
	{
		private static int MAX_TXT = 100;
		private static int MIN_GEN_ROW = 18;
		private static int MIN_GEN_COL = 18;
		private static int MAX_GEN_ROW = 18;
		private static int MAX_GEN_COL = 18;


		// csvファイルのディレクトリ
		public static string base_directory = @"C:\Users\Amano\Desktop\sample_tapa\";
		public static string working_directory = "default";
		public static string dir_name = "default";
		public static String csv_name;
		public static int dir_id = 0;
		public static List<Book> svc_list = new List<Book>();

		public static void makeSampleData()
		{
			for (Tapa.MAX_BOARD_ROW = MIN_GEN_ROW; Tapa.MAX_BOARD_ROW <= MAX_GEN_ROW; Tapa.MAX_BOARD_ROW++) {
				for (Tapa.MAX_BOARD_COL = MIN_GEN_COL; Tapa.MAX_BOARD_COL <= MAX_GEN_COL; Tapa.MAX_BOARD_COL++) {
					Tapa.BOX_SUM = Tapa.MAX_BOARD_COL * Tapa.MAX_BOARD_ROW;
					dir_name = "hsis_tapa_" + Tapa.MAX_BOARD_ROW + "_" + Tapa.MAX_BOARD_COL;
					makeFolder();

					for (int txt_id = 0; txt_id < MAX_TXT; txt_id++) {
						Tapa.file_name = "tapa" + Tapa.MAX_BOARD_ROW + Tapa.MAX_BOARD_COL + "_hsis" + String.Format("{0:0000}", txt_id) + ".txt";
						Tapa.processnum_kuromasu = 0;
						Tapa.processnum_kakuteijogaiid = 0;
						Tapa.processnum_dangoid = 0;
						Tapa.processnum_korituid = 0;
						Tapa.processnum_betu0id = 0;
						Tapa.processnum_kakuteimasu = 0;
						Tapa.processnum_numbox = 0;

						Problem.manageMakingProblem();

						Tapa.is_count = true;
						Tapa.solveTapa(Tapa.REPEAT_NUM);
						Tapa.is_count = false;

						// http://kageura.hatenadiary.jp/entry/2015/05/18/200000
						// csvファイルに書き込むlist
						MyCSVManagement.svc_list.Add(
						new Book() {
							tapa_name = Tapa.file_name,
							c_blackbox = Tapa.isolation_blackboxes_group_list[0].Count.ToString(),
							c_whitebox = (Tapa.BOX_SUM - Tapa.isolation_blackboxes_group_list[0].Count).ToString(),
							c_numbox = Tapa.processnum_numbox.ToString(),
							kuromasu = Tapa.processnum_kuromasu.ToString(),
							kakuteiIdJogai = Tapa.processnum_kakuteijogaiid.ToString(),
							dangoIdJogai = Tapa.processnum_dangoid.ToString(),
							korituIdJogai = Tapa.processnum_korituid.ToString(),
							betuId0Jogai = Tapa.processnum_betu0id.ToString(),
							kakuteimasu = Tapa.processnum_kakuteimasu.ToString()
						});
					}
					writeCSV();
				}
			}
		}

		public static void writeCSV()
		{

			// http://kageura.hatenadiary.jp/entry/2015/05/18/200000
			// csvの作成
			csv_name = dir_name + ".csv";
			using (TextWriter text_writer = File.CreateText(working_directory + csv_name)) {
				var csv_writer = new CsvWriter(text_writer);
				csv_writer.WriteRecords(MyCSVManagement.svc_list);
			}
		}

		public static void makeFolder()
		{
			// 作業ディレクトリのパス
			working_directory = base_directory + dir_name + @"\";

			// http://jeanne.wankuma.com/tips/csharp/directory/create.html
			// フォルダを作成して、作成先の DirectoryInfo を取得
			System.IO.DirectoryInfo dir_info;
			dir_info = System.IO.Directory.CreateDirectory(working_directory);

			// http://www.ipentec.com/document/document.aspx?page=csharp-directory-add-permission
			// 作成したディレクトリにアクセス許可を付与
			FileSystemAccessRule rule = new FileSystemAccessRule(
				new NTAccount("everyone"),
				FileSystemRights.FullControl,
				InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit,
				PropagationFlags.None,
				AccessControlType.Allow);
			DirectorySecurity security = Directory.GetAccessControl(working_directory);
			security.SetAccessRule(rule);
			Directory.SetAccessControl(working_directory, security);
		}
	}

	public class Book
	{
		public string tapa_name { get; set; }
		public string c_blackbox { get; set; }
		public string c_whitebox { get; set; }
		public string c_numbox { get; set; }
		public string kuromasu { get; set; }
		public string kakuteiIdJogai { get; set; }
		public string dangoIdJogai { get; set; }
		public string korituIdJogai { get; set; }
		public string betuId0Jogai { get; set; }
		public string kakuteimasu { get; set; }
	}
}

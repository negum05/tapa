using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CsvHelper;

namespace Tapa
{
	class MyCSVManagement
	{
		private static int MAX_TXT = 100;
		private static int MIN_GEN_ROW = 8;
		private static int MIN_GEN_COL = 8;
		private static int MAX_GEN_ROW = 8;
		private static int MAX_GEN_COL = 8;


		// csvファイルのディレクトリ
		public static string base_directory = @"C:\Users\Amano\Desktop\sample_tapa\time_search\rate88\";
		public static string working_directory = "default";
		public static string dir_name = "default";
		public static String csv_name;
		public static int dir_id = 0;
		public static List<Book> svc_list = new List<Book>();

		public static void makeSampleData()
		{
			//for (Tapa.MAX_BOARD_ROW = MIN_GEN_ROW; Tapa.MAX_BOARD_ROW <= MAX_GEN_ROW; Tapa.MAX_BOARD_ROW++) {
			//	for (Tapa.MAX_BOARD_COL = MIN_GEN_COL; Tapa.MAX_BOARD_COL <= MAX_GEN_COL; Tapa.MAX_BOARD_COL++) {

			Tapa.MAX_BOARD_ROW = Tapa.MAX_BOARD_COL = 8;
			for (int rate = 25; rate <= 90; rate += 5) {
				Problem.MAX_WHITEBOX_START_RATE = rate;
				Problem.MIN_WHITEBOX_START_RATE = rate;


				Tapa.BOX_SUM = Tapa.MAX_BOARD_COL * Tapa.MAX_BOARD_ROW;
				dir_name = "tapa_fast_normal_" + Tapa.MAX_BOARD_ROW + "" + Tapa.MAX_BOARD_COL + "_r" + rate;
				FolderManagement.makeFolder(base_directory, dir_name);

				// MAX_TXT個の問題に対する
				PreBook sum = new PreBook("average");	// 合計
				PreBook min = new PreBook("max");		// 最大
				PreBook max = new PreBook("min", true);	// 最少


				for (int txt_id = 0; txt_id < MAX_TXT; txt_id++) {
					Tapa.file_name = "tapa" + Tapa.MAX_BOARD_ROW + Tapa.MAX_BOARD_COL + String.Format("{0:00000}", txt_id) + ".txt";
					Tapa.processnum_kuromasu = 0;
					Tapa.processnum_kakuteijogaiid = 0;
					Tapa.processnum_dangoid = 0;
					Tapa.processnum_korituid = 0;
					Tapa.processnum_betu0id = 0;
					Tapa.processnum_kakuteimasu = 0;
					Tapa.processnum_numbox = 0;
					Tapa.visittimes_kuromasu = 0;
					Tapa.visittimes_kakuteijogaiid = 0;
					Tapa.visittimes_dangoid = 0;
					Tapa.visittimes_korituid = 0;
					Tapa.visittimes_betu0id = 0;
					Tapa.visittimes_kakuteimasu = 0;
					Tapa.sum_times_kuromasu = 0;
					Tapa.sum_times_kakuteijogaiid = 0;
					Tapa.sum_times_dangoid = 0;
					Tapa.sum_times_korituid = 0;
					Tapa.sum_times_betu0id = 0;
					Tapa.sum_times_kakuteimasu = 0;

					//// 時間計測開始
					System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();

					Problem.manageMakingProblem();

					////時間計測終了
					sw.Stop();
					Tapa.time_makeproblem = sw.ElapsedMilliseconds;

					Tapa.is_count = true;
					Tapa.solveTapa();
					Tapa.is_count = false;

					calcSum(sum);
					calcMax(min);
					calcMin(max);

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
						kakuteimasu = Tapa.processnum_kakuteimasu.ToString(),
						visittimes_kuromasu = Tapa.visittimes_kuromasu.ToString(),
						visittimes_kakuteijogaiid = Tapa.visittimes_kakuteijogaiid.ToString(),
						visittimes_dangoid = Tapa.visittimes_dangoid.ToString(),
						visittimes_korituid = Tapa.visittimes_korituid.ToString(),
						visittimes_betu0id = Tapa.visittimes_betu0id.ToString(),
						visittimes_kakuteimasu = Tapa.visittimes_kakuteimasu.ToString(),
						sum_times_kuromasu = Tapa.sum_times_kuromasu.ToString(),
						sum_times_kakuteijogaiid = Tapa.sum_times_kakuteijogaiid.ToString(),
						sum_times_dangoid = Tapa.sum_times_dangoid.ToString(),
						sum_times_korituid = Tapa.sum_times_korituid.ToString(),
						sum_times_betu0id = Tapa.sum_times_betu0id.ToString(),
						sum_times_kakuteimasu = Tapa.sum_times_kakuteimasu.ToString(),
						avetimes_kuromasu = (Tapa.visittimes_kuromasu == 0) ? 0 : Tapa.sum_times_kuromasu / (float)Tapa.visittimes_kuromasu,
						avetimes_kakuteijogaiid = (Tapa.visittimes_kakuteijogaiid == 0) ? 0 : Tapa.sum_times_kakuteijogaiid / (float)Tapa.visittimes_kakuteijogaiid,
						avetimes_dangoid = (Tapa.visittimes_dangoid == 0) ? 0 : Tapa.sum_times_dangoid / (float)Tapa.visittimes_dangoid,
						avetimes_korituid = (Tapa.visittimes_korituid == 0) ? 0 : Tapa.sum_times_korituid / (float)Tapa.visittimes_korituid,
						avetimes_betu0id = (Tapa.visittimes_betu0id == 0) ? 0 : Tapa.sum_times_betu0id / (float)Tapa.visittimes_betu0id,
						avetimes_kakuteimasu = (Tapa.visittimes_kakuteimasu == 0) ? 0 : Tapa.sum_times_kakuteimasu / (float)Tapa.visittimes_kakuteimasu,
						time_maketapa = Tapa.time_makeproblem.ToString()
					});
				}
				addAveBook(sum);
				addBook(max);
				addBook(min);
				writeCSV();
				MyCSVManagement.svc_list.Clear();
			}////
			//	}
			//}
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

		

		// 合計
		public static void calcSum(PreBook b){
		 b.c_blackbox += Tapa.isolation_blackboxes_group_list[0].Count;
		 b.c_whitebox += Tapa.BOX_SUM - Tapa.isolation_blackboxes_group_list[0].Count;
		 b.c_numbox += Tapa.processnum_numbox;
		 b.kuromasu += Tapa.processnum_kuromasu;
		 b.kakuteiIdJogai += Tapa.processnum_kakuteijogaiid;
		 b.dangoIdJogai += Tapa.processnum_dangoid;
		 b.korituIdJogai += Tapa.processnum_korituid;
		 b.betuId0Jogai += Tapa.processnum_betu0id;
		 b.kakuteimasu += Tapa.processnum_kakuteimasu;

		 b.visittimes_kuromasu += Tapa.visittimes_kuromasu;
		 b.visittimes_kakuteijogaiid += Tapa.visittimes_kakuteijogaiid;
		 b.visittimes_dangoid += Tapa.visittimes_dangoid;
		 b.visittimes_korituid += Tapa.visittimes_korituid;
		 b.visittimes_betu0id += Tapa.visittimes_betu0id;
		 b.visittimes_kakuteimasu += Tapa.visittimes_kakuteimasu;

		 b.sum_times_kuromasu += Tapa.sum_times_kuromasu;
		 b.sum_times_kakuteijogaiid += Tapa.sum_times_kakuteijogaiid;
		 b.sum_times_dangoid += Tapa.sum_times_dangoid;
		 b.sum_times_korituid += Tapa.sum_times_korituid;
		 b.sum_times_betu0id += Tapa.sum_times_betu0id;
		 b.sum_times_kakuteimasu += Tapa.sum_times_kakuteimasu;

		 b.avetimes_kuromasu += (Tapa.visittimes_kuromasu == 0) ? 0 : Tapa.sum_times_kuromasu / (float)Tapa.visittimes_kuromasu;
		 b.avetimes_kakuteijogaiid += (Tapa.visittimes_kakuteijogaiid == 0) ? 0 : Tapa.sum_times_kakuteijogaiid / (float)Tapa.visittimes_kakuteijogaiid;
		 b.avetimes_dangoid += (Tapa.visittimes_dangoid == 0) ? 0 : Tapa.sum_times_dangoid / (float)Tapa.visittimes_dangoid;
		 b.avetimes_korituid += (Tapa.visittimes_korituid == 0) ? 0 : Tapa.sum_times_korituid / (float)Tapa.visittimes_korituid;
		 b.avetimes_betu0id += (Tapa.visittimes_betu0id == 0) ? 0 : Tapa.sum_times_betu0id / (float)Tapa.visittimes_betu0id;
		 b.avetimes_kakuteimasu += (Tapa.visittimes_kakuteimasu == 0) ? 0 : Tapa.sum_times_kakuteimasu / (float)Tapa.visittimes_kakuteimasu;

		 b.time_maketapa += Tapa.time_makeproblem;
		}

		// 最大値
		public static void calcMax(PreBook b)
		{
			b.c_blackbox= System.Math.Max(b.c_blackbox, Tapa.isolation_blackboxes_group_list[0].Count);
			b.c_whitebox = System.Math.Max(b.c_whitebox, Tapa.BOX_SUM - Tapa.isolation_blackboxes_group_list[0].Count);
			b.c_numbox = System.Math.Max(b.c_numbox, Tapa.processnum_numbox);
			b.kuromasu = System.Math.Max(b.kuromasu, Tapa.processnum_kuromasu);
			b.kakuteiIdJogai = System.Math.Max(b.kakuteiIdJogai, Tapa.processnum_kakuteijogaiid);
			b.dangoIdJogai = System.Math.Max(b.dangoIdJogai, Tapa.processnum_dangoid);
			b.korituIdJogai = System.Math.Max(b.korituIdJogai, Tapa.processnum_korituid);
			b.betuId0Jogai = System.Math.Max(b.betuId0Jogai, Tapa.processnum_betu0id);
			b.kakuteimasu = System.Math.Max(b.kakuteimasu, Tapa.processnum_kakuteimasu);

			b.visittimes_kuromasu = System.Math.Max(b.visittimes_kuromasu, Tapa.visittimes_kuromasu);
			b.visittimes_kakuteijogaiid = System.Math.Max(b.visittimes_kakuteijogaiid, Tapa.visittimes_kakuteijogaiid);
			b.visittimes_dangoid = System.Math.Max(b.visittimes_dangoid, Tapa.visittimes_dangoid);
			b.visittimes_korituid = System.Math.Max(b.visittimes_korituid, Tapa.visittimes_korituid);
			b.visittimes_betu0id = System.Math.Max(b.visittimes_betu0id, Tapa.visittimes_betu0id);
			b.visittimes_kakuteimasu = System.Math.Max(b.visittimes_kakuteimasu, Tapa.visittimes_kakuteimasu);

			b.sum_times_kuromasu = System.Math.Max(b.sum_times_kuromasu, Tapa.sum_times_kuromasu);
			b.sum_times_kakuteijogaiid = System.Math.Max(b.sum_times_kakuteijogaiid, Tapa.sum_times_kakuteijogaiid);
			b.sum_times_dangoid = System.Math.Max(b.sum_times_dangoid, Tapa.sum_times_dangoid);
			b.sum_times_korituid = System.Math.Max(b.sum_times_korituid, Tapa.sum_times_korituid);
			b.sum_times_betu0id = System.Math.Max(b.sum_times_betu0id, Tapa.sum_times_betu0id);
			b.sum_times_kakuteimasu = System.Math.Max(b.sum_times_kakuteimasu, Tapa.sum_times_kakuteimasu);

			b.avetimes_kuromasu = System.Math.Max(b.avetimes_kuromasu, (Tapa.visittimes_kuromasu == 0) ? 0 : Tapa.sum_times_kuromasu / (float)Tapa.visittimes_kuromasu);
			b.avetimes_kakuteijogaiid = System.Math.Max(b.avetimes_kakuteijogaiid, (Tapa.visittimes_kakuteijogaiid == 0) ? 0 : Tapa.sum_times_kakuteijogaiid / (float)Tapa.visittimes_kakuteijogaiid);
			b.avetimes_dangoid = System.Math.Max(b.avetimes_dangoid, (Tapa.visittimes_dangoid == 0) ? 0 : Tapa.sum_times_dangoid / (float)Tapa.visittimes_dangoid);
			b.avetimes_korituid = System.Math.Max(b.avetimes_korituid, (Tapa.visittimes_korituid == 0) ? 0 : Tapa.sum_times_korituid / (float)Tapa.visittimes_korituid);
			b.avetimes_betu0id = System.Math.Max(b.avetimes_betu0id, (Tapa.visittimes_betu0id == 0) ? 0 : Tapa.sum_times_betu0id / (float)Tapa.visittimes_betu0id);
			b.avetimes_kakuteimasu = System.Math.Max(b.avetimes_kakuteimasu, (Tapa.visittimes_kakuteimasu == 0) ? 0 : Tapa.sum_times_kakuteimasu / (float)Tapa.visittimes_kakuteimasu);

			b.time_maketapa = System.Math.Max(b.time_maketapa, Tapa.time_makeproblem);
		}

		// 最小値
		public static void calcMin(PreBook b)
		{
			b.c_blackbox = System.Math.Min(b.c_blackbox, Tapa.isolation_blackboxes_group_list[0].Count);
			b.c_whitebox = System.Math.Min(b.c_whitebox, Tapa.BOX_SUM - Tapa.isolation_blackboxes_group_list[0].Count);
			b.c_numbox = System.Math.Min(b.c_numbox, Tapa.processnum_numbox);
			b.kuromasu = System.Math.Min(b.kuromasu, Tapa.processnum_kuromasu);
			b.kakuteiIdJogai = System.Math.Min(b.kakuteiIdJogai, Tapa.processnum_kakuteijogaiid);
			b.dangoIdJogai = System.Math.Min(b.dangoIdJogai, Tapa.processnum_dangoid);
			b.korituIdJogai = System.Math.Min(b.korituIdJogai, Tapa.processnum_korituid);
			b.betuId0Jogai = System.Math.Min(b.betuId0Jogai, Tapa.processnum_betu0id);
			b.kakuteimasu = System.Math.Min(b.kakuteimasu, Tapa.processnum_kakuteimasu);

			b.visittimes_kuromasu = System.Math.Min(b.visittimes_kuromasu, Tapa.visittimes_kuromasu);
			b.visittimes_kakuteijogaiid = System.Math.Min(b.visittimes_kakuteijogaiid, Tapa.visittimes_kakuteijogaiid);
			b.visittimes_dangoid = System.Math.Min(b.visittimes_dangoid, Tapa.visittimes_dangoid);
			b.visittimes_korituid = System.Math.Min(b.visittimes_korituid, Tapa.visittimes_korituid);
			b.visittimes_betu0id = System.Math.Min(b.visittimes_betu0id, Tapa.visittimes_betu0id);
			b.visittimes_kakuteimasu = System.Math.Min(b.visittimes_kakuteimasu, Tapa.visittimes_kakuteimasu);

			b.sum_times_kuromasu = System.Math.Min(b.sum_times_kuromasu, Tapa.sum_times_kuromasu);
			b.sum_times_kakuteijogaiid = System.Math.Min(b.sum_times_kakuteijogaiid, Tapa.sum_times_kakuteijogaiid);
			b.sum_times_dangoid = System.Math.Min(b.sum_times_dangoid, Tapa.sum_times_dangoid);
			b.sum_times_korituid = System.Math.Min(b.sum_times_korituid, Tapa.sum_times_korituid);
			b.sum_times_betu0id = System.Math.Min(b.sum_times_betu0id, Tapa.sum_times_betu0id);
			b.sum_times_kakuteimasu = System.Math.Min(b.sum_times_kakuteimasu, Tapa.sum_times_kakuteimasu);

			b.avetimes_kuromasu = System.Math.Min(b.avetimes_kuromasu, (Tapa.visittimes_kuromasu == 0) ? 0 : Tapa.sum_times_kuromasu / (float)Tapa.visittimes_kuromasu);
			b.avetimes_kakuteijogaiid = System.Math.Min(b.avetimes_kakuteijogaiid, (Tapa.visittimes_kakuteijogaiid == 0) ? 0 : Tapa.sum_times_kakuteijogaiid / (float)Tapa.visittimes_kakuteijogaiid);
			b.avetimes_dangoid = System.Math.Min(b.avetimes_dangoid, (Tapa.visittimes_dangoid == 0) ? 0 : Tapa.sum_times_dangoid / (float)Tapa.visittimes_dangoid);
			b.avetimes_korituid = System.Math.Min(b.avetimes_korituid, (Tapa.visittimes_korituid == 0) ? 0 : Tapa.sum_times_korituid / (float)Tapa.visittimes_korituid);
			b.avetimes_betu0id = System.Math.Min(b.avetimes_betu0id, (Tapa.visittimes_betu0id == 0) ? 0 : Tapa.sum_times_betu0id / (float)Tapa.visittimes_betu0id);
			b.avetimes_kakuteimasu = System.Math.Min(b.avetimes_kakuteimasu, (Tapa.visittimes_kakuteimasu == 0) ? 0 : Tapa.sum_times_kakuteimasu / (float)Tapa.visittimes_kakuteimasu);

			b.time_maketapa = System.Math.Min(b.time_maketapa, Tapa.time_makeproblem);
		}

		// prebookの内容をbookリストに追加
		public static void addBook(PreBook b)
		{
			MyCSVManagement.svc_list.Add(
					new Book() {
						tapa_name = b.tapa_name,
						c_blackbox = b.c_blackbox.ToString(),
						c_whitebox = b.c_whitebox.ToString(),
						c_numbox = b.c_numbox.ToString(),
						kuromasu = b.kuromasu.ToString(),
						kakuteiIdJogai = b.kakuteiIdJogai.ToString(),
						dangoIdJogai = b.dangoIdJogai.ToString(),
						korituIdJogai = b.korituIdJogai.ToString(),
						betuId0Jogai = b.betuId0Jogai.ToString(),
						kakuteimasu = b.kakuteimasu.ToString(),
						visittimes_kuromasu = b.visittimes_kuromasu.ToString(),
						visittimes_kakuteijogaiid = b.visittimes_kakuteijogaiid.ToString(),
						visittimes_dangoid = b.visittimes_dangoid.ToString(),
						visittimes_korituid = b.visittimes_korituid.ToString(),
						visittimes_betu0id = b.visittimes_betu0id.ToString(),
						visittimes_kakuteimasu = b.visittimes_kakuteimasu.ToString(),
						sum_times_kuromasu = b.sum_times_kuromasu.ToString(),
						sum_times_kakuteijogaiid = b.sum_times_kakuteijogaiid.ToString(),
						sum_times_dangoid = b.sum_times_dangoid.ToString(),
						sum_times_korituid = b.sum_times_korituid.ToString(),
						sum_times_betu0id = b.sum_times_betu0id.ToString(),
						sum_times_kakuteimasu = b.sum_times_kakuteimasu.ToString(),
						avetimes_kuromasu = b.avetimes_kuromasu,
						avetimes_kakuteijogaiid = b.avetimes_kakuteijogaiid,
						avetimes_dangoid = b.avetimes_dangoid,
						avetimes_korituid = b.avetimes_korituid,
						avetimes_betu0id = b.avetimes_betu0id,
						avetimes_kakuteimasu = b.avetimes_kakuteimasu,
						time_maketapa = b.time_maketapa.ToString()
					});
		}

		// bの平均をbookリストに追加
		public static void addAveBook(PreBook b)
		{
			MyCSVManagement.svc_list.Add(
					new Book() {
						tapa_name = b.tapa_name,
						c_blackbox = ((float)b.c_blackbox/MAX_TXT).ToString(),
						c_whitebox = ((float)b.c_whitebox/MAX_TXT).ToString(),
						c_numbox = ((float)b.c_numbox/MAX_TXT).ToString(),
						kuromasu = ((float)b.kuromasu/MAX_TXT).ToString(),
						kakuteiIdJogai = ((float)b.kakuteiIdJogai/MAX_TXT).ToString(),
						dangoIdJogai = ((float)b.dangoIdJogai/MAX_TXT).ToString(),
						korituIdJogai = ((float)b.korituIdJogai/MAX_TXT).ToString(),
						betuId0Jogai = ((float)b.betuId0Jogai/MAX_TXT).ToString(),
						kakuteimasu = ((float)b.kakuteimasu/MAX_TXT).ToString(),
						visittimes_kuromasu = ((float)b.visittimes_kuromasu/MAX_TXT).ToString(),
						visittimes_kakuteijogaiid = ((float)b.visittimes_kakuteijogaiid/MAX_TXT).ToString(),
						visittimes_dangoid = ((float)b.visittimes_dangoid/MAX_TXT).ToString(),
						visittimes_korituid = ((float)b.visittimes_korituid/MAX_TXT).ToString(),
						visittimes_betu0id = ((float)b.visittimes_betu0id/MAX_TXT).ToString(),
						visittimes_kakuteimasu = ((float)b.visittimes_kakuteimasu/MAX_TXT).ToString(),
						sum_times_kuromasu = ((float)b.sum_times_kuromasu/MAX_TXT).ToString(),
						sum_times_kakuteijogaiid = ((float)b.sum_times_kakuteijogaiid/MAX_TXT).ToString(),
						sum_times_dangoid = ((float)b.sum_times_dangoid/MAX_TXT).ToString(),
						sum_times_korituid = ((float)b.sum_times_korituid/MAX_TXT).ToString(),
						sum_times_betu0id = ((float)b.sum_times_betu0id/MAX_TXT).ToString(),
						sum_times_kakuteimasu = ((float)b.sum_times_kakuteimasu/MAX_TXT).ToString(),
						avetimes_kuromasu = ((float)b.avetimes_kuromasu/MAX_TXT),
						avetimes_kakuteijogaiid = ((float)b.avetimes_kakuteijogaiid/MAX_TXT),
						avetimes_dangoid = ((float)b.avetimes_dangoid/MAX_TXT),
						avetimes_korituid = ((float)b.avetimes_korituid/MAX_TXT),
						avetimes_betu0id = ((float)b.avetimes_betu0id/MAX_TXT),
						avetimes_kakuteimasu = ((float)b.avetimes_kakuteimasu/MAX_TXT),
						time_maketapa = ((float)b.time_maketapa / MAX_TXT).ToString()
					});
		}
	}

	class Book
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

		public string visittimes_kuromasu { get; set; }
		public string visittimes_kakuteijogaiid { get; set; }
		public string visittimes_dangoid { get; set; }
		public string visittimes_korituid { get; set; }
		public string visittimes_betu0id { get; set; }
		public string visittimes_kakuteimasu { get; set; }

		public string sum_times_kuromasu { get; set; }
		public string sum_times_kakuteijogaiid { get; set; }
		public string sum_times_dangoid { get; set; }
		public string sum_times_korituid { get; set; }
		public string sum_times_betu0id { get; set; }
		public string sum_times_kakuteimasu { get; set; }

		public float avetimes_kuromasu { get; set; }
		public float avetimes_kakuteijogaiid { get; set; }
		public float avetimes_dangoid { get; set; }
		public float avetimes_korituid { get; set; }
		public float avetimes_betu0id { get; set; }
		public float avetimes_kakuteimasu { get; set; }

		public string time_maketapa { get; set; }
	}

	class PreBook
	{
		public string tapa_name { get; set; }
		public int c_blackbox { get; set; }
		public int c_whitebox { get; set; }
		public int c_numbox { get; set; }
		public int kuromasu { get; set; }
		public int kakuteiIdJogai { get; set; }
		public int dangoIdJogai { get; set; }
		public int korituIdJogai { get; set; }
		public int betuId0Jogai { get; set; }
		public int kakuteimasu { get; set; }

		public long visittimes_kuromasu { get; set; }
		public long visittimes_kakuteijogaiid { get; set; }
		public long visittimes_dangoid { get; set; }
		public long visittimes_korituid { get; set; }
		public long visittimes_betu0id { get; set; }
		public long visittimes_kakuteimasu { get; set; }

		public float sum_times_kuromasu { get; set; }
		public float sum_times_kakuteijogaiid { get; set; }
		public float sum_times_dangoid { get; set; }
		public float sum_times_korituid { get; set; }
		public float sum_times_betu0id { get; set; }
		public float sum_times_kakuteimasu { get; set; }

		public float avetimes_kuromasu { get; set; }
		public float avetimes_kakuteijogaiid { get; set; }
		public float avetimes_dangoid { get; set; }
		public float avetimes_korituid { get; set; }
		public float avetimes_betu0id { get; set; }
		public float avetimes_kakuteimasu { get; set; }

		public long time_maketapa { get; set; }

		public PreBook(string s, bool init = false)
		{
			tapa_name = s;
			if (!init) {
				c_blackbox = 0;
				c_whitebox = 0;
				c_numbox = 0;
				kuromasu = 0;
				kakuteiIdJogai = 0;
				dangoIdJogai = 0;
				korituIdJogai = 0;
				betuId0Jogai = 0;
				kakuteimasu = 0;

				visittimes_kuromasu = 0;
				visittimes_kakuteijogaiid = 0;
				visittimes_dangoid = 0;
				visittimes_korituid = 0;
				visittimes_betu0id = 0;
				visittimes_kakuteimasu = 0;

				sum_times_kuromasu = 0;
				sum_times_kakuteijogaiid = 0;
				sum_times_dangoid = 0;
				sum_times_korituid = 0;
				sum_times_betu0id = 0;
				sum_times_kakuteimasu = 0;

				avetimes_kuromasu = 0;
				avetimes_kakuteijogaiid = 0;
				avetimes_dangoid = 0;
				avetimes_korituid = 0;
				avetimes_betu0id = 0;
				avetimes_kakuteimasu = 0;

				time_maketapa = 0;
			}
			else {
				c_blackbox = int.MaxValue;
				c_whitebox = int.MaxValue;
				c_numbox = int.MaxValue;
				kuromasu = int.MaxValue;
				kakuteiIdJogai = int.MaxValue;
				dangoIdJogai = int.MaxValue;
				korituIdJogai = int.MaxValue;
				betuId0Jogai = int.MaxValue;
				kakuteimasu = int.MaxValue;

				visittimes_kuromasu = long.MaxValue;
				visittimes_kakuteijogaiid = long.MaxValue;
				visittimes_dangoid = long.MaxValue;
				visittimes_korituid = long.MaxValue;
				visittimes_betu0id = long.MaxValue;
				visittimes_kakuteimasu = long.MaxValue;

				sum_times_kuromasu = float.MaxValue;
				sum_times_kakuteijogaiid = float.MaxValue;
				sum_times_dangoid = float.MaxValue;
				sum_times_korituid = float.MaxValue;
				sum_times_betu0id = float.MaxValue;
				sum_times_kakuteimasu = float.MaxValue;

				avetimes_kuromasu = float.MaxValue;
				avetimes_kakuteijogaiid = float.MaxValue;
				avetimes_dangoid = float.MaxValue;
				avetimes_korituid = float.MaxValue;
				avetimes_betu0id = float.MaxValue;
				avetimes_kakuteimasu = float.MaxValue;

				time_maketapa = long.MaxValue;
			}
		}
	}

}

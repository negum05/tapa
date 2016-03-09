using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text.RegularExpressions;

namespace Tapa
{
	class FolderManagement
	{
		// 引数で受け取ったパスのフォルダを作成
		// 作成したディレクトリパスを返す
		public static string makeFolder(string dir_path, string file_name)
		{
			// 末尾に \ がなければ追加
			dir_path += dir_path[dir_path.Length - 1].Equals('\\') ? "" : @"\";

			// 拡張子を削除したファイルパスを取得
			// http://jeanne.wankuma.com/tips/csharp/path/getfilenamewithoutextension.html
			file_name = System.IO.Path.GetFileNameWithoutExtension(file_name) + "_pzpr";

			// http://ja.stackoverflow.com/questions/4312/連番のファイル名を生成するには
			// 連番ファイル名の生成
			System.IO.DirectoryInfo dir_info = new DirectoryInfo(dir_path);
			var max = dir_info.GetDirectories(file_name + "???")							// パターンに一致するファイルを取得する
						.Select(fi => Regex.Match(fi.Name, @"(?i)(\d{3})$"))				// ファイルの中で数値のものを探す
						.Where(m => m.Success)                                      // 該当するファイルだけに絞り込む
						.Select(m => Int32.Parse(m.Groups[1].Value))               // 数値を取得する
						.DefaultIfEmpty(0)                                          // １つも該当しなかった場合は 0 とする
						.Max();                                                     // 最大値を取得する
			string create_file = dir_path + String.Format("{0}{1:d3}", file_name, max + 1) + @"\";

			// http://jeanne.wankuma.com/tips/csharp/directory/create.html
			// フォルダを作成
			System.IO.Directory.CreateDirectory(create_file);
			// CSV用
			MyCSVManagement.working_directory = create_file;

			Console.WriteLine("create_file >> " + create_file);

			// http://www.ipentec.com/document/document.aspx?page=csharp-directory-add-permission
			// 作成したディレクトリにアクセス許可を付与
			FileSystemAccessRule rule = new FileSystemAccessRule(
				new NTAccount("everyone"),
				FileSystemRights.FullControl,
				InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit,
				PropagationFlags.None,
				AccessControlType.Allow);
			DirectorySecurity security = Directory.GetAccessControl(create_file);
			security.SetAccessRule(rule);
			Directory.SetAccessControl(create_file, security);

			return create_file;
		}
	}
}

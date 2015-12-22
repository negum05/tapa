using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tapa
{
	class BackTrack
	{
		public static StateSave correct_save_point = new StateSave();

		private const int MAX_INT = 2147483647;
		private static int min_Depth = MAX_INT;	// 答えまでの最小先読み数
		public static int min_depth
		{
			private set { min_Depth = value; }
			get { return min_Depth; }
		}

		/*********************************
		 * 
		 * processingNotDeployedBoxListの範囲でバックトラックを行う
		 * バックトラックの結果が一度でも正しければtrueを返す
		 * 引数
		 * depth	: バックトラックの深さ
		 * processingNotDeployedBoxList	: バックトラックを行う未定マス群のリスト
		 *   
		 * *******************************/
		private bool doBackTrack(int depth, List<Coordinates> processingNotDeployedBoxList, bool is_first = false)
		{
			StateSave save_point = new StateSave();
			StateSave.saveNowState(save_point);
			bool ret_bool = false;	// true:(途中)盤面が正しい

			// 深さをインクリメント
			depth++;
			// 黒マスと接している未定マスのリストを作成
			List<Coordinates> adjacent_notdeployedbox_coord_list = new List<Coordinates>();
			// 黒マスと接している未定マスがある時
			foreach (Coordinates tmp_ndbox_coord in processingNotDeployedBoxList) {
				if (Box.existBlackBoxAround(tmp_ndbox_coord)) {
					adjacent_notdeployedbox_coord_list.Add(tmp_ndbox_coord);
				}
			}
			// 未定マスはあるが、黒マスと接している未定マスがない時
			if (adjacent_notdeployedbox_coord_list.Count == 0
				&& processingNotDeployedBoxList.Count > 0) {
				foreach (Coordinates tmp_ndbox_coord in processingNotDeployedBoxList) {
					adjacent_notdeployedbox_coord_list.Add(new Coordinates(tmp_ndbox_coord));
				}
			}

			if (adjacent_notdeployedbox_coord_list.Count == 1) {
				for (int i = 0; i < 2; i++) {
					Coordinates tmp_ndbox_coord = adjacent_notdeployedbox_coord_list[0];
					Tapa.box[tmp_ndbox_coord.x][tmp_ndbox_coord.y].Color = (i == 0) ? Box.BLACK : Box.WHITE;

					// 変化がなくなるまで処理を行う
					do {
						Tapa.was_change_board = false;
						PatternAroundNumBox.managePatternAroundNumBox();
						Box.manageBlackBox();
					} while (Tapa.was_change_board);

					// 処理中の未定マス群から色のついたマスを除外したリストを作成
					List<Coordinates> arg_list = new List<Coordinates>(processingNotDeployedBoxList);
					for (int j = arg_list.Count - 1; j >= 0; j--) {
						if (Tapa.box[arg_list[j].x][arg_list[j].y].Color != Box.NOCOLOR) {
							arg_list.RemoveAt(j);
						}
					}

					// 未定マスが存在すれば再起する
					if (arg_list.Count > 0) {
						ret_bool = ret_bool || doBackTrack(depth, arg_list);
					}
					else {	// 未定マスが存在しない
						if (Box.checkNotIsolationBlackBoxGroup() || Tapa.isCorrectAnswer()) {	// 盤面の一繋がりの黒マス群が孤立していないか
							StateSave.saveNowState(BackTrack.correct_save_point);		// 正しければ現在の盤面を保存
							if (depth < min_depth) {		// 先読みした深さ（のうち小さい方）を記録
								min_depth = depth;
							}
							ret_bool = true;
						}
						//else {
						//	tmp_ndbox_coord.printCoordinates();
						//	Console.WriteLine();
						//	Tapa.printBoard();
						//	Console.WriteLine();
						//}
					}
					// 元の状態に戻す
					StateSave.setSavedState(save_point);

					// 初めの試し塗り　かつ　黒マスで塗ったら全部false だった場合
					if (is_first && !ret_bool) {
						// 未定マスの色を白にする
						Tapa.box[tmp_ndbox_coord.x][tmp_ndbox_coord.y].Color = Box.WHITE;
						StateSave.saveNowState(save_point);
					}
				}
				return ret_bool;
			}

			// 未定マスをたどる
			for (int i = 0; i < adjacent_notdeployedbox_coord_list.Count; i++) {
				Coordinates tmp_ndbox_coord = adjacent_notdeployedbox_coord_list[i];

				// 未定マスの色を黒にする
				Tapa.box[tmp_ndbox_coord.x][tmp_ndbox_coord.y].Color = Box.BLACK;

				// 変化がなくなるまで処理を行う
				do {
					Tapa.was_change_board = false;
					PatternAroundNumBox.managePatternAroundNumBox();
					Box.manageBlackBox();
				} while (Tapa.was_change_board);

				// 処理中の未定マス群から色のついたマスを除外したリストを作成
				List<Coordinates> arg_list = new List<Coordinates>(processingNotDeployedBoxList);
				for (int j = arg_list.Count - 1; j >= 0; j--) {
					if (Tapa.box[arg_list[j].x][arg_list[j].y].Color != Box.NOCOLOR) {
						arg_list.RemoveAt(j);
					}
				}

				// 処理中の未定マス群に未定マスが残っていれば再起する
				if (arg_list.Count > 0) {
					ret_bool = ret_bool || doBackTrack(depth, arg_list);
				}
				else {	// 処理中の未定マス群に未定マスが存在しない
					// 盤面に一繋がりの黒マス群が孤立していない or 盤面が正解
					if (Box.checkNotIsolationBlackBoxGroup() || Tapa.isCorrectAnswer()) {
						StateSave.saveNowState(BackTrack.correct_save_point);		// 正しければ現在の盤面を保存
						if (depth < min_depth) {		// 先読みした深さ（のうち小さい方）を記録
							min_depth = depth;
						}
						ret_bool = true;
					}
					//else {
					//	tmp_ndbox_coord.printCoordinates();
					//	Console.WriteLine();
					//	Tapa.printBoard();
					//	Console.WriteLine();
					//}
				}
				// 元の状態に戻す
				StateSave.setSavedState(save_point);

				// 初めの試し塗り　かつ　黒マスで塗ったら全部false だった場合
				if (is_first && !ret_bool) {
					// 未定マスの色を白にする
					Tapa.box[tmp_ndbox_coord.x][tmp_ndbox_coord.y].Color = Box.WHITE;
					StateSave.saveNowState(save_point);
				}
			}
			return ret_bool;
		}

		/*********************************
		 * 
		 * 一繋がりの未定マス群毎にバックトラックを行う
		 *   
		 * *******************************/
		public void manageBackTrack()
		{
			//StateSave state_base = new StateSave();
			//StateSave.saveNowState(state_base);
			// 一繋がりの未定マス群のリストを作成
			Box.divideNotDeployedBoxToGroup();
			min_Depth = MAX_INT;
			do {
				if (doBackTrack(0, Tapa.isolation_notdeployedboxes_group_list[0], true)) {

					StateSave.setSavedState(BackTrack.correct_save_point);

					Console.WriteLine("バックトラック成功 深さ：{0}", min_Depth);
					Tapa.printBoard();
					Console.WriteLine();


				}
				if (Tapa.not_deployedbox_coord_list.Count > 0) {
					// 一繋がりの未定マス群のリストを作成
					Box.divideNotDeployedBoxToGroup();
				}

				min_Depth = MAX_INT;
			} while (Tapa.isolation_notdeployedboxes_group_list.Count > 0);
		}
	}
}

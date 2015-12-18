﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tapa
{
	class BackTrack
	{
		//　バックトラックの分岐保存用スタック
		private static Stack<StateSave> backtrack_stack = new Stack<StateSave>();
		public static StateSave correct_save_point = new StateSave();

		private static int min_Depth = 2147483647;	// 答えまでの最小先読み数
		public static int min_depth
		{
			private set { min_Depth = value; }
			get { return min_Depth; }
		}

		public void doBackTrack()
		{
			StateSave save_point = new StateSave();
			StateSave.saveNowState(save_point);

			// 現在の状態をスタックにpush
			backtrack_stack.Push(new StateSave(save_point));

			// 黒マスと接している未定マスのリストを作成
			List<Coordinates> adjacent_notdeployedbox_coord_list = new List<Coordinates>();
			// 黒マスと接している黒マスがある時
			foreach (Coordinates tmp_ndbox_coord in Tapa.not_deployedbox_coord_list) {
				if (Box.existBlackBoxAround(tmp_ndbox_coord)) {
					adjacent_notdeployedbox_coord_list.Add(tmp_ndbox_coord);
				}
			}
			// 黒マスと接している黒マスがない時
			if (adjacent_notdeployedbox_coord_list.Count == 0
				&& Tapa.not_deployedbox_coord_list.Count > 0) {
				foreach (Coordinates tmp_ndbox_coord in Tapa.not_deployedbox_coord_list) {
					adjacent_notdeployedbox_coord_list.Add(new Coordinates(tmp_ndbox_coord));
				}
			}

			// 未定マスをたどる
			for (int i = 0; i < adjacent_notdeployedbox_coord_list.Count; i++) {
				Coordinates tmp_ndbox_coord = new Coordinates(adjacent_notdeployedbox_coord_list[i]);

				// 未定マスの色を黒にする
				Tapa.box[tmp_ndbox_coord.x][tmp_ndbox_coord.y].Color = Box.BLACK;

				// 変化がなくなるまで処理を行う
				do {
					Tapa.was_change_board = false;
					PatternAroundNumBox.managePatternAroundNumBox();
					Box.manageBlackBox();
				} while (Tapa.was_change_board);

				// 未定マスが存在すれば再起する
				if (Tapa.not_deployedbox_coord_list.Count > 0) {
					BackTrack next_backtrack = new BackTrack();
					next_backtrack.doBackTrack();
				}
				else {	// 未定マスが存在しない
					//Console.WriteLine("【未定マスが存在しない】");
					//Tapa.printBoard();
					//Console.WriteLine();

					if (Tapa.isCorrectAnswer()) {	// 盤面が正しいか見る
						StateSave.saveNowState(new StateSave(correct_save_point));		// 正しければ現在の盤面を保存

						Console.Write("##############\n正解に到達 ");
						tmp_ndbox_coord.printCoordinates();
						Console.WriteLine();
						Tapa.printBoard();
						Console.WriteLine();
						if (backtrack_stack.Count < min_depth) {		// 先読みした深さ（のうち小さい方）を記録
							min_depth = backtrack_stack.Count;
						}
						StateSave.setSavedState(save_point);
					}
					else {
						// 元の状態に戻す
						StateSave.setSavedState(save_point);
					}
				}
			}
			if (backtrack_stack.Count > 0) {
				// スタックからpopし、状態をロードする。
				backtrack_stack.Pop();
				StateSave.setSavedState(new StateSave(backtrack_stack.Peek()));
			}
			else {
				// backtrack_stack.Pop();
				return;
			}
		}
	}
}

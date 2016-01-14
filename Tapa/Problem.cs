using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tapa
{
	class Problem
	{
		/*********************************
		 * 
		 * 問題生成のための初期盤面を生成
		 * （外枠：白マス　内側：未定マス）
		 *   
		 * *******************************/
		private static void makeBasicBoard()
		{
			// 色を塗る以外の処理をColorプロパティ内で行わないため
			Box.during_make_inputbord = true;

			for (int i = 1; i <= Tapa.MAX_BOARD_ROW; i++) {
				List<Box> tmp_box_list = new List<Box>();
				for (int j = 1; j <= Tapa.MAX_BOARD_COL; j++) {
					Box tmp_box = new Box();
					tmp_box.coord = new Coordinates(i, j);
					tmp_box.Color = Box.NOCOLOR;
					tmp_box_list.Add(tmp_box);
					Tapa.not_deployedbox_coord_list.Add(tmp_box.coord);		// 未定マスの座標Listに追加
				}
				Tapa.box.Add(tmp_box_list);
			}
			Tapa.makeOuterBox(Tapa.MAX_BOARD_ROW, Tapa.MAX_BOARD_COL);
			
			Box.during_make_inputbord = false;


			Tapa.printBoard();
		}

		/*********************************
		 * 
		 * 初期盤面にランダムで白マスを配置する
		 *   
		 * *******************************/
		private static void setRandomWhiteBox()
		{
			int whitebox_num = 30;

			for (int i = whitebox_num; i > 0; i--) {
				// 未定マスからランダムにマスを選択
				Coordinates will_whitebox = Tapa.not_deployedbox_coord_list[
					Problem.getRandomInt(0, Tapa.not_deployedbox_coord_list.Count)];
				// 選択したマスを白色にする
				Tapa.box[will_whitebox.x][will_whitebox.y].Color = Box.WHITE;

				// 一繋がりの未定マス群リストを作成する
				Box.divideNotDeployedBoxToGroup();
				
				if (Tapa.isolation_notdeployedboxes_group_list.Count > 1) {
					Tapa.box[will_whitebox.x][will_whitebox.y].revision_color = Box.NOCOLOR;
					Tapa.not_deployedbox_coord_list.Add(will_whitebox);	// 今回選択したマスを未定マスリストへ追加
					i++;
				}

				Tapa.printBoard();
				Console.WriteLine();

			}
		}


		/*********************************
		 * 
		 * 一繋がりの黒マスを生成する
		 * 始点はランダム
		 *   
		 * *******************************/
		private static void makeBlackBoxRoute()
		{
			// 未定マスからランダムに選択、それを黒マスにする。
			Coordinates base_blackbox_coord
				= new Coordinates(Tapa.not_deployedbox_coord_list[
					getRandomInt(0, Tapa.not_deployedbox_coord_list.Count)]);
			Tapa.box[base_blackbox_coord.x][base_blackbox_coord.y].Color = Box.BLACK;

			// 伸び代のある黒マスがなくなるまで以下を実行
			while (Tapa.edge_blackbox_coord_list.Count > 0) {
				// 伸び代のある黒マスリストからランダムに1つ選択、その黒マスから伸びることのできるマスをランダムに選択、そのマスを黒色にする。
				Coordinates new_blackbox_coord = getRandCoordAround(
					Tapa.edge_blackbox_coord_list[getRandomInt(0, Tapa.edge_blackbox_coord_list.Count)]);

				Tapa.box[new_blackbox_coord.x][new_blackbox_coord.y].Color = Box.BLACK;

				Tapa.printBoard();
				Console.WriteLine();
			}
			for (int i = Tapa.not_deployedbox_coord_list.Count - 1; i >= 0; i--) {
				Coordinates tmp_coord = Tapa.not_deployedbox_coord_list[i];
				Tapa.box[tmp_coord.x][tmp_coord.y].Color = Box.WHITE;
			}
		}

		/*********************************
		 * 
		 * base_coordの上下左右の未定マスの座標をランダムで1つ返す
		 * 
		 * 引数
		 * base_coord	: 中心座標
		 *   
		 * *******************************/
		private static Coordinates getRandCoordAround(Coordinates base_coord)
		{
			List<Coordinates> extendable_coord_list = Box.getNoColorBoxCoordinatesAround(base_coord);
			if (extendable_coord_list.Count == 0) { return null; }
			return extendable_coord_list[getRandomInt(0, extendable_coord_list.Count)];
		}

		/*********************************
		 * 
		 * int型の乱数を返す
		 * 引数
		 * min	: 乱数の下限
		 * max	: 乱数の上限
		 *   
		 * *******************************/
		// TickCount:PCを最後に起動してからの経過時間（ミリ秒）
		static int seed = Environment.TickCount;		// 乱数の種
		public static int getRandomInt(int min, int max)
		{
			return new Random(seed++).Next(min, max);
		}

		public static void manageMakingProblem()
		{
			makeBasicBoard();
			setRandomWhiteBox();
			Problem.makeBlackBoxRoute();
			Tapa.printBoard();
		}
	}
}

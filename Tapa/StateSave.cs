using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tapa
{
	class StateSave
	{
		public Box[][] saved_box;
		// 呼びだされた時点での数字マスの座標のリスト
		private List<Coordinates> saved_numbox_coord_list;
		// 呼びだされた時点での未定マスの座標リスト
		private List<Coordinates> saved_not_deployedbox_coord_list;
		// 呼びだされた時点での伸び代のある黒マスの座標リスト
		private List<Coordinates> saved_edge_blackbox_coord_list;
		// 呼びだされた時点での一繋がりの黒マス群の座標リスト
		private List<List<Coordinates>> saved_isolation_blackboxes_group_list;
		// 呼びだされた時点での一繋がりの未定マス群の座標リスト
		// private List<List<Coordinates>> saved_isolation_notdeployedboxes_group_list;
		// 
		private bool saved_was_changed_board;

		public StateSave()
		{
			this.saved_box = new Box[Tapa.MAX_BOARD_ROW + 2][];
			for (int i = 0; i < Tapa.MAX_BOARD_ROW + 2; i++) {
				this.saved_box[i] = new Box[Tapa.MAX_BOARD_COL + 2];
				for (int j = 0; j < Tapa.MAX_BOARD_COL + 2; j++) {
					this.saved_box[i][j] = new Box();
				}
			}

			this.saved_numbox_coord_list = new List<Coordinates>();
			this.saved_not_deployedbox_coord_list = new List<Coordinates>();
			this.saved_edge_blackbox_coord_list = new List<Coordinates>();
			this.saved_isolation_blackboxes_group_list = new List<List<Coordinates>>();
			// List<List<Coordinates>> saved_isolation_notdeployedboxes_group_list = new List<List<Coordinates>>();
			this.saved_was_changed_board = Tapa.was_change_board;
		}
		public StateSave(StateSave origin_state)
		{
			// 引数の盤面の全てのマス
			this.saved_box = new Box[Tapa.MAX_BOARD_ROW + 2][];
			for (int i = 0; i < Tapa.MAX_BOARD_ROW + 2; i++) {
				this.saved_box[i] = new Box[Tapa.MAX_BOARD_COL + 2];
				for (int j = 0; j < Tapa.MAX_BOARD_COL + 2; j++) {
					this.saved_box[i][j] = new Box(origin_state.saved_box[i][j]);
				}
			}
			// 引数の数字マスの座標のリスト
			this.saved_numbox_coord_list = StateSave.getStateCoordList(origin_state.saved_numbox_coord_list);
			// 引数の未定マスの座標リスト
			this.saved_not_deployedbox_coord_list = StateSave.getStateCoordList(origin_state.saved_not_deployedbox_coord_list);
			// 引数の伸び代のある黒マスの座標リスト
			this.saved_edge_blackbox_coord_list = StateSave.getStateCoordList(origin_state.saved_edge_blackbox_coord_list);
			// 引数の一繋がりの黒マス群の座標リスト
			this.saved_isolation_blackboxes_group_list
				= StateSave.getStateMultiCoordList(origin_state.saved_isolation_blackboxes_group_list);
			// 引数の一繋がりの未定マス群の座標リスト
			//this.saved_isolation_notdeployedboxes_group_list
			//	= StateSave.getStateMultiCoordList(origin_state.saved_isolation_notdeployedboxes_group_list);
			// 引数の盤面が変更されたかの情報
			this.saved_was_changed_board = origin_state.saved_was_changed_board;
		}

		/*********************************
		 * 
		 *   リストや盤面の状態を保存
		 *  
		 * *******************************/
		public static void saveNowState(StateSave save_point)
		{
			////// 呼びだされた時点での盤面の全てのマス
			for (int i = 0; i < Tapa.MAX_BOARD_ROW + 2; i++) {
				save_point.saved_box[i] = new Box[Tapa.MAX_BOARD_COL + 2];
				for (int j = 0; j < Tapa.MAX_BOARD_COL + 2; j++) {
					save_point.saved_box[i][j] = new Box(Tapa.box[i][j]);
				}
			}
			// 呼びだされた時点での数字マスの座標のリスト
			save_point.saved_numbox_coord_list = StateSave.getStateCoordList(Tapa.numbox_coord_list);
			// 呼びだされた時点での未定マスの座標リスト
			save_point.saved_not_deployedbox_coord_list = StateSave.getStateCoordList(Tapa.not_deployedbox_coord_list);
			// 呼びだされた時点での伸び代のある黒マスの座標リスト
			save_point.saved_edge_blackbox_coord_list = StateSave.getStateCoordList(Tapa.edge_blackbox_coord_list);
			// 呼びだされた時点での一繋がりの黒マス群の座標リスト
			save_point.saved_isolation_blackboxes_group_list
				= StateSave.getStateMultiCoordList(Tapa.isolation_blackboxes_group_list);
			// 呼びだされた時点での一繋がりの黒マス群の座標リスト
			//save_point.saved_isolation_notdeployedboxes_group_list
			//	= StateSave.getStateMultiCoordList(Tapa.isolation_notdeployedboxes_group_list);
			// 呼びだされた時点での盤面が変更されたかの情報
			save_point.saved_was_changed_board = Tapa.was_change_board;
		}

		/*********************************
		 * 
		 *   リストや盤面の状態を引数の状態に復元
		 *  
		 * *******************************/
		public static void loadSavedState(StateSave save_point)
		{
			for (int i = 0; i < Tapa.MAX_BOARD_ROW + 2; i++) {
				for (int j = 0; j < Tapa.MAX_BOARD_COL + 2; j++) {
					Tapa.box[i][j] = new Box(save_point.saved_box[i][j]);
				}
			}
			loadSavedStateEdgeBlackBoxCoordList(save_point.saved_edge_blackbox_coord_list);
			loadSavedStateIsolationBlackBoxesGroupList(save_point.saved_isolation_blackboxes_group_list);
			loadSavedStateNotDeployedBoxCoordList(save_point.saved_not_deployedbox_coord_list);
			loadSavedStateNumBoxCoordList(save_point.saved_numbox_coord_list);
			Tapa.was_change_board = save_point.saved_was_changed_board;
		}

		/*** (begin) save ***/
		private static List<Coordinates> getStateCoordList(List<Coordinates> coord_list)
		{
			if (coord_list == null) { return null; }
			List<Coordinates> tmp_co_list = new List<Coordinates>();

			foreach (Coordinates tmp_co in coord_list) {
				tmp_co_list.Add(new Coordinates(tmp_co));
			}
			return tmp_co_list;
		}
		//private static List<List<Box>> getStateBoard(Box[][] origin_board = null)
		//{
		//	//Box[][] copy_target;
		//	//if (origin_board == null) { copy_target = (Box[][])Tapa.box.Clone(); }
		//	//else { copy_target = (Box[][])origin_board.Clone(); }

		//	////Box[][] tmp_board = new Box[Tapa.MAX_BOARD_ROW][];

		//	////int max_row = Tapa.MAX_BOARD_ROW + 2; // +2は外周の分
		//	////int max_col = Tapa.MAX_BOARD_COL + 2;
		//	////// 要素ごとにaddする (インスタンスができる)
		//	////for (int i = 0; i < max_row; i++) {
		//	////	List<Box> tmp_list = new List<Box>();
		//	////	for (int j = 0; j < max_col; j++) {
		//	////		tmp_list.Add(new Box(copy_target[i][j]));
		//	////	}
		//	////	tmp_board.Add(new List<Box>(tmp_list));
		//	////	tmp_list.Clear();
		//	////}


		//	//return tmp_board;
		//}
		private static List<List<Coordinates>> getStateMultiCoordList(List<List<Coordinates>> origin_multi_coord_list)
		{
			List<List<Coordinates>> tmp_iso_group_list = new List<List<Coordinates>>();

			// if (max_row == 0) { return null; }
			// 要素ごとにaddする (インスタンスができる)
			for (int i = 0; i < origin_multi_coord_list.Count; i++) {
				List<Coordinates> tmp_list = new List<Coordinates>();
				for (int j = 0; j < origin_multi_coord_list[i].Count; j++) {
					tmp_list.Add(new Coordinates(origin_multi_coord_list[i][j]));
				}
				tmp_iso_group_list.Add(new List<Coordinates>(tmp_list));
				tmp_list.Clear();
			}
			return tmp_iso_group_list;
		}
		/*** (end) save ***/

		/*** (begin) set ***/
		private static void loadSavedStateNumBoxCoordList(List<Coordinates> co_list)
		{
			if (co_list == null) { return; }
			Tapa.numbox_coord_list.Clear();
			foreach (Coordinates tmp_co in co_list) {
				Tapa.numbox_coord_list.Add(new Coordinates(tmp_co));
			}
		}
		private static void loadSavedStateNotDeployedBoxCoordList(List<Coordinates> co_list)
		{
			if (co_list == null) { return; }
			Tapa.not_deployedbox_coord_list.Clear();
			foreach (Coordinates tmp_co in co_list) {
				Tapa.not_deployedbox_coord_list.Add(new Coordinates(tmp_co));
			}
		}
		private static void loadSavedStateEdgeBlackBoxCoordList(List<Coordinates> co_list)
		{
			if (co_list == null) { return; }
			Tapa.edge_blackbox_coord_list.Clear();
			foreach (Coordinates tmp_co in co_list) {
				Tapa.edge_blackbox_coord_list.Add(new Coordinates(tmp_co));
			}
		}
		//private static void loadSavedStateBoard(Box[][] saved_board)
		//{
		//	if (saved_board == null) { return; }

		//	List<List<Box>> tmp_board = new List<List<Box>>();

		//	int max_row = saved_board.Count;
		//	int max_col = saved_board[0].Count;
		//	for (int i = 0; i < max_row; i++) {
		//		for (int j = 0; j < max_col; j++) {
		//			Tapa.box[i][j] = new Box(saved_board[i][j]);
		//		}
		//	}
		//}
		private static void loadSavedStateIsolationBlackBoxesGroupList(List<List<Coordinates>> saved_iso_group_list)
		{
			if (Tapa.isolation_blackboxes_group_list.Count == 0) { return; }
			if (saved_iso_group_list == null) { return; }

			List<List<Coordinates>> tmp_iso_group_list = new List<List<Coordinates>>();
			int max_row = Tapa.isolation_blackboxes_group_list.Count;

			Tapa.isolation_blackboxes_group_list.Clear();

			for (int ite_iso_group_list = 0; ite_iso_group_list < saved_iso_group_list.Count; ite_iso_group_list++) {
				List<Coordinates> tmp_co_list = new List<Coordinates>();
				foreach (Coordinates tmp_co in saved_iso_group_list[ite_iso_group_list]) {
					tmp_co_list.Add(new Coordinates(tmp_co));
				}
				Tapa.isolation_blackboxes_group_list.Add(new List<Coordinates>(tmp_co_list));
				tmp_co_list.Clear();
			}
		}
		/*
		private static void setSavedStateIsolationNotDeployedBoxesGroupList(List<List<Coordinates>> saved_iso_group_list)
		{
			if (Tapa.isolation_notdeployedboxes_group_list.Count == 0) { return; }
			if (saved_iso_group_list == null) { return; }

			List<List<Coordinates>> tmp_iso_group_list = new List<List<Coordinates>>();
			int max_row = Tapa.isolation_notdeployedboxes_group_list.Count;

			Tapa.isolation_notdeployedboxes_group_list.Clear();

			for (int ite_iso_group_list = 0; ite_iso_group_list < saved_iso_group_list.Count; ite_iso_group_list++) {
				List<Coordinates> tmp_co_list = new List<Coordinates>();
				foreach (Coordinates tmp_co in saved_iso_group_list[ite_iso_group_list]) {
					tmp_co_list.Add(new Coordinates(tmp_co));
				}
				Tapa.isolation_notdeployedboxes_group_list.Add(new List<Coordinates>(tmp_co_list));
				tmp_co_list.Clear();
			}
		}
		 */
		/*** (end) set ***/

		public bool saved_was_change_board { get; set; }

		/*********************************
		 * 
		 * 盤面sのうち、bの座標を未定マスにした盤面を作成する。
		 *   
		 * *******************************/
		public static void makeEditBoard(StateSave s)
		{
			Tapa.resetBoard();
			for (int i = 1; i <= Tapa.MAX_BOARD_ROW; i++) {
				for (int j = 1; j <= Tapa.MAX_BOARD_COL; j++) {
					Tapa.box[i][j].connecting_color = s.saved_box[i][j].Color;
				}
			}
		}
	}
}


﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tapa
{
	class StateSave
	{
		public List<List<Box>> saved_box;
		// 呼びだされた時点での数字マスの座標のリスト
		public List<Coordinates> saved_numbox_coord_list;
		// 呼びだされた時点での未定マスの座標リスト
		public List<Coordinates> saved_not_deployedbox_coord_list;
		// 呼びだされた時点での伸び代のある黒マスの座標リスト
		public List<Coordinates> saved_edge_blackbox_coord_list;
		// 呼びだされた時点での一繋がりの黒マス群の座標リスト
		public List<List<Coordinates>> saved_isolation_blackboxes_group_list;

		/*********************************
		 * 
		 *   リストや盤面の状態を保存
		 *  
		 * *******************************/
		public static void saveNowState(StateSave save_point)
		{
			// 呼びだされた時点での盤面の全てのマス
			save_point.saved_box = StateSave.getStateBoard();
			// 呼びだされた時点での数字マスの座標のリスト
			save_point.saved_numbox_coord_list = getStateCoordList(Tapa.numbox_coord_list);
			// 呼びだされた時点での未定マスの座標リスト
			save_point.saved_not_deployedbox_coord_list = getStateCoordList(Tapa.not_deployedbox_coord_list);
			// 呼びだされた時点での伸び代のある黒マスの座標リスト
			save_point.saved_edge_blackbox_coord_list = getStateCoordList(Tapa.edge_blackbox_coord_list);
			// 呼びだされた時点での一繋がりの黒マス群の座標リスト
			save_point.saved_isolation_blackboxes_group_list = getStateIsolationBlackBoxesGroupList();
		}

		/*********************************
		 * 
		 *   リストや盤面の状態を引数の状態に復元
		 *  
		 * *******************************/
		public static void setSavedState(StateSave save_point)
		{
			setSavedStateBoard(save_point.saved_box);
			setSavedStateEdgeBlackBoxCoordList(save_point.saved_edge_blackbox_coord_list);
			setSavedStateIsolationBlackBoxesGroupList(save_point.saved_isolation_blackboxes_group_list);
			setSavedStateNotDeployedBoxCoordList(save_point.saved_not_deployedbox_coord_list);
			setSavedStateNumBoxCoordList(save_point.saved_numbox_coord_list);
		}

		/*** (begin) save ***/
		public static List<Coordinates> getStateCoordList(List<Coordinates> coord_list)
		{
			if (coord_list == null) { return null; }
			List<Coordinates> tmp_co_list = new List<Coordinates>();

			foreach (Coordinates tmp_co in coord_list) {
				tmp_co_list.Add(new Coordinates(tmp_co));
			}
			return tmp_co_list;
		}
		public static List<List<Box>> getStateBoard()
		{
			if (Tapa.box == null) { return null; }

			List<List<Box>> tmp_board = new List<List<Box>>();

			int max_row = Tapa.MAX_BOARD_ROW;
			int max_col = Tapa.MAX_BOARD_COL;
			// 要素ごとにaddする (インスタンスができる)
			for (int i = 0; i < max_row; i++) {
				List<Box> tmp_list = new List<Box>();
				for (int j = 0; j < max_col; j++) {
					tmp_list.Add(new Box(Tapa.box[i][j]));
				}
				tmp_board.Add(new List<Box>(tmp_list));
				tmp_list.Clear();
			}
			return tmp_board;
		}
		public static List<List<Coordinates>> getStateIsolationBlackBoxesGroupList()
		{
			if (Tapa.isolation_blackboxes_group_list == null) { return null; }

			List<List<Coordinates>> tmp_iso_group_list = new List<List<Coordinates>>();

			int max_row = Tapa.isolation_blackboxes_group_list.Count;
			// if (max_row == 0) { return null; }
			// 要素ごとにaddする (インスタンスができる)
			for (int i = 0; i < max_row; i++) {
				List<Coordinates> tmp_list = new List<Coordinates>();
				for (int j = 0; j < Tapa.isolation_blackboxes_group_list[i].Count; j++) {
					tmp_list.Add(new Coordinates(Tapa.isolation_blackboxes_group_list[i][j]));
				}
				tmp_iso_group_list.Add(new List<Coordinates>(tmp_list));
				tmp_list.Clear();
			}
			return tmp_iso_group_list;
		}
		/*** (end) save ***/

		/*** (begin) set ***/
		public static void setSavedStateNumBoxCoordList(List<Coordinates> co_list)
		{
			if (co_list == null) { return; }
			Tapa.numbox_coord_list.Clear();
			foreach (Coordinates tmp_co in co_list) {
				Tapa.numbox_coord_list.Add(new Coordinates(tmp_co));
			}
		}
		public static void setSavedStateNotDeployedBoxCoordList(List<Coordinates> co_list)
		{
			if (co_list == null) { return; }
			Tapa.not_deployedbox_coord_list.Clear();
			foreach (Coordinates tmp_co in co_list) {
				Tapa.not_deployedbox_coord_list.Add(new Coordinates(tmp_co));
			}
		}
		public static void setSavedStateEdgeBlackBoxCoordList(List<Coordinates> co_list)
		{
			if (co_list == null) { return; }
			Tapa.edge_blackbox_coord_list.Clear();
			foreach (Coordinates tmp_co in co_list) {
				Tapa.edge_blackbox_coord_list.Add(new Coordinates(tmp_co));
			}
		}
		public static void setSavedStateBoard(List<List<Box>> saved_board)
		{
			if (saved_board == null) { return; }

			List<List<Box>> tmp_board = new List<List<Box>>();

			int max_row = saved_board.Count;
			int max_col = saved_board[0].Count;
			for (int i = 0; i < max_row; i++) {
				for (int j = 0; j < max_col; j++) {
					Tapa.box[i][j] = new Box(saved_board[i][j]);
				}
			}
		}
		public static void setSavedStateIsolationBlackBoxesGroupList(List<List<Coordinates>> saved_iso_group_list)
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
		/*** (end) set ***/
	}
}

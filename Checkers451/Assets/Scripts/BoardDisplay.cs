using System.Collections;
using System.Collections.Generic;
using CheckersLogic;
using UnityEngine;

namespace Checkers {
	public class BoardDisplay : MonoBehaviour {

		public TileDisplay[, ] boardDisplay = new TileDisplay[8, 8];
		public GameObject turnMarker;
		public DisplayPiece emptyPiece;

		public static BoardDisplay gameBoardDisplay;

		void Awake() {
			gameBoardDisplay = this;
		}

		void Start() {
			Invoke("LateStart", 1);
		}

		void LateStart() {
			Debug.Log("LateStart");
			foreach (TileDisplay t in boardDisplay) {
				//White
				if (t.row == 0 && (t.col % 2 == 1)) {
					boardDisplay[t.row, t.col].MakePiece(Piece.PieceType.WHITE);

				} else if (t.row == 1 && (t.col % 2 == 0)) {
					boardDisplay[t.row, t.col].MakePiece(Piece.PieceType.WHITE);
				} else if (t.row == 2 && (t.col % 2 == 1)) {
					boardDisplay[t.row, t.col].MakePiece(Piece.PieceType.WHITE);
				}
				//RED
				else if (t.row == 5 && (t.col % 2 == 0)) {
					boardDisplay[t.row, t.col].MakePiece(Piece.PieceType.RED);
				} else if (t.row == 6 && (t.col % 2 == 1)) {
					boardDisplay[t.row, t.col].MakePiece(Piece.PieceType.RED);
				} else if (t.row == 7 && (t.col % 2 == 0)) {
					boardDisplay[t.row, t.col].MakePiece(Piece.PieceType.RED);
				} else {
					boardDisplay[t.row, t.col].MakePiece(Piece.PieceType.EMPTY);
				}

			}

		}

		public TileDisplay getTileDisplay(int row, int col) {
			if (row < 0 || row > 7 || col < 0 || col > 7) {
				return null;
			}
			return boardDisplay[row, col];
		}

		public void applyMove(Move move) {
			Board.gameBoard.applyMove(move);
			TileDisplay fromTile = getTileDisplay(move.from.row, move.from.col);
			TileDisplay toTile = getTileDisplay(move.to.row, move.to.col);

			DisplayPiece piece = GetDisplayPiece(move.from.row, move.from.col);
			piece.transform.position = toTile.transform.position + (Vector3.up * 3);
			piece.row = move.to.row;
			piece.col = move.to.col;

			if (move.jump) {
				DisplayPiece jp = GetDisplayPiece(move.over.row, move.over.col);
				jp.Remove();

			}
			KingPiece(piece);

			UpdateGlow(new List<Tile>());

		}

		private void KingPiece(DisplayPiece p) {

			Debug.Log(p.row+","+p.col+p.piece.type.ToString());

			if (p.piece.getColor() == Piece.PieceType.RED && p.row == 0) {
				p.KingMe();
			} else if (p.piece.getColor() == Piece.PieceType.WHITE && p.row == 7) {
				p.KingMe();
			}
		}

		public void switchPlayer(Player p) {
			
			if (p.name == "Player1") {
				turnMarker.GetComponent<Renderer>().material.color = Color.white;
			} else {
				turnMarker.GetComponent<Renderer>().material.color = Color.red;

			}
		}

		public void RemovePiece(Piece p) {
			Board.gameBoard.RemovePiece(p);
			DisplayPiece dp = GetDisplayPiece(p.row, p.col);
			dp.Remove();

		}

		public void UpdateGlow(List<Tile> tiles) {
			for (int x = 0; x < 8; x++) {
				for (int y = 0; y < 8; y++) {
					if (tiles.Contains(Board.gameBoard.getTile(x, y))) {
						boardDisplay[x, y].GetComponent<Renderer>().material.color = Color.green;
					} else if ((x + y) % 2 == 0) {
						boardDisplay[x, y].GetComponent<Renderer>().material.color = Color.white;
					} else {
						boardDisplay[x, y].GetComponent<Renderer>().material.color = Color.black;
					}
				}
			}
		}

		DisplayPiece GetDisplayPiece(int row, int col) {
			return DisplayPiece.Get(row, col);
		}
	}
}
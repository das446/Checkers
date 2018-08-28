using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Checkers.Network;
using CheckersLogic;
using UnityEngine;

namespace Checkers {
	public class DisplayPiece : MonoBehaviour {

		public int row, col;
		//public GameObject crown;

		static List<DisplayPiece> pieces = new List<DisplayPiece>();

		public Piece piece { get { return Board.gameBoard.getPiece(row, col); } }
		void Start() {
			if (!pieces.Any(p => p == this) && piece.type!=Piece.PieceType.EMPTY && piece.type!=Piece.PieceType.INVALID) {
				pieces.Add(this);
			}
			
		}

		

		void OnMouseDown() {

			if (piece.type.ToString() == "RED_KING" || piece.type.ToString() == "WHITE_KING") {
				//Debug.Log(piece.GetType());
			}

			bool ValidTurn = Player.currentPlayer == Player.local;
			Debug.Log(Player.local);
			bool ValidColor = Player.currentPlayer.color == piece.getColor();
			if (!ValidColor || !ValidTurn) {
				return;
			}

			List<Move> validMoves = Board.gameBoard.getMovesByColor(piece.getColor());
			if (validMoves.Count == 0) { return; }
			validMoves = validMoves.Where(m => m.from.row == row && m.from.col == col).ToList();
			List<Tile> tiles = new List<Tile>();
			BoardDisplay b = BoardDisplay.gameBoardDisplay;
			foreach (Move move in validMoves) {
				Tile t = Board.gameBoard.getTile(move.to.row, move.to.col);
				tiles.Add(t);
			}
			b.UpdateGlow(tiles);
			GameManager.manager.selectedPiece = this;
			Board.gameBoard.currentMoves = validMoves;
		}

		public void KingMe() {
			this.transform.GetChild(0).gameObject.SetActive(true);
		}

		public void Remove() {
			pieces.Remove(this);
			Destroy(gameObject);
		}

		public static DisplayPiece Get(int row, int col) {
			List<DisplayPiece> ps = pieces.Where(p => p.row == row && p.col == col).ToList();
			NetworkManager.debug("c=" + ps.Count);
			return ps.Last();
		}
	}
}
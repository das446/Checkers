using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CheckersLogic;
using UnityEngine;

namespace Checkers {
	public class DisplayPiece : MonoBehaviour {

		public int row, col;
		//public GameObject crown;

		static List<DisplayPiece> pieces = new List<DisplayPiece>();

		public Piece piece { get { return GameManager.manager.gameBoard.getPiece(row, col); } }
		void Start() {
			pieces.Add(this);
		}

		void OnMouseDown() {
            
			if(piece.type.ToString() == "RED_KING" || piece.type.ToString() == "WHITE_KING")
            {
				Debug.Log("KING");
			}

			if (GameManager.manager.currentPlayer.color != piece.getColor()) { return; }

			List<Move> validMoves = GameManager.manager.gameBoard.getMovesByColor(piece.getColor());
			if (validMoves.Count == 0) { return; }
			validMoves = validMoves.Where(m => m.from.row == row && m.from.col == col).ToList();
			List<Tile> tiles = new List<Tile>();
			Board b = GameManager.manager.gameBoard;
			foreach (Move move in validMoves) {
				Tile t = b.getTile(move.to.row, move.to.col);
				tiles.Add(t);
			}
			b.UpdateGlow(tiles);
			GameManager.manager.selectedPiece = this;
			GameManager.manager.gameBoard.currentMoves = validMoves;
		}

		public void KingMe() {
            this.transform.GetChild(0).gameObject.SetActive(true);
		}

		public void Remove() {
			pieces.Remove(this);
			Destroy(gameObject);
		}

		public static DisplayPiece Get(int row, int col) {
			return pieces.Where(p => p.row == row && p.col == col).First();
		}
	}
}
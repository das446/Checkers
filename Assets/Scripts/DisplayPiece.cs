using System.Collections;
using System.Collections.Generic;
using CheckersLogic;
using UnityEngine;

namespace Checkers {
	public class DisplayPiece : MonoBehaviour {

		public int row, col;

		public Piece piece { get { return GameManager.manager.gameBoard.getPiece(row, col); } }
		void Start() {

		}

		void Update() {

		}

		void OnMouseDown() {
			Debug.Log(row + "," + col+" "+ piece.getColor().ToString());

			if (GameManager.manager.currentPlayer.color != piece.getColor()) { return; }
			
			//Debug.Log(getColor());
			List<Move> validMoves = piece.ValidMoves();
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
	}
}
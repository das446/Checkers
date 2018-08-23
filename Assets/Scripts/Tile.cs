using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CheckersLogic {
	public class Tile : MonoBehaviour {

		public Piece piece;
		public Piece displayPiece;
		public int row, col;
		Color color;
		public Piece.PieceType type(){return piece.type;}

		

		/*
		public Tile(int row,int col, Piece piece){
			this.row = row;
			this.col = col;
			this.piece = piece;
		}
		*/

		public Tile(int row, int col, Piece.PieceType type) {
			this.row = row;
			this.col = col;
			this.piece = new Piece(row, col, type);
		}

		void Start() {
			name = "Tile " + row + "," + col;
		}

		public Piece.PieceType GetPieceType() {
			if (piece == null) {
				return Piece.PieceType.EMPTY;
			}
			return piece.type;
		}

		public void SetPiece(Piece p) {
			piece = p;
			if(p.display){
				displayPiece = p;
			}
		}

		public void SetPiece(Piece.PieceType p) {
			piece = new Piece(row,col,p);
		}

		public Piece getPiece() {
			if (piece == null) {
				return new Piece(row, col, Piece.PieceType.EMPTY);
			}
			return piece;
		}

		public void RemovePiece() {
			tile().SetPiece(new Piece(row, col, Piece.PieceType.EMPTY));
			Destroy(gameObject);

		}

		void OnMouseDown() {
			if(GameManager.manager.gameBoard.currentMoves==null){return;}
			if(GameManager.manager.gameBoard.currentMoves.Count==0){return;}

			Move move = GameManager.manager.gameBoard.currentMoves.FirstOrDefault(
				m => m.to.col == col && m.to.row == row
			);
			Debug.Log(move);
			if (move != null) {
				GameManager.manager.gameBoard.applyMove(move);
				//TODO Broadcast move to server instead
			}
		}

		public Tile tile(){
			return GameManager.manager.gameBoard.getTile(row,col);
		}
	}

}
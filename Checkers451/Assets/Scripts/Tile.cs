using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CheckersLogic {
	public class Tile {

		Piece piece;
		public int row, col;
		Color color;
		public Piece.PieceType type(){return piece.type;}

		
		public Tile(int row,int col){
			this.row = row;
			this.col = col;
			this.piece =  new Piece(row, col, Piece.PieceType.EMPTY);
		}
		

		public Tile(int row, int col, Piece.PieceType type) {
			this.row = row;
			this.col = col;
			this.piece = new Piece(row, col, type);
		}

		public Piece.PieceType GetPieceType() {
			return piece.type;
		}

		public void SetPiece(Piece p) {
			piece = p;
		}

		public void SetPiece(Piece.PieceType p) {
			piece = new Piece(row,col,p);
		}

		public Piece getPiece() {
			return piece;
		}

		public void RemovePiece() {
            SetPiece(Piece.PieceType.EMPTY);
			tile().SetPiece(new Piece(row, col, Piece.PieceType.EMPTY));
		}

		

		public Tile tile(){
			return Board.gameBoard.getTile(row,col);
		}
	}

}
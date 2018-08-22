using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CheckersLogic {
	public class Tile : MonoBehaviour{

		public Piece piece;
		public int row,col;
		Color color;
        public Piece.PieceType type;

        /*
		public Tile(int row,int col, Piece piece){
			this.row = row;
			this.col = col;
			this.piece = piece;
		}

		public Tile(int row, int col, Piece.PieceType type){
			this.row = row;
			this.col = col;
			this.piece = new Piece(row,col,type);
		}
        */

		void Start(){
			name = "Tile "+row+","+col;
		}

		public Piece.PieceType GetPieceType(){
			if(piece == null){
				return Piece.PieceType.EMPTY;
			}
			return piece.type;
		}

		public void SetPiece(Piece p){
			piece = p;
		}

		public Piece getPiece(){
			if(piece == null){
				return new Piece(row,col,Piece.PieceType.EMPTY);
			}
			return piece;
		}

		public void RemovePiece(){
			piece = new Piece(row,col,Piece.PieceType.EMPTY);
		}

	}
}
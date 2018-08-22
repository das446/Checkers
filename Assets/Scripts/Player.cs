using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace CheckersLogic {
	public class Player {

		public Piece.PieceType color;
		public string name;

		public Player(Piece.PieceType c){
			color = c;
		}


		public List<Piece> GetPieces(Board board){
			List<Piece> pieces = new List<Piece>();
			return board.GetPieces(color);
			
		}

		public List<Move> ValidMoves(){
			Board board = GameManager.manager.gameBoard;
			return board.getMovesByColor(color);

	}
}
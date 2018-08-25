using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CheckersLogic {
	public class KingPiece : Piece {
        public KingPiece(int row, int col, PieceType type) : base(row, col, type)
        {
			this.row = row;
			this.col = col;
			if (type == Piece.PieceType.RED) {
				type = Piece.PieceType.RED_KING;
			} else if (type == Piece.PieceType.WHITE) {
				type = Piece.PieceType.WHITE_KING;
			} else {
				this.type = type;
			}
        }

		public new bool king(){
            Debug.Log("Checking if king: true");
			return true;
		}

        public new List<Move> ValidMoves(Board b) {
            Debug.Log("checking King moves");
			List<Move> Moves = new List<Move>();
			//Red
			if (getColor() == PieceType.RED) {

				//UpRight

				if (b.getPiece(row - 1, col + 1).getColor() == PieceType.EMPTY) {
					Position from = new Position(row, col);
					Position to = new Position(row - 1, col + 1);
					Moves.Add(new Move(from, to));
				} else if (b.getPiece(row - 1, col + 1).getColor() == PieceType.WHITE && b.getPiece(row - 2, col + 2).getColor() == PieceType.EMPTY) {
					Position from = new Position(row, col);
					Position to = new Position(row - 2, col + 2);
					Position over = new Position(row - 1, col + 1);
					Moves.Add(new Move(from, to, over));
				}

				//UpLeft

				if (b.getPiece(row - 1, col - 1).getColor() == PieceType.EMPTY) {
					Position from = new Position(row, col);
					Position to = new Position(row - 1, col - 1);
					Moves.Add(new Move(from, to));
				} else if (b.getPiece(row - 1, col - 1).getColor() == PieceType.WHITE && b.getPiece(row - 2, col - 2).getColor() == PieceType.EMPTY) {
					Position from = new Position(row, col);
					Position to = new Position(row - 2, col - 2);
					Position over = new Position(row - 1, col - 1);
					Moves.Add(new Move(from, to, over));
				}

				//DownRight
				if (b.getPiece(row + 1, col + 1).getColor() == PieceType.EMPTY) {
					Position from = new Position(row, col);
					Position to = new Position(row + 1, col + 1);
					Moves.Add(new Move(from, to));
				} else if (b.getPiece(row + 1, col + 1).getColor() == PieceType.WHITE && b.getPiece(row + 2, col + 2).getColor() == PieceType.EMPTY) {
					Position from = new Position(row, col);
					Position to = new Position(row + 2, col + 2);
					Position over = new Position(row + 1, col + 1);
					Moves.Add(new Move(from, to, over));
				}

				//DownLeft

				if (b.getPiece(row + 1, col - 1).getColor() == PieceType.EMPTY) {
					Position from = new Position(row, col);
					Position to = new Position(row + 1, col - 1);
					Moves.Add(new Move(from, to));
				} else if (b.getPiece(row + 1, col - 1).getColor() == PieceType.WHITE && b.getPiece(row + 2, col - 2).getColor() == PieceType.EMPTY) {
					Position from = new Position(row, col);
					Position to = new Position(row + 2, col - 2);
					Position over = new Position(row + 1, col - 1);
					Moves.Add(new Move(from, to, over));
				}

			}

			//White
			else if (getColor() == PieceType.WHITE) {

				//DownRight

				if (b.getPiece(row + 1, col + 1).getColor() == PieceType.EMPTY) {
					Position from = new Position(row, col);
					Position to = new Position(row + 1, col + 1);
					Moves.Add(new Move(from, to));
				} else if (b.getPiece(row + 1, col + 1).getColor() == PieceType.RED && b.getPiece(row + 2, col + 2).getColor() == PieceType.EMPTY) {
					Position from = new Position(row, col);
					Position to = new Position(row + 2, col + 2);
					Position over = new Position(row + 1, col + 1);
					Moves.Add(new Move(from, to, over));
				}

				//DownLeft

				if (b.getPiece(row + 1, col - 1).getColor() == PieceType.EMPTY) {
					Position from = new Position(row, col);
					Position to = new Position(row + 1, col - 1);
					Moves.Add(new Move(from, to));
				} else if (b.getPiece(row + 1, col - 1).getColor() == PieceType.RED && b.getPiece(row + 2, col - 2).getColor() == PieceType.EMPTY) {
					Position from = new Position(row, col);
					Position to = new Position(row + 2, col - 2);
					Position over = new Position(row + 1, col - 1);
					Moves.Add(new Move(from, to, over));
				}

				//UpRight

				if (b.getPiece(row - 1, col + 1).getColor() == PieceType.EMPTY) {
					Position from = new Position(row, col);
					Position to = new Position(row - 1, col + 1);
					Moves.Add(new Move(from, to));
				} else if (b.getPiece(row - 1, col + 1).getColor() == PieceType.RED && b.getPiece(row - 2, col + 2).getColor() == PieceType.EMPTY) {
					Position from = new Position(row, col);
					Position to = new Position(row - 2, col + 2);
					Position over = new Position(row - 1, col + 1);
					Moves.Add(new Move(from, to, over));
				}

				//UpLeft

				if (b.getPiece(row - 1, col - 1).getColor() == PieceType.EMPTY) {
					Position from = new Position(row, col);
					Position to = new Position(row - 1, col - 1);
					Moves.Add(new Move(from, to));
				} else if (b.getPiece(row - 1, col - 1).getColor() == PieceType.RED && b.getPiece(row - 2, col - 2).getColor() == PieceType.EMPTY) {
					Position from = new Position(row, col);
					Position to = new Position(row - 2, col - 2);
					Position over = new Position(row - 1, col - 1);
					Moves.Add(new Move(from, to, over));
				}

			}
			return Moves;
		}

	}
}
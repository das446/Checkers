using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CheckersLogic {
	public class KingPiece : Piece {

		public KingPiece(Piece piece) {
			this.row = piece.row;
			this.col = piece.col;
			if (piece.type == Piece.PieceType.BLACK) {
				type = Piece.PieceType.BLACK_KING;
			} else if (piece.type == Piece.PieceType.WHITE) {
				type = Piece.PieceType.WHITE_KING;
			} else {
				type = piece.type;
			}
		}

		public List<Move> ValidMoves(Board b) {
			List<Move> Moves = new List<Move>();
			//Black
			if (getColor() == PieceType.BLACK) {

				//UpRight

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

				//UpLeft

				if (b.getPiece(row - 1, col + 1).getColor() == PieceType.EMPTY) {
					Position from = new Position(row, col);
					Position to = new Position(row - 1, col - 1);
					Moves.Add(new Move(from, to));
				} else if (b.getPiece(row - 1, col + 1).getColor() == PieceType.WHITE && b.getPiece(row - 2, col + 2).getColor() == PieceType.EMPTY) {
					Position from = new Position(row, col);
					Position to = new Position(row - 2, col + 2);
					Position over = new Position(row - 1, col + 1);
					Moves.Add(new Move(from, to, over));
				}

				//DownRight
				if (b.getPiece(row + 1, col - 1).getColor() == PieceType.EMPTY) {
					Position from = new Position(row, col);
					Position to = new Position(row + 1, col - 1);
					Moves.Add(new Move(from, to));
				} else if (b.getPiece(row + 1, col - 1).getColor() == PieceType.BLACK && b.getPiece(row + 2, col - 2).getColor() == PieceType.EMPTY) {
					Position from = new Position(row, col);
					Position to = new Position(row + 2, col - 2);
					Position over = new Position(row + 1, col - 1);
					Moves.Add(new Move(from, to, over));
				}

				//DownLeft

				if (b.getPiece(row + 1, col + 1).getColor() == PieceType.EMPTY) {
					Position from = new Position(row, col);
					Position to = new Position(row + 1, col - 1);
					Moves.Add(new Move(from, to));
				} else if (b.getPiece(row + 1, col + 1).getColor() == PieceType.WHITE && b.getPiece(row + 2, col + 2).getColor() == PieceType.EMPTY) {
					Position from = new Position(row, col);
					Position to = new Position(row + 2, col + 2);
					Position over = new Position(row + 1, col + 1);
					Moves.Add(new Move(from, to, over));
				}

			}

			//White
			else if (getColor() == PieceType.WHITE) {

				//DownRight

				if (b.getPiece(row + 1, col - 1).getColor() == PieceType.EMPTY) {
					Position from = new Position(row, col);
					Position to = new Position(row + 1, col - 1);
					Moves.Add(new Move(from, to));
				} else if (b.getPiece(row + 1, col - 1).getColor() == PieceType.BLACK && b.getPiece(row + 2, col - 2).getColor() == PieceType.EMPTY) {
					Position from = new Position(row, col);
					Position to = new Position(row + 2, col - 2);
					Position over = new Position(row + 1, col - 1);
					Moves.Add(new Move(from, to, over));
				}

				//DownLeft

				if (b.getPiece(row + 1, col + 1).getColor() == PieceType.EMPTY) {
					Position from = new Position(row, col);
					Position to = new Position(row + 1, col - 1);
					Moves.Add(new Move(from, to));
				} else if (b.getPiece(row + 1, col + 1).getColor() == PieceType.BLACK && b.getPiece(row + 2, col + 2).getColor() == PieceType.EMPTY) {
					Position from = new Position(row, col);
					Position to = new Position(row + 2, col + 2);
					Position over = new Position(row + 1, col + 1);
					Moves.Add(new Move(from, to, over));
				}

				//UpRight

				if (b.getPiece(row - 1, col - 1).getColor() == PieceType.EMPTY) {
					Position from = new Position(row, col);
					Position to = new Position(row - 1, col - 1);
					Moves.Add(new Move(from, to));
				} else if (b.getPiece(row - 1, col - 1).getColor() == PieceType.BLACK && b.getPiece(row - 2, col - 2).getColor() == PieceType.EMPTY) {
					Position from = new Position(row, col);
					Position to = new Position(row - 2, col - 2);
					Position over = new Position(row - 1, col - 1);
					Moves.Add(new Move(from, to, over));
				}

				//UpLeft

				if (b.getPiece(row - 1, col + 1).getColor() == PieceType.EMPTY) {
					Position from = new Position(row, col);
					Position to = new Position(row - 1, col - 1);
					Moves.Add(new Move(from, to));
				} else if (b.getPiece(row - 1, col + 1).getColor() == PieceType.BLACK && b.getPiece(row - 2, col + 2).getColor() == PieceType.EMPTY) {
					Position from = new Position(row, col);
					Position to = new Position(row - 2, col + 2);
					Position over = new Position(row - 1, col + 1);
					Moves.Add(new Move(from, to, over));
				}

			}
			return Moves;
		}

	}
}
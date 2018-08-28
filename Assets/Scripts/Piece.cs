using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CheckersLogic {

  public class Piece {

    public int row, col;

    public bool king() {
      return false;
    }

    public enum PieceType {
      INVALID,
      EMPTY,
      WHITE,
      RED,
      WHITE_KING,
      RED_KING
    }

    public PieceType type;

    public Piece(int row, int col, PieceType type) {
      this.row = row;
      this.col = col;
      this.type = type;
    }

    public PieceType getColor() {
      if (type == PieceType.RED_KING) {
        return PieceType.RED;
      }
      if (type == PieceType.WHITE_KING) {
        return PieceType.WHITE;
      }
      return type;
    }

    public virtual List<Move> ValidMoves() {

      return ValidMoves(Board.gameBoard);
    }

    public virtual List<Move> ValidMoves(Board b) {
      List<Move> Moves = new List<Move>();
      if (b.lastMoved().type == type && b.lastMoved() != this) {
        return new List<Move>();
      }
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

      }
      return Moves;
    }

    public bool canJump() {
      return ValidMoves().Any(m => m.jump);
    }

  }
}
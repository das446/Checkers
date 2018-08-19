using System.Collections.Generic;

namespace CheckersLogic {

  public class Piece {

    public int row, col;

    public enum PieceType {
      INVALID,
      EMPTY,
      WHITE,
      BLACK,
      WHITE_KING,
      BLACK_KING
    }

    public PieceType type;

    public Piece(int row, int col, PieceType type) {
      this.row = row;
      this.col = col;
      this.type = type;
    }

    public PieceType getColor() {
      if (type == PieceType.BLACK_KING) {
        return PieceType.BLACK;
      }
      if (type == PieceType.WHITE_KING) {
        return PieceType.WHITE;
      }
      return type;
    }

    public void KingMe(Board b) {
      if (type == PieceType.BLACK) {
        type = PieceType.BLACK_KING;
        b.getTile(row, col).SetPiece(new KingPiece(this));
      } else if (type == PieceType.WHITE) {
        type = PieceType.WHITE_KING;
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

      }
      return Moves;
    }

  }
}
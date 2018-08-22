using System.Collections.Generic;
using System.Collections;
using UnityEngine;

namespace CheckersLogic {

  public class Piece : MonoBehaviour {

    public int row, col;

    private void Start()
    {
        string TileToFind = "Tile " + row + "," + col;
        Tile ownTile = GameObject.Find(TileToFind).GetComponent<Tile>();
        ownTile.piece = this;
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

    public void KingMe(Board b) {
      if (type == PieceType.RED) {
        type = PieceType.RED_KING;
        b.getTile(row, col).SetPiece(new KingPiece(row, col, type));
      } else if (type == PieceType.WHITE) {
        type = PieceType.WHITE_KING;
      }
    }

    public List<Move> ValidMoves() {
      return ValidMoves(GameManager.manager.gameBoard);
    }

    public List<Move> ValidMoves(Board b) {
      List<Move> Moves = new List<Move>();
      //Black
      if (getColor() == PieceType.RED) {

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
        } else if (b.getPiece(row + 1, col - 1).getColor() == PieceType.RED && b.getPiece(row + 2, col - 2).getColor() == PieceType.EMPTY) {
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
        } else if (b.getPiece(row + 1, col + 1).getColor() == PieceType.RED && b.getPiece(row + 2, col + 2).getColor() == PieceType.EMPTY) {
          Position from = new Position(row, col);
          Position to = new Position(row + 2, col + 2);
          Position over = new Position(row + 1, col + 1);
          Moves.Add(new Move(from, to, over));
        }

      }
      return Moves;
    }

    void OnMouseDown() {
      Debug.Log(row + "," + col);
      //Debug.Log(getColor());
      List<Move> validMoves = ValidMoves();
      List<Tile> tiles = new List<Tile>();
      Board b = GameManager.manager.gameBoard;
      foreach (Move move in validMoves) {
        Tile t = b.getTile(move.to.row, move.to.col);
        tiles.Add(t);
      }
      b.UpdateGlow(tiles);
      GameManager.manager.selectedPiece = this;
    }
  }
}
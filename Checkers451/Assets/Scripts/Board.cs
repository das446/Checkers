using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace CheckersLogic {
  public class Board {
    public Tile[, ] board = new Tile[8, 8];

    public Position lastMovedPiece;
    public Move lastMove;
    public bool hasMoves1 = true, hasMoves2 = true;

    public List<Move> currentMoves;

    public static Board gameBoard;

    public Piece lastMoved() {
      if (lastMove == null) { return new Piece(0, 0, Piece.PieceType.INVALID); }
      return board[lastMovedPiece.row, lastMovedPiece.col].getPiece();
    }

    public void resetBoard() {
      
      for (int row = 0; row < 8; row++) {
        for (int col = 0; col < 8; col++) {
          board[row, col] = new Tile(row, col, Piece.PieceType.INVALID);
          //White
          if (row == 0 && (col % 2 == 1)) {
            board[row, col] = new Tile(row, col, Piece.PieceType.WHITE);
          } else if (row == 1 && (col % 2 == 0)) {
            board[row, col] = new Tile(row, col, Piece.PieceType.WHITE);
          } else if (row == 2 && (col % 2 == 1)) {
            board[row, col] = new Tile(row, col, Piece.PieceType.WHITE);
          }

          //Black
          else if (row == 5 && (col % 2 == 0)) {
            board[row, col] = new Tile(row, col, Piece.PieceType.RED);
          } else if (row == 6 && (col % 2 == 1)) {
            board[row, col] = new Tile(row, col, Piece.PieceType.RED);
          } else if (row == 7 && (col % 2 == 0)) {
            board[row, col] = new Tile(row, col, Piece.PieceType.RED);
          } else {
            board[row, col] = new Tile(row, col, Piece.PieceType.EMPTY);
          }

        }
      }
    }

    public Tile getTile(int row, int col) {
      if (row < 0 || row > 7 || col < 0 || col > 7) {
        return new Tile(col, row, Piece.PieceType.INVALID);
      }
      if (board[row, col] == null)
            {
                board[row, col] = new Tile(row, col);
            }
      return board[row, col];
    }


    public void setPiece(int row, int col, Piece.PieceType piece) {
            if (board[row, col] == null)
            {
                board[row, col] = new Tile(row, col, piece);
            }
      board[row, col].SetPiece(piece);
    }

    public Piece getPiece(int row, int col) {
      if (row < 0 || row > 7 || col < 0 || col > 7) {
        return new Piece(row, col, Piece.PieceType.INVALID);
      }
      if(board[row, col] == null)
            {
                board[row, col] = new Tile(row, col, Piece.PieceType.EMPTY);
            }
      Tile t = board[row, col];
      Piece p = t.getPiece();
      return p;
    }

    public void applyMove(Move move) {

      Tile fromTile = getTile(move.from.row, move.from.col);
      Tile toTile = getTile(move.to.row, move.to.col);

      Piece piece = fromTile.getPiece();

      toTile.SetPiece(piece);
      fromTile.SetPiece(new Piece(move.from.row, move.from.col, Piece.PieceType.EMPTY));
      piece.row = toTile.row;
      piece.col = toTile.col;

      if (move.jump) {
        Piece jp = getPiece(move.over.row, move.over.col);
        RemovePiece(jp);

      }

      lastMovedPiece = move.to;
      lastMove = move;
      KingPiece(move.to);
      currentMoves = new List<Move>();

      //Make this dependant on must jump

    }

    public void switchPlayer(Player p) {
      if (p.name == "Player1") {
        Player.currentPlayer = Player.player2;
      } else {
        Player.currentPlayer =Player.player1;

      }
    }

    public void KingPiece(Position pos) {
      Piece p = getPiece(pos.row, pos.col);
      if (p.type == Piece.PieceType.RED && pos.row == 0) {
        //Debug.Log("Kinged RED");
        getTile(pos.row, pos.col).SetPiece(new KingPiece(pos.row, pos.col, Piece.PieceType.RED_KING));
        //Debug.Log("TYPE" + getPiece(pos.row, pos.col).type);
      } else if (p.type == Piece.PieceType.WHITE && pos.row == 7) {
        //Debug.Log("Kinged WHITE");
        getTile(pos.row, pos.col).SetPiece(new KingPiece(pos.row, pos.col, Piece.PieceType.WHITE_KING));
      }
    }

    public void RemovePiece(Piece p) {
      setPiece(p.row, p.col, Piece.PieceType.EMPTY);
    }

    /**
    Only Black and White Supported
    **/
    public List<Move> getMovesByColor(Piece.PieceType color) {
      if (color != Piece.PieceType.RED || color != Piece.PieceType.WHITE) {
        //throw Exception here
        new List<Move>();
      }

      List<Move> moves = new List<Move>();

      for (int row = 0; row < 8; row++) {
        for (int col = 0; col < 8; col++) {
          if (color == getPiece(row, col).getColor()) {
            addMoves(row, col, moves);
          }
        }
      }
      if (moves.Any(move => move.jump)) {
        moves = moves.Where(move => move.jump).ToList();

        if (moves.Any(move => lastMovedPiece == move.to)) {
          moves = moves.Where(move => lastMovedPiece == move.from).ToList();
        }
      }

      return moves;
    }

    public List<Piece> GetPieces(Piece.PieceType color) {
      List<Piece> pieces = new List<Piece>();
      for (int row = 0; row < 8; row++) {
        for (int col = 0; col < 8; col++) {
          if (board[row, col].GetPieceType() == color) {
            pieces.Add(board[row, col].getPiece());
          }
        }
      }
      return pieces;
    }

    private void addMoves(int row, int col, List<Move> moves) {
      List<Move> newMoves = getTile(row, col).getPiece().ValidMoves(this);
      //Debug.Log("addMoves " + getTile(row, col).getPiece().row + "," + getTile(row, col).getPiece().col);
      moves.AddRange(newMoves);
    }

    

  }

}
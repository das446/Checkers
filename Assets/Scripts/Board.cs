using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CheckersLogic {
  public class Board : MonoBehaviour {
    private Tile[, ] board = new Tile[8, 8];
    public Position lastMovedPiece;

    public Piece emptyPiece;

    public Piece lastMoved() {
      return board[lastMovedPiece.row, lastMovedPiece.col].getPiece();
    }

    void Start() {
      int i = 0;
      for (int row = 0; row < 8; row++) {
        for (int col = 0; col < 8; col++) {
          board[row, col] = transform.GetChild(i).GetComponent<Tile>();
          i++;
        }
      }

      foreach (var t in board) {
        //Tile tilePlace = transform.GetChild(i).GetComponent<Tile>();
        board[t.row, t.col].type = Piece.PieceType.INVALID;
        //White
        if (t.row == 0 && (t.col % 2 == 1)) {
          board[t.row, t.col].type = Piece.PieceType.WHITE;

        } else if (t.row == 1 && (t.col % 2 == 0)) {
          board[t.row, t.col].type = Piece.PieceType.WHITE;
        } else if (t.row == 2 && (t.col % 2 == 1)) {
          board[t.row, t.col].type = Piece.PieceType.WHITE;
        }
        //RED
        else if (t.row == 5 && (t.col % 2 == 0)) {
          board[t.row, t.col].type = Piece.PieceType.RED;
        } else if (t.row == 6 && (t.col % 2 == 1)) {
          board[t.row, t.col].type = Piece.PieceType.RED;
        } else if (t.row == 7 && (t.col % 2 == 0)) {
          board[t.row, t.col].type = Piece.PieceType.RED;
        } else {
          board[t.row, t.col].type = Piece.PieceType.EMPTY;
        }

        if (t.piece == null) {
          t.piece = Instantiate(emptyPiece.GetComponent<Piece>());
          t.piece.row = t.row;
          t.piece.col = t.col;
        }
      }
      /*
      foreach(var t in board)
      {
          Debug.Log(t.piece.row + "," + t.piece.col + " type: " + t.piece.type);
      }
      */
      Debug.Log(getPiece(4, 1).type);
    }

    /*
    public void resetBoard() {
      for (int row = 0; row < 8; row++) {
        for (int col = 0; col < 8; col++) {
          board[row, col] = new Tile(row,col,Piece.PieceType.INVALID);
          //White
          if (row == 0 && (col % 2 == 1)) {
            board[row, col] =new Tile(row,col,Piece.PieceType.WHITE);
          }
          else if (row == 1 && (col % 2 == 0)) {
            board[row, col] = new Tile(row,col,Piece.PieceType.WHITE);
          }
          else if (row == 2 && (col % 2 == 1)) {
            board[row, col] = new Tile(row,col,Piece.PieceType.WHITE);
          }

          //Black
          else if (row == 5 && (col % 2 == 0)) {
            board[row, col] = new Tile(row,col,Piece.PieceType.BLACK);
          }
          else if (row == 6 && (col % 2 == 1)) {
            board[row, col] = new Tile(row,col,Piece.PieceType.BLACK);
          }
          else if (row == 7 && (col % 2 == 0)) {
            board[row, col] = new Tile(row,col,Piece.PieceType.BLACK);
          }

          else{
            board[row,col] = new Tile(row,col,Piece.PieceType.EMPTY);
          }
        }
      }
    }
    */
    public Tile getTile(int row, int col) {
      return board[row, col];
    }

    public Piece.PieceType getColor(Piece piece) {
      return piece.getColor();
    }

    public void setPiece(int row, int col, Piece piece) {
      board[row, col].SetPiece(piece);
    }

    public Piece getPiece(int row, int col) {
      if (row < 0 || row > 7 || col < 0 || col > 7)
        return new Piece(row, col, Piece.PieceType.INVALID);
      return board[row, col].getPiece();
    }

    public void applyMove(Move move) {
      board[move.to.row, move.to.col].SetPiece(board[move.from.row, move.from.col].getPiece());
      board[move.from.row, move.from.col].SetPiece(new Piece(move.from.row, move.from.col, Piece.PieceType.EMPTY));
      if (move.jump) {
        board[move.over.row, move.over.col].RemovePiece();
      }
      lastMovedPiece = move.to;
      KingMe(move.to);
    }

    private void KingMe(Position pos) {
      Piece p = board[pos.row, pos.col].getPiece();
      if (p.type == Piece.PieceType.RED && pos.row == 0) {
        p.KingMe(this);
      } else if (p.type == Piece.PieceType.WHITE && pos.row == 7) {
        p.KingMe(this);
      }
    }

    /**
    Only Black and White Supported
    **/
    public List<Move> getMovesByColor(Piece.PieceType color) {
      if (color != Piece.PieceType.RED || color != Piece.PieceType.WHITE) {
        //throw Exception here
        return null;
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
      moves.AddRange(newMoves);
    }

    public void UpdateGlow(List<Tile> tiles) {
      for (int x = 0; x < 8; x++) {
        for (int y = 0; y < 8; y++) {
          if (tiles.Contains(board[x, y])) {
            board[x, y].GetComponent<Renderer>().material.color = Color.green;
          } else if ((x + y) % 2 == 0) {
            board[x, y].GetComponent<Renderer>().material.color = Color.white;
          } else {
            board[x, y].GetComponent<Renderer>().material.color = Color.black;
          }
        }
      }
    }
    public string Print() {
      string board = "";
      for (int row = 0; row < 8; row++) {
        board += "\n   ---------------------------------\n " + (8 - row) + " |";
        for (int col = 0; col < 8; col++) {
          switch (this.board[row, col].getPiece().type) {
            case Piece.PieceType.RED:
              board += " b |";
              break;
            case Piece.PieceType.RED_KING:
              board += " B |";
              break;
            case Piece.PieceType.WHITE:
              board += " w |";
              break;
            case Piece.PieceType.WHITE_KING:
              board += " W |";
              break;
            default:
              board += "   |";
              break;
          }
        }
      }
      board += "\n   ---------------------------------";
      board += "\n   | h | g | f | e | d | c | b | a |";
      return board;
    }
  }

}
using System.Collections.Generic;
using System.Linq;
using Checkers;
using UnityEngine;

namespace CheckersLogic {
  public class Board : MonoBehaviour {
    public Tile[, ] board = new Tile[8, 8];
    public TileDisplay[, ] boardDisplay = new TileDisplay[8, 8];
    public Position lastMovedPiece;

    public DisplayPiece emptyPiece;

    public List<Move> currentMoves;

    public Piece lastMoved() {
      return board[lastMovedPiece.row, lastMovedPiece.col].getPiece();
    }

    void Start() {
      Invoke("LateStart", 1);
    }

    void LateStart() {

      foreach (TileDisplay t in boardDisplay) {
        //White
        if (t.row == 0 && (t.col % 2 == 1)) {
          boardDisplay[t.row, t.col].MakePiece(Piece.PieceType.WHITE);

        } else if (t.row == 1 && (t.col % 2 == 0)) {
          boardDisplay[t.row, t.col].MakePiece(Piece.PieceType.WHITE);
        } else if (t.row == 2 && (t.col % 2 == 1)) {
          boardDisplay[t.row, t.col].MakePiece(Piece.PieceType.WHITE);
        }
        //RED
        else if (t.row == 5 && (t.col % 2 == 0)) {
          boardDisplay[t.row, t.col].MakePiece(Piece.PieceType.RED);
        } else if (t.row == 6 && (t.col % 2 == 1)) {
          boardDisplay[t.row, t.col].MakePiece(Piece.PieceType.RED);
        } else if (t.row == 7 && (t.col % 2 == 0)) {
          boardDisplay[t.row, t.col].MakePiece(Piece.PieceType.RED);
        } else {
          boardDisplay[t.row, t.col].MakePiece(Piece.PieceType.EMPTY);
        }

      }
      /*
      foreach(var t in board)
      {
          Debug.Log(t.piece.row + "," + t.piece.col + " type: " + t.piece.type);
      }
      */
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
      return board[row, col];
    }

    public TileDisplay getTileDisplay(int row, int col) {
      if (row < 0 || row > 7 || col < 0 || col > 7) {
        return null;
      }
      return boardDisplay[row, col];
    }

    public Piece.PieceType getColor(Piece piece) {
      return piece.getColor();
    }

    public void setPiece(int row, int col, Piece piece) {
      board[row, col].SetPiece(piece);
    }

    public Piece getPiece(int row, int col) {
      if (row < 0 || row > 7 || col < 0 || col > 7) {
        return new Piece(row, col, Piece.PieceType.INVALID);
      }
      return board[row, col].getPiece();
    }

    public void applyMove(Move move) {
      Debug.Log(move.to.row+","+move.to.col);
      TileDisplay fromTile = getTileDisplay(move.from.row, move.from.col);
      TileDisplay toTile = getTileDisplay(move.to.row, move.to.col);

      DisplayPiece piece = GameManager.manager.selectedPiece;

      toTile.SetPiece(piece.piece);
      fromTile.SetPiece(new Piece(move.from.row, move.from.col, Piece.PieceType.EMPTY));
      piece.row = toTile.row;
      piece.col = toTile.col;

      Debug.Log(toTile.transform.position + (Vector3.up * 3));
      piece.transform.position = toTile.transform.position + (Vector3.up * 3);

      if (move.jump) {
        board[move.over.row, move.over.col].RemovePiece();
      }
      lastMovedPiece = move.to;
      KingMe(move.to);
      Debug.Log("Moved");
      currentMoves = new List<Move>();

      //Make this dependant on must jump
      UpdateGlow(new List<Tile>());
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
      Debug.Log("Valid Moves = " + tiles.Count);
      for (int x = 0; x < 8; x++) {
        for (int y = 0; y < 8; y++) {
          if (tiles.Contains(board[x, y])) {
            boardDisplay[x, y].GetComponent<Renderer>().material.color = Color.green;
          } else if ((x + y) % 2 == 0) {
            boardDisplay[x, y].GetComponent<Renderer>().material.color = Color.white;
          } else {
            boardDisplay[x, y].GetComponent<Renderer>().material.color = Color.black;
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
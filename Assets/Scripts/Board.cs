using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CheckersLogic {
  public class Board : MonoBehaviour {
    private Tile[, ] board = new Tile[8, 8];
    public Position lastMovedPiece;
    public Board() {
      resetBoard();
    }

    public Piece lastMoved(){
      return board[lastMovedPiece.row,lastMovedPiece.col].getPiece();
    }

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

    public Tile getTile(int row,int col){
      return board[row,col];
    }

    public Piece.PieceType getColor(Piece piece) {
      return piece.getColor();
    }

    public void setPiece(int row, int col, Piece piece) {
      board[row, col].SetPiece(piece);
    }

    public Piece getPiece(int row, int col) {
      if (row < 0 || row > 7 || col < 0 || col > 7)
        return new Piece(row,col,Piece.PieceType.INVALID);
      return board[row, col].getPiece();
    }

    public void applyMove(Move move) {
      board[move.to.row, move.to.col].SetPiece(board[move.from.row, move.from.col].getPiece());
      board[move.from.row, move.from.col].SetPiece(new Piece(move.from.row, move.from.col,Piece.PieceType.EMPTY));
      if(move.jump){
        board[move.over.row,move.over.col].RemovePiece();
      }
      lastMovedPiece = move.to;
      KingMe(move.to);
    }

    private void KingMe(Position pos) {
      Piece p = board[pos.row, pos.col].getPiece(); 
      if (p.type == Piece.PieceType.BLACK && pos.row == 0) {
        p.KingMe(this);
      }
      else if (p.type == Piece.PieceType.WHITE && pos.row == 7) {
        p.KingMe(this);
      }
    }

    /**
    Only Black and White Supported
    **/
    public List<Move> getMovesByColor(Piece.PieceType color) {
      if (color != Piece.PieceType.BLACK || color != Piece.PieceType.WHITE) {
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
      if(moves.Any(move=>move.jump))
      {
        moves = moves.Where(move=>move.jump).ToList();

        if(moves.Any(move =>  lastMovedPiece == move.to)){
          
        }
      }

      return moves;
    }

    public List<Piece> GetPieces(Piece.PieceType color){
      List<Piece> pieces = new List<Piece>();
      for (int row = 0; row < 8; row++)
      {
          for (int col = 0; col < 8; col++)
          {
              if(board[row,col].GetPieceType()==color){
                pieces.Add(board[row,col].getPiece());
              }
          }
      }
      return pieces;
    }

    private void addMoves(int row, int col, List<Move> moves) {
      //Kings can return from the top edge
      List<Move> newMoves = getTile(row,col).getPiece().ValidMoves(this);
      moves.AddRange(newMoves);
    }
    public override string ToString() {
      string board = "";
      for (int row = 0; row < 8; row++) {
        board += "\n   ---------------------------------\n " + (8 - row) + " |";
        for (int col = 0; col < 8; col++) {
          switch (this.board[row, col].getPiece().type) {
            case Piece.PieceType.BLACK:
              board += " b |";
              break;
            case Piece.PieceType.BLACK_KING:
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
      board+="\n   ---------------------------------";
      board+="\n   | h | g | f | e | d | c | b | a |";
      return board;
    }
  }

}
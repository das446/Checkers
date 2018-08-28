using Microsoft.VisualStudio.TestTools.UnitTesting;
using CheckersLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersLogic.Tests
{
    [TestClass()]
    public class BoardTests
    {
        [TestMethod()]
        public void getWhiteMovesTest()
        {
            Board b = SetUpBoard();
            List<Move> moves = b.getMovesByColor(Piece.PieceType.WHITE);
            Assert.AreEqual(7, moves.Count);
        }

        [TestMethod()]
        public void getMustJumpTest()
        {
            Board.gameBoard = new Board();
            Board b = Board.gameBoard;
            b.setPiece(1, 1, Piece.PieceType.WHITE);
            b.setPiece(2, 2, Piece.PieceType.RED);
            b.setPiece(3, 3, Piece.PieceType.RED);

            List<Move> moves = b.getMovesByColor(Piece.PieceType.RED);
            Assert.AreEqual(1, moves.Count);
        }

        [TestMethod()]
        public void getDoubleJumpTest()
        {
            Board.gameBoard = new Board();
            Board b = Board.gameBoard;
            b.lastMovedPiece = new Position(0,0);
            b.setPiece(1, 1, Piece.PieceType.WHITE);
            b.setPiece(2, 2, Piece.PieceType.RED);
            b.setPiece(3, 3, Piece.PieceType.RED);

            List<Move> moves = b.getMovesByColor(Piece.PieceType.RED);
            Assert.AreEqual(1, moves.Count);
        }

        [TestMethod()]
        public void TestGetPieces()
        {
            Board b = SetUpBoard();
            List<Piece> pieces = b.GetPieces(Piece.PieceType.RED);
            Assert.AreEqual(12, pieces.Count);
        }

        [TestMethod()]
        public void TestKingRedPiece()
        {
            Board b = SetUpBoard();
            b.setPiece(0, 0, Piece.PieceType.RED);
            b.KingPiece(new Position(0, 0));
            Assert.AreEqual(Piece.PieceType.RED_KING,b.getPiece(0,0).type);
        }

        [TestMethod()]
        public void TestDontKingRedPiece()
        {
            Board b = SetUpBoard();
            b.setPiece(1, 1, Piece.PieceType.RED);
            b.KingPiece(new Position(1, 1));
            Assert.AreEqual(Piece.PieceType.RED, b.getPiece(1, 1).type);
        }

        [TestMethod()]
        public void TestKingWhitePiece()
        {
            Board b = SetUpBoard();
            b.setPiece(7, 7, Piece.PieceType.WHITE);
            b.KingPiece(new Position(7, 7));
            Assert.AreEqual(Piece.PieceType.WHITE_KING, b.getPiece(7, 7).type);
        }

        [TestMethod()]
        public void TestDontKingWhitePiece()
        {
            Board b = SetUpBoard();
            b.setPiece(6, 6, Piece.PieceType.WHITE);
            b.KingPiece(new Position(6, 6));
            Assert.AreEqual(Piece.PieceType.WHITE, b.getPiece(6, 6).type);
        }

        [TestMethod()]
        public void TestApplyMove()
        {
            Board.gameBoard = new Board();
            Board b = Board.gameBoard;
            b.setPiece(3, 3, Piece.PieceType.RED);
            Position from = new Position(3, 3);
            Position to = new Position(2, 2);
            Move m = new Move(from, to);
            b.applyMove(m);
            Assert.AreEqual(Piece.PieceType.RED, b.getPiece(2, 2).getColor());
        }

        [TestMethod()]
        public void TestApplyMoveJump()
        {
            Board.gameBoard = new Board();
            Board b = Board.gameBoard;
            b.setPiece(3, 3, Piece.PieceType.RED);
            Position from = new Position(3, 3);
            Position to = new Position(1, 1);
            Position over = new Position(2, 2);

            Move m = new Move(from, to, over);
            b.applyMove(m);
            Assert.IsTrue(Piece.PieceType.RED== b.getPiece(1, 1).type && Piece.PieceType.EMPTY == b.getPiece(2, 2).type);
        }

        [TestMethod()]
        public void LastMoveNull()
        {
            Board.gameBoard = new Board();
            Board b = Board.gameBoard;
            Assert.IsNotNull(b.lastMoved());
        }

        [TestMethod()]
        public void SwitchPlayer1()
        {
            Board.gameBoard = new Board();
            Board b = Board.gameBoard;
            Player.player1 = new Player("Player1", Piece.PieceType.RED);
            Player.player2 = new Player("Player2", Piece.PieceType.WHITE);
            Player.currentPlayer = Player.player1;
            b.switchPlayer(Player.currentPlayer);
            Assert.AreEqual("Player2",Player.currentPlayer.name);
        }

        [TestMethod()]
        public void SwitchPlayer2()
        {
            Board.gameBoard = new Board();
            Board b = Board.gameBoard;
            Player.player1 = new Player("Player1", Piece.PieceType.RED);
            Player.player2 = new Player("Player2", Piece.PieceType.WHITE);
            Player.currentPlayer = Player.player2;
            b.switchPlayer(Player.currentPlayer);
            Assert.AreEqual("Player1", Player.currentPlayer.name);
        }

        [TestMethod()]
        public void TestInvalidGetTile()
        {
            Board.gameBoard = new Board();
            Board b = Board.gameBoard;
            Piece p = b.getPiece(10, 10);
            Assert.AreEqual(Piece.PieceType.INVALID, p.type);
        }











        public Board SetUpBoard()
        {
            Board.gameBoard = new Board();
            Board.gameBoard.resetBoard();
            return Board.gameBoard;
        }
    }
}
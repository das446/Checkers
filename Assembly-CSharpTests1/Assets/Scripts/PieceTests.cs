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
    public class PieceTests
    {
        [TestMethod()]
        public void PieceTest()
        {
            Piece p = new Piece(0, 0, Piece.PieceType.RED);
            Assert.IsTrue(p.row==0&&p.col==0&&p.type==Piece.PieceType.RED);
        }

        [TestMethod()]
        public void GetColorRedKing()
        {
            Piece p = new Piece(0, 0, Piece.PieceType.RED_KING);
            Assert.IsTrue(p.row == 0 && p.col == 0 && p.getColor() == Piece.PieceType.RED);
        }

        [TestMethod()]
        public void GetColorWhiteKing()
        {
            Piece p = new Piece(0, 0, Piece.PieceType.WHITE_KING);
            Assert.IsTrue(p.row == 0 && p.col == 0 && p.getColor() == Piece.PieceType.WHITE);
        }

        [TestMethod()]
        public void GetColorNormal()
        {
            Piece p = new Piece(0, 0, Piece.PieceType.WHITE);
            Assert.IsTrue(p.row == 0 && p.col == 0 && p.getColor() == Piece.PieceType.WHITE);
        }

        [TestMethod()]
        public void isKing()
        {
            Piece p = new Piece(0, 0, Piece.PieceType.WHITE);
            Assert.IsFalse(p.king());
        }
        [TestMethod()]
        public void ValidMovesRedNoJump()
        {
            Piece p = new Piece(4, 4, Piece.PieceType.RED);
            Assert.IsTrue(ValidMoves.contains(3, 3));
            Assert.IsTrue(ValidMoves.contains(3, 5));
        }
        [TestMethod()]
        public void ValidMovesWhiteNoJump()
        {
            Piece p = new Piece(4, 4, Piece.PieceType.WHITE);
            Assert.IsTrue(ValidMoves.contains(5, 5));
            Assert.IsTrue(ValidMoves.contains(5, 3));
        }
        [TestMethod()]
        public void ValidMovesWithRedJump()
        {
            Piece k = new Piece(4, 4, Piece.PieceType.RED);
            Piece v = new Piece(3, 5, PieceTest.PieceType.WHITE);
            Piece w = new Piece(3, 3, PieceTest.PieceType.WHITE);
            Assert.IsTrue(ValidMoves.contains(2, 6));
            Assert.IsTrue(ValidMoves.contains(2, 2));
        }
        [TestMethod()]
        public void ValidMovesWithWhiteJump()
        {
            Piece k = new Piece(4, 4, Piece.PieceType.WHITE);
            Piece v = new Piece(5, 5, PieceTest.PieceType.RED);
            Piece w = new Piece(5, 3, PieceTest.PieceType.RED);
            Assert.IsTrue(ValidMoves.contains(6, 6));
            Assert.IsTrue(ValidMoves.contains(6, 2));
        }
        [TestMethod()]
        public void ValidMovesWithKingRedNoJump()
        {
            Piece k = new Piece(4, 4, Piece.PieceType.RED_KING);
            Assert.IsTrue(ValidMoves.contains(3, 5));
            Assert.IsTrue(ValidMoves.contains(3, 3));
            Assert.IsTrue(ValidMoves.contains(5, 5));
            Assert.IsTrue(ValidMoves.contains(5, 3));
        }
        [TestMethod()]
        public void ValidMovesWithKingWhiteNoJump()
        {
            Piece k = new Piece(4, 4, Piece.PieceType.WHITE_KING);
            Assert.IsTrue(ValidMoves.contains(3, 5));
            Assert.IsTrue(ValidMoves.contains(3, 3));
            Assert.IsTrue(ValidMoves.contains(5, 5));
            Assert.IsTrue(ValidMoves.contains(5, 3));
        }
        [TestMethod()]
        public void ValidMovesWithKingRedJump()
        {
            Piece k = new Piece(4, 4, Piece.PieceType.RED_KING);
            Piece l = new Piece(5, 5, Piece.PieceType.WHITE);
            Piece m = new Piece(5, 3, Piece.PieceType.WHITE);
            Piece n = new Piece(3, 5, Piece.PieceType.WHITE);
            Piece o = new Piece(3, 3, Piece.PieceType.WHITE);
            Assert.IsTrue(ValidMoves.contains(6, 6));
            Assert.IsTrue(ValidMoves.contains(6, 2));
            Assert.IsTrue(ValidMoves.contains(2, 6));
            Assert.IsTrue(ValidMoves.contains(2, 2));
        }
        [TestMethod()]
        public void ValidMovesWithKingWhiteJump()
        {
            Piece k = new Piece(4, 4, Piece.PieceType.WHITE_KING);
            Piece l = new Piece(5, 5, Piece.PieceType.RED);
            Piece m = new Piece(5, 3, Piece.PieceType.RED);
            Piece n = new Piece(3, 5, Piece.PieceType.RED);
            Piece o = new Piece(3, 3, Piece.PieceType.RED);
            Assert.IsTrue(ValidMoves.contains(6, 6));
            Assert.IsTrue(ValidMoves.contains(6, 2));
            Assert.IsTrue(ValidMoves.contains(2, 6));
            Assert.IsTrue(ValidMoves.contains(2, 2));
        }
        public Board SetUpBoard()
        {
            Board.gameBoard = new Board();
            Board.gameBoard.resetBoard();
            return Board.gameBoard;
        }
    }
}
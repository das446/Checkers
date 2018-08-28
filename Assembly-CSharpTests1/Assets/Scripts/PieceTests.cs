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






        public Board SetUpBoard()
        {
            Board.gameBoard = new Board();
            Board.gameBoard.resetBoard();
            return Board.gameBoard;
        }
    }
}
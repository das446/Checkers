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
    public class TileTests
    {
        [TestMethod()]
        public void TileTest()
        {
            Tile t = new Tile(0, 0);
            Assert.IsTrue(t.row==0&&t.col==0&& t.getPiece().type == Piece.PieceType.EMPTY);
        }

        [TestMethod()]
        public void TileTest2()
        {
            Tile t = new Tile(0, 0,Piece.PieceType.WHITE);
            Assert.IsTrue(t.row == 0 && t.col == 0&&t.GetPieceType()==Piece.PieceType.WHITE);
        }

        [TestMethod()]
        public void TestSetPiece()
        {
            Tile t = new Tile(0, 0);
            Piece p = new Piece(0, 0,Piece.PieceType.WHITE);
            t.SetPiece(p);
            Assert.IsTrue(t.GetPieceType()== Piece.PieceType.WHITE);
        }

        [TestMethod()]
        public void TestSetType()
        {
            Tile t = new Tile(0, 0);
            t.SetPiece(Piece.PieceType.WHITE);
            Assert.IsTrue(t.type() == Piece.PieceType.WHITE);
        }

        [TestMethod()]
        public void TestRemove()
        {
            SetUpBoard();
            Tile t = new Tile(0, 0, Piece.PieceType.WHITE);
            t.RemovePiece();
            Assert.AreEqual(Piece.PieceType.EMPTY,t.type());
        }

        public Board SetUpBoard()
        {
            Board.gameBoard = new Board();
            Board.gameBoard.resetBoard();
            return Board.gameBoard;
        }


    }
}
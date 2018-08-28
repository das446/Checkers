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
    public class MoveTests
    {
        [TestMethod()]
        public void MoveTest()
        {
            Position p1 = new Position(0, 0);
            Position p2 = new Position(1, 1);
            Move m = new Move(p1, p2);
            Assert.IsNotNull(m);
        }

        [TestMethod()]
        public void MoveTestJ()
        {
            Position p1 = new Position(0, 0);
            Position p2 = new Position(1, 1);
            Position p3 = new Position(2, 2);
            Move m = new Move(p1, p2, p3);
            Assert.IsNotNull(m);
        }

        [TestMethod()]
        public void MoveNetworkString()
        {
            Position p1 = new Position(0, 0);
            Position p2 = new Position(1, 1);
            
            Move m = new Move(p1, p2);
            Assert.AreEqual(m.NetworkString(), "Move|0|0|1|1");
        }

        [TestMethod()]
        public void MoveNetworkStringJ()
        {
            Position p1 = new Position(0, 0);
            Position p2 = new Position(1, 1);
            Position p3 = new Position(2, 2);

            Move m = new Move(p1, p2,p3);
            Assert.AreEqual(m.NetworkString(), "MoveJ|0|0|1|1|2|2");
        }

        [TestMethod()]
        public void FromString()
        {
            string s= "Move|0|0|1|1";
            Move m = Move.fromString(s);
            Assert.IsTrue(m.from.row == 0 && m.from.col == 0 && m.to.row == 1 && m.to.col == 1);
        }

        [TestMethod()]
        public void FromStringJ()
        {
            string s = "MoveJ|0|0|1|1|2|2";
            Move m = Move.fromString(s);
            Assert.IsTrue(m.from.row == 0 && m.from.col == 0 && m.to.row == 1 && m.to.col == 1 && m.over.row==2 && m.over.col==2);
        }

        [TestMethod()]
        public void ToStringTest()
        {
            Position p1 = new Position(0, 0);
            Position p2 = new Position(1, 1);

            Move m = new Move(p1, p2);
            Assert.AreEqual(m.ToString(), "Move|0|0|1|1");
        }

        















    }
}

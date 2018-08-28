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
    }
}
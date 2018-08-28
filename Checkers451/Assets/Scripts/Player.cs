using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CheckersLogic {
	public class Player {

		public Piece.PieceType color;

		public static Player local;

		public string name;

		public static Player player1,player2,currentPlayer;


		public Player(string name,Piece.PieceType c) {
			color = c;
			this.name = name;
			
		}

		

	}
}
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Checkers.Network;
using Checkers;

namespace CheckersLogic {
	public class Player {

		public Piece.PieceType color;

		public static Player local;

		public string name;

		public static Player player1,player2,currentPlayer;


		public Player(string name,Piece.PieceType c) {
			color = c;
			this.name = name;
			Debug.Log(Client.client.clientName);
			if(Client.client.clientName == name){
				local = this;
			}
		}

		public List<Piece> GetPieces(Board board) {
			List<Piece> pieces = new List<Piece>();
			return board.GetPieces(color);

		}

		public List<Move> ValidMoves() {
			Board board = Board.gameBoard;
			return board.getMovesByColor(color);

		}

		public void SendToServer(string s){
			Client.client.Send(s);
		}
	}
}
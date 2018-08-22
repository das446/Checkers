﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CheckersLogic {
	public class Player : MonoBehaviour {

		public Piece.PieceType color;

		public Player(Piece.PieceType c) {
			color = c;
		}

		public List<Piece> GetPieces(Board board) {
			List<Piece> pieces = new List<Piece>();
			return board.GetPieces(color);

		}

		public List<Move> ValidMoves() {
			Board board = GameManager.manager.gameBoard;
			return board.getMovesByColor(color);

		}
	}
}
using System.Collections.Generic;
using UnityEngine;
using Checkers;

namespace CheckersLogic {
    public class GameManager : MonoBehaviour {
        public Board gameBoard;
        public Player player1, player2;
        public Player currentPlayer;
        public DisplayPiece selectedPiece;
        public static GameManager manager;
        void Awake() {
            manager = this;
            currentPlayer = player1;
        }
        public static GameManager getInstance() {
            return manager;
        }

        /// <summary>
        /// Moves the piece
        /// </summary>
        /// <param name="move"></param>
        /// <returns>Whether the move was valid</returns>
        public bool move(Move move) {
            Piece piece = gameBoard.getPiece(move.from.row, move.from.col);
            if (piece.getColor() != currentPlayer.color) {
                return false;
            }
            gameBoard.applyMove(move);
            if (gameBoard.lastMoved().ValidMoves(gameBoard).Count == 0) {
                currentPlayer = currentPlayer == player1? player2 : player1;
            }
            //CheckAnyValid()
            //CheckWin()
            return true;
        }

        public override string ToString() {
            var gameStr = gameBoard.ToString();
            gameStr += "\n Current Player Turn: " + (currentPlayer.color == Piece.PieceType.WHITE? "White": "Black");
            return gameStr;
        }
    }
}
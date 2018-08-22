using System.Collections.Generic;
using UnityEngine;

namespace CheckersLogic {
    public class GameManager : MonoBehaviour {
        public Board gameBoard;
        private Player player1, player2;
        private Player currentPlayer;
        public Piece selectedPiece;
        public static GameManager manager = new GameManager();
        void Start() {
            gameBoard = GameObject.FindObjectOfType<Board>();
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
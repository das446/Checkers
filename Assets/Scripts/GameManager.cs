using System.Collections.Generic;
using Checkers;
using UnityEngine;

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
            Checkers.Network.NetworkManager.debug("CurPlayer="+currentPlayer.name);

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

            gameBoard.applyMove(move);
            if (gameBoard.lastMoved().canJump() && gameBoard.lastMove.jump) {
                //Don't change player
            } else {
                gameBoard.switchPlayer(currentPlayer);
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
using System.Collections.Generic;
using Checkers;
using CheckersLogic;
using UnityEngine;

namespace Checkers {
    public class GameManager : MonoBehaviour {

        public DisplayPiece selectedPiece;
        public static GameManager manager;
        void Awake() {
            manager = this;
            Player.player1 = new Player("Player1", Piece.PieceType.RED);
            Player.player2 = new Player("Player2", Piece.PieceType.WHITE);
            Player.currentPlayer = Player.player1;
            Board.gameBoard = new Board();
            Board.gameBoard.resetBoard();
            Debug.Log(Board.gameBoard.board.Length);

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

            BoardDisplay.gameBoardDisplay.applyMove(move);
            if (Board.gameBoard.lastMoved().canJump() && Board.gameBoard.lastMove.jump) {
                //Don't change player
            } else {
                Board.gameBoard.switchPlayer(Player.currentPlayer);
            }
            //CheckAnyValid()
            //CheckWin()
            return true;
        }

        public override string ToString() {
            var gameStr = Board.gameBoard.ToString();
            gameStr += "\n Current Player Turn: " + (Player.currentPlayer.color == Piece.PieceType.WHITE? "White": "Black");
            return gameStr;
        }
    }
}
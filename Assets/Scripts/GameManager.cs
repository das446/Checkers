using System.Collections.Generic;

namespace CheckersLogic {
    class GameManager {
        private Board gameBoard { get; }
        private Player player1, player2;
        private Player currentPlayer;
        private static GameManager manager = new GameManager();
        private GameManager() {
            gameBoard = new Board();
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
            if(gameBoard.get)
            currentPlayer = currentPlayer == player1? player2 : player1;
            return true;
        }

        public override string ToString() {
            var gameStr = gameBoard.ToString();
            gameStr += "\n Current Player Turn: " + (currentPlayer.color == Piece.PieceType.WHITE? "White": "Black");
            return gameStr;
        }
    }
}
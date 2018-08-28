using System.Collections;
using System.Collections.Generic;
using CheckersLogic;
using UnityEngine;
using System.Linq;
using Checkers.Network;

namespace Checkers {
	public class TileDisplay : MonoBehaviour {

		Tile tile;
		public int row, col;

		void Start() {
			name = "Tile " + row + "," + col;
			Debug.Log(BoardDisplay.gameBoardDisplay);
			BoardDisplay.gameBoardDisplay.boardDisplay[row,col]=this;
		}

		void Update() {

		}

		public DisplayPiece MakePiece(Piece.PieceType t){
			
			Vector3 pos = transform.position+Vector3.up*2;
			DisplayPiece p = Instantiate(BoardDisplay.gameBoardDisplay.emptyPiece,pos,transform.rotation);
			if(t == Piece.PieceType.INVALID || t==Piece.PieceType.EMPTY){
				p.transform.position = Vector3.zero;
			}
			else if(t==Piece.PieceType.RED){
				p.GetComponent<Renderer>().material.color = Color.red;
			}
			p.col = col;
			p.row = row;
			return p;
		}

		public void SetPiece(Piece p) {
			tile.SetPiece(p);
		}

		public void SetPiece(Piece.PieceType p) {
			tile.SetPiece(p);
			Vector3 pos = transform.position + Vector3.up;
		}

		public Piece getPiece() {
			return tile.getPiece();
		}

		void OnMouseDown() {

			if(Board.gameBoard.currentMoves==null){return;}
			if(Board.gameBoard.currentMoves.Count==0){return;}

			Move move = Board.gameBoard.currentMoves.FirstOrDefault(
				m => m.to.col == col && m.to.row == row
			);
			


			if (move != null) {

				Client.client.Send(move.NetworkString());
			}
		}



		
	}
}
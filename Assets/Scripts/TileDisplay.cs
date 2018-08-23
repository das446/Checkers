using System.Collections;
using System.Collections.Generic;
using CheckersLogic;
using UnityEngine;
using System.Linq;

namespace Checkers {
	public class TileDisplay : MonoBehaviour {

		Tile tile;
		public int row, col;

		void Start() {
			name = "Tile " + row + "," + col;
			tile = new Tile(row, col);
			GameManager.manager.gameBoard.boardDisplay[row,col]=this;
			GameManager.manager.gameBoard.board[row,col]=tile;
		}

		void Update() {

		}

		public DisplayPiece MakePiece(Piece.PieceType t){
			
			Vector3 pos = transform.position+Vector3.up*2;
			DisplayPiece p = Instantiate(GameManager.manager.gameBoard.emptyPiece,pos,transform.rotation);
			if(t == Piece.PieceType.INVALID || t==Piece.PieceType.EMPTY){
				p.transform.position = Vector3.zero;
			}
			else if(t==Piece.PieceType.RED){
				p.GetComponent<Renderer>().material.color = Color.red;
			}
			p.col = col;
			p.row = row;
			tile.SetPiece(t);
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
			if(GameManager.manager.gameBoard.currentMoves==null){return;}
			if(GameManager.manager.gameBoard.currentMoves.Count==0){return;}

			Move move = GameManager.manager.gameBoard.currentMoves.FirstOrDefault(
				m => m.to.col == col && m.to.row == row
			);
			Debug.Log(move);
			if (move != null) {
				GameManager.manager.gameBoard.applyMove(move);
				//TODO Broadcast move to server instead
			}
		}

		
	}
}
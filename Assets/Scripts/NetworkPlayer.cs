using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CheckersLogic;

namespace Checkers.Network {
	public class NetworkPlayer : MonoBehaviour{

		public Player player;

		public static Player local;

		void Start () {
			
			
		}

		public Player SetPlayer(string n,Piece.PieceType p){
			player = new Player(n,p);
			if(Client.client.clientName == player.name){
				local = player;
			}
			return player;
		}
		
		void Update () {
			
		}

		public void SendToServer(string s){
			Client.client.Send(s);
		}
	}
}
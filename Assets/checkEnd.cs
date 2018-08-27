using CheckersLogic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkEnd : MonoBehaviour {

    public GameObject p1, p2;
    public GameObject MenuButton;
    public GameObject board;

    private Board b;

	// Use this for initialization
	void Start () {
        b = board.GetComponent<Board>();
	}
	
	// Update is called once per frame
	void Update () {
        if(b.GetPieces(Piece.PieceType.WHITE).Count == 0)// || b.getMovesByColor(Piece.PieceType.WHITE).Count == 0)
        {
            p1.SetActive(true);
            MenuButton.SetActive(true);
        }
        else if (b.GetPieces(Piece.PieceType.RED).Count == 0)// || b.getMovesByColor(Piece.PieceType.RED).Count == 0)
        {
            p2.SetActive(true);
            MenuButton.SetActive(true);
        }
        else
        {
            p1.SetActive(false);
            p2.SetActive(false);
            MenuButton.SetActive(false);
        }

    }
}

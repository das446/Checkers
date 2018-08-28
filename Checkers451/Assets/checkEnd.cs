using CheckersLogic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Checkers;
public class checkEnd : MonoBehaviour {

    public GameObject p1, p2;
    public GameObject MenuButton;
    public GameObject board;

    private Board b;

    private bool loaded = false;

	// Use this for initialization
	void Start () {
        b = Board.gameBoard;
	}
	
	// Update is called once per frame
	void Update () {
        StartCoroutine(checkForEnd());
    }

    public IEnumerator checkForEnd()
    {
        if(!loaded)
        {
            yield return new WaitForSeconds(3);
            loaded = true;
        }
        
        if(b.GetPieces(Piece.PieceType.WHITE).Count == 0 || !b.hasMoves2)
        {
            p1.SetActive(true);
            MenuButton.SetActive(true);
        }
        else if (b.GetPieces(Piece.PieceType.RED).Count == 0 || !b.hasMoves1)
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

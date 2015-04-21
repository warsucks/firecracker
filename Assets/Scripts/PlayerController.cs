using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

	private Vector3 mouseClickPosition;
	private bool mouseClicked;
	private CreateGrid createGrid;
	private List<PuzzlePieceBehavior> puzzlePiecesInLine = new List<PuzzlePieceBehavior>();
	private List < List <PuzzlePieceBehavior> > oldPuzzlePieceLines = new List < List < PuzzlePieceBehavior> > ();

	// Use this for initialization
	void Start () {
		createGrid = FindObjectOfType<CreateGrid>();
		for (int y = 0; y < createGrid.height; y++) 
		{
			for(int x = 0; x< createGrid.width; x++)
			{
				GameObject puzzlePiece = createGrid.GetPuzzlePiece(x,y);
				PuzzlePieceBehavior puzzlePieceBehavior = puzzlePiece.GetComponent<PuzzlePieceBehavior>();
				puzzlePieceBehavior.ClickEvent += PuzzlePieceClickHandler; //adding the function PuzzlePieceClickHandler to be executed when this particular puzzlePieceBehavior dispatches an event
				//add a listener
			}
		}
	}

	private void PuzzlePieceClickHandler (PuzzlePieceBehavior puzzlePieceBehavior)
	{	
		//Debug.Log ("PuzzlePieceClickHandler executed");
		//if the piece is different from the previous one added to the list
		if (puzzlePiecesInLine.Count > 0) 
		{	
			Debug.Log ("Piece is already in line: "+puzzlePieceBehavior.IsInLine ());
			if (puzzlePiecesInLine.Contains (puzzlePieceBehavior) == false && puzzlePieceBehavior.IsInLine () == false) 
			{
				int xDiff = Mathf.Abs (puzzlePiecesInLine [puzzlePiecesInLine.Count - 1].GetX () - puzzlePieceBehavior.GetX ());
				int yDiff = Mathf.Abs (puzzlePiecesInLine [puzzlePiecesInLine.Count - 1].GetY () - puzzlePieceBehavior.GetY ());

				if (xDiff + yDiff <= 1) 
				{
					if (puzzlePieceBehavior.GetColor () == Color.white)
					{
						AddPuzzlePieceToLine (puzzlePieceBehavior);
					} 
					else if (puzzlePieceBehavior.GetColor () == puzzlePiecesInLine [0].GetColor ()) 
					{
						//clear the list of pieces in the line so a new line can start (but they are still displayed as in the line)
						AddPuzzlePieceToLine (puzzlePieceBehavior);
						oldPuzzlePieceLines.Add (puzzlePiecesInLine);
						puzzlePiecesInLine.Clear ();
						//Debug.Log ("After the line was cleared, is the last part still InLine: "+puzzlePieceBehavior.IsInLine()+" and length is: "+puzzlePiecesInLine.Count);
					} 
					else 
					{ //puzzle piece is colored, but not the same color as the first puzzle piece in the line
						ClearLine ();
					}
				}
			}
		}
		else
		{
			if(puzzlePieceBehavior.GetColor () != Color.white)
			{
				AddPuzzlePieceToLine (puzzlePieceBehavior);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		//mouseClicked = Input.GetButtonDown ("Fire1");
		mouseClicked = Input.GetMouseButton (0);
		if (mouseClicked) {
			mouseClickPosition = Input.mousePosition;
		}
	}

	private void AddPuzzlePieceToLine(PuzzlePieceBehavior puzzlePieceBehavior)
	{
		puzzlePiecesInLine.Add (puzzlePieceBehavior);
		puzzlePieceBehavior.ShowInLine ();
	}

	private void ClearLine() //like this line never happened
	{	for (int i = 0; i < puzzlePiecesInLine.Count; i++) {
			puzzlePiecesInLine [i].UnshowInLine ();
		}
		puzzlePiecesInLine.Clear ();
	}

	public bool GetMouseClick ()
	{
		return mouseClicked;
	}

	public Vector3 GetMouseClickPosition(){
		return mouseClickPosition;
	}
	public Color GetFirstPuzzlePieceColor()
	{
		return puzzlePiecesInLine [0].GetColor ();
	}
}
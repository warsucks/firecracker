using UnityEngine;
using System.Collections;

public class PuzzlePieceBehavior : MonoBehaviour {
	//functions of the following format can be assigned to ClickEvent function
	//then when the ClickEvent function is executed, all those assigned functions are executed
	public delegate void ClickHandler (PuzzlePieceBehavior puzzlePieceBehavior);
	public event ClickHandler ClickEvent;

	private CreateGrid createGrid;
	private PlayerController playerController;
	private Color color;

	private int xInGrid;
	private int yInGrid;

	private bool inLine = false;

	// Use this for initialization
	void Start () {
		createGrid = FindObjectOfType<CreateGrid>();
		playerController = FindObjectOfType<PlayerController>();
		int randomValue = (int)(Mathf.Round (Random.value * 10));
		if (randomValue <= 7) 
		{
			color = Color.white;
		} 
		else if (randomValue == 8) 
		{
			color = Color.red;
		}
		else
		{
			color = Color.green;
		}
		renderer.material.color = color;
	}
	
	// Update is called once per frame
	void Update () {

		if (playerController.GetMouseClick ()) 
		{
			//mouse is down
			Vector3 mousePositionScreen = playerController.GetMouseClickPosition();
			mousePositionScreen.z = transform.position.z - Camera.main.transform.position.z;
			//Debug.Log ("Camera z:"+ Camera.main.transform.position.z);
			Vector3 mousePositionWorld = Camera.main.ScreenToWorldPoint(mousePositionScreen);
			Vector3 mousePositionRelative = mousePositionWorld - transform.position;
			//check if the mouse input is inside the gem's area
			
			/*if (playerController.GetMouseClick ()) {
			Debug.Log ("mousePositionScreen: " + mousePositionScreen + " mousePositionWorld: " + mousePositionWorld + " mousePositionRelative: " + mousePositionRelative);
			}*/
			if(
				Mathf.Abs(mousePositionRelative.x) <= createGrid.puzzlePieceSpacing / 2 &&
				Mathf.Abs(mousePositionRelative.y) <= createGrid.puzzlePieceSpacing / 2
			)
			{
				//the mouseclick is inside of the gem area
				if (ClickEvent != null)
				{	//Debug.Log ("Event dispatched");
					ClickEvent(this);
				}
				else
				{
					Debug.Log ("ClickEvent was null");
				}
			}
		}
	}

	public void ShowInLine ()
	{
		inLine = true;
		renderer.material.color = playerController.GetFirstPuzzlePieceColor();
	}

	public void UnshowInLine()
	{
		inLine = false;
		renderer.material.color = color;
	}

	public Color GetColor()
	{
		return color;
	}

	public void SetCoordinates(int theX, int theY)
	{
		xInGrid = theX;
		yInGrid = theY;
	}

	public int GetX()
	{
		return xInGrid;
	}

	public int GetY()
	{
		return yInGrid;
	}

	public bool IsInLine()
	{
		return inLine;
	}
}

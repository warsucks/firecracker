using UnityEngine;
using System.Collections;

public class CreateGrid : MonoBehaviour {
	public int width;
	public int height;
	public string puzzlePiecePrefabName;
	public float puzzlePieceSpacing; //center to center

	private GameObject[,] puzzlePieces;

	// Use this for initialization
	void Awake () {
		GameObject puzzlePiecePrefab = Resources.Load<GameObject> (puzzlePiecePrefabName);
		puzzlePieces = new GameObject[height, width];
		for (int h=0; h<height; h++) {
			for (int w=0; w<width; w++){
				Debug.Log ("w: "+width+" h: "+height);
				puzzlePieces[h, w] = Instantiate(puzzlePiecePrefab, new Vector3(puzzlePieceSpacing/2 + w*puzzlePieceSpacing, puzzlePieceSpacing/2 +h*puzzlePieceSpacing,0), new Quaternion(0,0,0,0)) as GameObject;
				Debug.Log("puzzle piece instantiated");
				puzzlePieces[h, w].GetComponent<PuzzlePieceBehavior>().SetCoordinates(w, h);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public GameObject GetPuzzlePiece(int x,int y)
	{
		return puzzlePieces[y,x];
	}
}

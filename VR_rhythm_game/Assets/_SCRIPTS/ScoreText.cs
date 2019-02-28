using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class holds and updates the score
public class ScoreText : MonoBehaviour {
	public static int score;//Updated by destroyer scripts
	TextMesh textMeshReference;//Hold reference to score counter text component
	// Use this for initialization
	void Start () {
		score = 0;
		textMeshReference = GetComponent<TextMesh> ();
	}
	
	// Update is called once per frame
	void Update () {
		textMeshReference.text = score.ToString ();
	}
}

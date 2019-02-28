using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateCube : MonoBehaviour {
	public GameObject cubeReference;
	GameObject[] cubes = new GameObject[512];
	public float scaleY;

	// Use this for initialization
	void Start () {
		for (int count = 0; count < 512; count++) {
			GameObject cubeReferenceInstance = (GameObject)Instantiate (cubeReference); //reference to hold new cube information
			cubeReferenceInstance.transform.position = this.transform.position; //set origin position 
			cubeReferenceInstance.transform.parent = this.transform; // set origin position
			cubeReferenceInstance.name = "sample" + count; //set a name to check the same
			this.transform.eulerAngles = new Vector3 (0, -0.703125f * count, 0); //rotate cube ***end result will be a circle*** (360/512 = 0.7)
			//this.transform.Rotate(0, -0.703125f * count, 0);
			cubeReferenceInstance.transform.position = Vector3.forward * 10; // move it outward after rotating
			cubes[count] = cubeReferenceInstance; //add new cube to array
		}
	}
	
	// Update is called once per frame
	void Update () {
		for (int count = 0; count < 512; count++) {
			if (cubes != null) {
				//make cube taller based on sample value calculated from Blackman
				//cubes [count].transform.localScale = new Vector3 (0.1f, (AudioVisualiser.samplesFromGet [count] * scaleY) + 2, 0.1f);
			}
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class rotates an object around the Y-Axis at a speed based on spectrum data
public class Orbitter2 : MonoBehaviour {

	public GameObject centreObject;
	public int band;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		transform.RotateAround (centreObject.transform.position, Vector3.up, AudioVisualiser.bufferBand [band]);
	}
}

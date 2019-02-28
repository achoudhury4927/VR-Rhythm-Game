using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSpheres : MonoBehaviour {

	public int band;
	public GameObject spawn;
	public float thresh;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (AudioVisualiser.bufferBand [band] > thresh) {
			Instantiate (spawn, transform.position, transform.rotation);
		}
	}
}

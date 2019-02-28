using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour {
	public int band;
	float min;
	public float debugLine;
	// Use this for initialization
	void Start () {
		min = 0;
		debugLine = 0;
	}
	
	// Update is called once per frame
	void Update () {
		min = +Time.deltaTime;
		float x = Mathf.Cos (min) * AudioVisualiser.bufferBand [band];
		float z = Mathf.Cos (min) * AudioVisualiser.bufferBand [band];
		float y = Mathf.Sin (min) * AudioVisualiser.bufferBand [band];
		debugLine = x;
		transform.position = new Vector3 (5f+x,y, 5f+z);
	}
}

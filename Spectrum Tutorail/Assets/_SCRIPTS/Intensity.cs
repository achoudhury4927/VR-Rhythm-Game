using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intensity : MonoBehaviour {
	public int band;
	public float min;//min intensity
	public float max;//max intensity
	Light l;
	// Use this for initialization
	void Start () {
		l = GetComponent<Light> ();
	}
	
	// Update is called once per frame
	void Update () {
		l.intensity = (AudioVisualiser.bufferBand[band] * (max - min)) + min;
	}
}

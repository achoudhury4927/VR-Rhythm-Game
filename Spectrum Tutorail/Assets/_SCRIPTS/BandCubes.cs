using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This band scales objects in the Y-Axis based on spectrum data
public class BandCubes : MonoBehaviour {

	public int band;
	public float startScale;
	public float scaleMultipliar;
	Material m;

	void Start(){
		m = GetComponent<MeshRenderer> ().material;
	}

	// Update is called once per frame
	void Update () {
		
		transform.localScale = new Vector3 (transform.localScale.x, (AudioVisualiser.bufferBand [band] * scaleMultipliar) + startScale, transform.localScale.z);
		Color c = new Color (AudioVisualiser.bufferBand[band],AudioVisualiser.bufferBand[band],AudioVisualiser.bufferBand[band]);
		m.SetColor ("_EmissionColor", c);
	}
}

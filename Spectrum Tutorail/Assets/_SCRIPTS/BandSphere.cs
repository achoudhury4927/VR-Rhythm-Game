using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class scales an object in all axes based on spectrum data
public class BandSphere : MonoBehaviour {

	public int band;
	public float startScale;
	public float scaleMultipliar;
	Material m;
	// Use this for initialization
	void Start () {
		m = GetComponent<MeshRenderer> ().material;
	}
	
	// Update is called once per frame
	void Update () {
		transform.localScale = new Vector3 ((AudioVisualiser.bufferBand [band] * scaleMultipliar) , (AudioVisualiser.bufferBand [band] * scaleMultipliar), (AudioVisualiser.bufferBand [band] * scaleMultipliar));
		Color c = new Color (AudioVisualiser.bufferBand[band],AudioVisualiser.bufferBand[band],AudioVisualiser.bufferBand[band]);
		m.SetColor ("_EmissionColor", c);
	}
}

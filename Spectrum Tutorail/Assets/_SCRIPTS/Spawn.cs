using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour {

	public int index { get; private set;}
	public int show;
	public void Create(Color color, float opac, int index){
		this.index = index;
		show = index;
		MeshRenderer mesh = GetComponent<MeshRenderer> ();
		color = Color.Lerp (Color.black, color, opac);
		//transform.position = new Vector3 (0f, 2f, 5f);
	}
}

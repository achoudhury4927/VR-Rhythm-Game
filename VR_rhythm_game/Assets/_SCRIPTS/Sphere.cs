using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//This class was modifies from RhythmTool line class
// Simple class containing a GameObject and an index.
// Used to synchronize a GameObject to a frame from RhythmTool.

public class Sphere : MonoBehaviour {

    public int index { get; private set; }
	public Vector3 direction;
    public void Init(Color color, float opacity, int index)
    {
        this.index = index;

        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        color = Color.Lerp(Color.black, color, opacity * .01f);
        meshRenderer.material.SetColor("_TintColor", color);

		direction = transform.position; //Hold its current position to access in AudioObjectsBeats.cs
    }
}

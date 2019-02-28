using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class destroys all objects the user failed to get
public class SampleDespawner : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//This method destoys game objects associated with a tag
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag ("Spawned")) {
			Destroy(other.gameObject);
		}
		if (other.gameObject.CompareTag ("Power")) {
			Destroy(other.gameObject);
		}
		if (other.gameObject.CompareTag ("DoublePoints")) {
			Destroy(other.gameObject);
		}
		if (other.gameObject.CompareTag ("LargeSize")) {
			Destroy(other.gameObject);
		}
		if (other.gameObject.CompareTag ("Minus")) {
			Destroy(other.gameObject);
		}
		if (other.gameObject.CompareTag ("Smaller")) {
			Destroy(other.gameObject);
		}
		if (other.gameObject.CompareTag ("Faster")) {
			Destroy(other.gameObject);
		}
	}
}

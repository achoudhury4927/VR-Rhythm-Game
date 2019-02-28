using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class is used to despawn power objects the user failed to score
public class SampleDespawnerBeats : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//This method destroys the objects associated to a power
	void OnTriggerEnter(Collider other)
	{
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

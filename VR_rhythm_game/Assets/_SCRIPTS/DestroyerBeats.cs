using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class scores points and activates power effects
public class DestroyerBeats : MonoBehaviour {

	public int standardPoint; //Hold standard point amount **MUST BE SET IN UI**
	public ParticleSystem particles; //Hold particle emitter reference **MUST BE SET IN UI**

	// Use this for initialization
	void Start () {
		particles.Stop ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//This method activates game effects based on the tag of the object it collides with
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag ("Spawned")) {
			ScoreText.score += standardPoint;
		}
		if (other.gameObject.CompareTag ("Power")) {
			ScoreText.score += standardPoint;
			StartCoroutine (ActivateSlowMo ());
		}
		if (other.gameObject.CompareTag ("DoublePoints")) {
			standardPoint = standardPoint * 2;
			ScoreText.score += standardPoint;
		}
		if (other.gameObject.CompareTag ("LargeSize")) {
			ScoreText.score += standardPoint;
			AudioObjectsBeats.useLargePrefab = true;
		}
		if (other.gameObject.CompareTag ("Minus")) {
			ScoreText.score -= 100;
		}
		if (other.gameObject.CompareTag ("Smaller")) {
			ScoreText.score -= standardPoint;
			AudioObjectsBeats.useLargePrefab = false;
		}
		if (other.gameObject.CompareTag ("Faster")) {
			ScoreText.score -= standardPoint;
			StartCoroutine (ActivateFaster());
		}
		//Play particles after applying game state changes
		StartCoroutine (PlayParticles());
	}

	//This coroutine plays a particle explosion effect when point is scored
	IEnumerator PlayParticles (){
		particles.Play ();
		yield return new WaitForSeconds (0.1f);
		particles.Stop ();
	}

	//This coroutine reduces the travel speed of the objects for 5 seconds
	IEnumerator ActivateSlowMo(){
		AudioObjectsBeats.travelSpeed = .1f;
		yield return new WaitForSeconds (5f);
		AudioObjectsBeats.travelSpeed = .2f;
	}

	//This coroutine increases the travel speed of the objects for 5 seconds
	IEnumerator ActivateFaster(){
		AudioObjectsBeats.travelSpeed = .5f;
		yield return new WaitForSeconds (5f);
		AudioObjectsBeats.travelSpeed = .2f;
	}
}

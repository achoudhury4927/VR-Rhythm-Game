using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class activates effect and scores points
public class Destroyer : MonoBehaviour {

	private float releaseTime;//Holds how long to keep time slowed down
	private float holdOldTime;//Holds normal travel time
	public int standardPoint;//Holds the value of a standard point
	public float slowTime;//Holds how slow the travel speed is
	public ParticleSystem particles;
	// Use this for initialization
	void Start () {
		particles.Stop (); //Stop particle system running instantly
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//This method activates effect based on the object the player scored
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag ("Spawned")) {
			ScoreText.score += standardPoint;
			//IS SLOWMOTION POWERUP ACTIVE
			if (AudioObjects.travelTime == slowTime) {
				releaseTime -= 1f;
				if (releaseTime == 0f) {
					AudioObjects.travelTime = holdOldTime;
				}
			}
			Destroy(other.gameObject);
		}
		if (other.gameObject.CompareTag ("Power")) {
			ScoreText.score += standardPoint;
			holdOldTime = AudioObjects.travelTime;
			AudioObjects.travelTime = slowTime;
			releaseTime = 5f;
			Destroy(other.gameObject);
		}
		if (other.gameObject.CompareTag ("DoublePoints")) {
			standardPoint = standardPoint * 2;
			ScoreText.score += standardPoint;
			Destroy(other.gameObject);
		}
		if (other.gameObject.CompareTag ("LargeSize")) {
			ScoreText.score += standardPoint;
			AudioObjects.useLargePrefab = true;
			Destroy(other.gameObject);
		}
		if (other.gameObject.CompareTag ("Minus")) {
			ScoreText.score -= 100;
			Destroy(other.gameObject);
		}
		if (other.gameObject.CompareTag ("Smaller")) {
			ScoreText.score -= standardPoint;
			AudioObjects.useLargePrefab = false;
			Destroy(other.gameObject);
		}
		if (other.gameObject.CompareTag ("Faster")) {
			ScoreText.score -= standardPoint;
			AudioObjects.travelTime += 1f;
			Destroy(other.gameObject);
		}
		StartCoroutine (PlayParticles());
	}

	//Plays particle explosion effect when user scores a point
	IEnumerator PlayParticles (){
		particles.Play ();
		yield return new WaitForSeconds (0.1f);
		particles.Stop ();
	}
}

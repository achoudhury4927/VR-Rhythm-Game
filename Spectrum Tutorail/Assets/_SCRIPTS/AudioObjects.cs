using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioObjects : MonoBehaviour {

	//Default Spawn Object
	public GameObject aPrefab;
	public GameObject aPrefabLarge;
	public static bool useLargePrefab; //Controls which default object to spawn

	//Power Objects
	public GameObject powerupPrefab;
	public GameObject powerupDoublePointsPrefab;
	public GameObject powerdownPrefab;
	public GameObject powerdownMinusPointsPrefab;
	public static bool powerSwitch; //Controls which power object to spawn

	//Holds all the Object Spawns
	private List<GameObject> spawned;

	public static float travelTime; //how fast objects should travel
	private float countDelta; //counts how many seconds have passed since last power object

	// Initialise any required variables and start spawning objects
	void Start () {
		useLargePrefab = false; 
		powerSwitch = true;
		travelTime = 20f;
		countDelta = 5f;
		spawned = new List<GameObject> ();
		InvokeRepeating ("addSpawn", 1f, 1f);
	}
	
	// Update is called once per frame
	void Update () {

		//Setting a random y position in the playspace for objects to travel towards
		float targetRandomY = randomSpawnValue ();
		float targetY = targetRandomY / 3; // Divided by 3 as method return 0-6 and play space has height of 0-2

		//Setting a random x position in the playspace for objects to travel towards
		float targetRandomX = randomSpawnValue ();
		float targetRandomReductionX = randomSpawnValue (); //the play area occupies x coordinates -1 to 1. This value is used to go into the negative x coordinates.
		float targetX = (targetRandomX - targetRandomReductionX) / 6;

		//Setting random position for each object to travel towards
		Vector3 targetPosition = new Vector3 (targetX, targetY, 0f);

		//Move objects towards play area, remove any object instance that has been intercepted
		foreach (GameObject g in spawned) {
			if (g == null) {
				spawned.Remove (g);
				continue;
			}
			g.transform.position = Vector3.MoveTowards (g.transform.position, targetPosition/*targetPoint.transform.position */, travelTime * Time.deltaTime);
		}
	}

	//This method spawns a new object
	public void addSpawn(){
		spawned.Add (SpawnAudioObject ());
		return;
	}

	//This method returns an instantiated prefab based on game conditions
	public GameObject SpawnAudioObject(){
		GameObject audioObject;
		if (Mathf.Abs(countDelta) == 0f) {
			float power = randomSpawnValue (); //Randomly retrieve a power up or power down object
			string tag = randomTag (power);
			if (powerSwitch) {
				if (tag.Equals ("DoublePoints")) {
					audioObject = Instantiate (powerupDoublePointsPrefab) as GameObject;
				} else {
					audioObject = Instantiate (powerupPrefab) as GameObject;
				}
			} else {
				if (tag.Equals ("Minus")) {
					audioObject = Instantiate (powerdownMinusPointsPrefab) as GameObject;
				} else {
					audioObject = Instantiate (powerdownPrefab) as GameObject;
				}
			}
			audioObject.gameObject.tag = tag;
			countDelta = 15f;
		} else {
			countDelta -= 1f;
			if (useLargePrefab) {
				audioObject = Instantiate (aPrefabLarge) as GameObject;
			} else {
				audioObject = Instantiate (aPrefab) as GameObject;
			}
			audioObject.gameObject.tag = "Spawned";
		}

		//Spawn in one lane
		audioObject.transform.position = new Vector3 (Random.Range (20f, 35f), Random.Range (0f, 8f), Random.Range (37f, 47f));
		return audioObject;
	}

	//This method returns a random float between 0 and 6
	private float randomSpawnValue(){
		float r;
		return r = Random.Range (0f, 6f);
	}

	//The method returns a random power based on float parameter
	private string randomTag(float r){
		string tag = "Spawned"; //Default case to catch errors
		if (r >= 0 && r < 1) {
			tag = "Power";
			powerSwitch = true;
		}
		if (r >= 1 && r < 2) {
			tag = "DoublePoints";
			powerSwitch = true;
		}
		if (r >= 2 && r <= 3) {
			tag = "LargeSize";
			powerSwitch = true;
			if (useLargePrefab) {
				tag = "Power";
			}
		}
		if (r >= 3 && r <= 4) {
			tag = "Minus";
			powerSwitch = false;
		}
		if (r >= 4 && r <= 5) {
			tag = "Faster";
			powerSwitch = false;
		}
		if (r >= 5 && r <= 6) {
			tag = "Smaller";
			powerSwitch = false;
		}
		return tag;
	}
}

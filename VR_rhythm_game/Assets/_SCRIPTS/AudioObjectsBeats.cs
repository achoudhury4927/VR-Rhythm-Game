using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections.ObjectModel;

//Controls audio feature based spawning behavior, 
//******this class is a modified version of RhythmTool VisualiserExample.cs********

public class AudioObjectsBeats : MonoBehaviour
{
    public RhythmTool rhythmTool;    
    public RhythmEventProvider eventProvider;
    public Text bpmText;

	public List<GameObject> powerList; //Holds all powers currently in play
	public GameObject targetPosition; 

	//Basic Point Objects
	public static float travelSpeed;
    public GameObject spherePrefab;
	public GameObject largeSpherePrefab;
	public static bool useLargePrefab;

	//Power Objects
	public GameObject powerupPrefab;
	public GameObject powerupDoublePointsPrefab;
	public GameObject powerdownPrefab;
	public GameObject powerdownMinusPointsPrefab;
	public static bool powerSwitch; //Controls which power object to spawn

	public GameObject destroyPoint;
    public List<AudioClip> audioClips;
    
	private List<Sphere> spheres;	
    private int currentSong;
    private ReadOnlyCollection<float> magnitudeSmooth;

	//Initialise variables and subscribe to events
	void Start ()
	{		
		currentSong = -1;
		Application.runInBackground = true;
        
		spheres = new List<Sphere>();
		powerList = new List<GameObject> ();
        eventProvider.Onset += OnOnset;
        eventProvider.SongLoaded += OnSongLoaded;
        eventProvider.SongEnded += OnSongEnded;

        magnitudeSmooth = rhythmTool.low.magnitudeSmooth;

		useLargePrefab = false;
		powerSwitch = true;
		travelSpeed = .1f;

		InvokeRepeating ("SpawnPower", 10f, 15f);
        if (audioClips.Count <= 0)
			Debug.LogWarning ("no songs configured");
		else        
			NextSong();		
		
	}	

	//Unchanged from VisualiserExample.cs
	private void OnSongLoaded()
	{          
		rhythmTool.Play ();					
	}

	//Unchanged from VisualiserExample.cs
    private void OnSongEnded()
	{		
		NextSong();	
	}

	//Unchanged from VisualiserExample.cs
	private void NextSong ()
	{
		ClearLines ();
		
		currentSong++;
		
		if (currentSong >= audioClips.Count)
			currentSong = 0;
			
		rhythmTool.audioClip = audioClips [currentSong];	
	}

	//Runs once every frame
	void Update ()
	{	
		if (Input.GetKeyDown (KeyCode.Space))
			NextSong ();

		if (Input.GetKey (KeyCode.Escape))
			Application.Quit ();			

		if (!rhythmTool.songLoaded)        						
			return;		

        UpdateSpheres();
		foreach (GameObject power in powerList) {
			if (power == null) {
				powerList.Remove (power);
				continue;
			}
			power.transform.position = Vector3.MoveTowards (power.transform.position, targetPosition.transform.position, 20f * Time.deltaTime);
		}
    }

	//This method controls the spawning, despawning and movement of the points objects
    private void UpdateSpheres()
    {
		List<Sphere> toRemove = new List<Sphere>();
		foreach(Sphere sphere in spheres)
        {
            if (sphere.index < rhythmTool.currentFrame || sphere.index > rhythmTool.currentFrame + eventProvider.offset)
            {
				if (sphere == null) {
					toRemove.Add (sphere);
					continue;
				} else {
					Destroy(sphere.gameObject);
					toRemove.Add(sphere);
				}
            }
        }

		foreach (Sphere sphere in toRemove)
			if (sphere == null) {
				continue;
			} else {
				spheres.Remove (sphere);
			}

		/*Code Below Unchanged from VisualiserExample.cs */
        float[] cumulativeMagnitudeSmooth = new float[eventProvider.offset + 1];
        float sum = 0;
        for (int i = 0; i < cumulativeMagnitudeSmooth.Length; i++)
        {
            int index = Mathf.Min(rhythmTool.currentFrame + i, rhythmTool.totalFrames-1);

            sum += magnitudeSmooth[index];
            cumulativeMagnitudeSmooth[i] = sum;
        } 
		/*Code Above Unchanged from VisualiserExample.cs */

		foreach (Sphere sphere in spheres)
        {
			if (sphere == null) {
				continue;
			}
			Vector3 pos = sphere.transform.position; //Current Position of sphere
			float magnitude = cumulativeMagnitudeSmooth[sphere.index - rhythmTool.currentFrame] * .003f - magnitudeSmooth[rhythmTool.currentFrame] * .003f * rhythmTool.interpolation;
			sphere.transform.position = sphere.direction * magnitude * travelSpeed;
        }
    }

	//This method spawns object when an Onset High or Low is detected
    private void OnOnset(OnsetType type, Onset onset)
    {
		Beat beat;
		if (onset.rank < 4 || onset.strength < 5)
            return;

        switch (type)
        {
			case OnsetType.Low:
				spheres.Add (CreateSphere (onset.index, Color.blue, onset.strength, onset.strength));
                break;
            case OnsetType.High:
				spheres.Add(CreateSphere(onset.index, Color.yellow, onset.strength, onset.strength));
                break;
        }
    }

	//This method instantiates the correct points object and places it in the lane
    private Sphere CreateSphere(int index, Color color, float opacity, float intensity)
    {
		GameObject sphereObject;
		if (useLargePrefab) {
			sphereObject = Instantiate (largeSpherePrefab) as GameObject;
		} else {
			 sphereObject = Instantiate(spherePrefab) as GameObject;
		}
		sphereObject.transform.position = new Vector3(Random.Range(20f, 35f), Random.Range (0f, 8f), Random.Range (37f, 47f)); //spawn objects in one lane

		Sphere sphere = sphereObject.GetComponent<Sphere>();
        sphere.Init(color, opacity, index);
		sphere.gameObject.tag = "Spawned";

        return sphere;
    }

	//This method instantiates the correct power prefab for the respective power
	public void SpawnPower(){
		GameObject audioObject;
		float power = randomSpawnValue (); 
		string tag = randomTag (power); //Randomly retrieve a power up or power down object
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

		//Spawn in one lane
		audioObject.transform.position = new Vector3 (Random.Range (20f, 35f), Random.Range (0f, 8f), Random.Range (37f, 47f));
		powerList.Add (audioObject);
	}

	//Returns a random float between 0 and 6
	private float randomSpawnValue(){
		float r;
		return r = Random.Range (0f, 6f);
	}
	//Returns a random power based on value in parameter
	private string randomTag(float r){
		string tag = "Spawned"; //Default case
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

	//Unchanged from VisualiserExample.cs
    private void ClearLines()
    {
		foreach (Sphere line in spheres)
            Destroy(line.gameObject);
		
        spheres.Clear();
    }
}

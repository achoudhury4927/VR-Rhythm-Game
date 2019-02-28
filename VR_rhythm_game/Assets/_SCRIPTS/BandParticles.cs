using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class controls the emission of a particle system based on values retrieved by AudioVisualiser
public class BandParticles : MonoBehaviour {

	public int band;//Which band to access ***MUST BE SET IN UI***
	ParticleSystem particle;//Holds particle emitter reference
	ParticleSystem.EmissionModule emission;//Holds emission property of particle emitter
	public float multiplier;//***MUST BE SET IN UI***

	// Use this for initialization
	void Start () {
		particle = GetComponent<ParticleSystem> ();
		emission = particle.emission;
	}
	
	// Update is called once per frame
	void Update () {
		emission.rateOverTime = AudioVisualiser.bufferBand [band] * multiplier;
		emission.rateOverDistance = AudioVisualiser.bufferBand [band] * multiplier;
	}
}

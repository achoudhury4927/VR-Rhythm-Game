using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (AudioSource))]
public class AudioVisualiser : MonoBehaviour {

	AudioSource audioSourceReferenceObject; //holds reference to AudioSource component it is a component of
	float[] samplesFromGetLeft = new float[512]; 
	float[] samplesFromGetRight = new float[512];
	float[] frequencyBand = new float[8]; 
	float[] buffer = new float[8];
	float[] bufferDecrease = new float[8];

	float[] highestValues = new float[8];
	public static float[] band = new float[8];
	public static float[] bufferBand = new float[8];
	// Use this for initialization
	void Start () {
		audioSourceReferenceObject = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		GetSpectrumAudioSource ();
		CreateFrequencyBands ();
		BufferBand ();
		CreateBands ();
	}

	//FFTWindow.BlackmanHarris Formula = 0.35875 - (0.48829 * COS(1.0 * n/N)) + (0.14128 * COS(2.0 * n/N)) - (0.01168 * COS(3.0 * n/N)).
	void GetSpectrumAudioSource() {
		audioSourceReferenceObject.GetSpectrumData (samplesFromGetLeft, 0, FFTWindow.BlackmanHarris);
		audioSourceReferenceObject.GetSpectrumData (samplesFromGetRight, 1, FFTWindow.BlackmanHarris);
	}

	//Updates band values
	void CreateBands(){
		for(int i = 0; i < 8; i++){
			if (frequencyBand [i] > highestValues [i]) {
				highestValues[i] = frequencyBand [i];
			}
			band [i] = frequencyBand [i] / highestValues [i];
			bufferBand [i] = buffer [i] / highestValues [i];
		}
	}

	//This method creates the frequency bands by going through the spectrum samples arrays and averaging the values belong to a band
	void CreateFrequencyBands(){
		//22050/512 = 43
		//Seperate bands based on powers of 2 since samples from GetSpectrumData returns info based on powers of 2
		int count = 0;//Holds reference to latest spectrum index used for calculates so all records earlier are not used for calculating the next band
		for(int i = 0; i < 8; i++){
			float average = 0;
			int setBand = (int)Mathf.Pow (2, i) * 2;//Calculates counter based on band i.e if i is 7 then band 7 values are average of samples[128] to samples[255]

			if (i == 7) {
				setBand = setBand + 2;
			}

			for (int j = 0; j < setBand; j++) {
				average = average + ((samplesFromGetLeft [count] +samplesFromGetRight [count]) * (count + 1));
				count++;
			}
			average = average / count;
			frequencyBand [i] = average * 10;

		}
	}

	//This method produces an effect to make scale reduce slower when closing in on point of origin
	void BufferBand(){
		for (int i = 0; i < 8; ++i) {
			if (frequencyBand [i] > buffer [i]) {
				buffer [i] = frequencyBand [i];
				bufferDecrease [i] = 0.001f;
			}
			if (frequencyBand [i] < buffer [i]) {
				bufferDecrease [i] = (buffer[i] - frequencyBand[i]) / 8;
				buffer [i] = buffer [i] - bufferDecrease [i];
			}
		}
	}
}

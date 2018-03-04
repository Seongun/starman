using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioScripts : MonoBehaviour {

	public int numChannels = 1;

	public float channelValue = 0.0f;
	public float basicVolume = 0.0f;
	const float MAXVOLUME = 1.0f;
	const float MAXSTEREOVOLUME = 1.0f;


	public AudioClip stereoSound;
	public Object[] songs;
	int numSongs;
	int numPickedSongs = 0;
	bool[] songHasPlayed;
	AudioSource[] musicChannels;
	AudioSource stereoChannel;

	// Use this for initialization
	void Start () {


	}

	void Awake(){
		Debug.Log ("awake");


		//load audio clips of mp3 files from "Music" folder
		songs = Resources.LoadAll ("", typeof(AudioClip));
		numSongs = songs.Length;
		Debug.Log ("this happened");
		Debug.Log (songs.Length);

		songHasPlayed = new bool[numSongs];

		//initialize an array that indicates whether a certain song has been played yet
		songHasPlayed = new bool[numSongs];
		for (int i = 0; i < numSongs; i++) {
			songHasPlayed [i] = false;
		}

		//generate music channels( that are of audio source types)
		musicChannels = new AudioSource[numChannels];

		//initialize channels
		for (int i = 0; i < numChannels; i++) {
			AudioSource channel = gameObject.AddComponent<AudioSource>();
			//set the initial song this channel will be playing
		//	channel.clip = songs[pickOneRandomSong()] as AudioClip;
			channel.playOnAwake = false;
			channel.loop = false;
			channel.volume = i==0 ? 1.0f : 0.0f;
			musicChannels [i] = channel;
		}

		//initialize stereo sound
		stereoChannel = gameObject.AddComponent<AudioSource>();
		stereoChannel.clip = stereoSound;
		stereoChannel.playOnAwake = false;
		stereoChannel.loop = true;
		stereoChannel.volume = 0.0f;


	}

	/*

	// Update is called once per frame
	void Update () {

		adjustRadioChannel ();
		playNextSong ();

	}

	//if the station's music has stopped, play the next song
	void playNextSong(){
		
		for (int i = 0; i < numChannels; i++) {
			
			if (! musicChannels[i].isPlaying ) {
				playRandomNewSong (i);
			}
		}

	}

	void playRandomNewSong(int channelNumber){
		
		musicChannels[channelNumber].clip = songs[pickOneRandomSong()] as AudioClip;
		musicChannels [channelNumber].Play ();
	
	}

	int pickOneRandomSong(){

		if (numPickedSongs == songs.Length) {
			for (int i = 0; i < songs.Length; i++) {
				songHasPlayed [i] = false;
			}
		}


		while ( true ) {
			
			int randInt = Random.Range (0, songs.Length);
	
			if (!songHasPlayed [randInt]) {
				songHasPlayed [randInt] = true;
				return randInt;
			}
		
		}
	
	}


	void adjustRadioChannel(){

	
		//loop through channels to set volume for each channel
		for (int i = 0; i < numChannels; i++) {

			//calculate the degree discrepancy for this channel and that of the current channel value.
			//This will be used not only as an indicator for determining the volume, but also whether the sound plays at all.

			float channelChecker = Mathf.Abs( channelValue - (180.0f * i / (float) numChannels ) ) ;

			//this channel is played
			if (channelChecker <= 90.0f / (float)numChannels) {
			
				float changedVolumeRaw = (90.0f / (float)numChannels) - channelChecker;
				float changedVolume = Mathf.Lerp ( 0.0f, MAXVOLUME, 1.0f - ((channelChecker) * (float)numChannels) / 90.0f);

				changeChannelVolume (i, changedVolume + basicVolume);
				

			} else {
				//this code may be redundant, but i'm not too sure how linear the transformation between channels will be.
			
				changeChannelVolume (i, 0);

			}



		}

		//change the volume of the stereo background sound. It's based on sign function.

		float stereoVolumeRaw = Mathf.Sin (2.0f * (float)numChannels * (channelValue - (180.0f * (float)numChannels))) + 1.0f; 
		float stereoVolume = Mathf.Lerp ( 0.0f, MAXSTEREOVOLUME , stereoVolumeRaw / 2.0f);

		changeStereoVolume (stereoVolume);


	}


	void changeChannelVolume(int i, float volume){
		musicChannels [i].volume = volume;

	}

	void changeStereoVolume(float volume){
		stereoChannel.volume = volume;	
	}
	*/
}

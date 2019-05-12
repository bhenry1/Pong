using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audio_Script : MonoBehaviour {

	public AudioClip musicClip;

	public AudioSource musicSrc;

	// Use this for initialization
	void Start () 
	{
		musicSrc.clip = musicClip;
		musicSrc.Play();

	}
	
	// Update is called once per frame
	void Update () 
	{
	}
}

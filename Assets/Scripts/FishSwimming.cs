using UnityEngine;
using System.Collections;

public class FishSwimming : MonoBehaviour
{
	public ParticleSystem splash;
	public ParticleSystem bubbles;


	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public void PlaySplash ()
	{
		splash.Play ();
	}

	public void StopBubbles ()
	{
		bubbles.Stop ();
	}

	public void PlayBubbles ()
	{
		bubbles.Play ();
	}

}

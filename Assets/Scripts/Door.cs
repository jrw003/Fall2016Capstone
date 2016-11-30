using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Door : MonoBehaviour
{

	public int ownHealth = 20;
	public Sprite[] doorSprites = new Sprite[2];
	SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start ()
	{
		spriteRenderer = GetComponent<SpriteRenderer> ();
	}

	void OnTriggerEnter2D (Collider2D other)
    // if enemy enters a trigger check to see what the trigger is attached to and do something
	{
		// if inside player's attack trigger health goes down by 10
		if (other.tag == "PlayerAttack") {
			Debug.Log ("Player Attack");
			spriteRenderer.sprite = doorSprites [1];
			GetComponent<AudioSource> ().Play ();
			ownHealth -= 10;
		}
	}

	// Update is called once per frame
	void Update ()
	{
		Debug.Log ("Door Health = " + ownHealth);
		if (ownHealth <= 0 && !GetComponent<AudioSource> ().isPlaying) {
			//GetComponent<AudioSource> ().Play ();
			Destroy (gameObject);
			if (GameObject.Find ("DoorText") != null)
				GameObject.Find ("DoorText").SetActive (false);
		}
	}
		
}

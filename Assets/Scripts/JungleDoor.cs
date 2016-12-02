using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class JungleDoor : MonoBehaviour
{
	public Sprite openDoor;
	public Image key;
	BoxCollider2D[] boxColliders;
	public Canvas doorText;
	float doorTextTimer;
	bool doorOpen = false;


	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (doorTextTimer > 0f && doorOpen) {
			doorTextTimer -= Time.deltaTime;
		} else if (doorOpen) {
			doorText.enabled = false;
		}
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Player") {
			if (key.enabled) {
				GetComponent<SpriteRenderer> ().sprite = openDoor;
				boxColliders = GetComponents<BoxCollider2D> ();
				foreach (BoxCollider2D boxCollider in boxColliders) {
					boxCollider.enabled = false;
				}
				key.enabled = false;
				doorOpen = true;
				doorTextTimer = 3f;
			}
		}
	}
}

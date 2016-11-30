using UnityEngine;
using System.Collections;

public class ShowCanvas : MonoBehaviour
{
	public bool showDoorText;

	// Use this for initialization
	void Start ()
	{
		GetComponent<Canvas> ().enabled = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Player" && showDoorText) {
			GetComponent<Canvas> ().enabled = true;
			if (GetComponent<CircleCollider2D> () != null) {
				GetComponent<CircleCollider2D> ().enabled = false;
			}
			if (GetComponent<BoxCollider2D> () != null) {
				GetComponent<BoxCollider2D> ().enabled = false;
			}
		}
	}
}

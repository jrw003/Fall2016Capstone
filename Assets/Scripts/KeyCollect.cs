using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class KeyCollect : MonoBehaviour
{
	public Canvas keyText;
	public Image key;
	float textTimer = 0;
	bool keyCollected = false;
	// Use this for initialization
	void Start ()
	{
		keyText.enabled = false;
		key.enabled = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (textTimer > 0.01f && keyCollected) {
			textTimer -= Time.deltaTime;
		} else if (keyCollected) {
			keyText.enabled = false;
		}
	
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Player") {
			Debug.Log ("Trigger entered");
			GetComponent<SpriteRenderer> ().enabled = false;
			GetComponent<BoxCollider2D> ().enabled = false;
			key.enabled = true;
			keyText.enabled = true;
			keyCollected = true;
			textTimer = 3f;
		}
	}
}

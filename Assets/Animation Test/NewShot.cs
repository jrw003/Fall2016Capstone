using UnityEngine;
using System.Collections;

public class NewShot : MonoBehaviour
{

	Vector2 speed;

	Rigidbody2D rb2D;

	void Awake ()
	{
		rb2D = GetComponent<Rigidbody2D> ();
	}

	// Use this for initialization
	void Start ()
	{
		Debug.Log ("NewShot Local Scale = " + transform.localScale);
		if (transform.localScale.x > 0) {
			speed = new Vector2 (10, 0);
		} else {
			speed = new Vector2 (-10, 0);
		}
		Debug.Log ("Speed = " + speed);
		rb2D.velocity = speed;
	}

	// Update is called once per frame
	void Update ()
	{
		Destroy (gameObject, 5f);
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Door" || other.tag == "Wall" || other.tag == "Ground")
			Destroy (gameObject);
	}

}
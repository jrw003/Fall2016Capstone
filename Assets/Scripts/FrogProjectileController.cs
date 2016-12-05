using UnityEngine;
using System.Collections;

public class FrogProjectileController : MonoBehaviour
{
	GameObject player;
	Rigidbody2D rb2d;
	public float xSpeed;
	public float ySpeed;

	// Use this for initialization
	void Start ()
	{
		player = GameObject.Find ("Player");
		rb2d = GetComponent<Rigidbody2D> ();
		if (player.transform.position.x < transform.position.x) {
			xSpeed = -xSpeed;
		}
		rb2d.velocity = new Vector2 (xSpeed, ySpeed);
	}
	
	// Update is called once per frame
	void Update ()
	{
		rb2d.velocity = new Vector2 (xSpeed, rb2d.velocity.y);
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Ground") {
			Destroy (gameObject, 0.5f);
		}
	}
}

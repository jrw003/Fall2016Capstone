using UnityEngine;
using System.Collections;

public class FrogProjectileController : MonoBehaviour
{
	GameObject player;
	public GameObject explosion;
	Rigidbody2D rb2d;
	public float xSpeed;
	public float ySpeed;
	public float upDistance;
	float xPosition;

	// Use this for initialization
	void Start ()
	{
		player = GameObject.Find ("Player");
		rb2d = GetComponent<Rigidbody2D> ();
		xSpeed = Random.Range (5f, 20f);
		ySpeed = Random.Range (10f, 15f);
		if (player.transform.position.x < transform.position.x) {
			Debug.Log ("if statement");
			xSpeed = -xSpeed;
		}
		Debug.Log ("X speed: " + xSpeed);
		rb2d.velocity = new Vector2 (xSpeed, ySpeed);
	}
	
	// Update is called once per frame
	void Update ()
	{
		rb2d.velocity = new Vector2 (xSpeed, rb2d.velocity.y);
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Ground" || other.tag == "Player") {
			Debug.Log ("Transform position of projectile" + transform.position.ToString ());
			Instantiate (explosion, transform.position, Quaternion.identity);
			Destroy (gameObject);
		}

		if (other.tag == "Death")
			Destroy (gameObject);
	}
}

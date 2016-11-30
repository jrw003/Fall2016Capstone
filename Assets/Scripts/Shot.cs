using UnityEngine;
using System.Collections;

public class Shot : MonoBehaviour
{

	public Vector2 speed = new Vector2 (-10, 0);

	Rigidbody2D rb2D;

	void Awake ()
	{
		rb2D = GetComponent<Rigidbody2D> ();
	}

	// Use this for initialization
	void Start ()
	{
		rb2D.velocity = speed * transform.localScale.x;
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

	// Added this comment for git demonstration.

}

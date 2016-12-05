using UnityEngine;
using System.Collections;

public class FrogShooting : MonoBehaviour
{

	Animator anim;
	public GameObject player;
	Rigidbody2D rb2d;
	public GameObject mouth;
	public GameObject projectile;
	public bool facingLeft = false;
	public float shotTime;
	float shotTimer = 1f;

	// Use this for initialization
	void Start ()
	{
		player = GameObject.Find ("Player");
		anim = GetComponent<Animator> ();
		rb2d = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		shotTimer -= Time.deltaTime;

		if (player.transform.position.x > transform.position.x && facingLeft ||
		    player.transform.position.x < transform.position.x && !facingLeft) {
			Reverse ();
		}

		if (shotTimer < 0) {
			anim.SetTrigger ("Shoot");
			shotTimer = shotTime;
		}

	}

	void Reverse ()
	// reverse the direction the enemy is facing by changing the changing the x scale to it's opposite
	{
		facingLeft = !facingLeft;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		//move *= -1;
		transform.localScale = theScale;
	}

	public void ShootProjectile ()
	{
		if (facingLeft) {
			Instantiate (projectile, mouth.transform.position, mouth.transform.rotation);
		}
	}
}

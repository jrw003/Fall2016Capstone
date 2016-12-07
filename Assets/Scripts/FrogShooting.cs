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
	float distanceToPlayer;
	bool frozen = false;
	public int ownHealth;
	bool dead = false;


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
		if (ownHealth <= 0)
			Death ();
		if (!dead) {
			shotTimer -= Time.deltaTime;
			distanceToPlayer = Mathf.Abs (transform.position.x - player.transform.position.x);
			if (player.transform.position.x > transform.position.x && facingLeft ||
			   player.transform.position.x < transform.position.x && !facingLeft) {
				Reverse ();
			}

			if (shotTimer < 0.5 && frozen) {
				anim.SetBool ("Freeze", false);
				frozen = false;
			}

			if (shotTimer < 0 && distanceToPlayer < 30f) {
				anim.SetTrigger ("Shoot");
				Instantiate (projectile, mouth.transform.position, mouth.transform.rotation);
				shotTimer = shotTime;
			}
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

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Freeze" && !frozen) {
			anim.SetBool ("Freeze", true);
			shotTimer = 3f;
			frozen = true;
			Destroy (other.gameObject);
		}

		if (other.tag == "Shot") {
			ownHealth -= 10;
			anim.SetTrigger ("Shot");
			Destroy (other.gameObject);
		}

		if (other.tag == "PlayerAttack") {
			ownHealth -= 20;
			anim.SetTrigger ("Shot");
		}
	}

	void Death ()
	{
		dead = true;
		anim.SetTrigger ("Death");
		Destroy (gameObject, 1f);
	}
}

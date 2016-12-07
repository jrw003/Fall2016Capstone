using UnityEngine;
using System.Collections;

public class FishSwimming : MonoBehaviour
{
	public ParticleSystem splash;
	public ParticleSystem bubbles;
	public GameObject wall;

	Animator anim;
	public GameObject player;
	Rigidbody2D rb2d;
	public GameObject playerHurt;

	public Transform groundCheck;
	public bool groundInFront = true;
	float groundRadius = 0.1f;
	public LayerMask whatIsGround;
	public AudioSource hurtSound;

	public Transform onGroundCheck;
	public bool onGround = true;
	float onGroundRadius = 0.1f;

	public Transform wallCheck;
	public bool wallInFront = false;
	public LayerMask whatIsWall;
	float wallRadius = 0.1f;

	public float maxSpeed;
	public bool followPlayer;
	public bool facingLeft = false;
	//bool grounded = false;
	public bool frozen = false;
	public float lengthOfFreeze = 5f;
	public float move = -1f;
	public int ownHealth;

	public float turnDistance;

	public float knockBack = 5f;
	public float knockBackTime = 0.2f;
	public float knockBackCounter = 0;
	public bool knockBackToRight;

	public bool dead;
	public bool isRobot;
	public ParticleSystem explosion;
	bool exploded = false;

	float xDistanceToPlayer;

	public float timeBetweenJumps = 1.5f;
	float jumpTimer;
	bool ableToJump = true;

	public float timeBetweenAttacks = 1f;
	float attackTimer;
	bool ableToAttack = true;
	public CircleCollider2D circleCollider;
	public HurtPlayer hurtPlayerScript;


	// Use this for initialization
	void Start ()
	{
		player = GameObject.Find ("Player");
		anim = GetComponent<Animator> ();
		rb2d = GetComponent<Rigidbody2D> ();
		circleCollider.enabled = false;
//		ownHealth = 100;
		//anim.SetBool ("Frozen", frozen);
		dead = false;
		if (facingLeft)
			move = -1f;
		else
			move = 1f;
		wall = GameObject.Find ("ShipWall");
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		if (!dead) {
			wallInFront = Physics2D.OverlapCircle (wallCheck.position, wallRadius, whatIsWall);
//		if (facingLeft) {
			rb2d.velocity = new Vector2 (move * maxSpeed, rb2d.velocity.y);
//		} else {
//			rb2d.velocity = new Vector2 (move * maxSpeed, rb2d.velocity.y);
//		}

			if (wallInFront)
				Reverse ();
		}
	}

	void Update ()
	{
		if (ownHealth <= 0 && (!hurtSound.isPlaying) & !dead) {
			death ();
		}

		xDistanceToPlayer = Mathf.Abs (transform.position.x - player.transform.position.x);
		if (xDistanceToPlayer < 5f) {
			if (ableToAttack) {
				//transform.position = new Vector3(transform.position.x, -67.16f, transform.position.z);
				Debug.Log ("Enable collider");
				circleCollider.enabled = true;
				hurtPlayerScript.enabled = true;
				anim.SetTrigger ("Attack");
				attackTimer = timeBetweenAttacks;
				ableToAttack = false;
			} else {
				attackTimer -= Time.deltaTime;
				if (attackTimer < 0f)
					ableToAttack = true;
			}
		} else {
			if (ableToJump) {
				anim.SetTrigger ("Jump");
				jumpTimer = timeBetweenJumps;
				ableToJump = false;
			} else {
				jumpTimer -= Time.deltaTime;
				if (jumpTimer < 0f)
					ableToJump = true;
			}
		}

//		if (ableToAttack) {
//			transform.position = new Vector3(transform.position.x, -67.16f, transform.position.z);
//			anim.SetTrigger ("Attack");
//			attackTimer = timeBetweenAttacks;
//			ableToAttack = false;
//		} else {
//			attackTimer -= Time.deltaTime;
//			if (attackTimer < 0f)
//				ableToAttack = true;
//		}
	}

	void Reverse ()
	// reverse the direction the enemy is facing by changing the changing the x scale to it's opposite
	{
		facingLeft = !facingLeft;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		move *= -1;
		transform.localScale = theScale;
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Shot" && !dead) {
			Debug.Log ("Hit by Shot");
			ownHealth -= 10;
			Debug.Log ("Health = " + ownHealth);
			hurtSound.Play ();
			knockBackCounter = knockBackTime;
			if (other.transform.position.x < transform.position.x) {
				knockBackToRight = true;
			} else {
				knockBackToRight = false;
			}

			Destroy (other.gameObject);
		}
	}

	void death ()
	{
		Debug.Log ("Death");
//		rb2d.GetComponent<SpriteRenderer> ().enabled = false;
//		if (explosion != null && !exploded) {
//			Instantiate (explosion, new Vector3 (gameObject.transform.position.x, -50.93f, gameObject.transform.position.z), gameObject.transform.rotation);
//			exploded = true;
//
//			Destroy (gameObject);
//		}

		dead = true;
		//		if (GetComponent<BoxCollider2D> () != null)
		//			GetComponent<BoxCollider2D> ().isTrigger = true;
		//		if (GetComponent<CircleCollider2D> () != null)
		//			GetComponent<CircleCollider2D> ().isTrigger = true;
		playerHurt.SetActive (false);
//		rb2d.GetComponent<SpriteRenderer> ().color = Color.white;
		anim.SetTrigger ("Death");
		wall.SetActive (false);
		Destroy (gameObject, 2f);
	}

	public void PlaySplash ()
	{
		splash.Play ();
	}

	public void StopBubbles ()
	{
		if (bubbles.isPlaying) {
			bubbles.Stop ();
		}
	}

	public void PlayBubbles ()
	{	
		if (!bubbles.isPlaying)
			bubbles.Play ();
	}

	public void SetSwimmingTransform ()
	{
		transform.position = new Vector3 (transform.position.x, -69.18f, transform.position.z);
	}
}

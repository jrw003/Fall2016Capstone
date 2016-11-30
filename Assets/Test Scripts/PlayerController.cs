using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

	public float maxSpeed = 10f;
	public static int health;
	public static bool won = false;
	public GameObject quill;
	public GameObject freezeShot;
	public Transform startLocation;
	public Transform shotLocation;
	public bool facingRight = true;
	Animator anim;
	Rigidbody2D rb2d;
	bool grounded = false;
	public Transform groundCheck;
	float groundRadius = 0.2f;
	public LayerMask whatIsGround;
	public float jumpForce = 700f;

	public float knockBack;
	public float knockBackTime;
	public float knockBackCounter;
	public bool knockBackToRight;

	//bool attack = false;

	bool doubleJump = false;
	// is false when you can double jump

	public bool onMovingPlatform = false;

	// Use this for initialization
	void Start ()
	{
		anim = GetComponent<Animator> ();
		rb2d = GetComponent<Rigidbody2D> ();
		health = 100;
		won = false;
	}

	// Update is called once per frame
	void FixedUpdate ()
	{
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
		anim.SetBool ("Ground", grounded);

		if (grounded)
			doubleJump = false;

		anim.SetFloat ("vSpeed", rb2d.velocity.y);

		// if you don't want to move when jumping uncomment following 2 lines of code
		// if (!grounded)  
		//  	return;

		float move = Input.GetAxis ("Horizontal");

		anim.SetFloat ("Speed", Mathf.Abs (move));
		if (knockBackCounter <= 0) {
			rb2d.GetComponent<SpriteRenderer> ().color = Color.white;
			rb2d.velocity = new Vector2 (move * maxSpeed, rb2d.velocity.y);
		} else {
			rb2d.GetComponent<SpriteRenderer> ().color = Color.red;
			if (knockBackToRight) {
				rb2d.velocity = new Vector2 (knockBack, knockBack);
			}
			if (!knockBackToRight) {
				rb2d.velocity = new Vector2 (-knockBack, knockBack);
			}
			knockBackCounter -= Time.deltaTime;
		}

		if (move > 0 && !facingRight)
			Reverse ();
		else if (move < 0 && facingRight)
			Reverse ();
	}

	void Update ()
	{
		if ((grounded || !doubleJump) && Input.GetButtonDown ("Jump")) {
			anim.SetBool ("Ground", false);
			rb2d.AddForce (new Vector2 (0, jumpForce));

			if (!doubleJump && !grounded)
				doubleJump = true;
		}

		if (Input.GetButtonDown ("Fire2")) {
			FreezeShot ();
		}

		if (Input.GetButtonDown ("Fire1")) {
			StandardShot ();
		}

		Debug.Log ("Health = " + health);
	}

	void Reverse ()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	public void FreezeShot ()
	{
		//		if (canShoot) {
		//			Instantiate (shot, mouth.position, mouth.rotation);
		//			canShoot = false;
		//			timeBetweenAttacks = 3f;
		//		}
		anim.SetTrigger ("FreezeShot");
		Instantiate (freezeShot, shotLocation.position, shotLocation.rotation);
	}

	void StandardShot ()
	{
		anim.SetTrigger ("Shoot");
		Instantiate (quill, shotLocation.position, shotLocation.rotation);
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "MovingPlatform") {
			ParentPlatform (other.gameObject);
		}

		if (other.tag == "Goal") {
			won = true;
		}

		if (other.tag == "Death")
			transform.position = startLocation.position;
		//  SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);

		//		if (other.tag == "EnemyShot") {
		//			KnightController.health -= 10;
		//			Destroy (other);
		//		}
		//

	}

	void OnTriggerExit2D (Collider2D other)
	{
		if (other.tag == "MovingPlatform") {
			UnParentPlatform (other.gameObject);
		}
	}

	void ParentPlatform (GameObject platform)
	{
		gameObject.transform.parent = platform.transform;
		onMovingPlatform = true;
	}

	void UnParentPlatform (GameObject platform)
	{
		gameObject.transform.parent = null;
		onMovingPlatform = false;
	}
}

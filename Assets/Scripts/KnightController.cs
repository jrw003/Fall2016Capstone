using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class KnightController : MonoBehaviour
{

	public float maxSpeed = 10f;
	public static int health;
	public static bool won = false;
	public GameObject rightStandardShot;
	public GameObject leftStandardShot;
	public GameObject rightFreezeShot;
	public GameObject leftFreezeShot;
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

	public float shotTimer;
	public float shotTime = 1f;
	public bool canShoot = true;
	//bool attack = false;
	public bool doubleJumpAvailable = false;
	public bool doubleJump = false;

	public bool quillShotAvailable = false;
	public bool freezeShotAvailable = false;


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

		if (grounded && doubleJumpAvailable)
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
		shotTimer -= Time.deltaTime;
		if (shotTimer < 0) {
			shotTimer = 0f;
		}
		if ((grounded || (!doubleJump && doubleJumpAvailable)) && Input.GetButtonDown ("Jump")) {
			anim.SetBool ("Ground", false);
			anim.SetTrigger ("Jump");
			rb2d.AddForce (new Vector2 (0, jumpForce));

			if (!doubleJump && !grounded)
				anim.SetTrigger ("Jump");
			doubleJump = true;
		}

		if (Input.GetButtonDown ("Fire2") && freezeShotAvailable && shotTimer <= 0) {
			shotTimer = 1f;
			anim.SetTrigger ("FreezeShot");
		}

		if (Input.GetButtonDown ("Fire1") && quillShotAvailable && shotTimer <= 0) {
			shotTimer = 1f;
			anim.SetTrigger ("Shoot");
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
		Debug.Log ("Freeze Shot");
		if (facingRight)
			Instantiate (rightFreezeShot, shotLocation.position, shotLocation.rotation);
		else
			Instantiate (leftFreezeShot, shotLocation.position, shotLocation.rotation);
	}

	void StandardShot ()
	{
		if (facingRight)
			Instantiate (rightStandardShot, shotLocation.position, shotLocation.rotation);
		else
			Instantiate (leftStandardShot, shotLocation.position, shotLocation.rotation);
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
			SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);

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

	public void setDoubleJumpAvailable (bool available)
	{
		doubleJumpAvailable = available;
	}

	public void setQuillShotAvailable (bool available)
	{
		quillShotAvailable = available;
	}

	public void setFreezeShotAvailable (bool available)
	{
		freezeShotAvailable = available;
	}
}
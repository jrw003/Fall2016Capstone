using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerCntrller : MonoBehaviour
{
	Rigidbody2D rb2d;
	Animator anim;

	public static int health;
	public static bool won = false;

	public GameObject rightStandardShot;
	public GameObject leftStandardShot;
	public GameObject rightFreezeShot;
	public GameObject leftFreezeShot;
	public Transform shotLocation;
	public AudioSource jumpSound;
	int numberOfFreezeShots = 0;
	int numberOfStandardShots = 0;
	public float maxSpeed = 10f;
	public bool facingRight = true;

	bool grounded = false;
	public Transform groundCheck;
	float groundRadius = 0.2f;
	public LayerMask whatIsGround;
	public float jumpForce = 700;

	bool usedDoubleJump = false;

	float shotTimer;

	bool freezeShotAvailable = false;
	bool quillShotAvailable = true;
	bool doubleJumpAvailable = false;

	public bool onMovingPlatform = false;


	public float knockBack;
	public float knockBackTime;
	public float knockBackCounter;
	public bool knockBackToRight;

	// Use this for initialization
	void Start ()
	{
		rb2d = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		shotTimer = 0f;
		won = false;
		health = 100;
		if (SceneManager.GetActiveScene ().name != "Tutorial Final") {
			freezeShotAvailable = true;
			quillShotAvailable = true;
			doubleJumpAvailable = true;
		}
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);

		if (grounded) {
			usedDoubleJump = false;
		}

		anim.SetBool ("Ground", grounded);

		anim.SetFloat ("vSpeed", rb2d.velocity.y);
		//Debug.Log ("vSpeed = " + rb2d.velocity.y);

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

		if (move > 0 && !facingRight) {
			Reverse ();
		} else if (move < 0 && facingRight) {
			Reverse ();
		}
	}

	void Update ()
	{
		if ((grounded || (!usedDoubleJump && doubleJumpAvailable)) && Input.GetButtonDown ("Jump")) {
			anim.SetBool ("Ground", false);
			rb2d.AddForce (new Vector2 (0, jumpForce));
			jumpSound.Play ();

			if (!usedDoubleJump && !grounded) {
				usedDoubleJump = true;
			}
		}

		shotTimer -= Time.deltaTime;
		if (shotTimer < 0f) {
			shotTimer = 0f;
			numberOfFreezeShots = 0;
			numberOfStandardShots = 0;
		}

		if (Input.GetButtonDown ("Fire2") && freezeShotAvailable && shotTimer <= 0) {
			shotTimer = .75f;
			Debug.Log ("SetTrigger Freezeshot");
			anim.SetTrigger ("FreezeShot");
		}

		if (Input.GetButtonDown ("Fire1") && quillShotAvailable && shotTimer <= 0) {
			shotTimer = .75f;
			anim.SetTrigger ("Shoot");
		}

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
		Debug.Log ("Freeze Shot");
		if (numberOfFreezeShots < 1) {
			if (facingRight) {
				Instantiate (rightFreezeShot, shotLocation.position, shotLocation.rotation);
				numberOfFreezeShots = 1;
			} else
				Instantiate (leftFreezeShot, shotLocation.position, shotLocation.rotation);
			numberOfFreezeShots = 1;
		}
	}

	void StandardShot ()
	{
		if (numberOfStandardShots < 1) {
			if (facingRight) {
				Instantiate (rightStandardShot, shotLocation.position, shotLocation.rotation);
				numberOfStandardShots = 1;
			} else {
				Instantiate (leftStandardShot, shotLocation.position, shotLocation.rotation);
				numberOfStandardShots = 1;
			}
		}
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

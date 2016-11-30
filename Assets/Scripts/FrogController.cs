using UnityEngine;
using System.Collections;

public class FrogController : MonoBehaviour
{
	Animator anim;
	public bool jumpAnimationPlaying = false;
	public GameObject player;
	GameObject thisEnemy;
	Rigidbody2D rb2d;

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

	public float maxHorizSpeed;
	public float maxVertSpeed;
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

	void Start ()
	{
		player = GameObject.Find ("Player");
		thisEnemy = gameObject;
		anim = GetComponent<Animator> ();
		rb2d = GetComponent<Rigidbody2D> ();
		ownHealth = 30;
		anim.SetBool ("Frozen", frozen);
		dead = false;
	}

	void FixedUpdate ()
	{
		if (!dead) {
			groundInFront = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
			onGround = Physics2D.OverlapCircle (onGroundCheck.position, onGroundRadius, whatIsGround);
			wallInFront = Physics2D.OverlapCircle (wallCheck.position, wallRadius, whatIsWall);

			if (onGround && !jumpAnimationPlaying) {
				if (facingLeft) {
					anim.SetTrigger ("Jump");
					rb2d.velocity = new Vector2 (move * maxHorizSpeed, maxVertSpeed);   
				} else {
					anim.SetTrigger ("Jump");
					rb2d.velocity = new Vector2 (-move * maxHorizSpeed, maxVertSpeed);
				}
			} else if (!jumpAnimationPlaying) {
				rb2d.velocity = new Vector2 (0, rb2d.velocity.y);
			}

			if ((!groundInFront || wallInFront) && onGround) {
				Reverse ();
			}

//			if (!frozen) {
//				//			if (!groundInFront || wallInFront) {
//				//				Reverse ();
//				//			}
//
//				if (knockBackCounter <= 0) {
//
//					rb2d.GetComponent<SpriteRenderer> ().color = Color.white;
//					if (onGround) {
//						if (facingLeft) {
//							rb2d.velocity = new Vector2 (move * maxHorizSpeed, maxVertSpeed);   
//						} else {
//							rb2d.velocity = new Vector2 (-move * maxHorizSpeed, maxVertSpeed);
//						}
//					} else {
//						rb2d.velocity = new Vector2 (0, rb2d.velocity.y);
//					}
//
//					//rb2d.velocity = new Vector2 (move * maxSpeed, rb2d.velocity.y);
//				} else {
//					rb2d.GetComponent<SpriteRenderer> ().color = Color.red;
//					if (knockBackToRight) {
//						rb2d.velocity = new Vector2 (knockBack, knockBack);
//					}
//					if (!knockBackToRight) {
//						rb2d.velocity = new Vector2 (-knockBack, knockBack);
//					}
//					knockBackCounter -= Time.deltaTime;
//					Debug.Log ("Knock Back Counter = " + knockBackCounter);
//				}
//
//				if ((!groundInFront || wallInFront) && onGround) {
//					Reverse ();
//				}

//
//			} else {
//				lengthOfFreeze -= Time.deltaTime;
//
//				if (lengthOfFreeze < 0f) {
//					frozen = false;
//					gameObject.GetComponentInChildren<HurtPlayer> ().enabled = true;
//					if (thisEnemy.GetComponent<ShootAtPlayer> () != null) {
//						thisEnemy.GetComponent<ShootAtPlayer> ().enabled = true;
//					}
//					anim.SetBool ("Frozen", frozen);
//					lengthOfFreeze = 5f;
//					anim.SetFloat ("Speed", Mathf.Abs (move));
//				}
//			}
		}
	}

	//	void Update ()
	//	{
	//		if (ownHealth <= 0 && (!hurtSound.isPlaying) & !dead) {
	//			death ();
	//		}
	//	}

	void Reverse ()
	// reverse the direction the enemy is facing by changing the changing the x scale to it's opposite
	{
		facingLeft = !facingLeft;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Freeze" && !dead) {
			Debug.Log ("Hit by freeze shot");
			frozen = true;
			gameObject.GetComponentInChildren<HurtPlayer> ().enabled = false;
			if (thisEnemy.GetComponent<ShootAtPlayer> () != null) {
				thisEnemy.GetComponent<ShootAtPlayer> ().enabled = false;
			}
			anim.SetBool ("Frozen", frozen);
			Destroy (other.gameObject);
			anim.SetFloat ("Speed", 0f);
		}

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

		// if inside player's attack trigger health goes down by 10
		if (other.tag == "PlayerAttack" && !dead) {
			Debug.Log ("Player Attack");
			ownHealth -= 15;
			player.GetComponent<Attack> ().setTimer (0f);
			hurtSound.Play ();
			knockBackCounter = knockBackTime;
			if (other.transform.position.x < transform.position.x) {
				knockBackToRight = true;
			} else {
				knockBackToRight = false;
			}

			Debug.Log ("enemy health =  " + ownHealth);
		}

		// if tag is Death destroy the object
		if (other.tag == "Death") {
			Destroy (gameObject);
		}
	}

	public void SetJumpAnimationPlaying ()
	{
		jumpAnimationPlaying = true;
	}

	public void SetJumpAnimationNotPlaying ()
	{
		jumpAnimationPlaying = false;
	}

	//	void death ()
	//	{
	//		if (isRobot) {
	//			rb2d.GetComponent<SpriteRenderer> ().enabled = false;
	//			if (explosion != null && !exploded) {
	//				Instantiate (explosion, gameObject.transform.position, gameObject.transform.rotation);
	//				exploded = true;
	//			}
	//			Destroy (gameObject);
	//		}

	//		dead = true;
	//		if (GetComponent<BoxCollider2D> () != null)
	//			GetComponent<BoxCollider2D> ().isTrigger = true;
	//		if (GetComponent<CircleCollider2D> () != null)
	//			GetComponent<CircleCollider2D> ().isTrigger = true;
	//		rb2d.GetComponent<SpriteRenderer> ().color = Color.white;
	//		anim.SetTrigger ("Death");
	//		Destroy (gameObject, 2f);
	//	}

}

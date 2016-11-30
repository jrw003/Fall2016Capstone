using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/*******************************************************************************************
 * Class to control enemy movement
 * Variables:
 * 		anim : Animator - Animator controller for this enemy
 * 		player: GameObject - The player GameObject
 *		groundCheck : Transform - Transform of ground check object on enemy's feet
 * 		whatIsGround : LayerMask - layer for everything that can be walked on (ground)
 * 		health : Text - GUI Text object for player's health
 * 		maxSpeed : float - X-axis speed
 *		followPlayer : bool - will the enemy follow the player
 * 		facingLeft : bool - is the enemy facing left
 *		grounded : bool - is the enemy on the ground
 * 		groundRadius : float - radius used to check for overlap with the ground and feet
 * 		move : float - direction enemy is moving Left = -1, Right = 1
 * 		attack : bool - is enemy attacking
 * 		meleeAttackDistance : float - distance from player that enemy can melee attack 
 * 		timeBetweenAttacks : float - time between each melee attack
 * 		ownHealth : int - This enemy's health
 * 
 ********************************************************************************************/

public class EnemyMovement : MonoBehaviour
{

	Animator anim;
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

	public float maxSpeed;
	public bool followPlayer;
	public bool facingLeft = true;
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
			if (!frozen) {
				//			if (!groundInFront || wallInFront) {
				//				Reverse ();
				//			}

				if (knockBackCounter <= 0) {
					
					rb2d.GetComponent<SpriteRenderer> ().color = Color.white;
					if (onGround) {
						if (facingLeft) {
							rb2d.velocity = new Vector2 (move * maxSpeed, rb2d.velocity.y);
						} else {
							rb2d.velocity = new Vector2 (-move * maxSpeed, rb2d.velocity.y);
						}
					} else {
						rb2d.velocity = new Vector2 (0, rb2d.velocity.y);
					}

					//rb2d.velocity = new Vector2 (move * maxSpeed, rb2d.velocity.y);
				} else {
					rb2d.GetComponent<SpriteRenderer> ().color = Color.red;
					if (knockBackToRight) {
						rb2d.velocity = new Vector2 (knockBack, knockBack);
					}
					if (!knockBackToRight) {
						rb2d.velocity = new Vector2 (-knockBack, knockBack);
					}
					knockBackCounter -= Time.deltaTime;
					Debug.Log ("Knock Back Counter = " + knockBackCounter);
				}

				if ((!groundInFront || wallInFront) && onGround) {
					Reverse ();
				}


			} else {
				lengthOfFreeze -= Time.deltaTime;

				if (lengthOfFreeze < 0f) {
					frozen = false;
					gameObject.GetComponentInChildren<HurtPlayer> ().enabled = true;
					if (thisEnemy.GetComponent<ShootAtPlayer> () != null) {
						thisEnemy.GetComponent<ShootAtPlayer> ().enabled = true;
					}
					anim.SetBool ("Frozen", frozen);
					lengthOfFreeze = 5f;
					anim.SetFloat ("Speed", Mathf.Abs (move));
				}
			}
		}
	}

	void Update ()
	{
		if (ownHealth <= 0 && (!hurtSound.isPlaying) & !dead) {
			death ();
		}
	}

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

	void death ()
	{
		if (isRobot) {
			rb2d.GetComponent<SpriteRenderer> ().enabled = false;
			if (explosion != null && !exploded) {
				Instantiate (explosion, gameObject.transform.position, gameObject.transform.rotation);
				exploded = true;
			}
			Destroy (gameObject);
		}

		dead = true;
//		if (GetComponent<BoxCollider2D> () != null)
//			GetComponent<BoxCollider2D> ().isTrigger = true;
//		if (GetComponent<CircleCollider2D> () != null)
//			GetComponent<CircleCollider2D> ().isTrigger = true;
		rb2d.GetComponent<SpriteRenderer> ().color = Color.white;
		anim.SetTrigger ("Death");
		Destroy (gameObject, 2f);
	}

	//		// enemies animator
	//		Animator anim;
	//
	//		// player
	//		public GameObject player;
	//
	//		public GameObject shot;
	//
	//		Rigidbody2D rb;
	//
	//		// public Text freezeTime;
	//
	//		// Transform of ground check object on enemies feet
	//		public Transform groundCheck;
	//
	//		// Transform of mouth object for shooting
	//		public Transform mouth;
	//
	//		// layer for everything that can be walked on (ground)
	//		public LayerMask whatIsGround;
	//
	//		// gui health text field
	//		public Text health;
	//
	//		// maximum horizontal speed
	//		public float maxSpeed = 10f;
	//
	//		// will enemy follow the player
	//		public bool followPlayer;
	//
	//		// is enemy facing left
	//		public bool facingLeft = true;
	//
	//		// is enemy on the ground
	//		bool grounded = false;
	//
	//		// is enemy frozen in place
	//		public bool frozen = false;
	//
	//		// amount of time to remain frozen
	//		public float lengthOfFreeze = 5f;
	//
	//		// radius used to check for overlap with the ground and feet
	//		float groundRadius = 0.2f;
	//
	//		// direction enemy is moving Left = -1, Right = 1
	//		public float move = -1f;
	//
	//		// is enemy attacking
	//		bool attack = false;
	//
	//		// distance from player that enemy can melee attack
	//		public float meleeAttackDistance = 1.25f;
	//
	//		// attack cool down time
	//		public float timeBetweenAttacks = 0f;
	//
	//		// does enemy have a ranged attack
	//		public bool hasRangedAttack = false;
	//
	//		// distance from player for ranged attack
	//		public float rangedAttackDistance = 10f;
	//
	//		// can enemy shoot
	//		public bool canShoot = true;
	//
	//		// enemy's health
	//		public int ownHealth;
	//
	//		// is there ground in front of the player
	//		public bool groundInFront = true;
	//
	//		// distance to check for ground in front of player
	//		public float turnDistance;
	//
	//		void Start ()
	//		{
	//			player = GameObject.Find ("Bird"); // find the player GameObject
	//			anim = GetComponent<Animator> (); // get this objects animator
	//			rb = GetComponent<Rigidbody2D> (); // get rigidbody of Gameobject
	//			ownHealth = 20; // enemy's health
	//		}
	//
	//		void FixedUpdate ()
	//		{
	//			if (!frozen) {
	//				// check if enemy is on the ground and set animator bool Ground to grounded
	//				grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
	//				anim.SetBool ("Ground", grounded);
	//
	//				checkForTurn ();
	//
	//				if (attack)
	//					move = 0f;
	//				else {
	//					// if enemy is set to follow the player, check for player's position set enemy's direction
	//					if (followPlayer) {
	//						if (player.transform.position.x < transform.position.x)
	//							move = -1;
	//						else
	//							move = 1;
	//					} else if (facingLeft)
	//						move = -1;
	//					else
	//						move = 1;
	//				}
	//
	//				// set the Speed parameter to absolute value of move amount
	//				anim.SetFloat ("Speed", Mathf.Abs (move));
	//
	//				// if enemy is not grounded set move speed to zero so idle animation will play
	//				if (attack) {
	//					return;
	//				}
	//
	//				// move enemy if not attacking
	//				if (!attack)
	//					GetComponent<Rigidbody2D> ().velocity = new Vector2 (move * maxSpeed, GetComponent<Rigidbody2D> ().velocity.y);
	//
	//				// set direction enemy is facing based on move variable by calling Reverse method
	//				if (move > .01 && facingLeft)
	//					Reverse ();
	//				else if (move < -.01 && !facingLeft)
	//					Reverse ();
	//			} else {
	//				lengthOfFreeze -= Time.deltaTime;
	//
	//				if (lengthOfFreeze < 0f) {
	//					frozen = false;
	//					lengthOfFreeze = 5f;
	//					anim.SetFloat ("Speed", Mathf.Abs (move));
	//				}
	//			}
	//		}
	//
	//		void Update ()
	//		{
	//			//check to see if its health is 0
	//			if (ownHealth <= 0) {
	//				Destroy (gameObject);
	//			}
	//
	//			if (!frozen) {
	//				// get the enemy's distance from the player
	//				float distanceToPlayerX = Mathf.Abs (transform.position.x - player.transform.position.x);
	//				float distanceToPlayerY = Mathf.Abs (transform.position.y - player.transform.position.y);
	//
	//				//		if (hasRangedAttack && !attack) {
	//				//			if (distanceToPlayer < rangedAttackDistance && canShoot) {
	//				//				Debug.Log ("Shoot");
	//				//				timeBetweenAttacks = 3f;
	//				//				canShoot = false;
	//				//				anim.SetTrigger ("Shoot");
	//				//			} else {
	//				//				timeBetweenAttacks -= Time.deltaTime;
	//				//				if (timeBetweenAttacks < 0) {
	//				//					canShoot = true;
	//				//				}
	//				//			}
	//				//		}
	//
	//				// check to see if enemy is close enough to attack
	//				if (distanceToPlayerX < meleeAttackDistance && distanceToPlayerY < meleeAttackDistance) {
	//					attack = true;
	//				} else {
	//					attack = false;
	//					if (timeBetweenAttacks > 0) {
	//						timeBetweenAttacks -= Time.deltaTime;
	//					} else {
	//						timeBetweenAttacks = 0f;
	//					}
	//				}
	//
	//				// check to see if enough time has gone by to attack
	//				if (attack && timeBetweenAttacks > 0f)
	//					timeBetweenAttacks -= Time.deltaTime;
	//				else if (attack) {
	//					EnemyAttack ();
	//				}
	//			}
	//		}
	//
	//		void checkForTurn ()
	//		{
	//			float scale = 0f;
	//			if (facingLeft) {
	//				scale = -1f;
	//			} else if (!facingLeft) {
	//				scale = 1f;
	//			}
	//
	//			groundInFront = Physics2D.OverlapCircle (new Vector3 (groundCheck.position.x + (scale * turnDistance), groundCheck.position.y, groundCheck.position.z), groundRadius, whatIsGround);
	//			if (!groundInFront) {
	//				Reverse ();
	//			}
	//
	//		}
	//
	//		void Reverse ()
	//		// reverse the direction the enemy is facing by changing the changing the x scale to it's opposite
	//		{
	//			Debug.Log ("Reverse Method");
	//			facingLeft = !facingLeft;
	//			Vector3 theScale = transform.localScale;
	//			theScale.x *= -1;
	//			transform.localScale = theScale;
	//		}
	//
	//		void EnemyAttack ()
	//		// play Attack animation, reduce player's health, put health on screen, and reset timeBetweenAttacks
	//		{
	//			anim.SetTrigger ("Attack");
	//			KnightController.health -= 10;
	//			//health.text = "Health: " + KnightController.health;
	//			timeBetweenAttacks = 1f;
	//		}
	//
	//		public void shoot ()
	//		{
	//			if (canShoot) {
	//				Instantiate (shot, mouth.position, mouth.rotation);
	//				canShoot = false;
	//				timeBetweenAttacks = 3f;
	//			}
	//		}
	//
	//		void OnTriggerEnter2D (Collider2D other)
	//		// if enemy enters a trigger check to see what the trigger is attached to and do something
	//		{
	//			// if tag is Wall change direction of movement and sprite
	//			if (other.tag == "Wall" || other.tag == "PlatformEdge") {
	//				move *= -1;
	//				Reverse ();
	//			}
	//
	//			if (other.tag == "Door")
	//				anim.SetFloat ("Speed", 0);
	//
	//			if (other.tag == "Freeze") {
	//				frozen = true;
	//				Destroy (other.gameObject);
	//				anim.SetFloat ("Speed", 0f);
	//			}
	//
	//			if (other.tag == "Shot") {
	//				Debug.Log ("Hit by Shot");
	//				ownHealth -= 10;
	//				Debug.Log ("Health = " + ownHealth);
	//				Destroy (other.gameObject);
	//			}
	//
	//			// if inside player's attack trigger health goes down by 10
	//			if (other.tag == "PlayerAttack") {
	//				Debug.Log ("Player Attack");
	//				ownHealth -= 10;
	//			}
	//
	//			// if tag is Death destroy the object
	//			if (other.tag == "Death") {
	//				Destroy (gameObject);
	//			}
	//		}
}
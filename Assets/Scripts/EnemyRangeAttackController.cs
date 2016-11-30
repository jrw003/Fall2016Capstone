using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyRangeAttackController : MonoBehaviour
{

	public float speed;

	public GameObject player;

	PlayerCntrller playerController;

	int damageToPlayer = 10;

	private Rigidbody2D rb2d;

	public AudioSource hurtPlayer;

	// Use this for initialization
	void Start ()
	{
		player = GameObject.Find ("Player");
		rb2d = GetComponent<Rigidbody2D> ();
		if (player.transform.position.x < transform.position.x) {
			speed = -speed;
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		rb2d.velocity = new Vector2 (speed, rb2d.velocity.y);
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Player") {
			playerController = other.GetComponent<PlayerCntrller> ();

			hurtPlayer.Play ();

			PlayerCntrller.health -= damageToPlayer;

			playerController.knockBackCounter = playerController.knockBackTime;
			if (other.transform.position.x > transform.position.x) {
				playerController.knockBackToRight = true;
			} else {
				playerController.knockBackToRight = false;
			}

		}

		if (other.tag == "PlatformEdge") {
			return;
		}

		Destroy (gameObject);
	}
}

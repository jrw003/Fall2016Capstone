using UnityEngine;
using System.Collections;

public class HurtPlayer : MonoBehaviour
{

	public int damageToPlayer;
	//public CircleCollider2D circleCollider;

	//KnightController playerController;
	PlayerCntrller playerController;

	bool bossCanDoDamage = true;
	public float bossDamageTimer = 1f;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		bossDamageTimer -= Time.deltaTime;
		if (bossDamageTimer < 0f) {
			bossDamageTimer = 0f;
		}
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (gameObject.transform.parent != null) {
			if (other.tag == "Player" && gameObject.transform.parent.tag == "FishBoss") {
				
				if (bossDamageTimer < 0.01f) { 
					playerController = other.GetComponent<PlayerCntrller> ();

					PlayerCntrller.health -= damageToPlayer;

					GetComponent<AudioSource> ().Play ();
					playerController.knockBackCounter = playerController.knockBackTime;
					if (other.transform.position.x > transform.position.x) {
						playerController.knockBackToRight = true;
					} else {
						playerController.knockBackToRight = false;
					}
					bossDamageTimer = 1f;
				} else
					return;
			} else if (other.tag == "Player") {
				Debug.Log ("Hurt Player");
				playerController = other.GetComponent<PlayerCntrller> ();

				PlayerCntrller.health -= damageToPlayer;

				GetComponent<AudioSource> ().Play ();
				playerController.knockBackCounter = playerController.knockBackTime;
				if (other.transform.position.x > transform.position.x) {
					playerController.knockBackToRight = true;
				} else {
					playerController.knockBackToRight = false;
				}
			}
			//			playerController.knockBackCounter = playerController.knockBackTime;
			//			if (other.transform.position.x > transform.position.x) {
			//				playerController.knockBackToRight = true;
			//			} else {
			//				playerController.knockBackToRight = false;
			//			}
//			Debug.Log ("disable collider");
//			circleCollider.enabled = false;
//			GetComponent<HurtPlayer> ().enabled = false;

//			playerController = other.GetComponent<PlayerCntrller> ();
//
//			PlayerCntrller.health -= damageToPlayer;
//
//			GetComponent<AudioSource> ().Play ();
//			playerController.knockBackCounter = playerController.knockBackTime;
//			if (other.transform.position.x > transform.position.x) {
//				playerController.knockBackToRight = true;
//			} else {
//				playerController.knockBackToRight = false;
//			}

//			if (gameObject.transform.parent.tag == "FishBoss") {
//				Debug.Log ("disable collider");
//				circleCollider.enabled = false;
//				GetComponent<HurtPlayer> ().enabled = false;
		

		}
	}
}

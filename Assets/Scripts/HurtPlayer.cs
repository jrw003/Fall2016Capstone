using UnityEngine;
using System.Collections;

public class HurtPlayer : MonoBehaviour
{

	public int damageToPlayer;

	//KnightController playerController;
	PlayerCntrller playerController;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (!gameObject.GetComponentInParent<EnemyMovement> ().frozen) {
			if (other.tag == "Player") {
				//playerController = other.GetComponent<KnightController> ();
				playerController = other.GetComponent<PlayerCntrller> ();

				//KnightController.health -= damageToPlayer;
				PlayerCntrller.health -= damageToPlayer;

				GetComponent<AudioSource> ().Play ();
				playerController.knockBackCounter = playerController.knockBackTime;
				if (other.transform.position.x > transform.position.x) {
					playerController.knockBackToRight = true;
				} else {
					playerController.knockBackToRight = false;
				}
			}
		}
	}
}

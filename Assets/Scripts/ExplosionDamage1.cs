using UnityEngine;
using System.Collections;

public class ExplosionDamage1 : MonoBehaviour
{
	public int damageToPlayer;
	//public CircleCollider2D circleCollider;

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
		if (other.tag == "Player") {
			playerController = other.GetComponent<PlayerCntrller> ();

			PlayerCntrller.health -= damageToPlayer;

			GetComponent<AudioSource> ().Play ();
		}
	}
}

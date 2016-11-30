using UnityEngine;
using System.Collections;

public class ActivateDoubleJump : MonoBehaviour
{

	PlayerCntrller player;

	// Use this for initialization
	void Start ()
	{
		player = GameObject.Find ("Player").GetComponent<PlayerCntrller> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Player") {
			player.setDoubleJumpAvailable (true);
		}
	}
}

using UnityEngine;
using System.Collections;

public class ActivateSpawner : MonoBehaviour
{

	public GameObject enemySpawner;
	PlayerCntrller player;

	// Use this for initialization
	void Start ()
	{
		enemySpawner.GetComponent<EnemySpawn> ().enabled = false;
		player = GameObject.Find ("Player").GetComponent<PlayerCntrller> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		enemySpawner.GetComponent<EnemySpawn> ().enabled = true;
		GetComponent<BoxCollider2D> ().enabled = false;
		player.setFreezeShotAvailable (true);
		player.setQuillShotAvailable (true);
	}
}

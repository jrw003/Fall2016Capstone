using UnityEngine;
using System.Collections;

public class EnemySpawn : MonoBehaviour
{
	public GameObject enemyToSpawn;
	public int numberOfEnemiesToSpawn;
	float timer;
	public float SPAWN_TIME;

	void Start ()
	{
		timer = SPAWN_TIME;
		Debug.Log ("Spawn Point Initiated");
	}

	void Update ()
	{
		if (numberOfEnemiesToSpawn > 0) {
			timer -= Time.deltaTime;
			Debug.Log ("timer = " + timer);
			if (timer < 0) {
				Debug.Log ("Call spawnEnemy");
				spawnEnemy ();
			}
		}
	}

	public void spawnEnemy ()
	{
		Debug.Log ("SpawnEnemy");
		Debug.Log ("transform = (" + gameObject.transform.position.x + ", " + gameObject.transform.position.y + ")");
		Instantiate (enemyToSpawn, gameObject.transform.position, gameObject.transform.rotation);
		timer = SPAWN_TIME;
		numberOfEnemiesToSpawn--;
	}
}

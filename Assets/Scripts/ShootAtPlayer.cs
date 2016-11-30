using UnityEngine;
using System.Collections;

public class ShootAtPlayer : MonoBehaviour
{
	EnemyMovement enemyMovement;

	public float maxRangeToPlayer;

	public float minRangeToPlayer;

	public GameObject enemyShot;

	public GameObject player;

	public Transform shootPoint;

	public float timeBetweenShots;

	private float shotTimer;

	public bool playerInRange;

	public bool facingPlayer;

	// Use this for initialization
	void Start ()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		shotTimer = timeBetweenShots;
		enemyMovement = GetComponent<EnemyMovement> ();
	}
	
	// Update is called once per frame
	void Update ()
	{	
		if (!enemyMovement.dead) {
			if (GetComponent<EnemyMovement> ().knockBackCounter <= 0) {
				shotTimer -= Time.deltaTime;

				facingPlayer = ((transform.localScale.x < 0 && transform.position.x < player.transform.position.x) ||
				(transform.localScale.x > 0 && transform.position.x > player.transform.position.x));

				playerInRange = ((player.transform.position.x > transform.position.x - maxRangeToPlayer &&
				player.transform.position.x < transform.position.x - minRangeToPlayer) ||
				(player.transform.position.x < transform.position.x + maxRangeToPlayer &&
				player.transform.position.x > transform.position.x + minRangeToPlayer));

				if (playerInRange && facingPlayer && shotTimer < 0) {
					Instantiate (enemyShot, shootPoint.position, shootPoint.rotation);
					shotTimer = timeBetweenShots;
				}
			}
		}
		
		//Debug.DrawLine (new Vector3 (transform.position.x - rangeToPlayer, transform.position.y, transform.position.z), new Vector3 (transform.position.x + rangeToPlayer, transform.position.y, transform.position.z));


//		if (transform.localScale.x < 0 && player.transform.position.x > transform.position.x && player.transform.position.x < transform.position.x + rangeToPlayer && shotTimer < 0) {
//			Instantiate (enemyShot, shootPoint.position, shootPoint.rotation);
//			shotTimer = timeBetweenShots;
//		}
//
//		if (transform.localScale.x > 0 && player.transform.position.x < transform.position.x && player.transform.position.x > transform.position.x - rangeToPlayer && shotTimer < 0) {
//			Instantiate (enemyShot, shootPoint.position, shootPoint.rotation);
//			shotTimer = timeBetweenShots;
//		}
	}
}

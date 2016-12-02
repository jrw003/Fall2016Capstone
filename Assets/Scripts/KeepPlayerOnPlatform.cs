using UnityEngine;
using System.Collections;

public class KeepPlayerOnPlatform : MonoBehaviour
{

	public GameObject player;
	// Player Game Object
	public Transform playerTransform;
	// transform of player
	public float yOffset;
	// y offset of player above platform center
	private Rigidbody2D rb2d;

	public float verticalOffset = 0.25f;
	// height the center of the player must be kept

	public Vector3 lastPos;
	public Vector3 curPos;
	public float y;
	public Vector3 delta;
	public float yVelocity;
	public bool onPlatform = false;

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Player") {
			onPlatform = true;
			player = other.gameObject;
			playerTransform = other.transform;
			yOffset = player.transform.position.y - transform.position.y + verticalOffset;
		}
	}

	void OnTriggerExit2D (Collider2D other)
	{
		if (other.tag == "Player") {
			onPlatform = false;
		}
	}

	void Start ()
	{
		lastPos = transform.position;
	}

	void Update ()
	{
		if (onPlatform) {
			curPos = transform.position;
			y = curPos.y;
			delta = curPos - lastPos;
			yVelocity = delta.y;
			delta.y = 0f;
			lastPos = curPos;
			rb2d = player.GetComponent<Rigidbody2D> ();
//			if ((rb2d.velocity.y > -0.01f && rb2d.velocity.y < -0.01f) ||
//			    (rb2d.velocity.y > yVelocity - 0.01f && rb2d.velocity.y < yVelocity + 0.01f)) {
//				Debug.Log ("in if statement");
			Vector3 pos = playerTransform.position;
			pos.y = y + yOffset;
			pos += delta;
			playerTransform.position = pos;
//			}
		}
	}
}

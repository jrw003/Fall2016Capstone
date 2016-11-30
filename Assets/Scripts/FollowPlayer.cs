using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour
{
	public float xDist = 10f;
	// Distance the player can move in the x axis before the camera will follow.
	public float yDist = 10f;
	// Distance the player can move in the y axis before the camera will follow.
	public float xSmooth = 10f;
	// Smoothing for the Lerp that catches up with the player's x position.
	public float ySmooth = 10f;
	// Smoothing for the Lerp that catches up with the player's y position.
	public Vector2 maxXY;
	// The maximum x and y coordinates the camera can have.
	public Vector2 minXY;
	// The minimum x and y coordinates the camera can have.

	public Transform player;
	// Reference to the player's transform.

	void Awake ()
	{
		//player = GameObject.Find ("Player").transform;
	}

	private bool CheckXDist ()
	{
		// checks the distance between x position of the player and the camera's x position
		// returns false if greater than the distance player can be from camera's x position
		// return true if less than the distance player can be from the camera's x position
		return (Mathf.Abs (transform.position.x - player.transform.position.x) < xDist);
	}

	private bool CheckYDist ()
	{
		// checks the distance between y position of the player and the camera's y position
		// returns false if greater than the distance player can be from camera's y position
		// return true if less than the distance player can be from the camera's y position
		return (Mathf.Abs (transform.position.y - player.transform.position.y) < yDist);
	}
	
	// Update is called once per frame
	void Update ()
	{
		CheckPlayer ();
	}

	void CheckPlayer ()
	{
		float cameraX = transform.position.x;
		float cameraY = transform.position.y;

		// if the player has moved beyond the xDist
		// Lerp the camera to the players position using the x smoothing
		if (CheckXDist ())
			cameraX = Mathf.Lerp (transform.position.x, player.position.x, xSmooth * Time.deltaTime);

		// if the player has moved beyond the yDist
		// Lerp the camera to the players position using the y smoothing
		if (CheckYDist ())
			cameraY = Mathf.Lerp (transform.position.y, player.position.y, ySmooth * Time.deltaTime);

		// Make sure camera doesn't go outside the minimum and maximum x and y coordinates
		cameraX = Mathf.Clamp (cameraX, minXY.x, maxXY.x);
		cameraY = Mathf.Clamp (cameraY, minXY.y, maxXY.y);

		// Set the camera's position to the target position with the same z component.
		transform.position = new Vector3 (cameraX, cameraY, transform.position.z);
	}
}

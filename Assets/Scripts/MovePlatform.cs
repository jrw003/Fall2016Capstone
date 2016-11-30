using UnityEngine;
using System.Collections;

// Class to move platforms up and down and right and left.
// Player will not stick to platform right nowgit

public class MovePlatform : MonoBehaviour
{
	public float xDist = 10f;
	// horizontal distance platform moves
	public float yDist = 10f;
	// vertical distance platform moves

	// starting x and y positions of platform
	float startXPosition;
	float startYPosition;

	//  current x and y positions of platform
	float currentXPosition;
	float currentYPosition;

	float rightPosition;
	// farthest right position platform can go
	float topPosition;
	// farthest up position platform can go

	public float moveSpeed = 3f;
	// speed platform moves

	public bool upAndDown;
	// does platform go up and down
	public bool leftAndRight;
	// does platform go right and left

	public bool up = true;
	// is platform going up
	public bool right = true;
	// is platform going down

	void Start ()
	{
		// initialize start, current, right, and top positions
		startXPosition = transform.position.x;
		startYPosition = transform.position.y;
		currentXPosition = transform.position.x;
		currentYPosition = transform.position.y;
		rightPosition = transform.position.x + xDist;
		topPosition = transform.position.y + yDist;
		// if you want the platform to go left to right or down then up put in a negative x or y Dist.
		if (xDist < 0) {
			right = false;
			rightPosition = transform.position.x;
			startXPosition = transform.position.x + xDist;
		}
		if (yDist < 0) {
			up = false;
			topPosition = transform.position.y;
			startYPosition = transform.position.y + yDist;
		}
	}
	
	// Update is called once per frame
	void Update ()
	{	
		// if platform is going up and down
		if (upAndDown) {
			// has platform reached its highest position
			if (up && currentYPosition < topPosition) {
				// move platform up and update current position
				transform.Translate (Vector2.up * moveSpeed * Time.deltaTime);
				currentYPosition = transform.position.y;
				// has platform gone below lowest position
			} else if (!up && currentYPosition > startYPosition) {
				// move platform down and update current position
				transform.Translate (-Vector2.up * moveSpeed * Time.deltaTime);
				currentYPosition = transform.position.y;
			}

			// if platform has reached top set up to false so it will go down
			if (currentYPosition > topPosition)
				up = false;

			// if platform has reached bottom set up to true so it will go up
			if (currentYPosition < startYPosition)
				up = true;
		}

		// if platform is going left and right
		if (leftAndRight) {
			// has platform reached its farthest right position
			if (right && currentXPosition < rightPosition) {
				// move platform right and update current position
				transform.Translate (Vector2.right * moveSpeed * Time.deltaTime);
				currentXPosition = transform.position.x;
				// has platform reached farthest left position
			} else if (!right && currentXPosition > startXPosition) {
				// move platform left and update current position
				transform.Translate (-Vector2.right * moveSpeed * Time.deltaTime);
				currentXPosition = transform.position.x;
			}

			// if platform has reached farthest right position set right to false so platform will go left
			if (currentXPosition > rightPosition)
				right = false;

			// if platform has reached farthest left position set right to true so platform will go right
			if (currentXPosition < startXPosition)
				right = true;
		}
	}
}

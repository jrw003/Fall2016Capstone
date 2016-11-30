using UnityEngine;
using System.Collections;

public class RotateBox : MonoBehaviour
{

	//Rigidbody2D	rb2d;
	public LineRenderer line;
	DistanceJoint2D dj2d;
	public float speed;
	float radius;
	float xPos;
	float yPos;
	float centerx;
	float centery;
	public float timer;
	public bool clockwise;
	float direction;
	float angle;

	// Use this for initialization
	void Start ()
	{
		//rb2d = GetComponent<Rigidbody2D> ();
		dj2d = GetComponent<DistanceJoint2D> ();
		radius = dj2d.distance;
		centerx = dj2d.connectedAnchor.x;
		centery = dj2d.connectedAnchor.y;
		if (clockwise)
			direction = 1f;
		else
			direction = -1f;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		timer += Time.deltaTime;
		angle = direction * timer * speed;
		this.transform.position = new Vector3 ((centerx + Mathf.Sin (angle) * radius), ((centery + Mathf.Cos (angle) * radius)), 0);
		line.SetPosition (0, dj2d.connectedAnchor);
		line.SetPosition (1, transform.position);
	}
}

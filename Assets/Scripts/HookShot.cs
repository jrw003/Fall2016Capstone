using UnityEngine;
using System.Collections;

public class HookShot : MonoBehaviour
{
	DistanceJoint2D joint;
	Vector3 targetLoc;
	RaycastHit2D hit;
	public LayerMask grappleMask;
	public float jointDistance = 20f;
	public float jointDecrease = 0.3f;
	public GameObject voidCollider;

	// Use this for initialization
	void Start ()
	{
		joint = GetComponent<DistanceJoint2D> ();
		joint.enabled = false;

	}
	
	// Update is called once per frame
	void Update ()
	{
		if (joint.distance > 1f) {
			joint.distance -= jointDecrease;
		} else {
			joint.enabled = false;
			gameObject.GetComponent<Rigidbody2D> ().gravityScale = 3;
		}

		if (Input.GetKeyDown (KeyCode.F)) {
			//targetLoc = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			//targetLoc.z = 0;
			if (gameObject.transform.localScale.x > 0) {
				hit = Physics2D.Raycast (transform.position, Vector2.right, jointDistance, grappleMask);

				if (hit.collider != null && hit.collider.gameObject.GetComponent<Rigidbody2D> () != null) {
					joint.enabled = true;
					joint.connectedBody = hit.collider.gameObject.GetComponent<Rigidbody2D> ();
					joint.distance = Vector2.Distance (transform.position, hit.point);
					gameObject.GetComponent<Rigidbody2D> ().gravityScale = 0;
				}
			} else if (gameObject.transform.localScale.x < 0) {
				hit = Physics2D.Raycast (transform.position, Vector2.left, jointDistance, grappleMask);

				if (hit.collider != null && hit.collider.gameObject.GetComponent<Rigidbody2D> () != null) {
					joint.enabled = true;
					joint.connectedBody = hit.collider.gameObject.GetComponent<Rigidbody2D> ();
					joint.distance = Vector2.Distance (transform.position, hit.point);
					gameObject.GetComponent<Rigidbody2D> ().gravityScale = 0;
				}
			}
				
		}

		if (Input.GetKeyUp (KeyCode.F)) {
			joint.enabled = false;
			gameObject.GetComponent<Rigidbody2D> ().gravityScale = 3;
		}
	}
}

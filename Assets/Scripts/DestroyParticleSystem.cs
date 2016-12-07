using UnityEngine;
using System.Collections;

public class DestroyParticleSystem : MonoBehaviour
{
	public float colliderTimer = .2f;
	public AudioSource sound;

	// Use this for initialization
	void Start ()
	{
		Destroy (gameObject, .75f);
		sound.Play ();
	}

	void Update ()
	{
		if (gameObject.GetComponent<PointEffector2D> () != null) {
			colliderTimer -= Time.deltaTime;
			if (colliderTimer < 0) {
				gameObject.GetComponent<PointEffector2D> ().enabled = false;
				gameObject.GetComponent<CircleCollider2D> ().enabled = false;
			}
		}
	}
}

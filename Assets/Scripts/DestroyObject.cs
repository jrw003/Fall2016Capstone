using UnityEngine;
using System.Collections;

public class DestroyObject : MonoBehaviour
{

	public GameObject[] objectsToDestroy;
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Player") {
			foreach (GameObject obj in objectsToDestroy) {
				Destroy (obj);
			}
		}
	}
}

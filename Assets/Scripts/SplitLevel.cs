using UnityEngine;
using System.Collections;

public class SplitLevel : MonoBehaviour
{

	public GameObject divider;
	public GameObject arrows;
	public GameObject fish;
	Vector3 fish1 = new Vector3 (446.19f, -67.16f, -1f);
	Vector3 fish2 = new Vector3 (484.8f, -67.16f, -1f);
	bool activated;

	// Use this for initialization
	void Start ()
	{
		divider.SetActive (true);
		arrows.SetActive (false);
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Player" & !activated) {
			divider.SetActive (true);
			arrows.SetActive (true);
			Instantiate (fish, fish1, Quaternion.identity);
			Instantiate (fish, fish2, Quaternion.identity);
			activated = true;
		}
	}
}

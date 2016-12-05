using UnityEngine;
using System.Collections;

public class SplitLevel : MonoBehaviour {

	public GameObject divider;
	public GameObject arrows;

	// Use this for initialization
	void Start () {
		divider.SetActive (true);
		arrows.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player") {
			divider.SetActive (true);
			arrows.SetActive (true);
		}
	}
}

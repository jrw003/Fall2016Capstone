using UnityEngine;
using System.Collections;

public class Arrows : MonoBehaviour {

	public GameObject[] arrows;
	int i;
	int currentArrow;
	int nextArrow;
	float flashTimer;
	public float flashTime = 0.5f;

	// Use this for initialization
	void Start () {
		for (i = 0; i < arrows.Length; i++) {
			arrows [i].GetComponent<SpriteRenderer>().enabled = false;
		}
		currentArrow = 0;
	}
	
	// Update is called once per frame
	void Update () {
		flashTime -= Time.deltaTime;
		if (flashTime < 0) {
			if (currentArrow < arrows.Length - 1) {
				nextArrow = currentArrow + 1;
			} else {
				nextArrow = 0;
			}
			arrows [currentArrow].GetComponent<SpriteRenderer>().enabled = false;
			arrows [nextArrow].GetComponent<SpriteRenderer>().enabled = true;
			currentArrow = nextArrow;
			flashTime = 0.5f;
		}
	}
}

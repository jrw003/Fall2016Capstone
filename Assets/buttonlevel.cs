using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class buttonlevel : MonoBehaviour {

	public Button startText;

	// Use this for initialization
	void Start () {
		startText = startText.GetComponent<Button>();

	}

	public void noPress()
	{
		startText.enabled = true;

	}

	public void StartLevel()
	{

		Application.LoadLevel(1);
	}
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class buttonlevel : MonoBehaviour
{

	public Button startText;

	// Use this for initialization
	void Start ()
	{
		startText = startText.GetComponent<Button> ();

	}

	//	public void noPress ()
	//	{
	//		startText.enabled = true;
	//
	//	}

	public void TutorialLevel ()
	{
		SceneManager.LoadScene (1);
	}

	public void DesertLevel ()
	{
		SceneManager.LoadScene (2);
	}

	public void Credits ()
	{
		SceneManager.LoadScene (4);
	}


	// quit game called from button click
	public void Quit ()
	{
		Application.Quit ();
	}
}

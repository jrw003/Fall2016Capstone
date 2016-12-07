using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CreditButtons : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public void StartScreen ()
	{
		SceneManager.LoadScene (0);
	}

	public void Quit ()
	{
		Application.Quit ();
	}
}

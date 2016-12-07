using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
	// Text displayed when player is dead
	public Text deathText;
	// Text displayed when player completes level
	public Text wonText;
	// Text for players health
	public Text playerHealth;
	// Restart and Quit buttons on canvas
	public GameObject buttons;
	public Text levelButton;

	public GameObject enemySpawner;
	// Array to put all enemies in game so they can all be stopped when game over
	GameObject[] enemies;
	// Feather sprites, Full, Half, and Blank
	public Sprite[] sprites = new Sprite[3];
	// Five feathers on the screen
	public Image[] feathers = new Image [5];
	// tag of player
	string PLAYER = "Player";
	// How many full feathers will be displayed
	int numberOfFullFeathers;
	// How many blank feathers will be displayed
	int numberOfBlankFeathers;
	// position of a half feather if one exists
	int halfFeatherPosition;
	// used in for loops
	int i;
	// is game over
	bool gameover = false;
	Rigidbody2D rb2d;
	int scene;

	public AudioSource winSound;

	void Start ()
	{
		// disable Canvas text and buttons
		deathText.GetComponent<Text> ().enabled = false;
		wonText.GetComponent<Text> ().enabled = false;
		buttons.SetActive (false);
		//enemySpawner.GetComponent<EnemySpawn> ().enabled = false;
		Debug.Log ("Active Scene = " + SceneManager.GetActiveScene ().name);
		// Make all of the feathers the full feather sprite
		for (i = 0; i < 5; i++) {
			feathers [i].sprite = sprites [0];
		}
	}

	void Update ()
	{	
		// if game is over do not update anything
		if (!gameover) {
			// get the number of full feathers to display
			numberOfFullFeathers = PlayerCntrller.health / 20;

			// get the number of blank feathers to display
			numberOfBlankFeathers = 5 - (int)((float)PlayerCntrller.health / 20f + 0.5f);

			// if there are blank feathers change those sprites to blank feather sprites
			if (numberOfBlankFeathers != 0) {
				for (i = 1; i <= numberOfBlankFeathers; i++) {
					feathers [5 - i].sprite = sprites [2];
				}
			}

			// if there are 1 to 4 feathers, check to see if there should be a half feather and put it where it should be
			if (numberOfFullFeathers != 0 || numberOfFullFeathers != 5) {
				if (PlayerCntrller.health % 20 != 0) {
					feathers [numberOfFullFeathers].sprite = sprites [1];
				}
			}

			playerHealth.text = "Health: " + PlayerCntrller.health;

			// if the player has died or completed the level
			//PlayerCntrller.health <= 0f || 
			if (PlayerCntrller.health <= 0f || PlayerCntrller.won) {

				// enable the buttons
				if (PlayerCntrller.won)
					buttons.SetActive (true);

				// disable the players script and animator
				GameObject.Find (PLAYER).GetComponent<PlayerCntrller> ().enabled = false;
				//GameObject.Find (PLAYER).GetComponent<Animator> ().enabled = false;
				rb2d = GameObject.Find (PLAYER).GetComponent<Rigidbody2D> ();
				rb2d.velocity = new Vector2 (0f, 0f);

				// find all "Enemy" objects
				enemies = GameObject.FindGameObjectsWithTag ("Enemy");

				// loop through enemies and disable animator, controller script, and shooting script
				foreach (GameObject enemy in enemies) {
					if (enemy.GetComponent<Animator> () != null) {
						enemy.GetComponent<Animator> ().enabled = false;
					}
					//enemy.GetComponent<Animator> ().SetFloat ("Speed", 0f);
					if (enemy.GetComponent<EnemyMovement> () != null) {
						enemy.GetComponent<EnemyMovement> ().enabled = false;
					}

					if (enemy.GetComponent<ShootAtPlayer> () != null) {
						enemy.GetComponent<ShootAtPlayer> ().enabled = false;
					}
				}

				// display death text if player is dead
				if (PlayerCntrller.health <= 0f) {
//					deathText.GetComponent<Text> ().enabled = true;
//					levelButton.text = "Restart Level";
					scene = SceneManager.GetActiveScene ().buildIndex;
					SceneManager.LoadScene (scene);

				}

				// display won text if player has completed level
				if (PlayerCntrller.won) {
					wonText.GetComponent<Text> ().enabled = true;
					//levelButton.text = "Next Level";
					winSound.Play ();
				}

				// set gameover to true so update won't run again
				gameover = true;
			} 
		}
	}

	// restart game called from button click

	public void StartScreen ()
	{
		SceneManager.LoadScene (0);
	}

	public void TutorialLevel ()
	{
		SceneManager.LoadScene (1);
	}

	public void DesertLevel ()
	{
		SceneManager.LoadScene (2);
	}

	public void JungleLevel ()
	{
		SceneManager.LoadScene (3);
	}

	public void Credits ()
	{
		SceneManager.LoadScene (4);
	}

	public void Restart ()
	{
		scene = SceneManager.GetActiveScene ().buildIndex;
		SceneManager.LoadScene (scene);
	}

	// quit game called from button click
	public void Quit ()
	{
		Application.Quit ();
	}
}

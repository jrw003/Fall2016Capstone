using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour
{

	private bool isAttacking = false;
	private float timer = 0;
	private float coolDown = 0.5f;
	public float timeBetweenAttacks = 0.5f;
	private float attackTimer = 0f;

	public Collider2D attackTrigger;

	private Animator anim;

	void Awake ()
	{
		anim = GetComponent<Animator> ();
		attackTrigger.enabled = false;
	}

	// Use this for initialization
	void Start ()
	{

	}

	// Update is called once per frame
	void Update ()
	{
		attackTimer -= Time.deltaTime;
		if (attackTimer <= 0f) {
			attackTimer = 0f;
			if (Input.GetButtonDown ("Fire3") && !isAttacking) {
				attackTrigger.enabled = true;
				anim.SetTrigger ("Attack");
				playAttack ();
			}
		}

		if (isAttacking) {
			if (timer > 0) {
				timer -= Time.deltaTime;
			} else {
				isAttacking = false;
				attackTrigger.enabled = false;
			}
		}
	}

	public void setTimer (float time)
	{
		timer = time;
	}

	public void playAttack ()
	{
		isAttacking = true;
		timer = coolDown;
		attackTimer = timeBetweenAttacks;
	}
}

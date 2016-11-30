using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Gate : MonoBehaviour
{

    public int totemHP = 20;
    public GameObject gate;


    // Use this for initialization
    void Start()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    // if enemy enters a trigger check to see what the trigger is attached to and do something
    {
        // if inside player's attack trigger health goes down by 10
        if (other.tag == "PlayerAttack")
        {
            Debug.Log("Player Attack");
            totemHP -= 10;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (totemHP <= 0)
        {
            Destroy(gate);
            Destroy(gameObject);
        }

    }
}

using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour
{

    private float goalsReached = 0;
    public GameObject other1;
    public GameObject other2;
    public GameObject gate;

    // Use this for initialization
    void Start()
    {
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collider>().name == "Goal 1")
        {
            if (goalsReached == 0)
            {
                Destroy(gate);
                Destroy(other1);
            }
            else
            {
                goalsReached++;
                Destroy(other1);
            }
        }

        else if (other.GetComponent<Collider>().name == "Goal 2")
        {
            if (goalsReached == 0)
            {
                Destroy(gate);
                Destroy(other2);
            }
            else
            {
                goalsReached++;
                Destroy(other2);
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinSPawn : MonoBehaviour {
    // A timer to countdown to the next coin spawn
    public float timer = 5;
    // Coin to be spawned
    public GameObject coin;
    // For testing purposes to make the spawner move.
    public bool change = false;
    public float changeTimer = 6;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //makes the timer count down.
        timer -= Time.deltaTime;
		
        // When the timer hits zero instantiate the coin and reset the timer.
        if (timer <= 0)
        {
            Instantiate(coin, transform.position,coin.transform.rotation);
            timer = 5;
        }


        /* All below here is for testing purposes real version will be simply bounce off walls within the rooms.
        A timer that determines the movement of the spawner counts down and when it hits zero.
        it changes direction.
        */
    changeTimer -= Time.deltaTime;

        if (changeTimer <= 0)
        {
            if (change == false)
            {
                change = true;
            }
            else change = false;

            changeTimer = 6;
        }

        if (change == false)
        {
            transform.position += Vector3.left;
        }
        else transform.position += Vector3.right;

	}
}

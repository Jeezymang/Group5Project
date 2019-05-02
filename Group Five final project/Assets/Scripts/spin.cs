using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spin : MonoBehaviour {
    public bool right;
    public bool side;
    public int speed;
    
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        // Makes the coin spin.
        if (side == true)
        {
            transform.Rotate(Vector3.forward, speed);
        }

        if (right == true)
        {
            transform.Rotate(0,speed, 0);
        }
        }

    
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour {

    public int speed = 3;
    public int phoneSpeed = 10;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += Vector3.forward * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position += Vector3.back * speed * Time.deltaTime;
        }
        //For phone movement
        Vector3 direction = Vector3.zero;
        direction.x = Input.acceleration.x;
        direction.z = -Input.acceleration.z;
        //if (direction.sqrMagnitude > 1)
        //    direction.Normalize();

        direction *= Time.deltaTime;

        transform.position = transform.position += (direction * phoneSpeed);
    }


    private void OnCollisionEnter(Collision collision)

    {
        if (collision.gameObject.tag == "Coin")
        {
            Destroy (collision.gameObject);
        }

        else if (collision.gameObject.tag == "Foe")
        {
            Destroy(this.gameObject);
            Destroy(collision.gameObject);
        }
        
    }
}

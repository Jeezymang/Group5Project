using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour {
    
    public int speed = 3;
    public int phoneSpeed = 10;
    int rotateSpeed = 2;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            var eulerAngles = this.transform.eulerAngles;
            var eulerQuat = Quaternion.Euler(eulerAngles.x, eulerAngles.y -= rotateSpeed, eulerAngles.z);
            this.transform.rotation = eulerQuat;
            //Gonna rotate the player instead
            //transform.position += Vector3.left * speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            var eulerAngles = this.transform.eulerAngles;
            var eulerQuat = Quaternion.Euler(eulerAngles.x, eulerAngles.y += rotateSpeed, eulerAngles.z);
            this.transform.rotation = eulerQuat;
            //Gonna rotate the player instead
            //transform.position += Vector3.right * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            var rads = (this.transform.eulerAngles.y - 90) * (Mathf.PI / 180);
            var plyDirection = new Vector3(Mathf.Cos(rads), 0, -Mathf.Sin(rads));
            transform.position += (plyDirection * speed) * Time.deltaTime;
            //This must be changed to move in the direction that the object is facing.
            //transform.position += Vector3.forward * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            var rads = (this.transform.eulerAngles.y - 90) * (Mathf.PI / 180);
            var plyDirection = new Vector3(-Mathf.Cos(rads), 0, Mathf.Sin(rads));
            transform.position += new Vector3(Mathf.Abs(plyDirection.x * speed), Mathf.Abs(plyDirection.y * speed), Mathf.Abs(plyDirection.z * speed)) * -Time.deltaTime;
            //transform.position += Vector3.back * speed * Time.deltaTime;
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

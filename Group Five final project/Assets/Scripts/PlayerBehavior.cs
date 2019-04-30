using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour {
    
    public int speed = 3;
    public int phoneSpeed = 10;
    public float rotateSpeed = 1;
    GameHandler gameHandler;
    public Rigidbody rig;
	// Use this for initialization
	void Start () {
        var handler = GameObject.Find("GameHandler");
        if (handler)
            gameHandler = handler.GetComponent<GameHandler>();
        rig = this.gameObject.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        if (gameHandler.IsPaused) return;
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
            //This must be changed to move in the direction that the object is facing.
            //transform.position += Vector3.forward * speed * Time.deltaTime;
            transform.localPosition += transform.forward * speed * Time.deltaTime ;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.localPosition -= transform.forward * speed * Time.deltaTime;
            //transform.position += (plyDirection * speed) * Time.deltaTime;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Coin") {
            gameHandler.addScore(1);
            Destroy(other.gameObject);
        }
    }


    private void OnCollisionEnter(Collision collision)

    {
        if (collision.gameObject.tag == "Coin")
        {
            //Made the coin into a trigger.
            //gameHandler.addScore(1);
            //Destroy (collision.gameObject);
        }

        else if (collision.gameObject.tag == "Foe")
        {
            Destroy(this.gameObject);
            Destroy(collision.gameObject);
            GameObject sceneCanvas = GameObject.Find("Canvas");
            if (sceneCanvas)
                sceneCanvas.GetComponent<GameOverMenu>().EndMenu.SetActive(true);
        }

        else if (collision.gameObject.tag == "speed")
        {
            Destroy(collision.gameObject);
            StopCoroutine(speedUp());
            StartCoroutine(speedUp());
        }
        
    }

    public IEnumerator speedUp()
    {

        speed *= 2;
        yield return new WaitForSeconds(5f);
        speed /= 2;


    }
}

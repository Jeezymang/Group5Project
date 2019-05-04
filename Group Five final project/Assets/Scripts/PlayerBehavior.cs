using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour {
    
    public int speed = 3;
    public int phoneSpeed = 10;
    public float rotateSpeed = 1;
    public int jumpPower = 250;
    public int maxVelocity = 20;
    public int floatingTextFontSize = 20;
    GameHandler gameHandler;
    public GameObject starEmitter;
    public Rigidbody rig;
    public bool invincible = false;
    public bool multiplier = false;
    public float groundDistance;
    public AudioSource audi;
    

	// Use this for initialization
	void Start () {
        var handler = GameObject.Find("GameHandler");
        if (handler)
            gameHandler = handler.GetComponent<GameHandler>();
        rig = this.gameObject.GetComponent<Rigidbody>();
        rig.freezeRotation = true;
        groundDistance = GetComponent<Collider>().bounds.extents.y;
        audi = this.gameObject.GetComponent<AudioSource>();
    }

    public bool OnGround()
    {
        return Physics.Raycast(transform.position, -Vector3.up, groundDistance + 0.3f);
    }
	
	// Update is called once per frame
	void Update () {
        if (gameHandler.IsPaused) return;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            var eulerAngles = this.transform.eulerAngles;
            var eulerQuat = Quaternion.Euler(eulerAngles.x, eulerAngles.y -= rotateSpeed, eulerAngles.z);
            this.transform.rotation = eulerQuat;
            var magnitude = new Vector3(rig.velocity.x, 0, rig.velocity.z).magnitude;
            var yVelocity = rig.velocity.y;
            rig.velocity = Vector3.RotateTowards(rig.velocity, transform.forward * magnitude, 360, 360);
            rig.velocity = new Vector3(rig.velocity.x, yVelocity, rig.velocity.z);
            //Gonna rotate the player instead
            //transform.position += Vector3.left * speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            var eulerAngles = this.transform.eulerAngles;
            var eulerQuat = Quaternion.Euler(eulerAngles.x, eulerAngles.y += rotateSpeed, eulerAngles.z);
            this.transform.rotation = eulerQuat;
            var magnitude = new Vector3(rig.velocity.x, 0, rig.velocity.z).magnitude;
            var yVelocity = rig.velocity.y;
            rig.velocity = Vector3.RotateTowards(rig.velocity, transform.forward * magnitude, 360, 360);
            rig.velocity = new Vector3(rig.velocity.x, yVelocity, rig.velocity.z);
            //Gonna rotate the player instead
            //transform.position += Vector3.right * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            //This must be changed to move in the direction that the object is facing.
            //transform.position += Vector3.forward * speed * Time.deltaTime;
            //Quaternion quat = Quaternion.AngleAxis(90, Vector3.forward);
            //Vector3 newForward = quat * transform.up;
            //rig.AddForce(newForward * speed);
            //rig.AddRelativeForce(transform.forward * speed);
            var yVelocity = rig.velocity.y;
            rig.velocity = Vector3.RotateTowards(rig.velocity, transform.forward * speed, 360, 360);
            rig.velocity = new Vector3(rig.velocity.x, yVelocity, rig.velocity.z);
            //transform.localPosition += transform.forward * speed * Time.deltaTime ;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            rig.AddForce(-rig.velocity.x * 2, 0, -rig.velocity.z * 2);
            //transform.localPosition -= transform.forward * speed * Time.deltaTime;
            //transform.position += (plyDirection * speed) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.Space) || Input.touchCount > 0)
        {
            if (Input.touchCount > 0 && Input.GetTouch(1).phase != TouchPhase.Began)
                return;

            var physObj = this.GetComponent<Rigidbody>();
            if (gameHandler.CanJump && OnGround())
            {
                physObj.AddForce(0, jumpPower, 0);
            }
        }

        
        //For phone movement
        Vector3 direction = Vector3.zero;
        direction.x = Input.acceleration.x;
        direction.z = -Input.acceleration.z;
        //if (direction.sqrMagnitude > 1)
        //    direction.Normalize();.

        direction *= Time.deltaTime;

        transform.position = transform.position += (direction * phoneSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        Vector3 otherPosition = other.transform.position;
        if (other.gameObject.tag == "Coin") {
            audi.Play();

            if (multiplier == false)
            {
                gameHandler.addScore(1);
            }
            else gameHandler.addScore(2);

            Instantiate(starEmitter, other.transform.position, Quaternion.identity);
            Destroy(other.gameObject);

            return;
        }
        else if (other.gameObject.tag == "star")
        {
            Destroy(other.gameObject);
            StopCoroutine(invulnerability());
            StartCoroutine(invulnerability());
        }

        else if (other.gameObject.tag == "speed")
        {
            Destroy(other.gameObject);
            StopCoroutine(speedUp());
            StartCoroutine(speedUp());
        }

        else if (other.gameObject.tag == "coi")
        {
            Destroy(other.gameObject);
            StopCoroutine(coinX2());
            StartCoroutine(coinX2());
        }

        else if (other.gameObject.tag == "jump")
        {
            Destroy(other.gameObject);
            StopCoroutine(JumpPowerup());
            StartCoroutine(JumpPowerup());
        }
        else if (other.gameObject.tag == "spawnrate")
        {
            Destroy(other.gameObject);
            StopCoroutine(SpawnRatePowerup());
            StartCoroutine(SpawnRatePowerup());
        }
        else if (other.gameObject.tag == "duration")
        {
            Destroy(other.gameObject);
            StopCoroutine(DurationPowerup());
            StartCoroutine(DurationPowerup());
        }
        else if (other.gameObject.tag == "Foe")
        {
            if (invincible != true)
            {
                gameHandler.gameAudio.clip = gameHandler.death;
                gameHandler.gameAudio.Play();
                gameHandler.gameAudio.loop = false;
                Destroy(this.gameObject);
                GameObject sceneCanvas = GameObject.Find("Canvas");
                if (sceneCanvas)
                    sceneCanvas.GetComponent<GameOverMenu>().EndMenu.SetActive(true);
            }
            Destroy(other.gameObject);
            return;
        }
        else
        {
            return;
        }
        Instantiate(starEmitter, otherPosition, Quaternion.identity);
        audi.PlayOneShot(gameHandler.powerupGet);
    }

    public IEnumerator speedUp()
    {
        gameHandler.addFloatingText("+Speedup", Color.green, floatingTextFontSize, gameHandler.PowerupDuration);
        speed *= 2;
        yield return new WaitForSeconds(gameHandler.PowerupDuration);
        speed /= 2;
        gameHandler.addFloatingText("-Speedup", Color.red, floatingTextFontSize, gameHandler.PowerupDuration);
    }

    public IEnumerator invulnerability()
    {
        gameHandler.addFloatingText("+Invulnerability", Color.green, floatingTextFontSize, gameHandler.PowerupDuration);
        invincible = true;
        yield return new WaitForSeconds(gameHandler.PowerupDuration);
        invincible = false;
        gameHandler.addFloatingText("-Invulnerability", Color.red, floatingTextFontSize, gameHandler.PowerupDuration);
    }

    public IEnumerator coinX2()
    {
        gameHandler.addFloatingText("+x2 Coins", Color.green, floatingTextFontSize, gameHandler.PowerupDuration);
        multiplier = true;
        yield return new WaitForSeconds(gameHandler.PowerupDuration);
        multiplier = false;
        gameHandler.addFloatingText("-x2 Coins", Color.red, floatingTextFontSize, gameHandler.PowerupDuration);
    }

    public IEnumerator JumpPowerup()
    {
        gameHandler.addFloatingText("+Jumping", Color.green, floatingTextFontSize, gameHandler.PowerupDuration);
        gameHandler.CanJump = true;
        yield return new WaitForSeconds(gameHandler.PowerupDuration);
        gameHandler.CanJump = false;
        gameHandler.addFloatingText("-Jumping", Color.red, floatingTextFontSize, gameHandler.PowerupDuration);
    }

    public IEnumerator SpawnRatePowerup()
    {
        gameHandler.addFloatingText("+Spawnrate", Color.green, floatingTextFontSize, gameHandler.PowerupDuration);
        gameHandler.SpawnRate = 2;
        yield return new WaitForSeconds(gameHandler.PowerupDuration);
        gameHandler.SpawnRate = 5;
        gameHandler.addFloatingText("-Spawnrate", Color.red, floatingTextFontSize, gameHandler.PowerupDuration);
    }

    public IEnumerator DurationPowerup()
    {
        gameHandler.addFloatingText("+Powerup Duration", Color.green, floatingTextFontSize, gameHandler.PowerupDuration);
        gameHandler.PowerupDuration = 10f;
        yield return new WaitForSeconds(gameHandler.PowerupDuration);
        gameHandler.PowerupDuration = 5f;
        gameHandler.addFloatingText("-Powerup Duration", Color.red, floatingTextFontSize, gameHandler.PowerupDuration);
    }
}

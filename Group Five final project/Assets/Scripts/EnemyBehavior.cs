using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour {

    GameObject player;
    public int speed = 6;
    NavMeshAgent agent;
    Vector3 initialScale;
    public GameObject FireEmitter;
    public GameObject explosionEmitter;
	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("fly");
        initialScale = transform.localScale;
        Instantiate(FireEmitter, this.transform.position, Quaternion.identity, this.transform);
	}

    float lerpVal = 0.0f;
    float minLerp = 0;
    float maxLerp = 0;
	// Update is called once per frame
	void Update () {
        //Scale the enemy up and down
        if (minLerp == 0f || maxLerp == 0f) {
            minLerp = initialScale.y;
            maxLerp = initialScale.y * 2;
        }
        transform.localScale = new Vector3(initialScale.x, Mathf.Lerp(minLerp, maxLerp, lerpVal), initialScale.z);

        lerpVal += 1.2f * Time.deltaTime;
        if (lerpVal > 1f)
        {
            float newMax = maxLerp;
            maxLerp = minLerp;
            minLerp = newMax;
            lerpVal = 0f;
        }

        if (player == null) return;
        transform.LookAt(player.transform);
        agent.SetDestination(player.transform.position);
        agent.speed = speed;
        //transform.position += transform.forward * speed * Time.deltaTime;*/
	}

    void OnDestroy()
    {
      //  Instantiate(explosionEmitter, this.transform.position, Quaternion.identity);

    }
}

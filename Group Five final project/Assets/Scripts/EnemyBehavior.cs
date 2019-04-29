using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour {

    GameObject player;
    public int speed = 6;
    NavMeshAgent agent;
	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("fly");
	}
	
	// Update is called once per frame
	void Update () {
        if (player == null) return;
        transform.LookAt(player.transform);
        agent.SetDestination(player.transform.position);
        agent.speed = speed;
        //transform.position += transform.forward * speed * Time.deltaTime;*/
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointSpawner : MonoBehaviour {

    public List<GameObject> SpawnPoints;
    public GameObject CoinPrefab;
    public GameObject EnemyPrefab;
    float currentCountDownVal;
	// Use this for initialization
	void Start () {
        StartCoroutine(Spawn(5));
	}

    public IEnumerator Spawn(float delay)
    {
        currentCountDownVal = delay;
        while (currentCountDownVal > 0)
        {
            yield return new WaitForSeconds(1.0f);
            currentCountDownVal--;
        }

        //Spawn Logic

        var rand = Random.Range(0, 4);
        var transform = SpawnPoints[Random.Range(0, SpawnPoints.Count)].transform;
        if (rand <= 1)
        {
            
            Instantiate(EnemyPrefab, transform.position, Quaternion.identity);
            //Spawn Enemy
        }
        else if (rand > 1)
        {
            //Spawn Coin
            Instantiate(CoinPrefab, transform.position + new Vector3(0, 1, 0), CoinPrefab.transform.rotation);
        }

        //Rerun routine
        StartCoroutine(Spawn(delay));
    }
}

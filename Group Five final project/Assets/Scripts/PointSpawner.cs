using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointSpawner : MonoBehaviour {

    public List<GameObject> SpawnPoints;
    public GameObject CoinPrefab;
    public GameObject EnemyPrefab;
    public GameObject JumpPrefab;
    public GameObject StarPrefab;
    public GameObject SpeedupPrefab;
    public GameObject DoubleCoinPrefab;

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

        //var rand = Random.Range(0, 4);
        var transform = SpawnPoints[UnityEngine.Random.Range(0, SpawnPoints.Count)].transform;
        Dictionary<GameObject, int> spawnChances = new Dictionary<GameObject, int>
        {
            { CoinPrefab, 45 },
            { SpeedupPrefab, 20 },
            { EnemyPrefab, 40 },
            { StarPrefab, 15 },
            { JumpPrefab, 30 },
            { DoubleCoinPrefab, 40 }
        };
        //Uses weighted chances
        GameObject chosenPrefab = null;
        var range = 0;
        foreach(KeyValuePair<GameObject, int> pair in spawnChances)
        {
            range += pair.Value;
        }

        var rand = UnityEngine.Random.Range(0, range);
        int top = 1;
        foreach (KeyValuePair<GameObject, int> pair in spawnChances)
        {
            top = top + pair.Value;
            if (rand < top)
            {
                chosenPrefab = pair.Key;
                break;
            }
        }
        
        Instantiate(chosenPrefab, transform.position, Quaternion.identity);
        //Rerun routine
        StartCoroutine(Spawn(delay));
    }
}

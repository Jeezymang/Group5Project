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
    public GameObject SpawnRatePrefab;
    public GameObject DurationPrefab;
    public GameHandler gameHandler;
    public float DespawnTime = 15f;

    float currentCountDownVal;
	// Use this for initialization
	void Start () {
        gameHandler = GameObject.Find("GameHandler").GetComponent<GameHandler>();
        StartCoroutine(Spawn(gameHandler.SpawnRate));
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
            { EnemyPrefab, 70 },
            { StarPrefab, 15 },
            { JumpPrefab, 30 },
            { DoubleCoinPrefab, 40 },
            { SpawnRatePrefab, 10 },
            { DurationPrefab, 15 }
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

        var boxCollider = chosenPrefab.GetComponent<BoxCollider>();
        //var colliderCenter = Vector3.Scale(chosenPrefab.transform.localScale, boxCollider.center);
        //Adjust collider size with scale of object.
        var colliderSize = Vector3.Scale(chosenPrefab.transform.localScale, boxCollider.size);
        //Check if powerup exist in spot already.
        float addY = colliderSize.y;
        if (Math.Abs(chosenPrefab.transform.localRotation.x) > 1) //Needed because prefabs with different rotations screws this up.
            addY = colliderSize.z;
        if (Physics.OverlapBox(transform.position + new Vector3(0, addY/4, 0), colliderSize * 2, Quaternion.identity, LayerMask.GetMask("SpawnItem"), QueryTriggerInteraction.Collide).Length > 0)
        {
            Debug.Log("Powerup exists here already.");
        }
        else
        {
            var gameObj = Instantiate(chosenPrefab, transform.position + new Vector3(0, addY/4, 0), chosenPrefab.transform.localRotation);
            if (chosenPrefab != EnemyPrefab) //Do not despawn enemies.
                StartCoroutine(Despawn(gameObj, DespawnTime));
        }

        //This method wasn't working too well although I left it commented incase we want to use it again.
        //RaycastHit rayHit;
        //if (Physics.Raycast(transform.position, transform.TransformDirection(-Vector3.up), out rayHit, 15))
        //{
        //var gameObj = Instantiate(chosenPrefab, transform.position + new Vector3(0, addY/4, 0), chosenPrefab.transform.localRotation);
        //}

        //Rerun routine
        StartCoroutine(Spawn(delay));
    }

    public IEnumerator Despawn(GameObject obj, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(obj);
    }
}

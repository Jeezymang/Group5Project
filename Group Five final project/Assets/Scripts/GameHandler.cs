using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour {

    public int Score = 0;
    public bool IsPaused = false;

	// Use this for initialization
	void Start () {
		
	}

    public void addScore(int points)
    {
        Score += points;
    }
}

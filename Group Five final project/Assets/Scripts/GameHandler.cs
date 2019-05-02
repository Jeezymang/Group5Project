using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour {

    public int Score = 0;
    public bool IsPaused = false;
    public bool CanJump = false;
    public AudioSource gameAudio;
    public AudioClip death;
    public AudioClip bgm;
    public AudioClip menuMusic;
    public AudioClip coinGet;
    public GameObject sceneCanvas;

	// Use this for initialization
	void Start () {
        gameAudio = this.GetComponent<AudioSource>();

         sceneCanvas = GameObject.Find("Canvas");

    }

    public void addScore(int points)
    {
        Score += points;
    }

    void Update()
    {
        
    }

}

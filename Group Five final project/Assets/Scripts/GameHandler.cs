using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour {

    public int Score = 0;
    public bool IsPaused = false;
    public bool CanJump = false;
    public int SpawnRate = 5; //In Seconds
    public float PowerupDuration = 5f; //In Seconds
    public AudioSource gameAudio;
    public AudioClip death;
    public AudioClip bgm;
    public AudioClip menuMusic;
    public AudioClip coinGet;
    public AudioClip powerupGet;
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

    public void addFloatingText(string text, Color color, int fontSize, float lifeTime)
    {
        sceneCanvas.GetComponent<IngameMenu>().AddFloatingText(text, color, fontSize, lifeTime);
    }

    void Update()
    {
        
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class playGamesController : MonoBehaviour {

   

	// Use this for initialization
	void Start () {

        AuthencateUser();

	}
	
    void AuthencateUser()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();
        Social.localUser.Authenticate((bool success) =>
        {
            if (success == true)
            {
                Debug.Log("Logged in to Google Play Services");


            }

            else
            {
                
               
            }

        });


    }

	// Update is called once per frame
	void Update () {
		
	}
}

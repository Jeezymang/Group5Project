﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuOps : MonoBehaviour {

	public void PlayButtonClicked()
    {
        //Load the game scene
        SceneManager.LoadScene("SampleScene");
    }

    public void ScoreboardButtonClicked()
    {
        //Load the scoreboard
        Application.OpenURL("https://antonop7.uwmsois.com/scoreboard.php");
    }

    public void ExitButtonClicked()
    {
        //Exit the game.
        Application.Quit();
    }

    public void SettingButtonClicked()
    {
        //Load the settings scene
    }
}

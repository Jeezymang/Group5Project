using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngameMenu : MonoBehaviour {

    public GameObject OptionsPanel;
    public Button PauseButton;
    public Text ScoreText;
    public GameObject PauseText;

    GameHandler gameHandler;
	// Use this for initialization
	void Start () {
        var handler = GameObject.Find("GameHandler");
        if (handler)
            gameHandler = handler.GetComponent<GameHandler>();

        OptionsPanel.SetActive(false);
	}

    public void SettingsButtonClicked()
    {
        if (OptionsPanel.activeSelf == true)
            OptionsPanel.SetActive(false);
        else
            OptionsPanel.SetActive(true);
        PauseButtonClicked();
    }

    public void PauseButtonClicked()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            var colors = PauseButton.colors;
            colors.normalColor = Color.red;
            PauseButton.colors = colors;
            gameHandler.IsPaused = true;
            PauseText.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            var colors = PauseButton.colors;
            colors.normalColor = Color.white;
            PauseButton.colors = colors;
            gameHandler.IsPaused = false;
            PauseText.SetActive(false);
        }
            
    }

    public void VolumeToggled()
    {
        if (AudioListener.volume == 1)
            AudioListener.volume = 0;
        else
            AudioListener.volume = 1;
    }

    void Update()
    {
        if (gameHandler == null) return;
        ScoreText.text = "Score: " + gameHandler.Score;
    }
}

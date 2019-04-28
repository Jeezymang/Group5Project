using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngameMenu : MonoBehaviour {

    public GameObject OptionsPanel;
    public Button PauseButton;
	// Use this for initialization
	void Start () {
        OptionsPanel.SetActive(false);
	}

    public void SettingsButtonClicked()
    {
        if (OptionsPanel.activeSelf == true)
            OptionsPanel.SetActive(false);
        else
            OptionsPanel.SetActive(true);
    }

    public void PauseButtonClicked()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            var colors = PauseButton.colors;
            colors.normalColor = Color.red;
            PauseButton.colors = colors;
        }
        else
        {
            Time.timeScale = 1;
            var colors = PauseButton.colors;
            colors.normalColor = Color.white;
            PauseButton.colors = colors;
        }
            
    }

    public void VolumeToggled()
    {
        if (AudioListener.volume == 1)
            AudioListener.volume = 0;
        else
            AudioListener.volume = 1;
    }
}

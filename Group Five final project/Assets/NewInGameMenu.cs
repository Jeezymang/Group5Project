using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewInGameMenu : MonoBehaviour
{

    

        public GameObject OptionsPanel;
        public Button PauseButton;
        public Text ScoreText;
        public GameObject PauseText;
        public List<FloatingText> FloatingTextList = new List<FloatingText>();

        
        // Use this for initialization
        void Start()
        {
           

            OptionsPanel.SetActive(false);
        }

        float lastAddTime = 0;
        int offset = 0;
        public void AddFloatingText(string text, Color color, int fontSize, float lifeTime)
        {
            if ((lastAddTime + 1) > Time.time)
                offset += 1;
            else
                offset = 0;

            lastAddTime = Time.time;
            var floatingText = new FloatingText(text, color, fontSize, lifeTime);
            floatingText.Offset = offset;
            FloatingTextList.Add(floatingText);
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
                
                PauseText.SetActive(true);
            }
            else
            {
                Time.timeScale = 1;
                var colors = PauseButton.colors;
                colors.normalColor = Color.white;
                PauseButton.colors = colors;
                
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
            
        }

        float labelHeight = 50;
        void OnGUI()
        {
            for (int i = 0; i < FloatingTextList.Count; i++)
            {
                FloatingText text = FloatingTextList[i];
                Rect textRect = new Rect(Screen.width * 0.2f, Screen.height * 0.1f + (labelHeight * text.Offset), Screen.width, labelHeight);
                if (Time.time > text.DeathTime)
                {
                    FloatingTextList.RemoveAt(i);
                    continue;
                }
                var timeOffset = (text.DeathTime - Time.time) / text.LifeTime;
                float maxYOffset = Screen.height * 0.35f;
                var currentColor = text.Style.normal.textColor;
                text.Style.normal.textColor = new Color(currentColor.r, currentColor.g, currentColor.b, 2f * timeOffset);
                //text.Style.fontSize = (int)Math.Round(Convert.ToSingle(text.FontSize) * timeOffset);
                GUI.Label(new Rect(textRect.x, textRect.y + (maxYOffset - (maxYOffset * timeOffset)), textRect.width, textRect.height), text.Text, text.Style);
            }
        }
    }


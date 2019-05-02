using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour {

    public InputField NameInput;
    public GameObject EndMenu;
    GameHandler gameHandler;
	// Use this for initialization
	void Start () {
        var handler = GameObject.Find("GameHandler");
        if (handler)
            gameHandler = handler.GetComponent<GameHandler>();
       
        EndMenu.SetActive(false);
	}
	
    public void SubmitScorePressed()
    {
        if (NameInput.text == "") return;
        StartCoroutine(PostScore(NameInput.text, gameHandler.Score));
        QuitPressed();
    }

    public void RetryPressed()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void QuitPressed()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    void Update()
    {
     if (EndMenu.activeSelf == true)
        {
            gameHandler.gameAudio.Stop();
        }    
    }

    IEnumerator PostScore(string name, int score)
    {
        WWWForm form = new WWWForm();
        form.AddField("Name", name);
        form.AddField("Score", score);
        form.AddField("Pass", "securepassword");

        UnityWebRequest req = UnityWebRequest.Post("https://antonop7.uwmsois.com/scoreboard.php", form);

        yield return req.SendWebRequest();

        if (req.isNetworkError)
        {
            Debug.Log(req.error);
        }
    }
}

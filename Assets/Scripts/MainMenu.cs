using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour
{
    public Text playerDisplay;
    public Button play;
    public Button level;
    public Button leaderboards;
    public Button help;
    public BackgroundMusic backgroundMusic;
    public void PlayGame()
    {
        backgroundMusic.Stop();
        SceneManager.LoadScene(7);
    }

    public void GoToHighScores()
    {
        if (Common.IsControllerConnected())
        {
            PlayerPrefs.SetString("lastButton", "leaderboards");
            PlayerPrefs.Save();
        }
        SceneManager.LoadScene(4);
    }

    public void GoToHelp()
    {
        if (Common.IsControllerConnected())
        {
            PlayerPrefs.SetString("lastButton", "help");
            PlayerPrefs.Save();
        }
        SceneManager.LoadScene(5);
    }

    public void GoToLevelSelect()
    {
        if (Common.IsControllerConnected())
        {
            PlayerPrefs.SetString("lastButton", "level");
            PlayerPrefs.Save();
        }
        SceneManager.LoadScene(6);
    }

    // Start is called before the first frame update
    void Start()
    {
        playerDisplay.text = "Player: " + DBManager.GetUsername();
        if (Common.IsControllerConnected())
        {
            var lastButton = PlayerPrefs.GetString("lastButton");
            if (lastButton == "level")
            {
                level.Select();
                level.OnSelect(null);
            }
            else if (lastButton == "leaderboards")
            {
                leaderboards.Select();
                leaderboards.OnSelect(null);
            }
            else if (lastButton == "help")
            {
                help.Select();
                help.OnSelect(null);
            }
            else
            {
                play.Select();
                play.OnSelect(null);
            }
            PlayerPrefs.SetString("lastButton", "");
            PlayerPrefs.Save();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Quit();
        }
    }

    public void Quit()
    {
        DBManager.Logout();
        SceneManager.LoadScene(0);
        //Application.Quit();
        //UnityEditor.EditorApplication.isPlaying = false;
    }
}

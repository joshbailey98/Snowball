using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    private bool isPaused = false;
    public GameObject pauseCanvas;
    public GameObject levelCompleteCanvas;
    public Button resume;
    public GameObject[] dialogBoxes;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (levelCompleteCanvas.activeSelf)
        {
            Time.timeScale = 0f;
        }
        else if (isPaused)
        {
            pauseCanvas.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            pauseCanvas.SetActive(false);
            Time.timeScale = 1f;
        }

        foreach (var dialog in dialogBoxes)
        {
            if (dialog.activeSelf)
                Time.timeScale = 0f;
        }

        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown("joystick button 7"))
        {
            isPaused = !isPaused;
            if (isPaused && Common.IsControllerConnected())
            {
                resume.Select(); 
                resume.OnSelect(null);
            }
        }
    }

    public void Resume()
    {
        isPaused = false;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToLevelSelect()
    {
        SceneManager.LoadScene(6);
    }

    public void Quit()
    {
        SceneManager.LoadScene(3);
    }
}

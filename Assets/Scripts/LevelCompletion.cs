using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelCompletion : MonoBehaviour
{
    public GameObject levelCompleteCanvas;
    public Button nextLevelButton;
    public Button levelSelectButton;
    private bool last = false;
    private bool levelComplete = false;
    // Start is called before the first frame update
    void Start()
    {
        levelCompleteCanvas.SetActive(false);
        if (SceneManager.GetActiveScene().buildIndex == 6 + DBManager.maxLevel)
        {
            nextLevelButton.gameObject.SetActive(false);
            last = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!levelComplete && levelCompleteCanvas.activeSelf && Common.IsControllerConnected())
        {
            levelComplete = true;
            if (!last)
            {
                nextLevelButton.Select();
                nextLevelButton.OnSelect(null);
            }
            else
            {
                levelSelectButton.Select();
                levelSelectButton.OnSelect(null);
            }
        }
    }

    public void NextLevel()
    {
        var currentIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentIndex + 1);
    }

    public void LevelSelect()
    {
        SceneManager.LoadScene(6);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        SceneManager.LoadScene(3);
    }
}

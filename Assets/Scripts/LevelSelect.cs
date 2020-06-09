using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    public Text score;
    public Text time;
    public Text gems;
    public Text levelText;
    public Image levelImage;
    public Image gemImage;
    public Image left;
    public Image right;
    public Button leftButton;
    public Button rightButton;
    public int level;
    private List<int> maxGems;
    public List<Sprite> levelPreviews;
    public BackgroundMusic backgroundMusic;
    // Start is called before the first frame update
    void Start()
    {
        DisableLeft();
        DisableRight();
        gemImage.enabled = false;
        level = 1;
        maxGems = new List<int>();
        for (var i = 0; i < DBManager.maxLevel; i++)
            maxGems.Add(3);
        LoadSprites();
        StartCoroutine(GetScores());
    }

    IEnumerator GetScores()
    {
        levelText.text = "Level " + level;
        var form = new WWWForm();
        form.AddField("level", level);
        form.AddField("username", DBManager.GetUsername());
        using (var www = UnityWebRequest.Post("http://snowball.us-west-2.elasticbeanstalk.com/getlevelscores.php", form))
        {
            yield return www.SendWebRequest();
            if (!www.isNetworkError && !www.isHttpError && www.downloadHandler.text != "")
            {
                var parts = www.downloadHandler.text.Split('\t');
                score.text = "High Score: " + parts[0];
                time.text = "Best Time: " + parts[1] + " s";
                gems.text = parts[2] + " / " + maxGems[level - 1];
                gemImage.enabled = true;
            }
            else
            {
                score.text = "";
                time.text = "";
                gems.text = "";
                gemImage.enabled = false;
            }
            if (level > 1)
                EnableLeft();
            else DisableLeft();
            if (level < DBManager.maxLevel && www.downloadHandler.text != "")
                EnableRight();
            else DisableRight();
            levelImage.sprite = levelPreviews[level - 1];
        }
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(3);
    }

    public void GoToLevel()
    {
        backgroundMusic.Stop();
        SceneManager.LoadScene(6 + level);
    }

    public void NextLevel()
    {
        level++;
        StartCoroutine(GetScores());
    }

    public void PreviousLevel()
    {
        level--;
        StartCoroutine(GetScores());
    }

    // Update is called once per frame
    void Update()
    {
        if (Common.IsControllerConnected())
        {
            if (Input.GetButtonDown("Cancel"))
                SceneManager.LoadScene(3);
            var input = Input.GetAxis("Horizontal");
            if (Input.GetKeyDown("joystick button 0"))
                GoToLevel();
            else if (input > 0 && right.enabled)
            {
                NextLevel();
                right.enabled = false;
            }
            else if (input < 0 && left.enabled)
            {
                PreviousLevel();
                left.enabled = false;
            }
        }
    }

    private void DisableLeft()
    {
        left.enabled = false;
        leftButton.enabled = false;
    }

    private void DisableRight()
    {
        right.enabled = false;
        rightButton.enabled = false;
    }

    private void EnableLeft()
    {
        left.enabled = true;
        leftButton.enabled = true;
    }

    private void EnableRight()
    {
        right.enabled = true;
        rightButton.enabled = true;
    }

    private void LoadSprites()
    {
        for (var i = 1; i <= DBManager.maxLevel; i++)
        {
            levelPreviews.Add(Resources.Load<Sprite>("level" + i));
        }
    }
}

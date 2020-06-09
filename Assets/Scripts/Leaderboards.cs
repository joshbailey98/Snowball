using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;
public class Leaderboards : MonoBehaviour
{
    public List<Text> players;
    public List<Text> scores;
    public List<Text> times;
    public List<Text> gems;
    public Image left;
    public Image right;
    public Button leftButton;
    public Button rightButton;
    public Text levelText;
    public int level;
    private string sort = "score";
    // Start is called before the first frame update
    void Start()
    {
        DisableLeft();
        DisableRight();
        level = 1;
        StartCoroutine(GetScores());
    }

    IEnumerator GetScores()
    {
        levelText.text = "Level " + level;
        var form = new WWWForm();
        form.AddField("level", level);
        form.AddField("sort", sort);
        using (var www = UnityWebRequest.Post("http://snowball.us-west-2.elasticbeanstalk.com/getscores.php", form))
        {
            yield return www.SendWebRequest();
            if (!www.isNetworkError && !www.isHttpError)
            { 
                var rows = www.downloadHandler.text.Split('\n');
                var count = 0;
                for (var i = 0; i < rows.Length - 1; i++)
                {
                    var parts = rows[i].Split('\t');
                    players[i].text = parts[0];
                    scores[i].text = parts[1];
                    times[i].text = parts[2];
                    gems[i].text = parts[3];
                    count++;
                }
                if (count < 9)
                {
                    for (var i = count; i < 10; i++)
                    {
                        players[i].text = "";
                        scores[i].text = "";
                        times[i].text = "";
                        gems[i].text = "";
                    }
                }
            }
            if (level > 1)
                EnableLeft();
            else DisableLeft();
            if (level < DBManager.maxLevel)
                EnableRight();
            else DisableRight();
        }
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
                Sort();
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

    public void Sort()
    {
        if (sort == "score")
        {
            GameObject.Find("Sort").GetComponentInChildren<Text>().text = "Sort by Score";
            sort = "time";
        }
        else
        {
            GameObject.Find("Sort").GetComponentInChildren<Text>().text = "Sort by Time";
            sort = "score";
        }

        StartCoroutine(GetScores());
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(3);
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
}

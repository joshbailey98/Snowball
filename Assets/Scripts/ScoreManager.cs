using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private int score = 0;
    private int totalGems;
    private int gems = 0;
    public GameObject scoreDisplay;
    public GameObject gemsDisplay;
    public GameObject timer;
    private float seconds;
    private double ms;

    // Start is called before the first frame update
    void Start()
    {
        totalGems = GameObject.FindGameObjectsWithTag("Gem").Length;
    }

    // Update is called once per frame
    void Update()
    {
        scoreDisplay.GetComponent<Text>().text = "Score:  " + score;
        gemsDisplay.GetComponent<Text>().text = gems + " / " + totalGems;
        seconds = (int)(Time.timeSinceLevelLoad);
        ms = Math.Round(Time.timeSinceLevelLoad - seconds, 3) * 1000;
        timer.GetComponent<Text>().text = "Time: " + seconds.ToString() + "." + ms.ToString("000");
    }

    void OnTriggerEnter2D(Collider2D trig)
    {
        if (trig.gameObject.tag == "Coin")
        {
            score += 100;
            Destroy(trig.gameObject);
        }

        if (trig.gameObject.tag == "Gem")
        {
            score += 1000;
            gems++;
            Destroy(trig.gameObject);
        }

        if (trig.gameObject.tag == "Igloo")
        {
            score += 3000;
            if (seconds < 60)
                score += (int)(60 - seconds) * 100;
            StartCoroutine(SaveScore());
            var levelCompletionScreen = GameObject.FindGameObjectWithTag("LevelCompletion");
            var levelCompletionCanvas = levelCompletionScreen.transform.GetChild(0).gameObject;
            levelCompletionCanvas.SetActive(true);
        }
    }

    IEnumerator SaveScore()
    {
        var form = new WWWForm();
        var level = SceneManager.GetActiveScene().name;
        form.AddField("username", DBManager.GetUsername());
        form.AddField("level", level);
        form.AddField("score", score);
        form.AddField("gems", gems);
        form.AddField("time", Math.Round(Time.timeSinceLevelLoad, 3).ToString());
        //using (var www = UnityWebRequest.Post("http://localhost/snowball/savescore.php", form))
        using (var www = UnityWebRequest.Post("http://snowball.us-west-2.elasticbeanstalk.com/savescore.php", form))
        {
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError || www.downloadHandler.text != "0")
            {
                Debug.Log("Error: " + www.downloadHandler.text);
            }
        }
    }
}

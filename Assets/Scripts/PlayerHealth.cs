using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public bool hasDied;
    public int health;
    public GameObject levelCompleteCanvas;
    private bool inDark;
    private bool inSpike;
    public GameObject pauseCanvas;
    private bool inWater;
    // Start is called before the first frame update
    void Start()
    {
        hasDied = false;
        inDark = false;
        inSpike = false;
        inWater = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasDied || gameObject.transform.localScale.x <= 0)
        {
            StartCoroutine("Die");
        }
        else if ((inSpike || gameObject.transform.localScale.x <= .5f) && gameObject.transform.localScale.x > 0)
        {
            gameObject.transform.localScale += new Vector3(-.02f, -.02f);
        }
        if (inWater)
            gameObject.transform.localScale += new Vector3(-.001f, -.001f);
        if (!hasDied && !inDark && GameObject.FindGameObjectsWithTag("Sun").Length > 0 && gameObject.transform.localScale.x > 0 && !levelCompleteCanvas.activeSelf && !pauseCanvas.activeSelf)
            Melt();
    }

    void OnTriggerEnter2D(Collider2D trig)
    {
        if (trig.gameObject.tag == "Death Plane")
        {
            hasDied = true;
        }
        else if (trig.CompareTag("Darkness"))
        {
            inDark = true;
        }
        else if (trig.CompareTag("Spike"))
        {
            inSpike = true;
        }
        else if (trig.CompareTag("water"))
        {
            inWater = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Darkness"))
        {
            inDark = false;
        }
        else if (other.CompareTag("Spike"))
        {
            inSpike = false;
        }
        else if (other.CompareTag("water"))
        {
            inWater = false;
        }
    }

    void Melt()
    {
        gameObject.transform.localScale += new Vector3(-.03f / 60, -.03f / 60);
    }

    IEnumerator Die ()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(.25f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

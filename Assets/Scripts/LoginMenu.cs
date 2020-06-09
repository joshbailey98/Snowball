using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginMenu : MonoBehaviour
{
    public InputField usernameField;
    public InputField passwordField;
    public Button login;
    private bool previousFocus = false;
    public Text loginError;
    public void CallLogin()
    {
        StartCoroutine(Login());
    }

    IEnumerator Login()
    {
        var form = new WWWForm();
        form.AddField("username", usernameField.text);
        form.AddField("password", passwordField.text);
        //using (var www = UnityWebRequest.Post("http://localhost/snowball/login.php", form))
        using (var www = UnityWebRequest.Post("http://snowball.us-west-2.elasticbeanstalk.com/login.php", form))
        {
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError || www.downloadHandler.text != "0")
            {
                loginError.text = www.downloadHandler.text;
            }
            else
            {
                DBManager.username = usernameField.text;
                //PlayerPrefs.SetString("username", usernameField.text);
                SceneManager.LoadScene(3);
            }
        }      
    }

    public void VerifyInputs()
    {
        login.interactable = usernameField.text != "" && passwordField.text != "";
    }

    // Update is called once per frame
    void Update()
    {
        if (usernameField.isFocused && Input.GetKey(KeyCode.Tab))
        {
            passwordField.Select();
        }

        if (previousFocus && usernameField.text != "" && passwordField.text != "" && (Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.KeypadEnter)))
        {
            StartCoroutine(Login());
        }
        previousFocus = passwordField.isFocused;
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }
}

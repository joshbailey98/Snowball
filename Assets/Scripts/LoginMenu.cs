using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginMenu : MonoBehaviour
{
    public InputField usernameField;
    public InputField passwordField;
    public Button login;
    private bool previousFocus = false;
    public void CallLogin()
    {
        StartCoroutine(Login());
    }
    IEnumerator Login()
    {
        var form = new WWWForm();
        form.AddField("username", usernameField.text);
        form.AddField("password", passwordField.text);
        var webRequest = new WWW("http://localhost/snowball/login.php", form);
        yield return webRequest;
        if (webRequest.text[0] == '0')
        {
            DBManager.username = usernameField.text;
            Debug.Log("User logged in successfully.");
            SceneManager.LoadScene(3);
        }
        else
        {
            Debug.Log("Login failed - " + webRequest.text);
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
    }
}

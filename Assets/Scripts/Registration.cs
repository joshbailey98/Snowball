using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Registration : MonoBehaviour
{
    public InputField emailField;
    public InputField usernameField;
    public InputField passwordField;
    public InputField confirmField;
    public Button register;
    public Text registerError;
    private bool previousFocus = false;

    public void CallRegister()
    {
        StartCoroutine(Register());
    }

    IEnumerator Register()
    {
        var form = new WWWForm();
        form.AddField("email", emailField.text);
        form.AddField("username", usernameField.text);
        form.AddField("password", passwordField.text);
        var webRequest = new WWW("http://localhost/snowball/register.php", form);
        yield return webRequest;
        if (webRequest.text == "0")
        {
            DBManager.username = usernameField.text;
            Debug.Log("User registered successfully.");
            UnityEngine.SceneManagement.SceneManager.LoadScene(3);
        }
        else
        {
            registerError.text = webRequest.text;
        }
    }

    public void VerifyInputs()
    {
        register.interactable = emailField.text != "" && usernameField.text != "" && passwordField.text != "" && passwordField.text == confirmField.text;
    }

    // Update is called once per frame
    void Update()
    {
        if (previousFocus && usernameField.text != "" && passwordField.text != "" && (Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.KeypadEnter)))
        {
            StartCoroutine(Register());
        }
        previousFocus = confirmField.isFocused;
        if (passwordField.text != "" && confirmField.text != "" && passwordField.text != confirmField.text)
            registerError.text = "Password and confirm password do not match";
        else registerError.text = "";
    }
}

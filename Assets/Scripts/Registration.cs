using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
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
    private bool dbError = false;

    public void CallRegister()
    {
        if (IsValidEmail(emailField.text))
            StartCoroutine(Register());
    }

    IEnumerator Register()
    {
        var form = new WWWForm();
        form.AddField("email", emailField.text);
        form.AddField("username", usernameField.text);
        form.AddField("password", passwordField.text);

        //using (var www = UnityWebRequest.Post("http://localhost/snowball/register.php", form))
        using (var www = UnityWebRequest.Post("http://snowball.us-west-2.elasticbeanstalk.com/register.php", form))
        {
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError || www.downloadHandler.text != "0")
            {
                registerError.text = www.downloadHandler.text;
                dbError = true;
            }
            else
            {
                DBManager.username = usernameField.text;
                SceneManager.LoadScene(3);
            }
        }
    }

    public void VerifyInputs()
    {
        register.interactable = emailField.text != "" && usernameField.text != "" && passwordField.text != "" && passwordField.text == confirmField.text && IsValidEmail(emailField.text);
    }

    // Update is called once per frame
    void Update()
    {
        if (previousFocus && usernameField.text != "" && passwordField.text != "" && (Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.KeypadEnter)))
        {
            StartCoroutine(Register());
        }
        previousFocus = confirmField.isFocused;
        if (!dbError)
        {
            if (emailField.text != "" && !emailField.isFocused && !IsValidEmail(emailField.text))
                registerError.text = "Invalid email";
            else registerError.text = "";
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }

    bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }
}

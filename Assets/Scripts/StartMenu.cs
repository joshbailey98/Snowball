using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    public Button register;
    public void GoToRegister()
    {
        SceneManager.LoadScene(1);
    }
    public void GoToLogin()
    {
        SceneManager.LoadScene(2);
    }
    // Start is called before the first frame update
    void Start()
    {
        if (Common.IsControllerConnected())
        {
            register.Select();
            register.OnSelect(null);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

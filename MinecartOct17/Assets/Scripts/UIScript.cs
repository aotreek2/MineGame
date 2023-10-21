using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClickQuitButton()
    {
        Debug.Log("Quit button was clicked");
        Application.Quit();
    }

    public void OnHelpButtonClick()
    {
        SceneManager.LoadScene("Help");
    }

    public void LevelSelectButtonClick()
    {
        SceneManager.LoadScene("Level Select");
    }

    public void StartButtonClick()
    {
        SceneManager.LoadScene("Level 0");
    }

    public void LevelOne()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void BackButtonClick()

    {
        SceneManager.LoadScene("MainMenu");

    }
}

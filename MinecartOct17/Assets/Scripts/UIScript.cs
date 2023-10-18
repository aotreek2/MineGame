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
        SceneManager.LoadScene("Level 1");
    }

<<<<<<< HEAD
    public void BackbuttonClick()
=======
    public void BackButtonClick()
>>>>>>> d64eec6d30bf1fd027a5ee374d4ef3f35a2d6089
    {
        SceneManager.LoadScene("MainMenu");

    }

  
}

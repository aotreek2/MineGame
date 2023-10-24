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
        MultiSceneScores.currentLevel = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void QuitButton()
    {
        Debug.Log("Quit button was clicked");
        Application.Quit();
    }

    public void HelpButton()
    {
        SceneManager.LoadScene("Help");
    }

    public void LevelSelection()
    {
        SceneManager.LoadScene("Level Select");
    }

    public void TutorialLevel()
    {
        SceneManager.LoadScene("Level 0");
        MultiSceneScores.currentLevel = 0;
    }

    public void LevelOne()
    {
        SceneManager.LoadScene("Level 1");
        MultiSceneScores.currentLevel = 1;
    }

    public void LevelTwo()
    {
        SceneManager.LoadScene("Level 2");
        MultiSceneScores.currentLevel = 2;
    }

    public void BackButton()
    {
        SceneManager.LoadScene("MainMenu");
        MultiSceneScores.totalHealth = 25;
        MultiSceneScores.totalMinecartHealth = 75;
        MultiSceneScores.gold = 0;
        MultiSceneScores.invine = 0;
        MultiSceneScores.diamond = 0;
        MultiSceneScores.ammo = 24;
        MultiSceneScores.ammoTwo = 0;
        MultiSceneScores.currentLevel = 0;
        MultiSceneScores.machineGunUnlocked = false;
    }
}

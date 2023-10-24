using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public bool isOpen = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (isOpen)
            {
                isOpen = false;
                transform.GetChild(0).gameObject.SetActive(false);
                transform.GetChild(1).gameObject.SetActive(false);
            }
            else
            {
                isOpen = true;
                transform.GetChild(0).gameObject.SetActive(true);
                transform.GetChild(1).gameObject.SetActive(true);
            }
        }
    }

    public void MainMenu()
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

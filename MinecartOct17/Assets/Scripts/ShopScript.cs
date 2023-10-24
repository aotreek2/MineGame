using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class ShopScript : MonoBehaviour
{

    public TMP_Text oreStats;
    public TMP_Text healthStats;

    public GameObject fadePanel;

    // Start is called before the first frame update
    void Start()
    {
        UpdateText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateText()
    {
        oreStats.text = $"Gold:{MultiSceneScores.gold}\r\nInvine:{MultiSceneScores.invine}\r\nDiamond:{MultiSceneScores.diamond}";
        healthStats.text = $"HP:{MultiSceneScores.totalHealth}/25\r\nCart HP:{MultiSceneScores.totalMinecartHealth}/75\r\nAmmo:{MultiSceneScores.ammo}";
    }

    public void HealPlayer()
    {
        if (MultiSceneScores.gold >= 5 && MultiSceneScores.totalHealth < 25)
        {
            MultiSceneScores.gold -= 5;
            MultiSceneScores.totalHealth += 5;
            if (MultiSceneScores.totalHealth > 25)
            {
                MultiSceneScores.totalHealth = 25;
            }
        }
        UpdateText();
    }

    public void HealCart(int degree)
    {
        if (degree == 0)
        {
            if(MultiSceneScores.gold >= 3 && MultiSceneScores.invine >= 3 && MultiSceneScores.totalMinecartHealth < 75)
            {
                MultiSceneScores.gold -= 3;
                MultiSceneScores.invine -= 3;
                MultiSceneScores.totalMinecartHealth += 5;
                if (MultiSceneScores.totalMinecartHealth > 75)
                {
                    MultiSceneScores.totalMinecartHealth = 75;
                }
            }
        }
        else
        {
            if (MultiSceneScores.diamond > 0 && MultiSceneScores.totalMinecartHealth < 75)
            {
                MultiSceneScores.diamond -= 1;
                MultiSceneScores.totalMinecartHealth += 25;
                if (MultiSceneScores.totalMinecartHealth > 75)
                {
                    MultiSceneScores.totalMinecartHealth = 75;
                }
            }
        }
        UpdateText();
    }

    public void Ammo()
    {
        if (MultiSceneScores.gold >= 10)
        {
            MultiSceneScores.gold -= 10;
            MultiSceneScores.ammo += 8;
        }
        UpdateText();
    }

    public void NextLevel()
    {
        if (MultiSceneScores.currentLevel == 0)
        {
            MultiSceneScores.currentLevel += 1;
            fadePanel.GetComponent<SceneChangeTrigger>().nextScene = "Level 1";
            fadePanel.GetComponent<Animator>().Play("FadeOutPanel");
        }
        else if (MultiSceneScores.currentLevel == 1)
        {
            MultiSceneScores.currentLevel += 1;
            fadePanel.GetComponent<SceneChangeTrigger>().nextScene = "Level 2";
            fadePanel.GetComponent<Animator>().Play("FadeOutPanel");
        }
    }
}

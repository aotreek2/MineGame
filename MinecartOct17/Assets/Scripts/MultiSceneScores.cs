using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiSceneScores : MonoBehaviour
{
    public static int totalHealth = 25;
    public static int totalMinecartHealth = 75;
    public static int gold = 0;
    public static int invine = 0;
    public static int diamond = 0;
    public static int ammo = 24;
    public static int ammoTwo = 0;

    public static int currentLevel = 0;
    public static bool machineGunUnlocked = false;

    //-----------Variables to store the starting variables for when they die and restart the level------

    public int totalHealth_L;
    public int totalMinecartHealth_L;
    public int gold_L;
    public int invine_L;
    public int diamond_L;
    public int ammo_L;
    public int ammoTwo_L;

    private void Start()
    {
        totalHealth_L = totalHealth;
        totalMinecartHealth_L = totalMinecartHealth;
        gold_L = gold;
        invine_L = invine;
        diamond_L = diamond;
        ammo_L = ammo;
        ammoTwo_L = ammoTwo;
    }

    public void ResetStats()
    {
        totalHealth = totalHealth_L;
        totalMinecartHealth = totalMinecartHealth_L;
        gold = gold_L;
        invine = invine_L;
        diamond = diamond_L;
        ammo = ammo_L;
        ammoTwo = ammoTwo_L;
    }
}

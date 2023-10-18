using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PickaxeManager : MonoBehaviour
{

    public bool inRange = false;
    public GameObject currentOre;
    public OreManager oreManager;

    public TextMeshProUGUI oreStats;
    public int gold = 0;
    public int diamond = 0;
    public int invine = 0;

    public AudioSource mining; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            transform.GetComponent<Animator>().Play("PickaxeSwing");
            

            if (inRange && oreManager.oreHealth > 0)
            {
                if (currentOre.tag == "goldOre")
                {
                    mining.Play();
                    gold++;
                }
                else if (currentOre.tag == "diamondOre")
                {
                    mining.Play();
                    diamond++;
                }
                else if (currentOre.tag == "invineOre")
                {
                    mining.Play();
                    invine++;
                }
                else
                {
                    mining.Play();
                    Debug.Log("Something went wrong. Or you mined rock.");
                }

                oreManager.oreHealth--;
                

                UpdateOres();
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "goldOre" || collision.tag == "diamondOre" || collision.tag == "invineOre" || collision.tag == "rock")
        {
            currentOre = collision.gameObject;
            oreManager = collision.GetComponent<OreManager>();
            inRange = true;
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "goldOre" || collision.tag == "diamondOre" || collision.tag == "invineOre" || collision.tag == "rock")
        {
            inRange = false;
        }
    }

    public void UpdateOres()
    {
        oreStats.text = $"Gold: {gold} / Diamond: {diamond} / Invine: {invine}";
    }

    public void Sparks()
    {
        if (inRange)
        {
            currentOre.transform.parent.GetChild(1).transform.GetComponent<ParticleSystem>().Play();
            //currentOre.transform.GetChild(0).transform.GetComponent<ParticleSystem>().Play();

            if (oreManager.oreHealth <= 0)
            {
                currentOre.SetActive(false);
            }
        }
    }
}

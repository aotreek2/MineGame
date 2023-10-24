using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PickaxeManager : MonoBehaviour
{
    public TextMeshProUGUI oreStats;
    public int gold = 0;
    public int diamond = 0;
    public int invine = 0;

    public AudioSource mining;
    public AudioSource diamondTing;

    // Ore ping thing
    public Animator orePing;
    private Animator animator;


    public bool active = false;
    private Collider2D standingCollider;

    private Switching switchScript;

    public void Awake()
    {
        //Sets up the UI when scene loads
        gold = MultiSceneScores.gold;
        invine = MultiSceneScores.invine;
        diamond = MultiSceneScores.diamond;
        UpdateOres();
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        standingCollider = GetComponent<Collider2D>();

        switchScript = transform.parent.GetComponent<Switching>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(switchScript.isDucking == false)
            {
                transform.GetComponent<Animator>().Play("PickaxeSwing");
            }
        }

        if (Input.GetKeyDown(KeyCode.S)) // when player presses S, miner ducks
        {
            if (!switchScript.isDucking)
            {
                animator.Play("axeduck");
                switchScript.isDucking = true;
                standingCollider.enabled = false;

                active = false;
            }
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            if (switchScript.isDucking)
            {
                animator.Play("axerising");
                switchScript.isDucking = false;
                standingCollider.enabled = true;

                active = true;
            }
        }
    }


    public void SwingPickaxe(GameObject minedObject)
    {
        if (active == true)
        {
            if (minedObject.tag == "goldOre" || minedObject.tag == "invineOre" || minedObject.tag == "diamondOre" || minedObject.tag == "rock")
            {
                //Plays sound and makes sparks
                mining.pitch = Random.Range(1f, 1.8f);
                mining.Play();
                minedObject.transform.parent.transform.GetChild(1).transform.GetComponent<ParticleSystem>().Play();

                //Adds correct ore
                switch (minedObject.tag)
                {
                    case "goldOre":
                        gold++;
                        break;
                    case "invineOre":
                        invine++;
                        break;
                    case "diamondOre":
                        diamond++;
                        diamondTing.Play();
                        break;
                    default:
                        break;
                }

                //Damages ore and checks if it should be destroyed
                minedObject.GetComponent<MineableObject>().objectHealth -= 1;
                if (minedObject.GetComponent<MineableObject>().objectHealth <= 0)
                {
                    Destroy(minedObject);
                    minedObject.transform.parent.transform.GetChild(2).transform.GetComponent<ParticleSystem>().Play();
                }

                //Updates UI
                UpdateOres();
            }
            else if (minedObject.tag == "Zombie")
            {
                mining.pitch = Random.Range(1f, 1.8f);
                mining.Play();

                minedObject.GetComponent<Zombie>().TakeDamage(2);
            }
            else if (minedObject.tag == "bat")
            {
                mining.pitch = Random.Range(1f, 1.8f);
                mining.Play();

                minedObject.GetComponent<Bat>().TakeDamage(2);
            }
        }
        else
        {
            Debug.Log("Pickaxe not active");
        }
    }

    public void UpdateOres()
    {
        oreStats.text = $"Gold: {gold} / Invine: {invine} / Diamond: {diamond}";
        MultiSceneScores.gold = gold;
        MultiSceneScores.invine = invine;
        MultiSceneScores.diamond = diamond;
    }
}

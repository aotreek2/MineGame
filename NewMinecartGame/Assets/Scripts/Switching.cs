using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Switching : MonoBehaviour
{

    public GameObject[] PlayerState;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Switching
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            PlayerState[0].SetActive(true);
            PlayerState[1].SetActive(false);
            PlayerState[2].SetActive(false);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            PlayerState[0].SetActive(false);
            PlayerState[1].SetActive(true);
            PlayerState[2].SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            PlayerState[0].SetActive(false);
            PlayerState[1].SetActive(false);
            PlayerState[2].SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }


}


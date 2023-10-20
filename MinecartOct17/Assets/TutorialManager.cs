using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{

    public GameObject startingPanel;
    public GameObject startingBlock;

    public GameObject secondPanel;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            startingBlock.SetActive(false);
            startingPanel.SetActive(false);

            secondPanel.SetActive(true);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    public GameObject previousPanel;
    public GameObject triggeredPanel;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Minecart")
        {
            previousPanel.SetActive(false);
            triggeredPanel.SetActive(true);
        }
    }
}

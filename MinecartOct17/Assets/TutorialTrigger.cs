using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    public GameObject previousPanel;
    public GameObject triggeredPanel;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        previousPanel.SetActive(false);
        triggeredPanel.SetActive(true);
    }
}

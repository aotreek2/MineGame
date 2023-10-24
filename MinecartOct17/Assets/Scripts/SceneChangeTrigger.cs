using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeTrigger : MonoBehaviour
{

    public string nextScene;
    public GameObject fadePanel;

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Minecart")
        {
            fadePanel.GetComponent<Animator>().Play("FadeOutPanel");
        }
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(nextScene);
    }
}

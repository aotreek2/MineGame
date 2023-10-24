using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathBox : MonoBehaviour
{

    public MultiSceneScores multiSceneScores;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Minecart")
        {
            multiSceneScores.ResetStats();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}

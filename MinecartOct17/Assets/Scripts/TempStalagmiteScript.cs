using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TempStalagmiteScript : MonoBehaviour
{
    public TMP_Text healthUI;
    public UnityEngine.UI.Slider healthSlider;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            healthSlider.value -= 5;
            healthUI.text = healthSlider.value + "/" + healthSlider.maxValue;

            if(healthSlider.value <= healthSlider.minValue)
            {
                //you die
            }
        }
    }
}

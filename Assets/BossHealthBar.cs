using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{

    public Slider slider;

    public void SetMaxHeatlh(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }
    public void SetHealth(int health)
    {
        
        slider.value = health;
    }
    public void Update()
    {
        if (slider.value <= (slider.maxValue / 4))
        {
            Image fill = transform.GetChild(0).GetComponent<Image>();
            fill.color = Color.blue;
        }
    }
}

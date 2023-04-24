using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider; //reference to the slider

    public void SetMaxHealth(int health) //method to set max health of the slider
    {
        slider.maxValue = health;
        slider.value = health;
    }
    public void SetHealth(int health) //method to set the current health of the slider
    {
        slider.value = health;
    }
}

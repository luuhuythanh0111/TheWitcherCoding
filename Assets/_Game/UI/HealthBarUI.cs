using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    public Slider sliderBloodBar;

    public void OnChangeHP(float value)
    {
        sliderBloodBar.value = Mathf.Clamp01(value);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpBarUI : MonoBehaviour
{
    public Slider sliderExpBar;

    public void OnChangeExp(float value)
    {
        sliderExpBar.value = Mathf.Clamp01(value);
    }
}

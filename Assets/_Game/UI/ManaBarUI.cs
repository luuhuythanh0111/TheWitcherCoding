using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBarUI : MonoBehaviour
{
    public Slider sliderManaBar;

    public void OnChangeMana(float value)
    {
        sliderManaBar.value = Mathf.Clamp01(value);
    }
}

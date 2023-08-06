using System.Collections;   
using UnityEngine;
using UnityEngine.UI;

public class LoadingLevel : MonoBehaviour
{
    public Slider slider;
    public float CounterTime = 0;

    public GameObject SliderLoading;
    public GameObject TapToPlayBtn;

    private float timer = 0;

    private void Start()
    {
        slider.value = 0;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer > CounterTime)
        {
            if(TapToPlayBtn.activeSelf)
            {

            }
            else
            {
                TapToPlayBtn.SetActive(true);
                SliderLoading.SetActive(false);
            }
        }
        else
        {
            slider.value += 30 * Time.deltaTime;
        }
    }

}

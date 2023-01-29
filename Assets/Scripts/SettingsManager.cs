using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public Slider heightSlider;
    public Slider widthSlider;
    public Slider numberCountSlider;
    
    public TextMeshProUGUI heightSliderText;
    public TextMeshProUGUI widthSliderText;
    public TextMeshProUGUI numberCountSliderText;

    public static int height;
    public static int width;
    public static int numbersCount;

    private void Start()
    {
        if (PlayerPrefs.HasKey("height"))
        {
            height = PlayerPrefs.GetInt("height");
            width = PlayerPrefs.GetInt("width");
            numbersCount = PlayerPrefs.GetInt("numbersCount");
        }
        else
        {
            height = 4;
            width = 5;
            numbersCount = 10;

            PlayerPrefs.SetInt("height", height);
            PlayerPrefs.SetInt("width", width);
            PlayerPrefs.SetInt("numbersCount", numbersCount);
        }

        heightSlider.value = height;
        widthSlider.value = width;
        numberCountSlider.value = numbersCount;

        heightSliderText.text = height.ToString();
        widthSliderText.text = width.ToString();
        numberCountSliderText.text = numbersCount.ToString();
    }

    public void OnSliderValueChanged()
    {
        height = Convert.ToInt32(heightSlider.value);
        width = Convert.ToInt32(widthSlider.value);
        numbersCount = Convert.ToInt32(numberCountSlider.value);

        heightSliderText.text = height.ToString();
        widthSliderText.text = width.ToString();
        numberCountSliderText.text = numbersCount.ToString();

        PlayerPrefs.SetInt("height", height);
        PlayerPrefs.SetInt("width", width);
        PlayerPrefs.SetInt("numbersCount", numbersCount);
    }
}

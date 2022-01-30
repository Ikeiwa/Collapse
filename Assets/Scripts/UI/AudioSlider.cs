using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.UI;

public class AudioSlider : MonoBehaviour
{
    public Slider slider;
    public string prefName;
    public AudioMixer mixerGroup;

    private void Awake()
    {
        slider.value = PlayerPrefs.GetFloat(prefName, 0.75f);
        slider.onValueChanged.AddListener(delegate { OnSliderValueChanged(); });
    }

    private void OnSliderValueChanged()
    {
        mixerGroup.SetFloat(prefName, Mathf.Lerp(-80, 0, slider.value));
        PlayerPrefs.SetFloat(prefName,slider.value);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioEffectsController : MonoBehaviour
{
    public AudioMixer audioMixer;
    public static AudioEffectsController instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        instance = this;
    }

    public void SetShieldEffect(bool state)
    {
        if (state)
        {
            StartCoroutine(UtilityCoroutines.FadeMixerParam(audioMixer, "ShieldLow", 0.25f, 0.5f));
            StartCoroutine(UtilityCoroutines.FadeMixerParam(audioMixer, "ShieldHigh", 0.75f, 0.5f));
        }
        else
        {
            audioMixer.SetFloat("ShieldLow", 0);
            audioMixer.SetFloat("ShieldHigh", 1);
        }
        
    }

    public void SetLowPassEffect(float intensity)
    {
        audioMixer.SetFloat("LowPass", (1 - intensity) * 22000.0f);
    }

    public void SetLowPassEffect(float intensity, float duration)
    {
        StartCoroutine(UtilityCoroutines.FadeMixerParam(audioMixer, "LowPass", (1-intensity)*22000.0f, 0.5f));
    }

    public void SetAudioSpeed(float speed)
    {
        audioMixer.SetFloat("Speed", speed);
    }

    public void SetAudioSpeed(float speed, float duration)
    {
        StartCoroutine(UtilityCoroutines.FadeMixerParam(audioMixer, "Speed", speed, 0.5f));
    }
}

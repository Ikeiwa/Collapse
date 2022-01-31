using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// Une collection de coroutines qui peuvent �tre utiles dans pas mal de situations.
/// Elles peuvent ètre appellé depuis n'importe quel monobehaviour
/// </summary>
public static class UtilityCoroutines
{

    public static IEnumerator FadeVolume(AudioSource audioSource, float newVolume, float duration, bool autoStop = false, AnimationCurve curve = null)
    {
        curve ??= CurveLibrary.linear;

        float baseVolume = audioSource.volume;
        float timer = 0;
        while (timer < duration)
        {
            audioSource.volume = Mathf.Lerp(baseVolume,newVolume, curve.Evaluate(timer / duration));
            timer += Time.unscaledDeltaTime;
            yield return null;
        }

        audioSource.volume = newVolume;

        if(autoStop && newVolume == 0)
            audioSource.Stop();
    }

    public static IEnumerator FadeTimeSpeed(float newSpeed, float duration, AnimationCurve curve = null)
    {
        curve ??= CurveLibrary.linear;

        float baseSpeed = Time.timeScale;
        float timer = 0;
        while (timer < duration)
        {
            Time.timeScale = Mathf.Lerp(baseSpeed, newSpeed, curve.Evaluate(timer / duration));
            timer += Time.unscaledDeltaTime;
            yield return null;
        }

        Time.timeScale = newSpeed;
    }

    public static IEnumerator FadeMixerParam(AudioMixer audioMixer, string paramName, float newValue, float duration, AnimationCurve curve = null)
    {
        curve ??= CurveLibrary.linear;

        audioMixer.GetFloat(paramName,out float baseValue);
        float timer = 0;
        while (timer < duration)
        {
            audioMixer.SetFloat(paramName, Mathf.Lerp(baseValue, newValue, curve.Evaluate(timer / duration)));
            timer += Time.unscaledDeltaTime;
            yield return null;
        }

        audioMixer.SetFloat(paramName, newValue);
    }
}

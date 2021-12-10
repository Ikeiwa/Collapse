using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessController : MonoBehaviour
{
    public PostProcessVolume volume;

    private LensDistortion lensDistortion;
    private ChromaticAberration chromaticAberration;

    public static PostProcessController instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        instance = this;

        volume.profile.TryGetSettings(out lensDistortion);
        volume.profile.TryGetSettings(out chromaticAberration);
    }

    public void SetLensDistortion(float value, float duration = 0, AnimationCurve curve = null)
    {
        if (duration == 0)
            lensDistortion.intensity.value = value;
        else
            StartCoroutine(FadeLensDistortion(value, duration, curve));
    }

    public IEnumerator FadeLensDistortion(float newValue, float duration, AnimationCurve curve = null)
    {
        curve ??= CurveLibrary.linear;

        float baseValue = lensDistortion.intensity.value;
        float timer = 0;
        while (timer < duration)
        {
            lensDistortion.intensity.value = Mathf.Lerp(baseValue, newValue, curve.Evaluate(timer / duration));
            timer += Time.unscaledDeltaTime;
            yield return null;
        }
        lensDistortion.intensity.value = newValue;
    }

    public void SetChromaticAberation(float value, float duration = 0, AnimationCurve curve = null)
    {
        if (duration == 0)
            lensDistortion.intensity.value = value;
        else
            StartCoroutine(FadeChromaticAberation(value, duration, curve));
    }

    public IEnumerator FadeChromaticAberation(float newValue, float duration, AnimationCurve curve = null)
    {
        curve ??= CurveLibrary.linear;

        float baseValue = lensDistortion.intensity.value;
        float timer = 0;
        while (timer < duration)
        {
            chromaticAberration.intensity.value = Mathf.Lerp(baseValue, newValue, curve.Evaluate(timer / duration));
            timer += Time.unscaledDeltaTime;
            yield return null;
        }
        chromaticAberration.intensity.value = newValue;
    }
}

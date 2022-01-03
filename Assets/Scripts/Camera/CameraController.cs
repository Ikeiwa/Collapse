using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    public PlayerController player;

    public float height = 4.5f;
    public float depth = 6;
    public float pitch = 0;

    public float heightCombat = 7.5f;
    public float depthCombat = 4;
    public float pitchCombat = 30;

    public float positionFactor = 0.4f;
    public float rotationFactor = 0.5f;
    public float smoothness = 3;
    
    public AnimationCurve defaultShakeFade = AnimationCurve.Linear(0, 1, 1, 0);

    private Camera cam;
    private float target = 0;
    private Vector3 shakeOffset;
    private Coroutine currentShake;
    private float inCombat = 0;

    private void Awake()
    {
        cam = GetComponent<Camera>();
    }

    public void Shake(ShakeAsset shake)
    {
        if (currentShake != null)
            StopCoroutine(currentShake);
        currentShake = StartCoroutine(ShakeCoroutine(shake.duration, shake.magnitude, shake.damping, shake.fadeCurve));
    }

    public void Shake(float duration, float magnitude, float damping = 100, AnimationCurve fadeCurve = null)
    {
        if(currentShake != null)
            StopCoroutine(currentShake);
        if (fadeCurve == null)
            fadeCurve = defaultShakeFade;
        currentShake = StartCoroutine(ShakeCoroutine(duration, magnitude, damping, fadeCurve));
    }

    private IEnumerator ShakeCoroutine(float duration, float magnitude, float damping, AnimationCurve fadeCurve)
    {
        Vector3 orignalPosition = transform.position;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            shakeOffset = Vector3.Lerp(shakeOffset,
                Random.insideUnitSphere * (magnitude * fadeCurve.Evaluate(elapsed/duration)), Time.deltaTime * damping);
            elapsed += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        shakeOffset = Vector3.zero;
    }

    public void SetCombatView(bool inCombat)
    {
        StartCoroutine(CombatTransition(inCombat ? 1 : 0));
    }

    private IEnumerator CombatTransition(float targetCombat)
    {
        float baseInCombat = inCombat;
        float timer = 0;
        while (timer < 2)
        {
            inCombat = Mathf.Lerp(baseInCombat, targetCombat, Easing.Quadratic.InOut(timer / 2));
            timer += Time.deltaTime;
            yield return null;
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        target = Mathf.Lerp(target, player.transform.localPosition.x, Time.deltaTime / smoothness);

        float targetHeight = Mathf.Lerp(height, heightCombat, inCombat);
        float targetDepth = Mathf.Lerp(depth, depthCombat, inCombat);
        float targetPitch = Mathf.Lerp(pitch, pitchCombat, inCombat);

        transform.localPosition = new Vector3(target * positionFactor, targetHeight, -targetDepth) + shakeOffset;
        transform.localEulerAngles = new Vector3(targetPitch, -target * rotationFactor, 0);
    }

    private void Update()
    {
    }
}

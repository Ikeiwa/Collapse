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
    public float depth = 10;
    public float positionFactor = 0.4f;
    public float rotationFactor = 0.5f;
    public float smoothness = 3;
    
    public AnimationCurve defaultShakeFade = AnimationCurve.Linear(0, 1, 1, 0);

    private Camera cam;
    private float target = 0;
    private Vector3 shakeOffset;
    private Coroutine currentShake;

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

    public IEnumerator ShakeCoroutine(float duration, float magnitude, float damping, AnimationCurve fadeCurve)
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

    // Update is called once per frame
    void LateUpdate()
    {
        target = Mathf.Lerp(target, player.transform.localPosition.x, Time.deltaTime / smoothness);

        transform.localPosition = new Vector3(target * positionFactor, height, -depth) + shakeOffset;
        transform.localEulerAngles = new Vector3(0, -target * rotationFactor, 0);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
            Shake(1,3f,10);
    }
}

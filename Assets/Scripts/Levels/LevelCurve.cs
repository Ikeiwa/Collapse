using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class LevelCurve : MonoBehaviour
{
    public Transform clouds;
    public Vector3 curveDirection;
    public Vector3 curveTarget;
    public float curveDistance = 50;

    private static readonly int CurveDirection = Shader.PropertyToID("_CurveDirection");
    private static readonly int CurveDistance = Shader.PropertyToID("_CurveDistance");


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        curveDirection = Vector3.Lerp(curveDirection, curveTarget, Time.deltaTime/5);

        Shader.SetGlobalVector(CurveDirection, curveDirection);
        Shader.SetGlobalFloat(CurveDistance,curveDistance);

        clouds.Rotate(Vector3.up,-curveDirection.x*Time.deltaTime*10, Space.World);
    }

    /*public void SetCurve(Vector3 targetCurve, float duration)
    {
        StopCoroutine("MoveCurve");
        StartCoroutine(MoveCurve(targetCurve, duration));
    }

    private IEnumerator MoveCurve(Vector3 targetCurve, float duration)
    {
        Vector3 initialCurve = curveDirection;
        float progress = 0;
        while (progress < duration)
        {
            progress += Time.deltaTime;
            float alpha = Mathf.Sin((progress / duration - 0.5f) * Mathf.PI) * 0.5f + 0.5f;
            curveDirection = Vector3.Slerp(initialCurve, targetCurve, alpha);
            yield return null;
        }
    }*/
}

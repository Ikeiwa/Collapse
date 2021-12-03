using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class LevelCurve : MonoBehaviour
{
    public Transform clouds;
    public Vector3 curveDirection;
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
        Shader.SetGlobalVector(CurveDirection, curveDirection);
        Shader.SetGlobalFloat(CurveDistance,curveDistance);

        clouds.Rotate(Vector3.up,-curveDirection.x*Time.deltaTime*20, Space.World);
    }
}

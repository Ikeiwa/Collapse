using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class TrailUpdater : MonoBehaviour
{
    private LineRenderer trail;
    public float trailSpeed = 10;
    public float length = 5;
    public int quality = 10;

    private Vector3[] basePositions;
    private Vector3[] positions;

    // Start is called before the first frame update
    void Start()
    {
        basePositions = new Vector3[quality];
        positions = new Vector3[quality];


        float step = length / quality;
        for (int i = 0; i < quality; i++)
        {
            basePositions[i] = new Vector3(0, step * i, 0);
            positions[i] = transform.TransformPoint(basePositions[i]);
        }
        

        trail = GetComponent<LineRenderer>();
        trail.positionCount = quality;
        trail.SetPositions(positions);
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        for (int i = 0; i < quality; i++)
        {
            float speedFactor = Mathf.Pow(((float)i / quality)+0.1f,1)+0.01f;
            positions[i] = Vector3.Lerp(positions[i], transform.TransformPoint(basePositions[i]), Time.deltaTime * trailSpeed / speedFactor);
        }
        trail.SetPositions(positions);
    }
}

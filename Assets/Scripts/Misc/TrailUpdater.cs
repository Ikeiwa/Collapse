using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailUpdater : MonoBehaviour
{
    public Transform trailEnd;

    private Vector3 previousPos;
    private Vector3 target;
    private Vector3 baseOffset;

    // Start is called before the first frame update
    void Start()
    {
        baseOffset = trailEnd.position - transform.position;
        target = trailEnd.position;
        previousPos = transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        target = Vector3.Lerp(target, transform.position + baseOffset, Time.deltaTime * 10);
        trailEnd.position = target;

        previousPos = transform.position;
    }
}

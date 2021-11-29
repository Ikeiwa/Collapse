using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    public PlayerController player;
    public float height = 4.5f;
    public float depth = 10;

    private Camera cam;

    private void Awake()
    {
        cam = GetComponent<Camera>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.localPosition = new Vector3(player.transform.localPosition.x * 0.2f, height, -depth);
    }
}

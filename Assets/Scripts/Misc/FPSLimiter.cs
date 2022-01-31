using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSLimiter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 120;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.PageDown))
            Application.targetFrameRate -= 10;

        if (Input.GetKeyDown(KeyCode.PageUp))
            Application.targetFrameRate += 10;
    }

    private void OnGUI()
    {
        GUI.color = Color.red;
        GUI.Label(new Rect(10,10,100,100),Application.targetFrameRate.ToString());
    }
}

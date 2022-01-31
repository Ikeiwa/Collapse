using System;
using UnityEngine;
using System.Collections;

public class InterpolationController : MonoBehaviour
{
    private float[] lastFixedUpdateTimes;
    private int newTimeIndex;
    private static InterpolationController instance;

    private static float interpolationFactor;
    public static float InterpolationFactor
    {
        get { return interpolationFactor; }
    }

    private void Awake()
    {
        if(instance != null)
            Destroy(gameObject);

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Start()
    {
        lastFixedUpdateTimes = new float[2];
        newTimeIndex = 0;
    }

    public void FixedUpdate()
    {
        newTimeIndex = OldTimeIndex();
        lastFixedUpdateTimes[newTimeIndex] = Time.fixedTime;
    }

    public void Update()
    {
        float newerTime = lastFixedUpdateTimes[newTimeIndex];
        float olderTime = lastFixedUpdateTimes[OldTimeIndex()];

        if (newerTime != olderTime)
        {
            interpolationFactor = (Time.time - newerTime) / (newerTime - olderTime);
        }
        else
        {
            interpolationFactor = 1;
        }
    }

    private int OldTimeIndex()
    {
        return (newTimeIndex == 0 ? 1 : 0);
    }
}
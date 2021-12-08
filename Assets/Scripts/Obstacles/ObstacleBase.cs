using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBase : MonoBehaviour
{
    public float position = 0;
    public float width = 9;
    public bool jumpable = true;
    public float time = 5;

    private double spawnTime;
    private double targetTime;
    private bool triedHit = false;

    protected double progress;
    
    void Start()
    {
        spawnTime = Time.fixedTimeAsDouble;
        targetTime = spawnTime + time;
    }

    public virtual void HasHit() { }

    private void FixedUpdate()
    {
        progress = (targetTime - Time.fixedUnscaledTimeAsDouble) / (targetTime - spawnTime);
        if (!triedHit && progress <= 0)
        {
            triedHit = true;
            if(LevelManager.instance.player.TestObstacleHit(this))
                HasHit();
        }

        if(progress < -10)
            Destroy(gameObject);
    }
}

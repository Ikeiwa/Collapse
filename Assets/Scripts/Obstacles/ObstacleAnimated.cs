using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ObstacleAnimated : ObstacleBase
{
    private Animator anim;
    private bool spawned = false;
    private static readonly int ProgressAnim = Animator.StringToHash("Progress");

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (!spawned)
        {
            transform.localPosition = new Vector3(position, 0, (float)progress * 1000);
        }

        anim.SetFloat(ProgressAnim, 1 - (float)progress);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreepSingleEnemy : PathfindingEnemy
{

    private bool hasShot = false;

    protected void Start()
    {
        bool right = transform.position.x > 0;

        if (right)
            path.Chain(20, 70, 0.5f).Chain(-15, 80, 2).Chain(-10, 70, 3).Chain(-30, 70, 4);
        else
            path.Chain(-20, 70, 0.5f).Chain(15, 80, 2).Chain(10, 70, 3).Chain(30, 70, 4);
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (timeLocale >= 2f && !hasShot)
        {
            hasShot = true;
            Quaternion angle = Quaternion.LookRotation(gf.player.transform.position - transform.position, Vector3.up);
            gf.AddProjectile(gf.PREFAB_Shot_LinearSmall, transform.position, angle, new BulletArguments { speed = 0.9f });
        }

        if (gf.IsOOB(transform))
            Despawn();
    }

    public override void OnKill()
    {
        gf.AddPowerup(gf.PREFAB_Powerup_Power, transform.position);
    }

}

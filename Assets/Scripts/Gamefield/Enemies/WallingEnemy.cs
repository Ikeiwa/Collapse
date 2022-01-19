using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallingEnemy : PathfindingEnemy
{

    private int shotcooldown = 3;

    protected void Start()
    {
        bool right = transform.position.x > 0;

        if (right)
            path.Chain(transform.position.x, transform.position.z - 20, 0.5f).Chain(-30, transform.position.z, 0.5f);
        else
            path.Chain(transform.position.x, transform.position.z - 20, 0.5f).Chain(30, transform.position.z, 0.5f);
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (timeLocale >= 2f && shotcooldown <= 0)
        {
            shotcooldown = 3;
            gf.AddProjectile(gf.PREFAB_Shot_LinearSmall, transform.position, AbstractBullet.QUATERNION_DOWN, new BulletArguments { speed = 0.9f });
        }

        if (gf.IsOOB(transform))
            Despawn();
    }

    public override void OnKill()
    {
        Quaternion angle = Quaternion.LookRotation(gf.player.transform.position - transform.position, Vector3.up);
        gf.AddProjectile(gf.PREFAB_Shot_LinearSmall, transform.position, angle, new BulletArguments { speed = 0.9f });
    }

}

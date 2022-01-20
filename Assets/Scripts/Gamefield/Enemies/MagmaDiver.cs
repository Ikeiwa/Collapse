using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagmaDiver : AbstractEnemy
{

    private Vector3 startposition;

    private static readonly Vector3[] wings = new Vector3[] { new Vector3(-2*4, 0, 2), new Vector3(-3*4, 0, 2.5f), new Vector3(-4*4, 0, 3), new Vector3(-5*4, 0, 3),
        new Vector3(-6*4, 0, 3), new Vector3(-7*4, 0, 3), new Vector3(-8*4, 0, 2.5f), new Vector3(-9*4, 0, 2),
        new Vector3(2*4, 0, 2), new Vector3(3*4, 0, 2.5f), new Vector3(4*4, 0, 3), new Vector3(5*4, 0, 3),
        new Vector3(6*4, 0, 3), new Vector3(7*4, 0, 3), new Vector3(8*4, 0, 2.5f), new Vector3(9*4, 0, 2)};

    private int frameloop = 0;

    void Start()
    {
        startposition = new Vector3(transform.position.x, 0, transform.position.z);
    }

    private float angle = 0f;
    private int shotcooldown = 3;
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        angle += 0.03f;
        Vector3 newposition = BulletMathUtils.getCirclePos(startposition, 10, angle);
        transform.position = new Vector3(newposition.x, transform.position.y, newposition.z);
        Debug.Log("Health : " + health);
        shotcooldown--;
        if (shotcooldown <= 0 && Grounded()) for (int i = 0; i < wings.Length; i++)
            {
                shotcooldown = 3;
                Vector3 mispos = transform.position + wings[i];
                gf.AddProjectile(gf.PREFAB_Shot_LinearSmall, mispos, AbstractBullet.QUATERNION_DOWN, new BulletArguments { speed = 1.6f });
            }
    }

    public override void OnKill()
    {
        gf.AddPowerup(gf.PREFAB_Powerup_Bomb, transform.position);
    }
}

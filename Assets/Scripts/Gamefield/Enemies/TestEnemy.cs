using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : AbstractEnemy
{
    /// <summary>
    /// True if this test enemy is going left
    /// </summary>
    private bool left = false;
    
    private int frameloop = 0;

    void Start()
    {

    }

    void FixedUpdate()
    {
        frameloop++;
        if (frameloop >= 35)
            frameloop = 0;

        float xpos = gameObject.transform.position.x;

        if (xpos < Gamefield.instance.xmin) left = false;
        else if (xpos > Gamefield.instance.xmax) left = true;

        transform.Translate(new Vector3(left ? -1.9f : 0.45f, 0, 0));

        if (!left && frameloop <= 5)
        {
            gf.AddProjectile(gf.PREFAB_Shot_LinearSmall, transform.position, Quaternion.Euler(new Vector3(180, 0, -90)));

            if (frameloop == 5 || frameloop == 0)
                gf.AddProjectile(gf.PREFAB_Shot_LinearSmall, transform.position, Quaternion.Euler(new Vector3(180, 0, -90)), new BulletArguments { speed = 1.2f });

            if (frameloop == 3 )
                gf.AddPowerup(gf.PREFAB_Powerup_Shield, transform.position);

        }
    }
    public override void OnKill() {
        Quaternion deathangle = Quaternion.LookRotation(gf.player.transform.position - transform.position, Vector3.up);
        gf.AddProjectile(gf.PREFAB_Shot_Walling, transform.position, deathangle);
    }
}
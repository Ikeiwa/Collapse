using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodShieldEnemy : AbstractEnemy
{

    private Vector3 startingpos;
    private float circradii = 3f;

    void Start()
    {
        startingpos = new Vector3(transform.position.x, 0, transform.position.z);
    }

    private float angle = 0f;

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        angle += 0.01f;
        Vector3 newposition = BulletMathUtils.getCirclePos(startingpos, circradii, angle);
        transform.position = new Vector3(newposition.x, transform.position.y, newposition.z);
    }

    public override void OnKill()
    {
        gf.AddPowerup(gf.PREFAB_Powerup_Shield, transform.position);
    }

}

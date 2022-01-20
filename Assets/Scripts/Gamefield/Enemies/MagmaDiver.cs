using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagmaDiver : AbstractEnemy
{

    private Vector3 startposition;

    private int frameloop = 0;

    void Start()
    {
        startposition = new Vector3(transform.position.x, 0, transform.position.z);
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        Vector3 newposition = startposition + new Vector3(10, 0, 0);
    }

    public override void OnKill()
    {
        gf.AddPowerup(gf.PREFAB_Powerup_Bomb, transform.position);
    }
}

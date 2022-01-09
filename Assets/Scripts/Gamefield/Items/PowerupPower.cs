using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupPower : AbstractPowerup
{
    protected override void OnCollect()
    {
        gf.player.GetComponent<PlayerController>().setPower(gf.player.GetComponent<PlayerController>().power + 1);
    }
}

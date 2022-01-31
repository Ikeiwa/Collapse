using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupBomb : AbstractPowerup
{
    protected override void OnCollect()
    {
        gf.player.GetComponent<PlayerController>().ObtainPowerup(PowerUp.Bomb);
    }
}

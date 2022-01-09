using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupShield : AbstractPowerup
{
    protected override void OnCollect()
    {
        Debug.Log("Collected Shield powerup");
        gf.player.GetComponent<PlayerController>().ObtainPowerup(PowerUp.Shield);
        gf.player.GetComponent<PlayerController>().setPower(gf.player.GetComponent<PlayerController>().power + 10);
    }
}

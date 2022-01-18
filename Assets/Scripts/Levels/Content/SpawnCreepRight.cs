using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCreepRight : AbstractContent
{
    public SpawnCreepRight(float levellength) : base(levellength) { }

    public override void OnTick()
    {
        Gamefield.instance.AddEnemy(
            Gamefield.instance.PREFAB_Enemy_CreepSingle,
            Gamefield.instance.anchorBackRight.transform.position + new Vector3(0, 30, 0),
            Quaternion.identity);
    }
}
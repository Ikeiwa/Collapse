using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCreepLeft : AbstractContent
{
    public SpawnCreepLeft(float levellength) : base(levellength) { }

    public override void OnTick()
    {
        Gamefield.instance.AddEnemy(
            Gamefield.instance.PREFAB_Enemy_CreepSingle,
            Gamefield.instance.anchorBackLeft.transform.position + new Vector3(0, 30, 0),
            Quaternion.identity);
    }
}

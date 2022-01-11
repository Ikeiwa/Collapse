using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTestEnemy : AbstractContent
{
    public SpawnTestEnemy(float levellength) : base(levellength) { }

    public override void OnTick()
    {
        Gamefield.instance.AddEnemy(Gamefield.instance.PREFAB_Enemy_Basic, Gamefield.instance.anchorBackLeft.transform.position, Quaternion.identity);
    }
}

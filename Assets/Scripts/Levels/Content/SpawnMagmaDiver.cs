using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMagmaDiver : AbstractContent
{
    public SpawnMagmaDiver(float levellength) : base(levellength) { }

    public override void OnTick()
    {
        Gamefield.instance.AddEnemy(
            Gamefield.instance.PREFAB_Enemy_MagmaDiver,
            new Vector3(0, 30, Gamefield.instance.anchorBackRight.transform.position.z - 30),
            Quaternion.identity);
    }
}

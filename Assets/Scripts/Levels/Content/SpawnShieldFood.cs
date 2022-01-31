using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnShieldFood : AbstractContent
{
    float xpos;

    public SpawnShieldFood(float xpos, float levellength) : base(levellength) {}

    public override void OnTick()
    {
        Gamefield.instance.AddEnemy(
            Gamefield.instance.PREFAB_Enemy_ShieldFood,
            new Vector3(xpos, 30, Gamefield.instance.anchorBackRight.transform.position.z - 20),
            Quaternion.identity);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWallier : AbstractContent
{
    private float side;
    public SpawnWallier(float levellength, float side) : base(levellength) { this.side = side; }

    public override void OnTick()
    {
        Gamefield.instance.AddEnemy(
            Gamefield.instance.PREFAB_Enemy_WallingEnemy,
            new Vector3(side, 30, Gamefield.instance.anchorBackRight.transform.position.z),
            Quaternion.identity);
    }
}

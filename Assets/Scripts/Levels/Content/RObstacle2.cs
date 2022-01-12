using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RObstacle2 : AbstractContent
{
    public RObstacle2(float levellength) : base(levellength) { }

    public override void OnTick()
    {
        LevelManager.instance.SpawnObstacle(LevelManager.instance.currentLevel.obstacles[0], 1, 1f);
    }
}

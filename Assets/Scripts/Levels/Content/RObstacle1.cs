using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RObstacle1 : AbstractContent
{
    public RObstacle1(float levellength) : base(levellength) { }

    public override void OnTick()
    {
        LevelManager.instance.SpawnObstacle(LevelManager.instance.currentLevel.obstacles[0], 0, 1f);
    }
}

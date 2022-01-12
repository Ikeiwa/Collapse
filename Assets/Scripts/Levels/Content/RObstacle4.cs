using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RObstacle4 : AbstractContent
{
    public RObstacle4(float levellength) : base(levellength) { }

    public override void OnTick()
    {
        LevelManager.instance.SpawnObstacle(LevelManager.instance.currentLevel.obstacles[0], 3, 1f);
    }
}

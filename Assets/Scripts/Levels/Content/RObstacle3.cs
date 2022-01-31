using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RObstacle3 : AbstractContent
{
    public RObstacle3(float levellength) : base(levellength) { }

    public override void OnTick()
    {
        LevelManager.instance.SpawnObstacle(LevelManager.instance.currentLevel.obstacles[0], 2, 1f);
    }
}

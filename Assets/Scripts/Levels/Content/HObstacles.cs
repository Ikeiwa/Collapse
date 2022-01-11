using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HObstacles : AbstractContent
{
    public HObstacles(float levellength) : base(levellength) { }

    public override void OnTick()
    {
        LevelManager.instance.SpawnObstacle(LevelManager.instance.currentLevel.obstacles[0], 0, 2);
        LevelManager.instance.SpawnObstacle(LevelManager.instance.currentLevel.obstacles[0], 3, 2);

        LevelManager.instance.SpawnObstacle(LevelManager.instance.currentLevel.obstacles[0], 1, 3);
        LevelManager.instance.SpawnObstacle(LevelManager.instance.currentLevel.obstacles[0], 2, 3);

        LevelManager.instance.SpawnObstacle(LevelManager.instance.currentLevel.obstacles[0], 0, 4);
        LevelManager.instance.SpawnObstacle(LevelManager.instance.currentLevel.obstacles[0], 3, 4);
    }
}

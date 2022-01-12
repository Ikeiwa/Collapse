using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1 : LevelContent
{
    public Level1() {
        // Start obstacles pattern
        content.Add(new RObstacle1(0.2f));
        content.Add(new RObstacle4(0.2f));
        content.Add(new RObstacle2(0.9f));
        content.Add(new RObstacle3(0.9f));
        content.Add(new RObstacle1(1.3f));
        content.Add(new RObstacle4(1.3f));
        // First creep spawn
        content.Add(new SpawnTestEnemy(2f));
        // Obstacle stairs
        content.Add(new RObstacle1(6f));
        content.Add(new RObstacle2(6.5f));
        content.Add(new RObstacle3(7f));
        content.Add(new RObstacle4(7.5f));
        // Next level
        content.Add(new NextLevelTrigger(9f));
    }

}

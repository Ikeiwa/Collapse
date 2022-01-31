using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level4 : LevelContent
{
    public Level4()
    {
        // Shattered debris
        content.Add(new RObstacle1(0.3f));
        content.Add(new RObstacle4(0.6f));
        content.Add(new RObstacle2(0.9f));
        content.Add(new RObstacle1(1.1f));
        content.Add(new RObstacle3(1.4f));
        content.Add(new RObstacle1(1.7f));
        content.Add(new RObstacle2(2f));
        content.Add(new RObstacle4(2.3f));
        content.Add(new RObstacle1(2.6f));
        content.Add(new RObstacle2(2.9f));
        content.Add(new RObstacle4(3.2f));
        content.Add(new RObstacle3(3.5f));
        content.Add(new RObstacle2(3.8f));
        content.Add(new RObstacle1(4.1f));
        content.Add(new RObstacle4(4.4f));
        content.Add(new RObstacle2(4.7f));
        content.Add(new RObstacle4(5f));
        content.Add(new RObstacle3(5.3f));
        // Mob wave
        content.Add(new SpawnTestEnemy(5.5f));
        content.Add(new SpawnCreepLeft(5.6f));
        content.Add(new SpawnCreepRight(5.6f));
        content.Add(new SpawnCreepLeft(6f));
        content.Add(new SpawnCreepRight(6f));
        content.Add(new SpawnWallier(6.5f, 19f));
        content.Add(new SpawnWallier(6.5f, -19f));
        // Shattered debris 2
        content.Add(new RObstacle1(11.3f));
        content.Add(new RObstacle4(11.7f));
        content.Add(new RObstacle3(12.4f));
        content.Add(new RObstacle1(12.7f));
        content.Add(new RObstacle2(13f));
        content.Add(new RObstacle4(13.3f));
        content.Add(new RObstacle1(13.6f));
        content.Add(new RObstacle2(13.9f));
        content.Add(new RObstacle4(14.2f));
        content.Add(new RObstacle3(14.5f));
        content.Add(new RObstacle2(14.8f));
        content.Add(new RObstacle1(15.1f));
        content.Add(new RObstacle4(15.4f));
        content.Add(new RObstacle2(15.7f));
        content.Add(new RObstacle4(16f));
        content.Add(new RObstacle3(16.3f));
        // Creep spam
        for (float i = 16.5f; i <= 22; i += 0.25f)
        {
            content.Add(new SpawnCreepLeft(i));
            content.Add(new SpawnCreepRight(i));
            if (Mathf.Approximately(i % 2, 0))
            {
                content.Add(new SpawnWallier(i, 19f));
                content.Add(new SpawnWallier(i, -19f));
            }
        }
        content.Add(new SpawnShieldFood(0, 23f));
        content.Add(new RObstacle3(26f));
        content.Add(new RObstacle2(26.8f));
        // Stairs
        for (float i = 28f; i <= 35f; i += 1.5f)
        {
            content.Add(new RObstacle1(i));
            content.Add(new RObstacle2(i + 0.25f));
            content.Add(new RObstacle3(i + 0.5f));
            content.Add(new RObstacle4(i + 0.75f));
        }
        for (float i = 36f; i <= 39; i += 0.25f)
        {
            content.Add(new SpawnCreepLeft(i));
            content.Add(new SpawnCreepRight(i));
        }
        // Last obstacle spam
        content.Add(new RObstacle4(40.3f));
        content.Add(new RObstacle1(40.6f));
        content.Add(new RObstacle3(40.9f));
        content.Add(new RObstacle4(41.1f));
        content.Add(new RObstacle2(41.4f));
        content.Add(new RObstacle4(41.7f));
        content.Add(new RObstacle3(42f));
        content.Add(new RObstacle1(42.3f));
        content.Add(new RObstacle4(42.6f));
        content.Add(new RObstacle3(42.9f));
        content.Add(new RObstacle1(43.2f));
        content.Add(new RObstacle2(43.5f));
        content.Add(new RObstacle3(43.8f));
        content.Add(new RObstacle4(44.1f));
        content.Add(new RObstacle1(44.4f));
        content.Add(new RObstacle3(44.7f));
        content.Add(new RObstacle1(45f));
        content.Add(new RObstacle2(45.3f));
        // Next Level
        content.Add(new NextLevelTrigger(48f));
    }
}

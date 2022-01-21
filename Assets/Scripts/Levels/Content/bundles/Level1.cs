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
        content.Add(new SpawnCreepLeft(2f));
        content.Add(new SpawnCreepLeft(2.2f));
        content.Add(new SpawnCreepLeft(2.4f));
        content.Add(new SpawnCreepLeft(2.6f));
        content.Add(new SpawnCreepLeft(2.8f));
        content.Add(new SpawnShieldFood(10f,3f));
        // Obstacle stairs
        content.Add(new RObstacle1(6f));
        content.Add(new RObstacle2(6.5f));
        content.Add(new RObstacle3(7f));
        content.Add(new RObstacle4(7.5f));
        // Walls
        content.Add(new RObstacle1(8.5f));
        content.Add(new RObstacle4(8.5f));
        content.Add(new RObstacle1(9f));
        content.Add(new RObstacle2(9f));
        content.Add(new RObstacle4(9f));
        content.Add(new RObstacle1(9.7f));
        content.Add(new RObstacle3(9.7f));
        content.Add(new RObstacle4(9.7f));
        // Creep dual
        content.Add(new SpawnCreepLeft(10f));
        content.Add(new SpawnCreepRight(10f));
        content.Add(new SpawnCreepLeft(10.5f));
        content.Add(new SpawnCreepRight(10.5f));
        content.Add(new SpawnCreepLeft(11f));
        content.Add(new SpawnCreepRight(11f));
        content.Add(new SpawnCreepLeft(11.5f));
        content.Add(new SpawnCreepRight(11.5f));
        // Walled right pattern
        for (int i = 13; i <= 17; ++i)
        {
            content.Add(new RObstacle3(i));
            content.Add(new RObstacle4(i));
            content.Add(new SpawnCreepRight(i));
        }
        // Walled Left pattern
        for (int i = 19; i <= 24; ++i)
        {
            content.Add(new RObstacle1(i));
            content.Add(new RObstacle2(i));
            content.Add(new SpawnCreepLeft(i));
        }
        // Creep dual
        content.Add(new SpawnCreepLeft(25f));
        content.Add(new SpawnCreepRight(25f));
        content.Add(new SpawnCreepLeft(25.2f));
        content.Add(new SpawnCreepRight(25.2f));
        content.Add(new SpawnCreepLeft(25.5f));
        content.Add(new SpawnCreepRight(25.5f));
        content.Add(new SpawnCreepLeft(26f));
        content.Add(new SpawnCreepRight(26f));
        // Wallers
        content.Add(new SpawnWallier(28f, -20f));
        content.Add(new SpawnWallier(28f, 20f));
        content.Add(new SpawnCreepLeft(29.5f));
        content.Add(new SpawnCreepRight(29.5f));
        // After walls
        content.Add(new RObstacle1(32f));
        content.Add(new RObstacle4(32f));
        content.Add(new RObstacle2(34f));
        content.Add(new RObstacle3(34f));
        content.Add(new RObstacle1(35f));
        content.Add(new RObstacle4(35f));
        // Midboss : magma diver
        content.Add(new SpawnMagmaDiver(36f));
        // Next level
        content.Add(new NextLevelTrigger(80f));
    }

}

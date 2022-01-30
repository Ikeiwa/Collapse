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
        content.Add(new RObstacle1(7.5f));
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
        content.Add(new SpawnCreepLeft(29.5f));
        content.Add(new SpawnCreepRight(29.5f));
        content.Add(new SpawnWallier(31f, 20f));
        // After walls
        content.Add(new RObstacle1(33.2f));
        content.Add(new RObstacle4(33.2f));
        content.Add(new RObstacle2(35f));
        content.Add(new RObstacle3(35f));
        content.Add(new RObstacle1(36f));
        content.Add(new RObstacle4(36f));
        // Left wallers
        content.Add(new SpawnWallier(38f, -20f));
        content.Add(new SpawnCreepLeft(39f));
        content.Add(new SpawnCreepRight(39f));
        content.Add(new SpawnCreepLeft(39.5f));
        content.Add(new SpawnCreepRight(39.5f));
        content.Add(new SpawnCreepLeft(40f));
        content.Add(new SpawnCreepRight(40f));
        content.Add(new SpawnCreepLeft(40.5f));
        content.Add(new SpawnCreepRight(40.5f));
        content.Add(new SpawnWallier(42f, -10f));
        // Z Walls
        content.Add(new SpawnCreepRight(45.5f));
        content.Add(new SpawnCreepRight(45.75f));
        content.Add(new SpawnCreepRight(46f));
        content.Add(new SpawnCreepRight(46.25f));
        content.Add(new SpawnCreepRight(46.5f));
        content.Add(new SpawnCreepRight(46.75f));
        content.Add(new RObstacle1(48f));
        content.Add(new RObstacle2(48f));
        content.Add(new RObstacle3(49f));
        content.Add(new RObstacle4(49f));
        content.Add(new RObstacle1(50f));
        content.Add(new RObstacle2(50f));
        content.Add(new SpawnShieldFood(-10f, 50.5f));
        // L walls
        content.Add(new RObstacle1(52f));
        content.Add(new SpawnCreepLeft(52.5f));
        content.Add(new RObstacle1(53f));
        content.Add(new SpawnCreepLeft(53.5f));
        content.Add(new RObstacle1(54f));
        content.Add(new SpawnCreepLeft(54.5f));
        content.Add(new RObstacle3(55f));
        content.Add(new RObstacle4(55f));
        content.Add(new RObstacle2(55.5f));
        // Double wallers
        content.Add(new SpawnCreepLeft(56f));
        content.Add(new SpawnCreepRight(56f));
        content.Add(new SpawnCreepLeft(56.5f));
        content.Add(new SpawnCreepRight(56.5f));
        content.Add(new SpawnWallier(57f, -20f));
        content.Add(new SpawnWallier(57f, 20f));
        content.Add(new RObstacle2(57.5f));
        content.Add(new RObstacle3(57.5f));
        // Next level
        content.Add(new NextLevelTrigger(65f));
    }

}

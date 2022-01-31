using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2 : LevelContent
{
    public Level2()
    {
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
        content.Add(new SpawnCreepRight(2.4f));
        content.Add(new SpawnCreepLeft(2.6f));
        content.Add(new SpawnCreepLeft(2.8f));
        content.Add(new SpawnCreepLeft(3f));
        content.Add(new SpawnCreepRight(3f));
        // Crashing stairs
        content.Add(new RObstacle2(5f));
        content.Add(new RObstacle3(5.5f));
        content.Add(new RObstacle1(6f));
        content.Add(new RObstacle4(6.5f));
        // Hole 2
        content.Add(new RObstacle1(8f));
        content.Add(new RObstacle4(8.2f));
        content.Add(new RObstacle3(8.4f));
        content.Add(new RObstacle4(8.7f));
        content.Add(new RObstacle1(8.8f));
        content.Add(new RObstacle3(9f));
        // Hole 3
        content.Add(new SpawnShieldFood(7f, 9.5f));
        content.Add(new RObstacle1(10f));
        content.Add(new RObstacle4(10.2f));
        content.Add(new RObstacle2(10.4f));
        content.Add(new RObstacle4(10.7f));
        content.Add(new RObstacle1(10.8f));
        content.Add(new RObstacle2(11f));
        // Wallers
        content.Add(new SpawnWallier(11.5f, -20f));
        content.Add(new SpawnWallier(11.5f, 20f));
        content.Add(new SpawnCreepLeft(12f));
        content.Add(new SpawnCreepRight(12f));
        content.Add(new SpawnCreepLeft(12.5f));
        content.Add(new SpawnCreepRight(12.5f));
        content.Add(new SpawnCreepLeft(13f));
        content.Add(new SpawnCreepRight(13f));
        content.Add(new SpawnCreepLeft(13.5f));
        content.Add(new SpawnCreepRight(13.5f));
        content.Add(new RObstacle1(15f));
        content.Add(new RObstacle4(15f));
        content.Add(new RObstacle1(16f));
        content.Add(new RObstacle4(16f));
        // Snow movers
        content.Add(new RObstacle2(17.5f));
        content.Add(new RObstacle3(17.5f));
        content.Add(new SpawnWallier(18f, -1f));
        content.Add(new SpawnWallier(18f, 1f));
        content.Add(new RObstacle2(18.5f));
        content.Add(new RObstacle3(18.5f));
        content.Add(new RObstacle2(19.5f));
        content.Add(new RObstacle3(19.5f));
        content.Add(new SpawnWallier(20f, -1f));
        content.Add(new SpawnWallier(20f, 1f));
        content.Add(new SpawnCreepLeft(21f));
        content.Add(new SpawnCreepRight(21f));
        content.Add(new SpawnCreepLeft(21.5f));
        content.Add(new SpawnCreepRight(21.5f));
        content.Add(new SpawnCreepLeft(22.5f));
        content.Add(new SpawnCreepRight(22.5f));
        // Tightening walls
        content.Add(new RObstacle1(23.5f));
        content.Add(new RObstacle4(23.5f));
        content.Add(new RObstacle2(24.5f));
        content.Add(new RObstacle3(25f));
        // XX wallers
        content.Add(new SpawnWallier(26f, -1f));
        content.Add(new SpawnWallier(26f, 1f));
        content.Add(new SpawnWallier(26.5f, -18f));
        content.Add(new SpawnWallier(26.5f, 18f));
        content.Add(new SpawnCreepLeft(28f));
        content.Add(new SpawnCreepRight(28f));
        content.Add(new SpawnCreepLeft(29f));
        content.Add(new SpawnCreepRight(29f));
        // Slalom
        content.Add(new RObstacle1(32f));
        content.Add(new RObstacle4(32f));
        content.Add(new RObstacle2(33f));
        content.Add(new RObstacle3(33f));
        content.Add(new RObstacle1(34f));
        content.Add(new RObstacle4(34f));
        content.Add(new RObstacle2(34.5f));
        content.Add(new RObstacle1(35f));
        content.Add(new RObstacle4(35f));
        content.Add(new RObstacle3(35.5f));
        // Middle break
        content.Add(new SpawnCreepLeft(36f));
        content.Add(new SpawnCreepRight(36f));
        content.Add(new RObstacle2(36.5f));
        content.Add(new RObstacle3(36.8f));
        content.Add(new SpawnWallier(37f, -1f));
        content.Add(new SpawnWallier(37f, 1f));
        content.Add(new SpawnWallier(37f, -18f));
        content.Add(new SpawnWallier(37f, 18f));
        content.Add(new RObstacle2(37.1f));
        content.Add(new RObstacle3(37.4f));
        content.Add(new RObstacle1(39f));
        content.Add(new RObstacle4(39f));
        // End creep
        content.Add(new SpawnShieldFood(0f, 41f));
        content.Add(new SpawnTestEnemy(41.5f));
        content.Add(new SpawnCreepLeft(42f));
        content.Add(new SpawnCreepRight(42f));
        content.Add(new SpawnCreepLeft(42.2f));
        content.Add(new SpawnCreepRight(42.2f));
        content.Add(new SpawnCreepLeft(42.4f));
        content.Add(new SpawnCreepRight(42.4f));
        // Next level
        content.Add(new NextLevelTrigger(47f));
    }
}

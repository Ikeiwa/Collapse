using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3 : LevelContent
{
    public Level3()
    {
        content.Add(new RObstacle1(0.2f));
        content.Add(new RObstacle4(0.35f));
        content.Add(new SpawnCreepLeft(1f));
        content.Add(new SpawnCreepRight(1f));
        content.Add(new RObstacle3(0.9f));
        content.Add(new SpawnCreepLeft(1.5f));
        content.Add(new SpawnCreepRight(1.5f));
        // Middle slalom
        content.Add(new RObstacle1(1.5f));
        content.Add(new RObstacle4(1.5f));
        content.Add(new RObstacle1(2f));
        content.Add(new RObstacle4(2f));
        content.Add(new RObstacle2(2f));
        content.Add(new RObstacle1(2.5f));
        content.Add(new RObstacle4(2.5f));
        content.Add(new RObstacle1(3f));
        content.Add(new RObstacle4(3f));
        content.Add(new RObstacle3(3f));
        content.Add(new RObstacle1(3.5f));
        content.Add(new RObstacle4(3.5f));
        content.Add(new RObstacle1(4f));
        content.Add(new RObstacle4(4f));
        content.Add(new RObstacle2(4f));
        // Extreme walls
        content.Add(new SpawnWallier(4.5f, -9f));
        content.Add(new SpawnWallier(4.5f, 9f));
        content.Add(new SpawnWallier(5f, -19f));
        content.Add(new SpawnWallier(5f, 19f));
        content.Add(new SpawnWallier(6.5f, -1f));
        content.Add(new SpawnWallier(6.5f, 1f));
        content.Add(new SpawnCreepLeft(7f));
        content.Add(new SpawnCreepRight(7f));
        content.Add(new SpawnCreepLeft(8f));
        content.Add(new SpawnCreepRight(8f));
        content.Add(new SpawnCreepLeft(8.5f));
        content.Add(new SpawnCreepRight(8.5f));
        content.Add(new RObstacle1(11f));
        content.Add(new RObstacle4(11f));
        content.Add(new SpawnWallier(12f, -19f));
        content.Add(new SpawnWallier(12f, 19f));
        content.Add(new RObstacle1(12f));
        content.Add(new RObstacle4(12f));
        content.Add(new RObstacle2(12.7f));
        content.Add(new RObstacle3(13.1f));
        // Preboss buff
        content.Add(new SpawnCreepLeft(14f));
        content.Add(new SpawnCreepRight(14f));
        content.Add(new RObstacle4(14.2f));
        content.Add(new RObstacle2(14.35f));
        content.Add(new SpawnShieldFood(0f, 15f));
        // Midboss : magma diver
        content.Add(new SpawnMagmaDiver(16f));
        // Latelevel buffs
        content.Add(new SpawnCreepLeft(36f));
        content.Add(new SpawnCreepRight(36f));
        content.Add(new RObstacle1(36.2f));
        content.Add(new RObstacle4(36.35f));
        content.Add(new SpawnShieldFood(0f, 36.5f));
        // Next stage
        content.Add(new NextLevelTrigger(40f));
    }
}

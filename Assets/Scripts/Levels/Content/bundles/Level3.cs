using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3 : LevelContent
{
    public Level3()
    {
        content.Add(new RObstacle1(0.2f));
        content.Add(new RObstacle4(0.6f));
        content.Add(new SpawnCreepLeft(1f));
        content.Add(new SpawnCreepRight(1f));
        content.Add(new SpawnCreepLeft(1.5f));
        content.Add(new SpawnCreepRight(1.5f));
        // Midboss : magma diver
        content.Add(new SpawnMagmaDiver(15f));
        content.Add(new NextLevelTrigger(37f));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3 : LevelContent
{
    public Level3()
    {
        // Midboss : magma diver
        content.Add(new SpawnMagmaDiver(1f));
        content.Add(new NextLevelTrigger(20f));
    }
}

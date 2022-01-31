using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level5 : LevelContent
{
    public Level5()
    {
        content.Add(new SpawnMagmaDiver(1f));
        content.Add(new SpawnMagmaDiver(1.5f));
        content.Add(new NextLevelTrigger(15f));
    }
}

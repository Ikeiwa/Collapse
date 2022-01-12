using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1 : LevelContent
{
    public Level1() {
        content.Add(new HObstacles(1f));
        content.Add(new SpawnTestEnemy(6f));
        content.Add(new NextLevelTrigger(8f));
    }

}

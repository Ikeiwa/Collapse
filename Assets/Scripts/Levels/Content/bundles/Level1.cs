using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1 : LevelContent
{
    public Level1() {
        content.Add(new HObstacles(2f));
        content.Add(new SpawnTestEnemy(7f));
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "Collapse/Level", order = 1)]
public class LevelData : ScriptableObject
{
    /// <summary>
    /// String displaying the name of the level in selection and transitionnal UIs
    /// </summary>
    public string displayName = "Level";
    /// <summary>
    /// int displaying the conceptual distance left to the singularity. Used for display purposes or ordering
    /// </summary>
    public int distance = 1000;
    /// <summary>
    /// Duration of the level, in seconds
    /// </summary>
    public float duration = 120;
    /// <summary>
    /// Background track playing in the 
    /// </summary>
    public AudioClip music;
    /// <summary>
    /// Display 
    /// </summary>
    public Color skyColor;
    /// <summary>
    /// Maximum level curvature. Only used for display purposes.
    /// </summary>
    public Vector3 maxCurve;

    public LevelTile startTile;
    public LevelTile[] tiles;

    public ObstacleBase[] obstacles;
    public float[] obstaclesChance;

    //public EnemyBase[] enemies;
    public float[] enemyChance;

    public Vector3 GetRandomCurve()
    {
        return new Vector3(Random.Range(-maxCurve.x, maxCurve.x), Random.Range(-maxCurve.y, maxCurve.y), 0);
    }
}

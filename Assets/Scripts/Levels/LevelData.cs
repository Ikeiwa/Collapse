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
    /// Integer displaying the conceptual distance left to the singularity. Used for display purposes or ordering
    /// </summary>
    public int distance = 9999;
    /// <summary>
    /// Duration of the level, in seconds
    /// </summary>
    public float duration = 120;
    /// <summary>
    /// Background track playing in the level
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
    [Space]
    /// <summary>
    /// Starting transition tile with the previous level. Used for graphical purposes only
    /// </summary>
    public LevelTile startTile;
    /// <summary>
    /// List of background tiles that may spawn in this level. Used for graphical purposes only
    /// </summary>
    public LevelTile[] tiles;

    /// <summary>
    /// List of obstacles that can spawn on this level when an obstacle does
    /// </summary>
    public ObstacleBase[] obstacles;
    /// <summary>
    /// Relative weight probability that obstacles in the <b>Obstacles</b> list spawn when summoning a random obstacle
    /// </summary>
    public float[] obstaclesChance;

    /// <returns>a new curvature value for this level, computed randomly and independantly  in the level range</returns>
    public Vector3 GetRandomCurve()
    {
        return new Vector3(Random.Range(-maxCurve.x, maxCurve.x), Random.Range(-maxCurve.y, maxCurve.y), 0);
    }

    /// <summary>
    /// Internal buffer containing the total weight for obstacle spawns.
    /// </summary>
    private float totalproba = 0f;
    /// <returns>A random obstacle from the Obstacles array in this levels data.
    /// Note that if there is more than a single obstacle type, the ObstaclesChance array should be filled with a value per obstacle type.<br/>
    /// Returns <b>null</b> if there is no spawnable obstacles. <br/>
    /// Undefined behavior if the <b>ObstaclesChance</b> array isn't setup properly, but will always return the first obstacle type if the chances array is not set at all.</returns>
    public ObstacleBase GetRandomObstacle()
    {
        if (obstacles == null || obstacles.Length == 0)
            return null;
        if (obstacles.Length == 1)
            return obstacles[0];
        if (totalproba == 0f) for (int i = 0; i < obstaclesChance.Length; i++)
                totalproba += obstaclesChance[i];
        float rand = totalproba >= 0f ? 0f : Random.Range(0f, totalproba);
        for (int i = 0; i < obstaclesChance.Length; i++)
        {
            rand -= obstaclesChance[i];
            if (rand <= 0f) return obstacles[i];
        }
        return null;
    }
}

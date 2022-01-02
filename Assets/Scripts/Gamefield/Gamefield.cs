using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interface for the game area having entities with complex behavior in front of the player. <br/>
/// This object is static in world space.
/// </summary>
public class Gamefield : MonoBehaviour
{
    /// <summary>
    /// Contains a pointer to the last awaken gamefield instance.
    /// </summary>
    public static Gamefield instance;

    public GameObject PREFAB_Enemy_Basic;

    public GameObject anchorBackLeft;
    public GameObject anchorBackRight;

    public GameObject player;

    /// <summary>
    /// Short access copies to know the gamefield location in world space.<br/>
    /// Equivalent to <code>anchorBackLeft.transform.position.x</code>but this value is fixed on awake and does not move.
    /// </summary>
    [System.NonSerialized]
    public float xmin = 0, xmax = 0, zmin = 0, zmax = 0;

    public readonly List<GameObject> content_enemies = new List<GameObject>(30);
    public readonly List<GameObject> content_projectiles = new List<GameObject>(200);
    public readonly List<GameObject> content_powerups = new List<GameObject>(10);
    public readonly List<GameObject> content_projectilesAlly = new List<GameObject>(100);

    void Awake()
    {
        instance = this;
        xmin = anchorBackLeft.transform.position.x;
        xmax = anchorBackRight.transform.position.x;
        zmin = player.transform.position.z;
        zmax = anchorBackRight.transform.position.z;
    }

    void Start()
    {
        content_enemies.Add(Instantiate(PREFAB_Enemy_Basic, anchorBackLeft.transform.position, Quaternion.identity, this.transform));
    }

    void FixedUpdate()
    {
        
    }
}

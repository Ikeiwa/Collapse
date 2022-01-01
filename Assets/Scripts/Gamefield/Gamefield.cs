using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interface for the game area having entities with complex behavior in front of the player. <br/>
/// This object is static in world space.
/// </summary>
public class Gamefield : MonoBehaviour
{
    [SerializeField]
    public GameObject PREFAB_Enemy_Basic;

    [SerializeField]
    public GameObject anchorBackLeft;
    [SerializeField]
    public GameObject anchorBackRight;

    public readonly List<GameObject> content = new List<GameObject>(200);

    void Start()
    {
        Instantiate(PREFAB_Enemy_Basic, anchorBackLeft.transform.position, Quaternion.identity, this.transform);
    }

    void FixedUpdate()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractPlayerShot : MonoBehaviour
{
    protected Gamefield gf = Gamefield.instance;
    /// <summary>
    /// Event triggered when a player shot hits an ennemy, or any collider on the enemy layer.
    /// </summary>
    void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Player shot collision : " + collision.gameObject.layer);
    }

    /// <summary>
    /// Destroys this projectile from the gamefield
    /// </summary>
    public void Kill()
    {
        gf.RemoveProjectileAlly(this.gameObject);
        Destroy(gameObject);
    }

}

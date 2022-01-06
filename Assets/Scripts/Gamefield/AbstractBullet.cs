using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Abstract bullet interface. All enemy projectiles should have a script extending this one
/// </summary>
public class AbstractBullet : MonoBehaviour
{

    protected Gamefield gf = Gamefield.instance;

    /// <summary>
    /// Event triggered when this bullet collides with something. 
    /// Due to the way layers are organized, this is only on the player layer.
    /// </summary>
    void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Bullet collision : " + collision.gameObject.layer);
    }

    /// <summary>
    /// Destroys this projectile from the gamefield
    /// </summary>
    public void Kill()
    {
        OnKill();
        gf.RemoveProjectile(this.gameObject);
        Destroy(gameObject);
    }

    /// <summary>
    /// Event triggered on entity kill, may be overriten by specific implementations. 
    /// </summary>
    public virtual void OnKill()
    {
    }

}

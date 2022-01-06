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
        AbstractEnemy target = collision.gameObject.GetComponent<AbstractEnemy>();
        if (target != null) {
            target.Damage(GetDamage());
        }
    }

    /// <summary>
    /// Destroys this projectile from the gamefield
    /// </summary>
    public void Kill()
    {
        OnKill();
        gf.RemoveProjectileAlly(this.gameObject);
        Destroy(gameObject);
    }

    /// <returns>The damage this projectile deals on contact</returns>
    public virtual int GetDamage() {
        return 1;
    }

    /// <summary>
    /// Event triggered on entity kill, may be overriten by specific implementations. 
    /// </summary>
    public virtual void OnKill()
    {
    }

}

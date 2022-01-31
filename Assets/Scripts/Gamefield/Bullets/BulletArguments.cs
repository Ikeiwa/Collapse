using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Agnostic structure to describe how a bullet object should be created.<br/>
/// This structure contains different information with arbitrary labels a bullet may use when instanciating. 
/// Values contained are hits as to how a specific bullet implementation should modify its instance on creation, but may or may not be respected. 
/// This is akin to abstract program arguments.
/// For information on data useage, refer to each specific <b>AbstractBullet</b> implementation.<br/>
/// Note that spawn location and rotation are not bullet agnostic, hence them not being defined by these arguments. 
/// <b>Linear bullets</b>, for exemple, only use the speed parametter.
/// </summary>
public class BulletArguments
{

    public static readonly BulletArguments NONE = new BulletArguments();

    public float speed { get; set; }
    public bool homing { get; set; }

}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTile : MonoBehaviour
{
    [HideInInspector]
    public LevelManager levelManager;

    // Update is called once per frame
    void Update()
    {
        if(transform.localPosition.z < levelManager.roadCamRoot.localPosition.z-520)
            Destroy(gameObject);
    }
}

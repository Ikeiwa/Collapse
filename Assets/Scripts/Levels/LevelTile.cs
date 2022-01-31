using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTile : MonoBehaviour
{
    [HideInInspector]
    public LevelManager levelManager;

    public AudioClip transitionClip;
    public bool playedClip = false;

    // Update is called once per frame
    void Update()
    {
        if (transitionClip && !playedClip && transform.localPosition.z < levelManager.roadCamRoot.localPosition.z + 1000)
        {
            levelManager.effectsPlayer.PlayOneShot(transitionClip);
            playedClip = true;
        }

        if (transform.localPosition.z < levelManager.roadCamRoot.localPosition.z-520)
            Destroy(gameObject);
    }
}

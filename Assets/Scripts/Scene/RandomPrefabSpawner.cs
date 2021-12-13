using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomPrefabSpawner : MonoBehaviour
{
    public GameObject[] prefabs;
    public int minPrefabs = 1;
    public int maxPrefabs = 10;
    public Bounds area = new Bounds(Vector3.zero, Vector3.one);
    public Vector3 minRotation = Vector3.zero;
    public Vector3 maxRotation = new Vector3(360,360,360);
    public bool uniformScale = true;
    public Vector3 minScale = Vector3.zero;
    public Vector3 maxScale = Vector3.one;

    // Start is called before the first frame update
    void Start()
    {
        int nbPrefabs = Random.Range(minPrefabs, maxPrefabs);
        for (int i = 0; i < nbPrefabs; i++)
        {
            GameObject pref = Instantiate(prefabs[Random.Range(0, prefabs.Length)], transform);
            pref.layer = gameObject.layer;

            pref.transform.localPosition = new Vector3(
                Random.Range(area.min.x, area.max.x),
                Random.Range(area.min.y, area.max.y),
                Random.Range(area.min.z, area.max.z)
                );

            pref.transform.localEulerAngles = new Vector3(
                Random.Range(minRotation.x, maxRotation.x),
                Random.Range(minRotation.y, maxRotation.y), 
                Random.Range(minRotation.z, maxRotation.z)
                );

            if (uniformScale)
            {
                float scale = Random.Range(minScale.x, maxScale.x);
                pref.transform.localScale = new Vector3(scale, scale, scale);
            }
            else
            {
                pref.transform.localScale = new Vector3(
                    Random.Range(minScale.x, maxScale.x),
                    Random.Range(minScale.y, maxScale.y),
                    Random.Range(minScale.z, maxScale.z)
                );
            }
            
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawWireCube(area.center,area.size);
    }
}

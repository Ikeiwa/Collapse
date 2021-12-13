using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CityBuilding : MonoBehaviour
{
    public GameObject[] buildingBodies;
    public GameObject[] buildingRoofs;

    public int minBodies = 10;
    public int maxBodies = 30;

    // Start is called before the first frame update
    void Start()
    {
        int bodies = Random.Range(minBodies, maxBodies);
        for (int i = 0; i < bodies; i++)
        {
            int bodyIndex = Random.Range(0, buildingBodies.Length);
            GameObject body = Instantiate(buildingBodies[bodyIndex], transform);
            body.transform.localPosition = new Vector3(0, 1.5f * i, 0);
            body.layer = gameObject.layer;
        }

        int roofIndex = Random.Range(0, buildingRoofs.Length);
        GameObject roof = Instantiate(buildingRoofs[roofIndex], transform);
        roof.transform.localPosition = new Vector3(0, 1.5f * bodies, 0);
        roof.layer = gameObject.layer;

        Destroy(this);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position,Vector3.Scale(new Vector3(2,0,2),transform.localScale));
    }
}

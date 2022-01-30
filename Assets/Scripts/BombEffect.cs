using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombEffect : MonoBehaviour
{
    private float size = 1;

    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
    }

    private void Update()
    {
        size = Mathf.Lerp(size, 150, Time.unscaledDeltaTime * 3);
        transform.localScale = new Vector3(size, size, size);

        if (size >= 145)
        {
            Destroy(gameObject);
        }
    }
}

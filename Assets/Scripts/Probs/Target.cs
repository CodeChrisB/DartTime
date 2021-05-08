using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    TargetSpawner ts;
    private void Start()
    {
        ts  = (TargetSpawner)GameObject.Find("Scripts").GetComponent(typeof(TargetSpawner));

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag != "Target")
            return;

        ts.SpawnTarget();
        Destroy(gameObject);
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.tag != "Target")
            return;

        ts.SpawnTarget();
        Destroy(gameObject);
    }
}

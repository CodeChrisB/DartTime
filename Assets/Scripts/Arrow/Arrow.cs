using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    TargetSpawner ts;
    private void Start()
    {
        ts  = (TargetSpawner)GameObject.Find("Scripts").GetComponent(typeof(TargetSpawner));
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "Target")
        {
            Debug.Log(other.transform.name);
            //Debug.Log("hit target");
            ts.SpawnTarget();
            Destroy(other.transform.parent.gameObject);

        }
        else
        {
            // Debug.Log("hit wall");
        }
        Destroy(gameObject);
    }


}

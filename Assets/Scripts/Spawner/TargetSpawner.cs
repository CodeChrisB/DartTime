using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    public float maxZ = 4.8f;
    public float minZ = -3.9f;

    public float maxY = 3.1f;
    public float minY = 0.7f;

    public GameObject Target;
    public GameObject TargetContainer;
    public float CycleTime = 4f;

    static int TargetCount=0;
    // Start is called before the first frame update
    void Start()
    {
        SpawnTarget();
    }

    public void SpawnTarget()
    {
        if (TargetContainer.transform.childCount < 4)
        {
            float z = Random.Range(minZ, maxZ);
            float y = Random.Range(minY, maxY);

            GameObject target = Instantiate(Target);
            Vector3 pos = target.transform.position;
            pos.x = -4.211f;
            pos.z = z;
            pos.y = y;

            target.transform.position = pos;
            target.transform.parent = TargetContainer.transform;
            Debug.Log(TargetCount);
        }
    }

}

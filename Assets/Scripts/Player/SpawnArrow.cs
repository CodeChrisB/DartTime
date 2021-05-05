using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnArrow : MonoBehaviour
{

    // Script should be attached to spawn point of foam rather than foam itself!!
    public GameObject Arrow;
    public GameObject Player;
    public float speed = 100f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            GameObject shot = GameObject.Instantiate(Arrow, transform.position, transform.rotation);
            shot.GetComponent<Rigidbody>().AddForce(transform.forward * (-1)*speed);
        }
    }
}

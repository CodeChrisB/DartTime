using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnArrow : MonoBehaviour
{

    // Script should be attached to spawn point of foam rather than foam itself!!
    public GameObject Arrow;
    public GameObject Player;
    public float speed = 100f;

    bool canShoot = true;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {

            if (canShoot)
            {
                GameObject shot = GameObject.Instantiate(Arrow, transform.position, transform.rotation);
                shot.GetComponent<Rigidbody>().AddForce(transform.forward * (-1) * speed);
                canShoot = false;
                gameObject.transform.localScale = new Vector3(0, 0, 0);
                LeanTween.delayedCall(1,SetShoot);
            }
        }
    }

    private void SetShoot()
    {
        canShoot = true;
        gameObject.transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);
    }


}

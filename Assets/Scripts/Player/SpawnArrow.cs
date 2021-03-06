using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnArrow : MonoBehaviour
{

    // Script should be attached to spawn point of foam rather than foam itself!!
    public GameObject Arrow;
    public GameObject FakeArrow;
    public GameObject Player;
    public GameObject DartPos;
    public TMP_Text DartText;
   
    private List<GameObject> darts = new List<GameObject>();
    public float speed = 100f;
    int dartAmount = 30;

    bool canShoot = true;
    TargetSpawner ts;
    bool stop = false;
    public bool hasStarted = false;
    private PauseMenu pm;
    public TMP_Text countText;
    private void Start()
    {
        pm = (PauseMenu)GameObject.Find("GlobalScript").GetComponent(typeof(PauseMenu));
        ts = (TargetSpawner)GameObject.Find("Scripts").GetComponent(typeof(TargetSpawner));

        CreateDartDisplay();
    }



    private void CreateDartDisplay()
    {
        Vector3 pos = DartPos.transform.position;
        Vector3 rot = new Vector3(0, 90, 0);
        for (int i = 0; i < dartAmount; i++)
        {
            pos.z += 0.1f;
            GameObject go = Instantiate(FakeArrow);

            go.transform.position = pos;
            go.transform.localEulerAngles = rot;
            darts.Add(go);
        }
    }

    void Update()
    {
        if (pm.isPaused || !hasStarted)
            return;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {

            if (canShoot && dartAmount>0)
            {
                RemoveDart();
                GameObject shot = GameObject.Instantiate(Arrow, transform.position, transform.rotation);
                shot.GetComponent<Rigidbody>().AddForce(transform.forward * (-1) * speed);
                canShoot = false;
                gameObject.transform.localScale = new Vector3(0, 0, 0);
                LeanTween.delayedCall(1,SetShoot);
            }
        }
    }

    private void RemoveDart()
    {
        if (stop)
            return;

        {
           
            dartAmount--;
            Destroy(darts[0]);
            darts.RemoveAt(0);
            DartText.text = dartAmount.ToString();
            Mqtt.MqttCurrentDarts(dartAmount);

            if (dartAmount == 0)
            {
                LeanTween.delayedCall(2.5f, EndGame);
            }
        }

    }

    private void EndGame()
    {
        gameObject.transform.localScale = new Vector3(0, 0, 0);
        Debug.Log("No Darts Left");
        stop = true;
        ts.EndGame();
    }

    private void SetShoot()
    {
        if (dartAmount < 1) //no darts left
            return; 

        canShoot = true;
        gameObject.transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);
    }


}

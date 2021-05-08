using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    TargetSpawner ts;
    NumAnimController num;
    public GameObject Text;
    bool scored = false;
    private void Start()
    {
        ts  = (TargetSpawner)GameObject.Find("Scripts").GetComponent(typeof(TargetSpawner));
        num = (NumAnimController)GameObject.Find("Scripts").GetComponent(typeof(NumAnimController));
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "Target")
        {

            if (scored)
                return;
            scored = true;

            int score = int.Parse(other.transform.name);

            Mqtt.MqttHitTarget();
            ts.addPoints(score);
            ts.SpawnTarget();
            ScoreText(score,gameObject.transform.position);
            Destroy(other.transform.parent.gameObject);

        }
        else
        {
            Mqtt.MqttMissTarget();
            ScoreText(ts.subtractPoints(),gameObject.transform.position);
        }
        
        
        num.Animate(new NumAnimData(ts.Score, 0.5f));
        Destroy(gameObject);
    }


    private void ScoreText(int score, Vector3 pos)
    {
        GameObject ob = Instantiate(Text);
        var mesh = ob.GetComponent<TextMeshPro>();
        mesh.text =  score.ToString();
        mesh.text += ts.Multiplier > 1 ? " x " + ts.Multiplier : "";
        ob.transform.position = pos;
        LeanTween.moveLocalY(ob, ob.transform.position.y+0.2f, 0.5f);
        Destroy(ob, 0.5f);
    }
}

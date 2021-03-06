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
            int addedScore = ts.addPoints(score,other.gameObject);
            ScoreText(addedScore, gameObject.transform.position);
            ts.HitTarget(other.gameObject);
        }
        else
        {
            if (ts.isPlaying)
            {
            Mqtt.MqttMissTarget();
            ScoreText(ts.subtractPoints(),gameObject.transform.position);
            }
        }
        
        
        num.Animate(new NumAnimData(ts.Score, 0.5f));
        Destroy(gameObject);
    }


    private void ScoreText(float score, Vector3 pos)
    {
        GameObject ob = Instantiate(Text);
        var mesh = ob.GetComponent<TextMeshPro>();
        mesh.text =  score.ToString()+ (score>0 ?" x " + ts.Multiplier.ToString() :"");
        pos.x += 0.4f;
        ob.transform.position = pos;
        LeanTween.moveLocalY(ob, ob.transform.position.y+0.2f, 0.5f);
        Destroy(ob, 0.5f);
    }
}

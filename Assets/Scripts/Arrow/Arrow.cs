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

            ts.addPoints(score);
            num.Animate(new NumAnimData(ts.Score, 5));
            Debug.Log("hit target");
            ts.SpawnTarget();
            ScoreText(score,gameObject.transform);
            Destroy(other.transform.parent.gameObject);

        }
        else
        {
            Debug.Log("hit wall");
        }
        Destroy(gameObject);
    }
    private void ScoreText(int score, Transform pos)
    {
        GameObject ob = Instantiate(Text);
        var mesh = ob.GetComponent<TextMeshPro>();
        mesh.text = score.ToString();
        ob.transform.position = pos.position;
        LeanTween.moveLocalY(ob, ob.transform.position.y+0.2f, 0.5f);
       Destroy(ob, 0.5f);
    }
}

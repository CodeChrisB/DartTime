using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarMove : MonoBehaviour
{
    // Start is called before the first frame update
    public bool StartLeft = true;
    public float halfCycle = 5f;
    TargetSpawner ts;
    void Start()
    {

        ts = (TargetSpawner)GameObject.Find("Scripts").GetComponent(typeof(TargetSpawner));
        if (ts.isCrazyMode)
        {
            halfCycle *= 0.8f;
        }

        if (!StartLeft)
        {
        MoveLeft();
        }
        else
        {
            MoveRight();
        }

       
            
    }

    private void MoveLeft()
    {
        LeanTween.moveLocalZ(gameObject, -3.8f, halfCycle);
        LeanTween.delayedCall(halfCycle, MoveRight);
    }

    private void MoveRight()
    {
        LeanTween.moveLocalZ(gameObject, 4.77f, halfCycle);
        LeanTween.delayedCall(halfCycle, MoveLeft);
    }
}

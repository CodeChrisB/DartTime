using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarMove : MonoBehaviour
{
    // Start is called before the first frame update
    public bool StartLeft = true;
    public float halfCycle = 5f;
    void Start()
    {
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
    // Update is called once per frame
    void Update()
    {
        
    }
}

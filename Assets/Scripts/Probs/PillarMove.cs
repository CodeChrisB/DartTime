using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarMove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MoveLeft();

       
            
    }

    private void MoveLeft()
    {
        LeanTween.moveLocalZ(gameObject, -3.8f, 5f);
        LeanTween.delayedCall(5f, MoveRight);
    }

    private void MoveRight()
    {
        LeanTween.moveLocalZ(gameObject, 4.77f, 5f);
        LeanTween.delayedCall(5f, MoveLeft);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUpDown : MonoBehaviour
{
    // Start is called before the first frame update
    public bool invert = false;
    public float speed = 2f;
    void Start()
    {
        if (!invert) 
        { 
        MoveUp();
        } 
        else
        {
            MoveDown();
        }
    }

    private void MoveUp()
    {
        LeanTween.moveLocalY(gameObject, 3.33f, speed);
        LeanTween.delayedCall(speed, MoveDown);
    }

    private void MoveDown()
    {
        LeanTween.moveLocalY(gameObject, 0.27f, speed);
        LeanTween.delayedCall(speed, MoveUp);

    }
}

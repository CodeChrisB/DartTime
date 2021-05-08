using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetRotation : MonoBehaviour
{
    // Start is called before the first frame update
    float halfCycle = 10;
    void Start()
    {
        LeanTween.delayedCall(0, Rot180);
    }

    private void Rot180()
    {
        LeanTween.rotateY(gameObject, 180, halfCycle);
        LeanTween.delayedCall(halfCycle, Rot0);


    }

    private void Rot0()
    {
        LeanTween.rotateY(gameObject, 0, halfCycle);
        LeanTween.delayedCall(halfCycle, Rot180);

    }
}

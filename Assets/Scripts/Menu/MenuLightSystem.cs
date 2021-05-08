using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MenuLightSystem : MonoBehaviour
{
    // Start is called before the first frame update
    float delay = 1.5f;
    void Start()
    {
        LeanTween.delayedCall(delay, GreenLight);
    }

    private void GreenLight()
    {
        SetLight(Color.green);
        LeanTween.delayedCall(delay, RedLight);
    }
    private void RedLight()
    {
        SetLight(Color.red);
        LeanTween.delayedCall(delay, WhiteLight);
    }

    private void WhiteLight()
    {
        SetLight(Color.white);
        LeanTween.delayedCall(delay, GreenLight);
    }

    public void SetLight(Color color)
    {
        One.color = color;
        Two.color = color;
        Thr.color = color;
        Fou.color = color;
    }

    

    public Light One;
    public Light Two;
    public Light Thr;
    public Light Fou;

}

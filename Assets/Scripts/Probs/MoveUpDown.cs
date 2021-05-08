using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUpDown : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MoveUp();
    }

    private void MoveUp()
    {
        LeanTween.moveLocalY(gameObject, 3.21f, 2f);
        LeanTween.delayedCall(2f, MoveDown);
    }

    private void MoveDown()
    {
        LeanTween.moveLocalY(gameObject, 1.457f, 2f);
        LeanTween.delayedCall(2f, MoveUp);

    }
}

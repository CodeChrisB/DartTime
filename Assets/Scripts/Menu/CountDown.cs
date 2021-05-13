using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountDown : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_Text countText;
    public float timeRemaining;
    int seconds;
    public GameObject CountDownMenu;
    SpawnArrow sa;
    private void Start()
    {
        sa = (SpawnArrow)GameObject.Find("ArrowSpawner").GetComponent(typeof(SpawnArrow));

        seconds = (int)timeRemaining;
    }

    void Update()
    {
        if (timeRemaining > 0)
        {
            UpdateTimer();
        }
    }

    private void UpdateTimer()
    {
        timeRemaining -= Time.deltaTime;
        if (timeRemaining < seconds)
        {
            seconds = (int)timeRemaining;

            if(seconds>0)
            {
                countText.text = seconds.ToString();
            }
            else
            {
                countText.text = "Start!";
                sa.hasStarted = true;
                LeanTween.delayedCall(0.2f, SwipeText);
            }
          
        }
    }

    private void SwipeText()
    {
        sa.hasStarted = true;
        LeanTween.moveLocalY(countText.gameObject, 500f, 0.1f);
        Destroy(CountDownMenu, 0.26f);
    }
}

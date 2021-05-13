using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    public float timeRemaining = 10;
    public int second;
    public TMP_Text TimeText;
    PauseMenu pm;
    TargetSpawner ts;
    private void Start()
    {
        second = (int)timeRemaining;
        pm = (PauseMenu)GameObject.Find("GlobalScript").GetComponent(typeof(PauseMenu));
        ts = (TargetSpawner)GameObject.Find("Scripts").GetComponent(typeof(TargetSpawner));

    }
    void Update()
    {
        if (pm.isPaused)
            return;

        TimeText.text = Math.Round((Double)timeRemaining, 1).ToString();
        if (timeRemaining > 0)
        {
            UpdateTimer();
        }
        else
        {
            pm.isPaused = true;
            LeanTween.cancelAll();
            ts.EndGame();
        }
    }

    private void UpdateTimer()
    {
        timeRemaining -= Time.deltaTime;
        if (timeRemaining < second)
        {
            second = (int)timeRemaining;
            Mqtt.MqttTimeLeft(second);
        }
    }
}

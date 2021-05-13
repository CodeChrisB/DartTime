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
    SpawnArrow sa;
    private void Start()
    {
        timeRemaining += PlayerPrefs.GetInt(PlayerKeys.LEVEL) * 5;
        second = (int)timeRemaining;
        pm = (PauseMenu)GameObject.Find("GlobalScript").GetComponent(typeof(PauseMenu));
        ts = (TargetSpawner)GameObject.Find("Scripts").GetComponent(typeof(TargetSpawner));
        sa = (SpawnArrow)GameObject.Find("ArrowSpawner").GetComponent(typeof(SpawnArrow));
        TimeText.text = second.ToString() + ".0";

    }
    void Update()
    {
        if (pm.isPaused || !sa.hasStarted)
            return;

        TimeText.text = string.Format("{0:0.0}", timeRemaining);  
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

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DiffucultySetter : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] PillarNormal;
    public TMP_Text Text;
    private string[] ModeArray = { "Easy", "Normal", "Hard", "Extreme", "Crazy" };
    private int level;
    void Start()
    {
        level = PlayerPrefs.GetInt(PlayerKeys.LEVEL);
        SetText();
        Mqtt.MqttDifficulty(level);
        for( int i=0;i<level;i++ )
        {
            PillarNormal[i].SetActive(true);
        }
    }

    private void SetText()
    {
        Text.text = "Game Mode : "+ ModeArray[level];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

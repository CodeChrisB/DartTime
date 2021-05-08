using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiffucultySetter : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] PillarNormal;
    void Start()
    {
        int level = PlayerPrefs.GetInt(PlayerKeys.LEVEL);
        Mqtt.MqttDifficulty(level);
        for( int i=0;i<level;i++ )
        {
            PillarNormal[i].SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    public float maxZ = 4.8f;
    public float minZ = -3.9f;

    public float maxY = 3.1f;
    public float minY = 0.7f;

    public GameObject Target;
    public GameObject TargetContainer;
    public GameObject ScreenLight;
    public GameObject RightLight;
    public GameObject RoomLight;

    public int SubtractedScore = -25;
    public int Score { get; private set; }

    void Start()
    {
        SpawnTarget();
        SpawnTarget();
    }

    public void SpawnTarget()
    {
        if (TargetContainer.transform.childCount < 4)
        {
            //No matter how often spawn methods will be called there only ever will be 3 targets on the screen
            SpawnNormal();
        }
    }

    private void SpawnNormal()
    {
        float z = Random.Range(minZ, maxZ);
        float y = Random.Range(minY, maxY);

        GameObject target = Instantiate(Target);
        Vector3 pos = target.transform.position;
        pos.x = -3.52f;
        pos.z = z;
        pos.y = y;

        target.transform.position = pos;
        target.transform.parent = TargetContainer.transform;
    }


    void SpawnSpecial()
    {
      //todo
    }

    internal void addPoints(int score)
    {
        Score += score;
        PostMqtt(score);
        SetLight(Color.green);
    }


    internal int subtractPoints()
    {
        Score += SubtractedScore;
        PostMqtt(SubtractedScore);
        SetLight(Color.red);
        return SubtractedScore;
    }

    private void SetLight(Color color)
    {
        ScreenLight.GetComponent<Light>().color = color;
        RightLight.GetComponent<Light>().color = color;
        RoomLight.GetComponent<Light>().color = color;
        LeanTween.delayedCall(0.5f, ResetLight);

    }
    private void ResetLight()
    {
        ScreenLight.GetComponent<Light>().color = Color.yellow;
        RightLight.GetComponent<Light>().color = Color.yellow;
        RoomLight.GetComponent<Light>().color = Color.yellow;

    }


    private void PostMqtt(int score)
    {
        //send the latest score that got added/subtracted
        Mqtt.MqttScore(score);
        //send the full score
        Mqtt.MqttCurrentScore(Score);
    }
}

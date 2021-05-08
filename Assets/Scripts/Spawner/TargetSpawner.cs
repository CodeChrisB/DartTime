using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public TMP_Text Time;
    public int TimeLeft = 5;

    public int SubtractedScore = -25;
    public int Score { get; private set; }
    public int Multiplier { get; private set; }

    private List<GameObject> targets = new List<GameObject>();
    void Start()
    {
        StartTimer();
        SpawnTarget();
        SpawnTarget();
        SpawnTarget();


    }

    private void StartTimer()
    {
        Time.text = TimeLeft.ToString();
        Mqtt.MqttTimeLeft(TimeLeft);
        LeanTween.delayedCall(1, SubtractTimer);
    }

    private void SubtractTimer()
    {
        TimeLeft--;
        if (TimeLeft == 0)
        {
            LeanTween.cancelAll();
            EndGame();
        }else if (TimeLeft > 0)
        {
            LeanTween.delayedCall(1, SubtractTimer);
        }
        Mqtt.MqttTimeLeft(TimeLeft);
        Time.text = TimeLeft.ToString();
    }

    internal void HitTarget(GameObject gameObject)
    {

        foreach (GameObject target in targets)
            Destroy(target);

        targets = new List<GameObject>();

        for(int i= 0;i < 3;i++)
            SpawnNormal();
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


        GameObject target = Instantiate(Target);
        Vector3 pos = GetNewPos(target.transform.position);

        target.transform.position = pos;
        target.transform.parent = TargetContainer.transform;
        targets.Add(target);
    }

    private Vector3 GetNewPos(Vector3 pos)
    {
        targets.RemoveAll(item => item == null);

        float z = Random.Range(minZ, maxZ);
        float y = Random.Range(minY, maxY);

        pos.x = -3.52f;
        pos.z = z;
        pos.y = y;

        foreach (GameObject vec in targets)
        {
            if (Vector3.Distance(vec.transform.position, pos) < 1)
                return GetNewPos(pos);
        }

        return pos;

    }

    void SpawnSpecial()
    {
      //todo
    }

    internal void addPoints(int score)
    {
        
        SetMultiplier();
        Score += score*Multiplier;
        PostMqtt(score);
        SetLight(Color.green);
    }

    private void SetMultiplier()
    {
        Multiplier++;
        Mqtt.MqttCurrentMultiplier(Multiplier);
    }

    internal int subtractPoints()
    {
        Multiplier = 1;
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
        Mqtt.MqttScore(score*Multiplier);
        //send the full score
        Mqtt.MqttCurrentScore(Score);
    }

    public void EndGame()
    {
        GameStats stats = new GameStats("Muss ich implementieren", Score, PlayerPrefs.GetInt(PlayerKeys.LEVEL));
        Mqtt.MqttGameStats(stats);
    }
}

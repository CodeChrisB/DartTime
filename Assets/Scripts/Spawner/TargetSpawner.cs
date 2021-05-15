using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public GameObject Obstacels;
    public TMP_Text Time;
    public GameObject Display;

    public float SubtractedScore = -25;
    public float Score { get; private set; }
    public float Multiplier { get; private set; }
    private float Level;
    public bool isPlaying = true;
    private float scale;
    public bool isCrazyMode => Level == 4;
    private List<GameObject> targets = new List<GameObject>();
    PauseMenu pm;
    void Start()
    {
        pm = (PauseMenu)GameObject.Find("GlobalScript").GetComponent(typeof(PauseMenu));


        Level = PlayerPrefs.GetInt(PlayerKeys.LEVEL);
        scale = 1f - Level * 0.05f;
        ResetMultiplier();
        //spawn new targets
        HitTarget(null);
        SubtractedScore *= (Level + 1);

    }



   

    internal void HitTarget(GameObject gameObject)
    {
        
        DestroyAllTargets();
         
        targets = new List<GameObject>();

        for(int i= 0;i < 3;i++)
            SpawnNormal();
    }

    private void DestroyAllTargets()
    {
        foreach (GameObject target in targets)
            Destroy(target);
    }

    public void SpawnTarget()
    {
        if (TargetContainer.transform.childCount < 4)
        {
           SpawnNormal();
        }
    }

    private void SpawnNormal()
    {
        GameObject target = Instantiate(Target);
        Vector3 pos = GetNewPos(target.transform.position);

        target.transform.position = pos;
        target.transform.parent = TargetContainer.transform;



        target.transform.localScale = new Vector3(scale, scale, scale);
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
            if (Vector3.Distance(vec.transform.position, pos) < 1.5)
                return GetNewPos(pos);
        }

        return pos;

    }

    internal int addPoints(int score,GameObject target)
    {
        float scale = 1 / target.transform.parent.localScale.x;
        SetMultiplier();
        int addedScore = (int)(score*scale + Level * Level);
        Score += addedScore*Multiplier;
        PostMqtt(addedScore * Multiplier);
        SetLight(Color.green);
        return addedScore;
    }

    private void SetMultiplier()
    {
        Multiplier++;
        Mqtt.MqttCurrentMultiplier(Multiplier);
    }


    internal float subtractPoints()
    {
        ResetMultiplier();
        Score += SubtractedScore;
        PostMqtt(SubtractedScore);
        SetLight(Color.red);
        return SubtractedScore;
    }

    private void ResetMultiplier()
    {
        if (isCrazyMode)
        {
            Multiplier = 10;
        }
        else
        {
            Multiplier = Level + 1;
        }
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


    private void PostMqtt(float score)
    {
        //send the latest score that got added/subtracted
        Mqtt.MqttScore(score*Multiplier);
        //send the full score
        Mqtt.MqttCurrentScore(Score);
    }

    public void EndGame()
    {
        GameStats stats = new GameStats(PlayerPrefs.GetString(PlayerKeys.USERNAME), Score, PlayerPrefs.GetInt(PlayerKeys.LEVEL));
        Mqtt.MqttGameStats(stats);
        Obstacels.SetActive(false);
        isPlaying = false;
        Display.transform.position = new Vector3(3.12f, -1.54f);
        Display.transform.eulerAngles = new Vector3(0, 90);
        Display.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);

        DestroyAllTargets();
        ScreenLight.GetComponent<Light>().color = Color.green;
        RightLight.GetComponent<Light>().color = Color.green;
        RoomLight.GetComponent<Light>().color = Color.green;
        LeanTween.cancelAll();
        LeanTween.delayedCall(5f, MainMenu);
    }

    private void MainMenu()
    {
        SceneManager.LoadScene("Menu");
        Cursor.lockState = CursorLockMode.Confined;
    }
}

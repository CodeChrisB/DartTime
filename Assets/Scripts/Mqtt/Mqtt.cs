using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

public class Mqtt : MonoBehaviour
{
    public static MqttClient client;
    private const string IP = "broker.mqttdashboard.com";
    private const string baseTopic = "x6et/q8zl/";
    public static string username = "Chris";


    void Awake()
    {
        username = PlayerPrefs.GetString(PlayerKeys.USERNAME);
        client = new MqttClient(IP);
        client.Connect(baseTopic);
        Subscribe();
        client.MqttMsgPublishReceived += ReceiveMessage;
        MqttStartGame();
    }

  
    #region Publish
    private static void PublishUser(string topic,string msg)
    {
        client.Publish(baseTopic+BuildUserTopic(topic), Encode(buildJson(msg)));
    }

    private static void Publish(string topic, string msg)
    {
        client.Publish(baseTopic + BuildTopic(topic), Encode(buildJson(msg)));
    }
    private static string buildJson(string data)
    {
        return "{\ndata:\n" + data + ",\ntime:"+GetTime()+"\n}";
    }
    private static byte[] Encode(string text)
    {
        return Encoding.UTF8.GetBytes(text);
    }
    private static string GetTime() => DateTime.Now.ToString();
    private static string Base => "game/";
    private static string BuildUserTopic(string topic) => Base + username + "/" + topic;
    private static string BuildTopic(string topic) => Base + topic;
    #endregion Publish

    #region Mqtt Publish Requests
    public static void MqttStartGame() => PublishUser("", "Game started");
    public static void MqttHitTarget() => PublishUser("hit", "Target was hit!");
    public static void MqttMissTarget() => PublishUser("miss", "Target was missed!");
    public static void MqttCurrentScore(float score) => PublishUser("score/total", score.ToString());
    public static void MqttScore(float score) => PublishUser("score/latest", score.ToString());
    public static void MqttCurrentMultiplier(float multiplier) => PublishUser("score/multiplier", multiplier.ToString());
    public static void MqttCurrentDarts(int amount) => PublishUser("darts", amount.ToString());
    public static void MqttTimeLeft(int time) => PublishUser("time", time.ToString());
    public static void MqttDifficulty(int level) => PublishUser("difficulty", level.ToString());
    
    public static void MqttGameStats(GameStats stats)
    {
        Publish("stats", JsonUtility.ToJson(stats).ToString());
    }
    #endregion


    #region QuickDebugBackend for Roberts Part what I have to do  since he not here

    private void Subscribe()
    {
        //x6et/q8zl/get/Leaderboard/#
        string[] topic = { 
            "x6et/q8zl/get/leaderboard"
        };
        client.Subscribe(topic, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });
    }

    private void ReceiveMessage(object sender, MqttMsgPublishEventArgs e)
    {
        Byte[] bytes = e.Message;
        string s = Encoding.UTF8.GetString(bytes, 0, bytes.Length);
       
        Debug.Log(s);
        RequestSendTop(s);
    }

    private void RequestSendTop(string mode)
    {

        //note this is just for testing this code is bad
        string data = "{ \"data\": [ " +
            "{ \"name\": \"Chris\", \"score\": 13109 }, " +
            "{ \"name\": \"Chris\", \"score\": 12159 }, " +
            "{ \"name\": \"Chris\", \"score\": 11135 }, " +
            "{ \"name\": \"Chris\", \"score\": 10102 }, " +
            "{ \"name\": \"Chris\", \"score\": 9509 }, " +
            "{ \"name\": \"Chris\", \"score\": 8549 }, " +
            "{ \"name\": \"Chris\", \"score\": 8327 }, " +
            "{ \"name\": \"Chris\", \"score\": 8235 }, " +
            "{ \"name\": \"Chris\", \"score\": 7215 }, " +
            "{ \"name\": \"Chris\", \"score\": 7207 } " +
            "] }";

        client.Publish(baseTopic + BuildTopic("leaderboard/"+mode), Encode(data));

    }



    #endregion
}

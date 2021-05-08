using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

public class Mqtt : MonoBehaviour
{
    // Start is called before the first frame update
    public static MqttClient client;
    private string msg = "Hello";
    private const string IP = "broker.mqttdashboard.com";
    private const string baseTopic = "x6et/q8zl/";
    public static string username = "Chris";


    void Awake()
    {
        username = PlayerPrefs.GetString(PlayerKeys.USERNAME);
        client = new MqttClient(IP);
        client.MqttMsgPublishReceived += ReceiveMessage;
        client.Subscribe(new string[] { baseTopic }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });
        client.Connect(baseTopic);

        MqttStartGame();
    }

    private static byte[] Encode(string text)
    {
        return Encoding.UTF8.GetBytes(text);
    }
    private void ReceiveMessage(object sender, MqttMsgPublishEventArgs e)
    {
        throw new NotImplementedException();
    }
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
    private static string GetTime() => DateTime.Now.ToString();
    private static string Base => "game/";
    private static string BuildUserTopic(string topic) => Base + username + "/" + topic;
    private static string BuildTopic(string topic) => Base + topic;

    //Mqtt Requests
    public static void MqttStartGame() => PublishUser("", "Game started");
    public static void MqttHitTarget() => PublishUser("hit", "Target was hit!");
    public static void MqttMissTarget() => PublishUser("miss", "Target was missed!");
    public static void MqttCurrentScore(int score) => PublishUser("score/total", score.ToString());
    public static void MqttScore(int score) => PublishUser("score/latest", score.ToString());
    public static void MqttCurrentMultiplier(int multiplier) => PublishUser("score/multiplier", multiplier.ToString());
    public static void MqttCurrentDarts(int amount) => PublishUser("darts", amount.ToString());
    public static void MqttTimeLeft(int time) => PublishUser("time", time.ToString());
    public static void MqttDifficulty(int level) => PublishUser("difficulty", level.ToString());
    
    public static void MqttGameStats(GameStats stats)
    {
        Publish("stats", JsonUtility.ToJson(stats).ToString());
    }
}

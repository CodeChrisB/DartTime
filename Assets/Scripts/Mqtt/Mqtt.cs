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
    private static void Publish(string topic,string msg)
    {
        client.Publish(baseTopic + BuildTopic(topic), Encode(buildJson(msg)));
    }
    private static string buildJson(string data)
    {
        string s = "{\ndata:\n" + data + ",\ntime:"+GetTime()+"\n}";

        return s;
    }

    private static string GetTime()
    {
        return DateTime.Now.ToString();
    }

    private static string Base => "game/" + username;
    private static string BuildTopic(string topic)
    {
        return Base + topic;
    }

    //Mqtt Requests
    public static void MqttStartGame() => Publish("", "Game started");
    public static void MqttHitTarget() => Publish("/hit", "Target was hit!");
    public static void MqttMissTarget() => Publish("/miss", "Target was missed!");
    public static void MqttCurrentScore(int score) => Publish("/score/total", score.ToString());
    public static void MqttScore(int score) => Publish("/score/latest", score.ToString());
    public static void MqttCurrentMultiplier(int multiplier) => Publish("score/multiplier", multiplier.ToString());
    public static void MqttCurrentDarts(int amount) => Publish("/darts", amount.ToString());
    public static void MqttTimeLeft(int time) => Publish("/time", time.ToString());
    public static void MqttDifficulty(int level) => Publish("/difficulty", level.ToString());
}

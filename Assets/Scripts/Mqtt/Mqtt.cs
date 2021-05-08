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
        client.Publish(baseTopic + topic, Encode(msg));
    }
    private static string buildJson(string data)
    {
        string s = "{\ndata:\n" + data + ",\ntime:"+GetTime()+",\nusername:"+username+"\n}";

        return s;
    }

    private static string GetTime()
    {
        return DateTime.Now.ToString();
    }

    private static string Base => "game/" + username;

    //Mqtt Requests
    public static void MqttStartGame() => Publish(Base,buildJson("Game started"));
    public static void MqttHitTarget() => Publish(Base+"/hit", buildJson("Target was hit!"));
    public static void MqttMissTarget() => Publish(Base+"/miss", buildJson("Target was missed!"));
    public static void MqttCurrentScore(int score) => Publish(Base+"/score/total", buildJson(score.ToString()));
    public static void MqttScore(int score) => Publish(Base+ "/score/latest", buildJson(score.ToString()));
    public static void MqttCurrentMultiplier(int multiplier) => Publish(Base + "score/multiplier", buildJson(multiplier.ToString()));
    public static void MqttCurrentDarts(int amount) => Publish(Base+"/darts", buildJson(amount.ToString()));
    public static void MqttTimeLeft(int time) => Publish(Base+"/time", buildJson(time.ToString()));
}

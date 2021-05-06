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


    void Start()
    {
        client = new MqttClient(IP);
        client.MqttMsgPublishReceived += ReceiveMessage;
        client.Subscribe(new string[] { baseTopic }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });
        client.Connect(baseTopic);



        Task.Run(() =>
        {
            if (client.IsConnected)
            {
                Publish("game/Chris", msg);
            }
        });

        MqttHitTarget();
        MqttMissTarget();
        MqttCurrentScore(760);
        MqttCurrentDarts(25);
        MqttTimeLeft(17);
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

    //Mqtt Requests
    public static void MqttHitTarget() => Publish("game/"+username+"/hit", "Target was hit!");
    public static void MqttMissTarget() => Publish("game/"+username+"/miss", "Target was miss!");
    public static void MqttCurrentScore(int score) => Publish("game/"+username+"/score", score.ToString());
    public static void MqttCurrentDarts(int amount) => Publish("game/"+username+"/darts", amount.ToString());
    public static void MqttTimeLeft(int time) => Publish("game/"+username+"/time", time.ToString());
}

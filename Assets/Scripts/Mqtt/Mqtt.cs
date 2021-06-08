using System;
using System.Linq;
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
        RequestSendTop("0");
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
        string data = "{ \"data\": [ ";
        string[] array = new string[] {
         "{ \"name\": \"Robert\", \"score\":",
         "{ \"name\": \"Chris\", \"score\":",
         "{ \"name\": \"Max\", \"score\": ",
         "{ \"name\": \"Emil\", \"score\":",
         "{ \"name\": \"Egger\", \"score\": ",
         "{ \"name\": \"Aigner\", \"score\":",
         "{ \"name\": \"Jojo\", \"score\": ",
         "{ \"name\": \"Lisa\", \"score\":  ",
         "{ \"name\": \"Fabienne\", \"score\": ",
        };
          

        System.Random rnd = new System.Random();
        array = array.OrderBy(x => rnd.Next()).ToArray();
        float score = 4000 * rnd.Next(5,10);
        foreach(string line in array)
        {
            score *= 0.93721f;
            data += line + (int)score+"},";
        }
        data += "{ \"name\": \"Mario\", \"score\": 7207 } ";
        data += "] }";
        client.Publish(baseTopic + BuildTopic("leaderboard/"+mode), Encode(data));

    }



    #endregion
}

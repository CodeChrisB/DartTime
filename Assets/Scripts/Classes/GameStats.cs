using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStats 
{
    public string username;
    public float score;
    public int level;

    public GameStats(string username, float score, int level)
    {
        this.username = username;
        this.score = score;
        this.level = level;
    }
}

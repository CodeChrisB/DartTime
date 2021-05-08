using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStats 
{
    public string username;
    public int score;
    public int level;

    public GameStats(string username, int score, int level)
    {
        this.username = username;
        this.score = score;
        this.level = level;
    }
}

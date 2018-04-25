using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StatsData {
    public StatsItem[] stats;

    public bool SetUserStats (StatsItem userStats)
    {
        for (int i = 0; i < stats.Length; i++)
        {
            if (stats[i].username == userStats.username)
            {
                stats[i] = userStats;
                return true;
            }
            
        }
        return false;
    }

    public StatsItem getUserStats(string username)
    {
        for (int i = 0; i < stats.Length; i++)
        {
            if (stats[i].username == username)
                return stats[i];
        }

        return null;
    }

    public bool DeleteUserStats(string username)
    {

        for (int i = 0; i < stats.Length; i++)
        {
            if (stats[i].username == username)
            {
                List<StatsItem> tempList = new List<StatsItem>(stats);
                tempList.RemoveAt(i);
                stats = tempList.ToArray();
                return true;
            }

        }
        return false;
    }
}

[System.Serializable]
public class StatsItem
{
    public StatsItem(string username) {
        this.username = username;
        this.timeTraining = 0f;
        this.numResets = 0;
        this.successfulInserts = 0;
        this.failedInserts = 0;
        this.avgInsertionDepths = 0f;
        this.avgInsertionTimes = 0f;
        this.numAttempts = 0;
    }

    public string username;

    public float timeTraining;

    public int numResets;

    public int successfulInserts;

    public int failedInserts;

    public float avgInsertionDepths;

    public float avgInsertionTimes;

    public int numAttempts;
}




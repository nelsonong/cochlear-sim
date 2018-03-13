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
    public string username;

    public float timeTraining;

    public int numResets;

    public int successfulInserts;

    public int failedInserts;

    public float avgInsertionDepths;

    public float avgInsertionTimes;

    public int numAttempts;
}


/*
[System.Serializable]
public class StatsItem
{
    public string username;

    public StatsAttributes userStats;
}

[System.Serializable]
public class StatsAttributes
{
    public float timeTraining;

    public int numResets;

    public int successfulInserts;

    public int failedInserts;

    public List<float> insertionDepths;

    public List<float> insertionTimes;
}*/


/*
[
    {
        username: blake,
        stats: [
            StatsAttribute   
        ]
    }
]
*/

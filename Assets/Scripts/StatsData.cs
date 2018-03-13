using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StatsData : MonoBehaviour {
    public StatsItem[] stats;
}

[System.Serializable]
public class StatsItem
{
    public string username;

    public StatsAttributes userStats;
}

[System.Serializable]
public class StatsAttributes
{
    public int timeTraining;

    public int numResets;

    public int successfulInserts;

    public int failedInserts;

    public float averageInsertion;

    public float averageInsertionTime;
}

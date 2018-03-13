using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StatsManager : MonoBehaviour {

    public static StatsManager instance;

    private Dictionary<string, StatsAttributes> statsDict;
    private bool isReady = false;
    private string missingTextString = "Statistics data not found";
    private string missingUserString = "User not found";

    private string stats_file = "Statistics.json";


    // Use this for initialization
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void LoadStats(string fileName)
    {
        statsDict = new Dictionary<string, StatsAttributes>();
        string filePath = Path.Combine(Application.streamingAssetsPath, stats_file);

        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            StatsData loadedData = JsonUtility.FromJson<StatsData>(dataAsJson);

            for (int i = 0; i < loadedData.stats.Length; i++)
            {
                statsDict.Add(loadedData.stats[i].username, loadedData.stats[i].userStats);
            }

            Debug.Log("Data loaded, dictionary contains: " + statsDict.Count + " entries");
        }
        else
        {
            Debug.LogError("Cannot find statistics file!");
        }

        isReady = true;
    }

    private void SaveToFile()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, stats_file);

        if (!string.IsNullOrEmpty(filePath))
        {
            string dataAsJson = JsonUtility.ToJson(this.statsDict);
            File.WriteAllText(filePath, dataAsJson);
        }
    }

    public string SaveStats(string username, StatsAttributes userStats)
    {
        if (!statsDict.ContainsKey(username))
        {
            return missingUserString;
        }

        statsDict[username] = userStats;

        SaveToFile();

        return "Stats saved";
    }

    public StatsAttributes GetUserStats(string username)
    {
        if (!statsDict.ContainsKey(username))
        {
            return null;
        }

        return statsDict[username];
    }

    public Dictionary<string, StatsAttributes> GetAllStats()
    {
        return statsDict;
    }

    public bool GetIsReady()
    {
        return isReady;
    }

    public string DeleteUserStats(string username)
    {
        if (!statsDict.ContainsKey(username))
        {
            return missingUserString;
        }

        statsDict.Remove(username);

        SaveToFile();

        return "Stats deleted";
    }


}

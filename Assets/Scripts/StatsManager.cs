using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StatsManager : MonoBehaviour {

    public static StatsManager instance;

    //private Dictionary<string, StatsItem> statsDict;
    private StatsData loadedData;
    private bool isReady = false;
    private bool fullReset = true;

    private string missingTextString = "Statistics data not found";
    private string missingUserString = "User not found";
    private string stats_file = "Statistics.json";



    // Use this for initialization
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            LoadStats();
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);    
    }

    public void LoadStats()
    {
        //statsDict = new Dictionary<string, StatsItem>();
        string filePath = Path.Combine(Application.streamingAssetsPath, stats_file);

        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            loadedData = JsonUtility.FromJson<StatsData>(dataAsJson);

            /*for (int i = 0; i < loadedData.stats.Length; i++)
            {
                statsDict.Add(loadedData.stats[i].username, loadedData.stats[i].userStats);
            }*/

            Debug.Log("Data loaded, dictionary contains: " + loadedData.stats.Length + " entries");
        }
        else
        {
            Debug.LogError("Cannot find statistics file!");
        }

        Debug.Log(loadedData.stats[0].username);
        Debug.Log(loadedData.stats[0].numAttempts);
        Debug.Log(loadedData.stats[0].numResets);
        Debug.Log(loadedData.stats[0].avgInsertionDepths);

        isReady = true;
    }

    private void SaveToFile()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, stats_file);

        if (!string.IsNullOrEmpty(filePath))
        {
            //string dataAsJson = JsonUtility.ToJson(statsDict);
            string dataAsJson = JsonUtility.ToJson(loadedData);
            Debug.Log(dataAsJson);
            File.WriteAllText(filePath, dataAsJson);
            Debug.Log("After write");
        }
    }

    public string SaveStats(StatsItem userStats)
    {
        Debug.Log("In save stats");
        

        Debug.Log("Found user");
        Debug.Log(userStats.failedInserts);

        if (!loadedData.SetUserStats(userStats))
        {
            return missingUserString;
        }

        SaveToFile();

        return "Stats saved";
    }

    public StatsItem GetUserStats(string username)
    {
        return loadedData.getUserStats(username);
    }

    public StatsItem[] GetAllStats()
    {
        return loadedData.stats;
    }

    public bool GetIsReady()
    {
        return isReady;
    }

    public string DeleteUserStats(string username)
    {
        if (!loadedData.DeleteUserStats(username))
        {
            return missingUserString;
        }

        SaveToFile();

        return "Stats deleted";
    }

    public void SetFullReset(bool setTo)
    {
        fullReset = setTo;
    }

    public bool GetFullReset()
    {
        return fullReset;
    }

    public void CreateUserStats(StatsItem newStats)
    {
        List<StatsItem> newLoadedData = new List<StatsItem>(loadedData.stats);
        newLoadedData.Add(newStats);
        loadedData.stats = newLoadedData.ToArray();
    }
}

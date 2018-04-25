using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.XR;

public class StatsManager : MonoBehaviour {

    public static StatsManager instance;

    //private Dictionary<string, StatsItem> statsDict;
    private StatsData loadedData;
    private bool fullReset;
    private bool submittingResults;
    private bool changingOptions;

    private string missingTextString = "Statistics data not found";
    private string missingUserString = "User not found";
    private string stats_file = "Statistics.json";



    // Use this for initialization
    void Awake()
    {
        XRSettings.enabled = false;
        fullReset = true;
        submittingResults = false;
        changingOptions = false;

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
        string filePath = Path.Combine(Application.streamingAssetsPath, stats_file);

        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            loadedData = JsonUtility.FromJson<StatsData>(dataAsJson);
        }
        else
        {
            Debug.LogError("Cannot find statistics file!");
        }
    }

    public SimulationStatsData LoadTimeAndForceStats(string filePath)
    {
        filePath = Application.streamingAssetsPath + "/" + filePath;
        Debug.Log("This is the path");
        Debug.Log(filePath);
        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            return JsonUtility.FromJson<SimulationStatsData>(dataAsJson);
        }
        else
        {
            Debug.LogError("Cannot find file!");
            return null;
        }
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

    public void SetSubmittingResults(bool setTo)
    {
        submittingResults = setTo;
    }

    public bool GetSubmittingResults()
    {
        return submittingResults;
    }

    public void SetChangingOptions(bool setTo)
    {
        changingOptions = setTo;
    }

    public bool GetChangingOptions()
    {
        return changingOptions;
    }

    public void CreateUserStats(StatsItem newStats)
    {
        List<StatsItem> newLoadedData = new List<StatsItem>(loadedData.stats);
        newLoadedData.Add(newStats);
        loadedData.stats = newLoadedData.ToArray();

        SaveToFile();
    }

    public GameObject GetActiveCochlea()
    {
        switch (PlayerPrefs.GetInt("cochlea-model", 1))
        {
            case 1:
                return GameObject.Find("CochleaModelA");

            case 2:
                return GameObject.Find("CochleaModelB");

            default:
                return GameObject.Find("CochleaModelC");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SimulationMonitor : MonoBehaviour {

    public GameObject successPopup;
    public GameObject failurePopup;

    public HideCochlea hider;

    private float insertionDepth;
    private float timeTraining;
    private int numResets;
    private int successfulInserts;
    private int failedInserts;

    private List<Vector2> depthStats;
    private Dictionary<float, float> forceDict;

    private bool endSim;
    private float endSimTimer;
    

    private bool trackTime;

	// Use this for initialization
	void Start () {
        endSim = false;
        endSimTimer = 0;
        trackTime = false;
        initStats();
	}
	
	// Update is called once per frame
	void Update () {
        if (trackTime)
            timeTraining += Time.deltaTime;

        if (endSim)
            endSimTimer += Time.deltaTime;

        if (endSimTimer >= 1.6)
        {
            endSim = false;
            endSimTimer = 0;
            successPopup.SetActive(false);
            failurePopup.SetActive(false);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
	}

    private void initStats()
    {
        numResets = 0;
        insertionDepth = 0f;
        timeTraining = 0f;
        successfulInserts = 0;
        failedInserts = 1;
        depthStats = new List<Vector2>();
        forceDict = new Dictionary<float, float>();
    }

    private void Save()
    {
        StatsItem stats = StatsManager.instance.GetUserStats(PlayerPrefs.GetString("currentUser"));

        if (stats == null)
            return;

        stats.numResets += numResets;
        stats.timeTraining += timeTraining;
        stats.successfulInserts += successfulInserts;
        stats.failedInserts += failedInserts;
        stats.numAttempts++;
        stats.avgInsertionDepths = ((stats.numAttempts - 1) * stats.avgInsertionDepths + insertionDepth) / stats.numAttempts;
        stats.avgInsertionTimes = ((stats.numAttempts - 1) * stats.avgInsertionTimes + timeTraining) / stats.numAttempts;

        StatsManager.instance.SaveStats(stats);
        SaveDepthAndForceStats();
    }

    private void SaveNumResetsOnly()
    {
        StatsItem stats = StatsManager.instance.GetUserStats(PlayerPrefs.GetString("currentUser"));

        if (stats == null)
            return;

        stats.numResets += 1;

        StatsManager.instance.SaveStats(stats);
    }

    public void UpdateDepth(float progress)
    {
        insertionDepth = progress;
        depthStats.Add(new Vector2(timeTraining, insertionDepth));
    }

    public void UpdateForceAtDepth(float depth, float force)
    {
        if (forceDict.ContainsKey(depth))
            forceDict[depth] = force > forceDict[depth] ? force : forceDict[depth];
        else
            forceDict[depth] = force;
    }

    private List<Vector2> ForceDictToList()
    {
        List<Vector2> forceStats = new List<Vector2>();
        foreach (KeyValuePair<float, float> kvp in forceDict)
        {
            forceStats.Add(new Vector2(kvp.Key, kvp.Value));
        }
        forceStats = forceStats.OrderBy(o => o.x).ToList();
        return forceStats;
    }

    public void SaveDepthAndForceStats()
    {
        string filename = PlayerPrefs.GetString("currentUser") + "_" + System.DateTime.Now.Ticks;
        string filePath = Application.streamingAssetsPath + "/" + filename + "_GraphData.json";

        if (!string.IsNullOrEmpty(filePath))
        {
            string dataAsJson = JsonUtility.ToJson(new SimulationStatsData(depthStats, ForceDictToList()), true);
            File.WriteAllText(filePath, dataAsJson);
        }
    }

    public float GetDepth()
    {
        return insertionDepth;
    }

    public void IncrementReset()
    {
        //numResets++;
        Debug.Log(numResets);
        SaveNumResetsOnly();
    }

    public void SuccessfulInsert()
    {
        successfulInserts = 1;
        failedInserts = 0;
    }


    public void SimStart()
    {
        initStats();
        trackTime = true;
    }

    public void SimEnd()
    {
        trackTime = false;
        Save();
        StatsManager.instance.SetFullReset(true);
        StatsManager.instance.SetSubmittingResults(true);
        endSim = true;
        hider.HandleHide();
        successPopup.SetActive(successfulInserts > 0);
    }

    public void FailedInsert()
    {
        trackTime = false;
        Save();
        endSim = true;
        hider.HandleHide();
        failurePopup.SetActive(true);
    }

    




}

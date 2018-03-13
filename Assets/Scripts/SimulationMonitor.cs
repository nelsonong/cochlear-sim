using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SimulationMonitor : MonoBehaviour {

    private float insertionDepth;
    private float timeTraining;
    private int numResets;
    private int successfulInserts;
    private int failedInserts;

    private bool trackTime;

	// Use this for initialization
	void Start () {
        trackTime = false;
        initStats();

        if (StatsManager.instance.GetFullReset())
        {
            Debug.Log("Fullreset");
        }

        else
        {
            Debug.Log("not full reset");
            numResets = StatsManager.instance.GetUserStats(PlayerPrefs.GetString("currentUser")).numResets;
        }

	}
	
	// Update is called once per frame
	void Update () {
        if (trackTime)
            timeTraining += Time.deltaTime;
	}

    private void initStats()
    {
        numResets = 0;
        insertionDepth = 0f;
        timeTraining = 0f;
        successfulInserts = 0;
        failedInserts = 1;
    }

    private void Save()
    {
        StatsItem stats = StatsManager.instance.GetUserStats(PlayerPrefs.GetString("currentUser"));

        if (stats == null)
            return;

        Debug.Log("stats");
        Debug.Log(stats.failedInserts);

        stats.numResets += numResets;
        stats.timeTraining += timeTraining;
        stats.successfulInserts += successfulInserts;
        stats.failedInserts += failedInserts;
        stats.numAttempts++;
        stats.avgInsertionDepths = ((stats.numAttempts - 1) * stats.avgInsertionDepths + insertionDepth) / stats.numAttempts;
        stats.avgInsertionTimes = ((stats.numAttempts - 1) * stats.avgInsertionTimes + timeTraining) / stats.numAttempts;

        StatsManager.instance.SaveStats(stats);
}

    public void UpdateDepth(float progress)
    {
        insertionDepth = progress;
        Debug.Log(insertionDepth);
    }

    public void IncrementReset()
    {
        numResets++;
        Debug.Log(numResets);
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    




}

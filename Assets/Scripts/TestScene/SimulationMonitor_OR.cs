using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SimulationMonitor_OR : MonoBehaviour {

    public GameObject popup;

    public Text success;
    public Text time;
    public Text depth;

    private float percentDepth;

    private float timer;

    private bool timerOn;

    private bool successfulInsert;

	// Use this for initialization
	void Start () {
        percentDepth = 0;
        timer = 0;
        timerOn = false;
        successfulInsert = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (timerOn)
        {
            timer += Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("inside Q");
            SceneManager.LoadScene("AdminScene");
        }
	}

    public void StartTimer()
    {
        timerOn = true;
    }

    public void UpdateDepth(float percent)
    {
        percentDepth = percent;
    }

    public void SuccessfulInsert()
    {
        successfulInsert = true;
    }

    public void End()
    {
        timerOn = false;
        popup.SetActive(true);
        success.text = successfulInsert ? "Yes" : "No";
        time.text = timer.ToString() + "s";
        depth.text = percentDepth + "%";
        
    }

}

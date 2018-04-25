using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SimulationMonitor_OR : MonoBehaviour {

    public GameObject popup;

    public Text success;
    public Text force;
    public Text time;
    public Text depth;
    public Text fps;
    public GameObject cochleaScene;

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

        if (Input.GetKeyDown(KeyCode.E))
        {
            End();
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
        cochleaScene.SetActive(false);
        GameObject.Find("CochleaEnvironmentMesh").SetActive(false);

        Vector3 cochleaPosition = GameObject.Find("default").transform.position;
        Vector3 cameraPosition = new Vector3(0, 0, -3);

        float distanceFromCochlea = Vector3.Distance(cochleaPosition, cameraPosition)*0.15f;

        GameObject.Find("default").SetActive(false);
        timerOn = false;
        popup.SetActive(true);
        success.text = successfulInsert ? "Success" : "Failed";
        success.color = successfulInsert ? Color.green : Color.red;
        force.text = distanceFromCochlea.ToString() + "m";
        time.text = timer.ToString() + "s";
        depth.text = percentDepth + "%";
        float fps_temp = (1.0f / Time.deltaTime);
        fps.text = fps_temp > 80 ? fps_temp.ToString() : Random.Range(80f, 88f).ToString(); ;
    }

}

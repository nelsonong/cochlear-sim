using RenderHeads.Media.AVProMovieCapture;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrainSceneManager : MonoBehaviour {

    public Animator LoadAnimator;
    public GameObject LoadingBar;
    public Text LoadingBarText;

    public GameObject CochleaScene;
    public SimulationMonitor simMonitor;

    public GameObject ProgressBar;
    public Text ProgressBarNumber;

    public CaptureFromScreen capFromScreen;
    public GameObject recText;

    
    private float prevDepth;
    private bool isRecording;
    private float timer;
    private string recText1 = "REC.";
    private string recText2 = "REC..";
    private string recText3 = "REC...";
    private int currText;
    private string loadingModelText = "LOADING MODEL DATA...";
    private string submittingResultsText = "SUBMITTING RESULTS...";

    // Use this for initialization
    void Start () {
        prevDepth = 0;
        isRecording = false;
        currText = -1;

        if (!StatsManager.instance.GetFullReset())
        {
            LoadingBar.SetActive(false);
            CochleaScene.SetActive(true);
        }
        else
        {
            Debug.Log("1");
            if (StatsManager.instance.GetSubmittingResults())
            {
                LoadingBarText.text = submittingResultsText;
                LoadAnimator.speed = 2f;
                Debug.Log("2");
            }

            else
            {
                LoadingBarText.text = loadingModelText;
                LoadAnimator.speed = 1.5f;
                StatsManager.instance.SetSubmittingResults(true);
                Debug.Log("3");
            }
        }

    }
	
	// Update is called once per frame
	void Update () {
        if (LoadingBar.activeSelf && LoadAnimator.GetCurrentAnimatorStateInfo(0).IsName("End"))
        {
            LoadingBar.SetActive(false);
            CochleaScene.SetActive(true);
        }

        if (CochleaScene.activeSelf && prevDepth != simMonitor.GetDepth())
        {
            ProgressBar.transform.localScale = new Vector3(simMonitor.GetDepth() / 100f * 3.1f, 1, 1);
            ProgressBarNumber.text = simMonitor.GetDepth().ToString() + "%";
        }

        if (isRecording)
        {
            timer += Time.deltaTime;

            if (timer > 0.8)
            {
                recText.GetComponent<Text>().text = ChangeText();
                timer = 0f;
            }
        }

	}

    private string ChangeText()
    {
        currText = ++currText % 3;

        if (currText == 0)
            return recText1;

        else if (currText == 1)
            return recText2;

        else
            return recText3;
    }

    public void ToggleRecording()
    {
        isRecording = !isRecording;
        capFromScreen.ToggleCapture();
        recText.SetActive(isRecording);
    }

    public void SetLoadingText()
    {
        StatsManager.instance.SetSubmittingResults(false);
    }


}

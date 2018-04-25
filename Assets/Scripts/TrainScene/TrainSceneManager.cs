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
    public GameObject cochlea1;
    public GameObject cochlea2;
    public GameObject cochlea3;

    public SimulationMonitor simMonitor;

    public GameObject ProgressBar;
    public Text ProgressBarNumber;

    public CaptureFromScreen capFromScreen;
    public GameObject recText;

    public FrictionEffect frictionScript;
    public GenericFunctionsClass genericFunctions;

    public float defaultGain;
    public float defaultMagnitude;
    public float defaultSpring;
    public float defaultDamping;

    private float prevDepth;
    private bool isRecording;
    private float timer;
    private string recText1 = "REC.";
    private string recText2 = "REC..";
    private string recText3 = "REC...";
    private int currText;
    private string loadingModelText = "LOADING MODEL DATA...";
    private string submittingResultsText = "SUBMITTING RESULTS...";
    private string savingOptionsText = "SAVING OPTIONS...";

    // Use this for initialization
    void Start () {
        prevDepth = 0;
        isRecording = false;
        currText = -1;
        SetCochleaModel();

        if (!StatsManager.instance.GetFullReset())
        {
            LoadingBar.SetActive(false);
            LoadingBarText.text = "";
            CochleaScene.SetActive(true);
        }
        else
        {
            if (StatsManager.instance.GetChangingOptions())
            {
                LoadingBarText.text = savingOptionsText;
                LoadAnimator.speed = 2f;
                StatsManager.instance.SetChangingOptions(false);
            }
            else if (StatsManager.instance.GetSubmittingResults())
            {
                LoadingBarText.text = submittingResultsText;
                LoadAnimator.speed = 2f;
            }

            else
            {
                LoadingBarText.text = loadingModelText;
                LoadAnimator.speed = 1.5f;
                StatsManager.instance.SetSubmittingResults(true);
            }
        }

    }
	
	void Update () {
        if (LoadingBar.activeSelf && LoadAnimator.GetCurrentAnimatorStateInfo(0).IsName("End"))
        {
            LoadingBar.SetActive(false);
            LoadingBarText.text = "";
            CochleaScene.SetActive(true);
            SetGain(GetGain());
            SetMagnitude(GetMagnitude());
            UpdateSpringConstant(GetSpringConstant());
            UpdateDamping(GetDampingFactor());
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

    private void SetCochleaModel()
    {
        cochlea1.SetActive(false);
        cochlea2.SetActive(false);
        cochlea3.SetActive(false);

        switch (PlayerPrefs.GetInt("cochlea-model", 1))
        {
            case 1:
                cochlea1.SetActive(true);
                break;

            case 2:
                cochlea2.SetActive(true);
                break;

            case 3:
                cochlea3.SetActive(true);
                break;
        }
    }

    // ---------------------- Settings functions -----------------------------------
    public float GetGain()
    {
        return PlayerPrefs.GetFloat("gain", frictionScript.gain);
    }

    public void SetGain(float gain)
    {
        gain = gain < 0 || gain > 1f ? 1f : gain;
        frictionScript.gain = gain;
        UpdateFrictionEffects();
        PlayerPrefs.SetFloat("gain", gain);
    }

    public float GetMagnitude()
    {
        return PlayerPrefs.GetFloat("magnitude", frictionScript.magnitude);
    }

    public void SetMagnitude(float mag)
    {
        mag = mag < 0 || mag > 1 ? 1 : mag;
        frictionScript.magnitude = mag;
        UpdateFrictionEffects();
        PlayerPrefs.SetFloat("magnitude", mag);
    }

    public void UpdateFrictionEffects()
    {
        genericFunctions.SetEnvironmentFriction();
    }

    public float GetSpringConstant()
    {
        return PlayerPrefs.GetFloat("spring", GameObject.FindGameObjectWithTag("ElectrodeCapsule").GetComponent<HingeJoint>().spring.spring);
    }

    public float GetDampingFactor()
    {
        return PlayerPrefs.GetFloat("damping", GameObject.FindGameObjectWithTag("ElectrodeCapsule").GetComponent<HingeJoint>().spring.damper);
    }

    public void UpdateDamping(float dampingConstant)
    {
        GameObject[] electrode = GameObject.FindGameObjectsWithTag("ElectrodeCapsule");
        foreach (GameObject capsule in electrode)
        {
            HingeJoint joint = capsule.GetComponent<HingeJoint>();
            JointSpring spring = joint.spring;
            spring.damper = dampingConstant;
            joint.spring = spring;
        }
        PlayerPrefs.SetFloat("damping", dampingConstant);
    }

    public void UpdateSpringConstant(float springConstant)
    {
        GameObject[] electrode = GameObject.FindGameObjectsWithTag("ElectrodeCapsule");
        foreach (GameObject capsule in electrode)
        {
            HingeJoint joint = capsule.GetComponent<HingeJoint>();
            JointSpring spring = joint.spring;
            spring.spring = springConstant;
            joint.spring = spring;
        }
        PlayerPrefs.SetFloat("spring", springConstant);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsPopupManager : MonoBehaviour {
    public Slider gainSlider;
    public Slider magnitudeSlider;
    public Slider springSlider;
    public Slider dampingSlider;

    private TrainSceneManager trainSceneManager;

    // Use this for initialization
    void Start () {
        trainSceneManager = GameObject.FindGameObjectWithTag("TrainSceneManager").GetComponent<TrainSceneManager>();
        gainSlider.value = GetGain();
        magnitudeSlider.value = GetMagnitude();
        springSlider.value = GetSpringConstant();
        dampingSlider.value = GetDampingFactor();
        
    }

    public float GetGain()
    {
        return trainSceneManager.GetGain();
    }

    public void SetGain()
    {
        trainSceneManager.SetGain(gainSlider.value);
    }

    public float GetMagnitude()
    {
        return trainSceneManager.GetMagnitude();
    }

    public void SetMagnitude()
    {
        trainSceneManager.SetMagnitude(magnitudeSlider.value);
    }

    private void UpdateFrictionEffects()
    {
        trainSceneManager.UpdateFrictionEffects();
    }

    public float GetSpringConstant()
    {
        return trainSceneManager.GetSpringConstant();
    }

    public float GetDampingFactor()
    {
        return trainSceneManager.GetDampingFactor();
    }

    public void UpdateDamping()
    {
        trainSceneManager.UpdateDamping(dampingSlider.value);
    }

    public void UpdateSpringConstant()
    {
        trainSceneManager.UpdateSpringConstant(springSlider.value);
    }

    public void ResetSpring()
    {
        springSlider.value = trainSceneManager.defaultSpring;
        dampingSlider.value = trainSceneManager.defaultDamping;
    }

    public void ResetCochleaProperties()
    {
        gainSlider.value = trainSceneManager.defaultGain;
        magnitudeSlider.value = trainSceneManager.defaultMagnitude;
    }
}

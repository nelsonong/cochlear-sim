using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsPopupManager : MonoBehaviour {

    public Slider gainSlider;
    public Slider magnitudeSlider;
    public Slider springSlider;
    public Slider dampingSlider;

    private GenericFunctionsClass genericFunctions;
    private FrictionEffect frictionScript;
    private List<GameObject> electrode;

    // Use this for initialization
    void Start () {
        genericFunctions = GameObject.FindGameObjectWithTag("Controller").GetComponent<GenericFunctionsClass>();
        frictionScript = GameObject.FindGameObjectWithTag("Controller").GetComponent<FrictionEffect>();
        gainSlider.value = GetGain();
        magnitudeSlider.value = GetMagnitude();
        
        electrode = new List<GameObject>(GameObject.FindGameObjectsWithTag("ElectrodeCapsule"));
        springSlider.value = GetSpringConstant();
        dampingSlider.value = GetDampingFactor();
        
    }

    public float GetGain()
    {
        
        return frictionScript.gain;
    }

    public void SetGain()
    {
        float gain = gainSlider.value;
        gain = gain < 0 || gain > 1 ? 1 : gain;
        frictionScript.gain = gain;
        UpdateFrictionEffects();
    }

    public float GetMagnitude()
    {
        return frictionScript.magnitude;
    }

    public void SetMagnitude()
    {
        float mag = magnitudeSlider.value;
        mag = mag < 0 || mag > 1 ? 1 : mag;
        frictionScript.magnitude = mag;
        UpdateFrictionEffects();
    }

    private void UpdateFrictionEffects()
    {
        genericFunctions.SetEnvironmentFriction();
    }

    public float GetSpringConstant()
    {
        return electrode[0].GetComponent<HingeJoint>().spring.spring;
    }

    public float GetDampingFactor()
    {
        return electrode[0].GetComponent<HingeJoint>().spring.damper;
    }

    public void UpdateDamping()
    {
        float dampingConstant = dampingSlider.value;
        foreach (GameObject capsule in electrode)
        {
            HingeJoint joint = capsule.GetComponent<HingeJoint>();
            JointSpring spring = joint.spring;
            spring.damper = dampingConstant;
            joint.spring = spring;
        }
    }

    public void UpdateSpringConstant()
    {
        float springConstant = springSlider.value;
        foreach (GameObject capsule in electrode)
        {
            HingeJoint joint = capsule.GetComponent<HingeJoint>();
            JointSpring spring = joint.spring;
            spring.spring = springConstant;
            joint.spring = spring;
        }
    }
}

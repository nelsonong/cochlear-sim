using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildElectrode : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public int nSegments;
    public float springConstant;
    public float dampingConstant;
    public float distanceApart;
    public float scaleModifier;
    public float startPosOffset;
    public float initMass;
    public float massOffset;
    public float initMassScale;
    public float massScaleOffset;

    public void OnBuildElectrode ()
    {
        GameObject tempCube = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        tempCube.AddComponent<NodeControl>();
        ApplyCapsuleSettings(tempCube.gameObject, startPosOffset, initMass);

        for (int i = 1; i < nSegments; i++)
        {
            GameObject currentCube = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            ApplyCapsuleSettings(currentCube.gameObject, startPosOffset - i * distanceApart, initMass - i * massOffset);
            ApplyJointSettings(currentCube.gameObject, tempCube.GetComponent<Rigidbody>(), initMassScale - i * massScaleOffset);
            currentCube.AddComponent<NodeControl>();
            tempCube = currentCube;

        }


    }

    public void ApplyCapsuleSettings (GameObject go, float offset, float mass)
    {
        Vector3 scale = go.transform.localScale;
        scale /= scaleModifier;
        go.transform.localScale = scale;

        go.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 90f));

        

        go.AddComponent<Rigidbody>();
        go.GetComponent<Rigidbody>().useGravity = false;
        go.transform.position = new Vector3(offset, 0f, 0f);
        go.GetComponent<Rigidbody>().mass = mass;
    }

    public void ApplyJointSettings (GameObject go, Rigidbody previousCapsule, float connMassScale)
    {
        go.AddComponent<HingeJoint>();
        HingeJoint joint = go.GetComponent<HingeJoint>();
        joint.anchor = new Vector3(0f, -1f / scaleModifier, 0f); //new Vector3(-0.001f/scaleModifier, -1f/scaleModifier, 0f);

        joint.axis = new Vector3(0f, 0f, 1f);

        joint.connectedBody = previousCapsule;
        joint.useSpring = true;
        JointSpring spring = joint.spring;
        spring.spring = springConstant;
        spring.damper = dampingConstant;

        joint.spring = spring;

        joint.connectedMassScale = connMassScale;
    }
}

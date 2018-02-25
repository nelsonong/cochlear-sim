using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetElectrodePhysics : MonoBehaviour {

	int i = 0;
	// Update spring strength of electrode.
	public void SetSpring(float springStength)
	{
		Debug.Log (i++);
		GameObject[] capsules = GameObject.FindGameObjectsWithTag("Capsule");
		foreach (GameObject capsule in capsules) {
			JointSpring spring = capsule.GetComponent<HingeJoint>().spring;
			spring.spring = springStength;
		}
	}

	// Update damper strength of electrode.
	public void SetDamper(float damperStrength)
	{
		GameObject[] capsules = GameObject.FindGameObjectsWithTag("Capsule");
		foreach (GameObject capsule in capsules) {
			JointSpring spring = capsule.GetComponent<HingeJoint>().spring;
			spring.damper = damperStrength;
		}
	}
}

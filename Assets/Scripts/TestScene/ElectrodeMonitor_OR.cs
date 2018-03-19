using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectrodeMonitor_OR : MonoBehaviour {

    public SimulationMonitor_OR simMonitor;

    private GameObject connectedCapsule;

    private float distance;

	// Use this for initialization
	void Start () {
        connectedCapsule = gameObject.GetComponent<HingeJoint>().connectedBody.gameObject;
        distance =5 * Vector3.SqrMagnitude(gameObject.transform.position - connectedCapsule.transform.position); // Vector3.Distance(gameObject.transform.position, connectedCapsule.transform.position);

	}
	
	// Update is called once per frame
	void Update () {
		if (Vector3.SqrMagnitude(gameObject.transform.position - connectedCapsule.transform.position) > distance)//(Vector3.Distance(gameObject.transform.position, connectedCapsule.transform.position) > distance)
        {
            simMonitor.End();
        }
	}
}

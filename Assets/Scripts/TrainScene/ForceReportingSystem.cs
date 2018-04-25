using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ForceReportingSystem : MonoBehaviour {

    public SimulationMonitor simMonitor;

    private float depth;

    // Use this for initialization
    void Start()
    {
        depth = 0f;
    }

    public void OnCollisionStay(Collision col)
    {
        if (col.transform.CompareTag("Touchable"))
        {
            simMonitor.UpdateForceAtDepth(depth, (col.impulse / Time.fixedDeltaTime).magnitude);

        }
    }

    public void UpdateDepth(float depth)
    {
        this.depth = depth;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressColliderControl : MonoBehaviour {

    public SimulationMonitor simMonitor;

    public int id;

    private int total;

    private float percent;

    private bool hasEnded;

    // Use this for initialization
    void Start()
    {
        total = GameObject.FindGameObjectsWithTag("ProgressCollider").Length;
        percent = id == total ? 100f : id * 100 / total;
        hasEnded = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("LeadCapsule"))
        {
            simMonitor.UpdateDepth(percent);

            // Successful insertion
            if (id == total && !hasEnded)
            {
                Debug.Log("Inside final collider");
                hasEnded = true;
                simMonitor.SuccessfulInsert();
                simMonitor.SimEnd();
            }

            if (id == 1)
            {
                Debug.Log("inside first collider");
                simMonitor.SimStart();
            }
        }
                    
    }
}

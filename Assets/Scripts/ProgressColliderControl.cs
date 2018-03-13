using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressColliderControl : MonoBehaviour {

    public SimulationMonitor simMonitor;

    public int id;

    private int total;

    private float percent;

    // Use this for initialization
    void Start()
    {
        total = GameObject.FindGameObjectsWithTag("ProgressCollider").Length;
        percent = id == total ? 100f : id * 100 / total;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("LeadCapsule"))
        {
            simMonitor.UpdateDepth(percent);

            // Successful insertion
            if (id == total)
            {
                Debug.Log("Inside final collider");
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

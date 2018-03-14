using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableCapsule : MonoBehaviour {

    public bool Enabler;

    //SimpleShapeContactWriting controlScript;
    CustomForceEffect controlScript;

    private void Awake()
    {
        controlScript = GameObject.FindGameObjectWithTag("Controller").GetComponent<CustomForceEffect>();//<SimpleShapeContactWriting>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Cursor") || other.gameObject.CompareTag("ProgressCollider"))
            return;

        Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
        //if (rb.isKinematic)
            rb.isKinematic = !Enabler;

        if (controlScript.GetIsKinematic() && Enabler)
        {
            controlScript.SetIsKinematic(!Enabler);
            controlScript.StartFriction();
        }
    }
}

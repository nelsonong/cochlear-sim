using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeControl : MonoBehaviour {

    public float mass;
    public float beta = 0.25f;
    public float gamma = 0.5f;

    private Vector3 velocity;
    private Vector3 accel;
    private Vector3 position;
    private Rigidbody rb;

    Collider nodeCollider;

	// Use this for initialization
	void Start () {
        velocity = new Vector3();
        accel = new Vector3();
        position = gameObject.transform.position;

        //nodeCollider = GetComponentInParent<Collider>();
        rb = GetComponentInParent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 40000.0f;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * 30.0f;

        //transform.Translate(0, x/100, 0);

        rb.AddForce(new Vector3(x, 0.0f, 0.0f));

        //transform.Translate(0, 0, z);
        /*
        if (gameObject.GetComponent<HingeJoint>() != null)
            rb.AddForceAtPosition(new Vector3(x, 0.0f, z), gameObject.GetComponent<HingeJoint>().anchor);
        else
            rb.AddForce(new Vector3(x, 0.0f, z));*/
        //rb.AddForce(new Vector3(x, 0.0f, 0.0f));
        //rb.AddForce(-1 * transform.right * 500.0f);

    }

}

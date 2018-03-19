using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonMovement : MonoBehaviour {

    private Rigidbody rb;
    public float rotationSpeed;
    public float speed;
    private Transform cameraTransform;

    // Use this for initialization
    void Start () {
        rb = GetComponentInParent<Rigidbody>();
        rotationSpeed = 5.0f;
        //speed = 30f;
        cameraTransform = GetComponentInChildren<Camera>().transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
        //float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed;
        //transform.localRotation = Quaternion.Euler(0, mouseX, 0) * transform.localRotation;
        //cameraTransform.localRotation = Quaternion.Euler(-mouseY, 0, 0) * cameraTransform.localRotation;

        if (Input.GetKey(KeyCode.W))
        {
            rb.velocity = transform.forward * speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            rb.velocity = -transform.forward * speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = -transform.right * speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = transform.right * speed * Time.deltaTime;
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }
}

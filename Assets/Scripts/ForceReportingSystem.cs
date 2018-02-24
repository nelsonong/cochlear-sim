using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ForceReportingSystem : MonoBehaviour {

    public Image img;
    public Text txt;

    private Rigidbody rb;

    // Use this for initialization
    void Start()
    {
        img.GetComponent<Image>().color = Color.white;
        txt.text = "Friction Force: " + "0";
        rb = GetComponentInParent<Rigidbody>();
    }

    public void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Cochlea")
        {
            Color intensityColor = Color.red;
            intensityColor.r = 1 - rb.velocity.magnitude;
            img.GetComponent<Image>().color = intensityColor;
            txt.text = "Friction Force: " + rb.velocity.magnitude.ToString();
        }
    }

    public void OnCollisionExit(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.tag == "Cochlea")
        {
            img.GetComponent<Image>().color = Color.white;
            txt.text = "Friction Force: " + "0";
        }
    }

}
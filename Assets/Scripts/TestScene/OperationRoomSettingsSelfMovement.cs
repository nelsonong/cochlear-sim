using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.VR;
using UnityEngine.XR;

public class OperationRoomSettingsSelfMovement : MonoBehaviour {

    public GameObject electrode;
    public GameObject operationPlace;
    private float Timer;
    private bool isGlowing;
    private bool isTaken;
    private bool isZoom;
    private Behaviour electrodeHalo;
    private Behaviour operationPlaceHalo;
    private Camera camera;
    public Camera staticCamera;
    float zoom;

    // Use this for initialization
    void Start()
    {
        Timer = 2;
        isGlowing = true;
        isTaken = false;
        isZoom = false;
        zoom = 60;
        operationPlace.SetActive(false);
        electrodeHalo = (Behaviour)electrode.GetComponent("Halo");
        operationPlaceHalo = (Behaviour)operationPlace.GetComponent("Halo");
        camera = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (isZoom)
        //{
        //    Debug.Log("Inside Zoom");
        //    staticCamera.fieldOfView = zoom;
        //    if (zoom >= 4)
        //        zoom -= 1;
        //    else if(Input.GetKeyDown(KeyCode.Z)){
        //        SceneManager.LoadScene("TestScene2");
        //    }
        //}

        Timer -= Time.deltaTime;
        if (Timer <= 0f)
        {
            if (isZoom)
            {
                Debug.Log("Inside Zoom");
                staticCamera.fieldOfView = zoom;
                if (zoom >= 4)
                    zoom -= 0.05f;
                else
                {
                    SceneManager.LoadScene("AssessmentScene");
                }
            }

            if (!isTaken)
            {
                if (isGlowing)
                {
                    electrodeHalo.enabled = false;
                    Timer = 2f;
                    isGlowing = false;
                }
                else
                {
                    electrodeHalo.enabled = true;
                    Timer = 2f;
                    isGlowing = true;
                }
            }
            //else
            //{
            //    if (isGlowing)
            //    {
            //        operationPlaceHalo.enabled = false;
            //        Timer = 2f;
            //        isGlowing = false;
            //    }
            //    else
            //    {
            //        operationPlaceHalo.enabled = true;
            //        Timer = 2f;
            //        isGlowing = true;
            //    }
            //}
        }
    }

    void OnCollisionStay(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.tag == "electrode")
        {
            isTaken = true;
            isGlowing = true;
            electrode.SetActive(false);
            operationPlace.SetActive(true);
        }

        if (collisionInfo.gameObject.tag == "operationPlace" && isTaken)
        {
            //transform.position = -UnityEngine.XR.InputTracking.GetLocalPosition(XRNode.CenterEye);
            //transform.rotation = Quaternion.Inverse(UnityEngine.XR.InputTracking.GetLocalRotation(XRNode.CenterEye));
            //UnityEngine.XR.InputTracking.disablePositionalTracking = true;
            //Application.LoadLevel(1);
            
            if (Input.GetKeyDown(KeyCode.Z)) {
                camera.gameObject.SetActive(false);
                staticCamera.gameObject.SetActive(true);
                staticCamera.fieldOfView = zoom;
                isZoom = true;
            }
        }
    }
}

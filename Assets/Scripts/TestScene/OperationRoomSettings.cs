using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OperationRoomSettings : MonoBehaviour
{

    public GameObject electrode;
    public GameObject operationPlace;
    public Text Instructions;
    private float Timer;
    private bool isGlowing;
    private bool isTaken;
    private Behaviour electrodeHalo;
    private Behaviour operationPlaceHalo;

    // Use this for initialization
    void Start()
    {
        Timer = 2;
        isGlowing = true;
        isTaken = false;
        operationPlace.SetActive(false);
        electrodeHalo = (Behaviour)electrode.GetComponent("Halo");
        operationPlaceHalo = (Behaviour)operationPlace.GetComponent("Halo");
        Instructions.text = "Pick up the electrode on the table beside the patient";
    }

    // Update is called once per frame
    void Update()
    {

        Timer -= Time.deltaTime;
        if (Timer <= 0f)
        {
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
            else
            {
                if (isGlowing)
                {
                    operationPlaceHalo.enabled = false;
                    Timer = 2f;
                    isGlowing = false;
                }
                else
                {
                    operationPlaceHalo.enabled = true;
                    Timer = 2f;
                    isGlowing = true;
                }
            }
        }
    }

    void OnCollisionStay(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.tag == "electrode")
        {
            Instructions.text = "Press Space to pick up the electrode";
            if (Input.GetKey(KeyCode.Space))
            {
                isTaken = true;
                isGlowing = true;
                electrode.SetActive(false);
                operationPlace.SetActive(true);
                Instructions.text = "Go to the patient and press Space to start the operation";
            }
        }

        if (collisionInfo.gameObject.tag == "operationPlace" && isTaken)
        {
            Instructions.text = "Press Space to sart the operation";
            if (Input.GetKey(KeyCode.Space))
            {
                Application.LoadLevel(1);
            }
        }
    }

    void OnCollisionExit(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.tag == "electrode" && !isTaken)
        {
            Instructions.text = "Pick up the electrode on the table beside the patient";
        }

        if (collisionInfo.gameObject.tag == "operationPlace" && isTaken)
        {
            Instructions.text = "Go to the patient and press Space to start the operation";
        }
    }
}
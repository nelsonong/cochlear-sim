using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    private GameObject cochlea;

    public float distance;

    public float moveSpeed;

	void Update () {
        if (cochlea == null)
        {
            cochlea = StatsManager.instance.GetActiveCochlea();
        }

        else if (cochlea.activeSelf)
        {
            Vector3 position = cochlea.transform.position;
            position.y += 0.05f;
            position.z -= 0.85f;
            transform.position = Vector3.Lerp(transform.position, position, Time.deltaTime * moveSpeed);
        }   
    }
}

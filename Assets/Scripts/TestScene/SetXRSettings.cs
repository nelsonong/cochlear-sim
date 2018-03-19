using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class SetXRSettings : MonoBehaviour {

    void Awake()
    {
        if (SceneManager.GetActiveScene().name == "TestScene" || SceneManager.GetActiveScene().name == "TestScene2")
        {
            if (!XRSettings.enabled)
            {
                XRSettings.enabled = true;
            }
        }
        else if (XRSettings.enabled)
        {
            XRSettings.enabled = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

	void OnDestroy () {
        XRSettings.enabled = false;
    }
}

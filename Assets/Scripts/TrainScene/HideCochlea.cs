using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideCochlea : MonoBehaviour {

	public void HandleHide() {
		GameObject cochlea = GameObject.Find("default");
		cochlea.GetComponent<Renderer>().enabled = false;

		GameObject electrode = GameObject.Find("Electrode");
		int numChildren = electrode.transform.childCount;
        for (int i = 0; i < numChildren; i++) {
			GameObject capsule = electrode.transform.GetChild(i).gameObject;
            capsule.GetComponent<Renderer>().enabled = false;
        }

		GameObject startScreen = GameObject.Find("Start Screen");
		startScreen.SetActive(false);
	}

	public void HandleShow() {
		GameObject cochlea = GameObject.Find("default");
		cochlea.GetComponent<Renderer>().enabled = true;

		GameObject electrode = GameObject.Find("Electrode");
		int numChildren = electrode.transform.childCount;
        for (int i = 0; i < numChildren; i++) {
			GameObject capsule = electrode.transform.GetChild(i).gameObject;
            capsule.GetComponent<Renderer>().enabled = true;
        }
	}
}

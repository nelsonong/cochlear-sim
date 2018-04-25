using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSimulation : MonoBehaviour {

	public GraphManager graphManager;
	public Text timestampText;
	public Text typeText;

	public void HandleChange() {
		string type = (typeText.text.Equals("Depth vs. Time")) ? "depth" : "force";
        Debug.Log("ChangeSimulation handlechange is called with type: ");
        Debug.Log(type);
        graphManager.ChangeSimulation(timestampText.text, type);
	}
}

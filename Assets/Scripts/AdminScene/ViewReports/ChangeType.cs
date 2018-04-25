using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeType : MonoBehaviour {

	public GraphManager graphManager;
	public Text timestampText;
	public Text typeText;
	public Text chartTitle;

	public void HandleChange() {
		string type = (typeText.text.Equals("Depth vs. Time")) ? "depth" : "force";
		chartTitle.text = type.Equals("depth") ? "Depth vs. Time" : "Force vs. Depth";
        Debug.Log("ChangeType handlechange is called with type: ");
        Debug.Log(type);
        Debug.Log(chartTitle.text);
        Debug.LogError("Does this show up?");

		if (timestampText.text.Equals("Select Trial"))
			return;

		graphManager.ChangeChart(type);
	}
}

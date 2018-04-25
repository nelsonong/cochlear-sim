using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopulateStatistics : MonoBehaviour {

	public Text usernameText;
	private GameObject reportsPopup;
	public Dropdown timestampDropdown;
	public Dropdown chartTypeDropdown;

	void Start () {
		string statsUser = PlayerPrefs.GetString("statsUser");
		usernameText.text = statsUser + "'s Statistics";

		GraphManager graphManager = GetComponent<GraphManager>();
		List<string> timestamps = graphManager.GetDataTimeStamps(statsUser);

		timestampDropdown.options.Clear();
		foreach (string timestamp in timestamps) {
			timestampDropdown.options.Add (new Dropdown.OptionData() {text=timestamp});
		}

		//graphManager.ChangeSimulation(timestamps[0]);

		// chartTypeDropdown.options.Clear();
		// chartTypeDropdown.options.Add (new Dropdown.OptionData() {text="hi2"});

		reportsPopup = GameObject.Find("Reports-Popup(Clone)");
		reportsPopup.SetActive(false);
	}
	
	public void EnableReportsPopup() {
		reportsPopup.SetActive(true);
	}
}

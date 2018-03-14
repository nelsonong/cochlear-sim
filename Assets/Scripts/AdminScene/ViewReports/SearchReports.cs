using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SearchReports : MonoBehaviour {

	public InputField searchField;
	public GameObject listItemPrefab;
	public GameObject scrollViewContent;

	public void HandleSearch() {
		string input = searchField.text;
		List<string> users = PlayerPrefs.GetString("users").Split(';').ToList();
		if (!input.Equals("")) {
			ClearPanel();
			Regex r = new Regex(input, RegexOptions.IgnoreCase);
			foreach (string username in users) {
				Match m = r.Match(username);
				if (m.Success) {
					AddReportToPanel(username);
				}
			}
		} else {
			ClearPanel();
			foreach (string username in users) {
				AddReportToPanel(username);
			}
		}
	}

	void ClearPanel() {
		int numChildren = scrollViewContent.transform.childCount;
        for (int i = 0; i < numChildren; i++) {
			GameObject childGameObject = scrollViewContent.transform.GetChild(i).gameObject;
            Destroy(childGameObject);
        }
	}

	public void AddReportToPanel(string username) {
		GameObject listItem = Instantiate(listItemPrefab);
		listItem.SetActive(true);
		listItem.transform.localScale = new Vector3( 1.0f, 1.0f, 1.0f );
		listItem.transform.SetParent(scrollViewContent.transform, false);

		// Set username and color.
		Color red = new Color32(248, 56, 56, 255);
		Color yellow = new Color32(255, 183, 0, 255);

		List<string> admins = PlayerPrefs.GetString("admins").Split(';').ToList();
		bool isAdmin = admins.Exists(e => e.EndsWith(username));
		Text[] textItems = listItem.GetComponentsInChildren<Text>();
		textItems[0].text = username + "\n";
		textItems[0].color = isAdmin ? red : yellow;

		// Set stats.
		StatsItem userData = StatsManager.instance.GetUserStats(username);
		textItems[1].text = userData.timeTraining.ToString();
		textItems[2].text = userData.successfulInserts.ToString();
		textItems[3].text = userData.failedInserts.ToString();
		textItems[4].text = userData.avgInsertionTimes.ToString();
		textItems[5].text = userData.avgInsertionDepths.ToString();
	}
}

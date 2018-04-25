using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PopulateReportsPanel : MonoBehaviour {

	public GameObject listItemPrefab;
	public GameObject scrollViewContent;

	void Start() {
		PopulatePanel();
	}

	public void PopulatePanel(bool clearPanel = true) {
		ClearPanel();
		
		List<string> users = PlayerPrefs.GetString("users").Split(';').ToList();
		foreach (string username in users) {
            if (StatsManager.instance.GetUserStats(username) != null)
                AddUserToPanel(username);
		}
	}

	void ClearPanel() {
		int numChildren = scrollViewContent.transform.childCount;
        for (int i = 0; i < numChildren; i++) {
			GameObject childGameObject = scrollViewContent.transform.GetChild(i).gameObject;
            Destroy(childGameObject);
        }
	}

	void AddUserToPanel(string username) {
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
		textItems[1].text = userData.timeTraining.ToString() + "s";
		textItems[2].text = userData.successfulInserts.ToString();
		textItems[3].text = userData.failedInserts.ToString();
		textItems[4].text = userData.avgInsertionTimes.ToString() + "s";
		textItems[5].text = userData.avgInsertionDepths.ToString() + "%";
	}
	
}

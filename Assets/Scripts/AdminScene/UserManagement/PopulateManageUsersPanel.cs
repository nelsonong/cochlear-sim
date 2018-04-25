using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PopulateManageUsersPanel : MonoBehaviour {

	public GameObject listItemPrefab;
	public GameObject scrollViewContent;

	void Start() {
		PopulatePanel();
	}

	public void PopulatePanel(bool clearPanel = true) {
		ClearPanel();
		
		List<string> users = PlayerPrefs.GetString("users").Split(';').ToList();
		foreach (string username in users) {
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

	public void RepopulateListItem(string username) {
		int numChildren = scrollViewContent.transform.childCount;
        for (int i = 0; i < numChildren; i++) {
			GameObject listItem = scrollViewContent.transform.GetChild(i).gameObject;
            
			// Set username text.
			Text[] textItems = listItem.GetComponentsInChildren<Text>();
			if (textItems[0].text == username + "\n") {
				// Set privileges text.
				Color red = new Color32(248, 56, 56, 255);
				Color yellow = new Color32(255, 183, 0, 255);
				List<string> admins = PlayerPrefs.GetString("admins").Split(';').ToList();
				bool isAdmin = admins.Exists(e => e.EndsWith(username));
				textItems[1].text = isAdmin ? "/ Admin\n" : "/ User\n";
				textItems[1].color = isAdmin ? red : yellow;
			}
        }
	}

	void AddUserToPanel(string username) {
		GameObject listItem = Instantiate(listItemPrefab);
		listItem.SetActive(true);
		listItem.transform.localScale = new Vector3( 1.0f, 1.0f, 1.0f );
		listItem.transform.SetParent(scrollViewContent.transform, false);

		// Set username text.
		Text[] textItems = listItem.GetComponentsInChildren<Text>();
		textItems[0].text = username + "\n";

		// Set privileges text.
		Color red = new Color32(248, 56, 56, 255);
		Color yellow = new Color32(255, 183, 0, 255);
		List<string> admins = PlayerPrefs.GetString("admins").Split(';').ToList();
		bool isAdmin = admins.Exists(e => e.EndsWith(username));
		textItems[1].text = isAdmin ? "/ Admin\n" : "/ User\n";
		textItems[1].color = isAdmin ? red : yellow;

        StatsItem userData = StatsManager.instance.GetUserStats(username);
        string timeTraining = userData.timeTraining.ToString() + "s";
        string successfulInserts = userData.successfulInserts.ToString();
        string failedInserts = userData.failedInserts.ToString();
        string avgInsertionTime = userData.avgInsertionTimes.ToString() + "s";
        string avgInsertionDepth = userData.avgInsertionDepths.ToString() + "%";
        textItems[2].text = "Time trained: " + timeTraining + ". Successful inserts: " + successfulInserts + ". Failed inserts: " + failedInserts + ".";

    }
}

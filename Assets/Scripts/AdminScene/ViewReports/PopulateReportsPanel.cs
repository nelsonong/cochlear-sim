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
		// if (clearPanel) {
		 	ClearPanel();
		// }
		
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

	void AddUserToPanel(string username) {
		GameObject listItem = Instantiate(listItemPrefab);
		listItem.SetActive(true);
		listItem.transform.localScale = new Vector3( 1.0f, 1.0f, 1.0f );
		listItem.transform.SetParent(scrollViewContent.transform, false);

		// Set username text.
		Text[] textItems = listItem.GetComponentsInChildren<Text>();
		textItems[0].text = username + "\n";
		textItems[1].text = "timeTest";
		textItems[2].text = "successTest";
		textItems[3].text = "failedTest";
		textItems[4].text = "avgTimeTest";
		textItems[5].text = "avgDepthTest";
	}
	
}

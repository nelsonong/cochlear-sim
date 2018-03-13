using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SearchUsers : MonoBehaviour {

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
					AddUserToPanel(username);
				}
			}
		} else {
			ClearPanel();
			foreach (string username in users) {
				AddUserToPanel(username);
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

	void AddUserToPanel(string username) {
		GameObject listItem = Instantiate(listItemPrefab);
		listItem.SetActive(true);
		listItem.transform.localScale = new Vector3( 1.0f, 1.0f, 1.0f );
		listItem.transform.SetParent(scrollViewContent.transform, false);

		// Set username text.
		Text[] textItems = listItem.GetComponentsInChildren<Text>();
		textItems[0].text = username + "\n";

		// Set privileges text.
		List<string> admins = PlayerPrefs.GetString("admins").Split(';').ToList();
		bool isAdmin = admins.Exists(e => e.EndsWith(username));
		if (isAdmin) {
			textItems[1].text = "/ Admin\n";
			textItems[1].color = new Color32(248, 56, 56, 255);
		} else {
			textItems[1].text = "/ User\n";
			textItems[1].color = new Color32(255, 183, 0, 255);
		}
	}
}

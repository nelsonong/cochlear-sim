using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Ricimi;

public class PopulateModifyUserPanel : MonoBehaviour {

	public GameObject modifyUserPanel;
	public InputField usernameField;
	public GameObject usernameFieldBackground;
	public InputField passwordField;
	public Slider privilegeSlider;

	public void Start() {
		PopulateExistingUser();
	}

	void PopulateExistingUser() {
		// Set existing username & password.
		string username = PlayerPrefs.GetString("modifyUser");
		usernameField.text = username;
		usernameFieldBackground.GetComponent<Image>().color = new Color32(100, 100, 100, 100);
		usernameField.enabled = false;
		passwordField.text = PlayerPrefs.GetString(username);

		// Set existing privilege.
		List<string> admins = PlayerPrefs.GetString("admins").Split(';').ToList();
		bool isAdmin = admins.Exists(e => e.EndsWith(username));
		privilegeSlider.value = isAdmin ? 1 : 0;
	}
}

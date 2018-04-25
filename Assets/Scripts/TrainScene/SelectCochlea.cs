using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectCochlea : MonoBehaviour {

	public void HandleSelect(int model) {
		PlayerPrefs.SetInt("cochlea-model", model);
		SceneManager.LoadScene("TrainScene");
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenExplorer : MonoBehaviour {

	public void HandleOpen() {
        string path = "C:\\Users\\achal\\Downloads\\capstone\\Assets";
        System.Diagnostics.Process.Start("explorer.exe","/select,"+path);
	}
}

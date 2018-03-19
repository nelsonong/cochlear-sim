using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitOrReloadScene : MonoBehaviour {

    public void LoadScene(int sceneIndex)
    {
        Application.LoadLevel(sceneIndex);
    }

    public void Quit()
    {
        //#if UNITY_EDITOR
        //		UnityEditor.EditorApplication.isPlaying = false;
        //#else
        //		Application.QuitOnClick ();
        //#endif
        Application.Quit();
    }
}

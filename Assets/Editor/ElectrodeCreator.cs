using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BuildElectrode))]
public class ElectrodeCreator : Editor
{
    public override void OnInspectorGUI()
    {

        DrawDefaultInspector();

        BuildElectrode builder = (BuildElectrode)target;
        if (GUILayout.Button("Build Electrode"))
        {
            builder.OnBuildElectrode();
        }
    }



}
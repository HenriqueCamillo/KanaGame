using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(KanaDatabase))]
public class KanaDatabaseEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        
        KanaDatabase myScript = (KanaDatabase)target;
        if(GUILayout.Button("Reset Hits/Misses"))
        {
            myScript.ResetHitsMisses();
        }
    }
}
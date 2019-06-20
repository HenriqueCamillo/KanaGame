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
        Undo.RecordObject(myScript, "Clearing Hiragana");

        if(GUILayout.Button("Reset Hiragana Hits/Misses"))
            myScript.ResetHitsMisses(myScript.hiraganaStats);

        Undo.RecordObject(myScript, "Clearing Katakana");

        if(GUILayout.Button("Reset Katakana Hits/Misses"))
            myScript.ResetHitsMisses(myScript.katakanaStats); 
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MIMG_settings))]
public class MIMG_settingsEditor : Editor
{
    MIMG_settings myScript;



    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GuiLine();
        EditorGUILayout.LabelField("Save default schemes:", GUILayout.MinWidth(60));


        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Save Scheme 1"))
        {
            myScript.SaveDefaultScheme(1);
            AssetDatabase.Refresh();
        }
        if (GUILayout.Button("Save Scheme 2"))
        {
            myScript.SaveDefaultScheme(2);
            AssetDatabase.Refresh();
        }
        if (GUILayout.Button("Save Scheme 3"))
        {
            myScript.SaveDefaultScheme(3);
            AssetDatabase.Refresh();
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Separator();
        EditorGUILayout.LabelField("Load default schemes:", GUILayout.MinWidth(60));

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Load Scheme 1"))
        {
            myScript.LoadDefaultScheme(1);
        }
        if (GUILayout.Button("Load Scheme 2"))
        {
            myScript.LoadDefaultScheme(2);
        }
        if (GUILayout.Button("Load Scheme 3"))
        {
            myScript.LoadDefaultScheme(3);
        }
        EditorGUILayout.EndHorizontal();
    }

    public void OnEnable()
    {
        if (Application.isEditor && !Application.isPlaying)
        {
            myScript = (MIMG_settings)target;
        }
    }

    void GuiLine(int i_height = 1)
    {
        EditorGUILayout.Separator();
        Rect rect = EditorGUILayout.GetControlRect(false, i_height);
        rect.height = i_height;
        EditorGUI.DrawRect(rect, new Color(0.5f, 0.5f, 0.5f, 1));
        EditorGUILayout.Separator();
    }
}

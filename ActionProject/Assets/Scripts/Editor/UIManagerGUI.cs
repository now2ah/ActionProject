using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Action.Manager;

[CustomEditor(typeof(UIManager))]
public class UIManagerGUI : Editor
{
    //float x;
    //float y;
    float width;
    float height;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUILayout.BeginVertical();

        if (GUILayout.Button("Hide Unit Info UI"))
        {
            UIManager.Instance.ShowUnitInfoUI(!UIManager.Instance.IsShowUnitPanel);
        }

        GUILayout.BeginHorizontal();
        //GUILayout.Label("X");
        //x = float.Parse(GUILayout.TextField(x.ToString()));
        //GUILayout.Label("Y");
        //y = float.Parse(GUILayout.TextField(y.ToString()));
        GUILayout.Label("Width");
        width = float.Parse(GUILayout.TextField(width.ToString()));
        GUILayout.Label("Height");
        height = float.Parse(GUILayout.TextField(height.ToString()));
        GUILayout.EndHorizontal();

        if (GUILayout.Button("Apply Rect"))
        {
            UIManager.Instance.SetUnitInfoRect(width, height);
        }

        GUILayout.EndVertical();
    }
}

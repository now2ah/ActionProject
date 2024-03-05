using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TestScene))]
public class TestSceneGUI : Editor
{
    TestScene testScene;

    private void OnEnable()
    {
        testScene = (TestScene)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if(GUILayout.Button("TimeTest"))
        {
            testScene.TimeTest();
        }
    }
}

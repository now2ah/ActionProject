using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Action.Manager;

[CustomEditor(typeof(GameManager))]
public class GameManagerGUI : Editor
{
    GameManager _gameManager;

    private void OnEnable()
    {
        _gameManager = (GameManager)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        serializedObject.Update();
        SerializedProperty _townBuildPhaseTime = serializedObject.FindProperty("_townBuildPhaseTime");
        SerializedProperty _huntPhaseTime = serializedObject.FindProperty("_huntPhaseTime");
        SerializedProperty _defensePhaseTime = serializedObject.FindProperty("_defensePhaseTime");
    }
}

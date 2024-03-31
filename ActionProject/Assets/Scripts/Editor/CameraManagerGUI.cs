using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Action.Manager;

[CustomEditor(typeof(CameraManager))]
public class CameraManagerGUI : Editor
{
    CameraManager _cameraManager;
    float _offsetY;
    float _offsetZ;
    float _fov;

    private void OnEnable()
    {
        _cameraManager = target as CameraManager;
        _offsetY = _cameraManager.VCamOffsetY;
        _offsetZ = _cameraManager.VCamOffsetZ;
        _fov = _cameraManager.VCamFOV;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        serializedObject.Update();

        EditorGUILayout.BeginVertical();
        GUILayout.Label("Camera Setting");
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("OffsetY");
        _offsetY = EditorGUILayout.FloatField(_offsetY);
        GUILayout.Label("OffsetZ");
        _offsetZ = EditorGUILayout.FloatField(_offsetZ);
        GUILayout.Label("FOV");
        _fov = EditorGUILayout.FloatField(_fov);
        EditorGUILayout.EndHorizontal();
        if (GUILayout.Button("APPLY"))
        {
            _cameraManager.SetVCamOffset(_offsetY, _offsetZ);
            _cameraManager.SetVCamFOV(_fov);
        }
        EditorGUILayout.EndVertical();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.Manager;

public class CharacterTestScene : MonoBehaviour
{
    private void OnGUI()
    {
        GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
        buttonStyle.fontSize = 25;
        GUILayout.BeginVertical();
        if (GUI.Button(new Rect(50, 50, 300, 50), "Game Start", buttonStyle))
        {
            GameManager.Instance.GameStart();
            //GameManager.Instance.CommanderUnit.ActivateAutoAttack(1);
        }
        if (GUI.Button(new Rect(50, 150, 300, 50), "Summon Monster", buttonStyle))
        {
            //GameManager.Instance.AddAllEnemySpawners();
            GameManager.Instance.CreateTestWave();
        }
        GUILayout.EndVertical();
    }

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.Initialize();
        GameManager.Instance.SetCharacterTestScene();
        //GameManager.Instance.CreateCharacter();
        InputManager.Instance.Initialize();
        CameraManager.Instance.Initialize();
        UIManager.Instance.Initialize();
        CameraManager.Instance.CreateFixedVirtualCamera();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

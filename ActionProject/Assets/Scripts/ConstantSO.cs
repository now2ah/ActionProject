using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Constant", menuName = "ScriptableObject/ConstantSO", order = 1)]
public class ConstantSO : ScriptableObject
{
    //���� ī�޶� ����
    public float GAMECAMERA_VERTICAL_DIST = 60.0f;
    //���� ī�޶� �Ÿ�
    public float GAMECAMERA_HORIZONTAL_DIST = -30.0f;
    //���� ī�޶� fov
    public float GAMECAMERA_FOV = 60.0f;
    //���� �ٴ� y ��
    public float GROUND_Y_POS = 6.0f;

    public string[] ACTIONS = { "up", "left", "down", "right" };

    //Town Stage ���̽� �ǹ� ��ġ
    public Vector3 VILLAGE_BASE_START_POS = new Vector3(-200.0f, 6.0f, 0.0f);

    //InGame UI ǥ�� �Ÿ�
    public float INGAMEUI_VISIBLE_DISTANT = 15.0f;

    //Phase �ð�
    public float TOWNBUILD_PHASE_TIME = 10.0f;
    public float HUNT_PHASE_TIME = 10.0f;
    public float DEFENSE_PHASE_TIME = 10.0f;

    //Game Refresh �ð�
    public float GAME_REFRESH_TIME = 2.0f;

    public enum eCharacterState
    {
        IDLE,
        MOVING
    }
}
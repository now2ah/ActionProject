using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Constant", menuName = "ScriptableObject/ConstantSO", order = 1)]
public class Constant : ScriptableObject
{
    //���� ī�޶� ����
    public float GAMECAMERA_VERTICAL_DIST;// = 60.0f;
    //���� ī�޶� �Ÿ�
    public float GAMECAMERA_HORIZONTAL_DIST;// = -30.0f;
    //���� ī�޶� fov
    public float GAMECAMERA_FOV;// = 60.0f;
    //���� �ٴ� y ��
    public float GROUND_Y_POS;// = 6.0f;
    //����ü y��
    public float DEFENSE_PROJECTILE_Y_POS;// = 7.0f;
    public float HUNT_PROJECTILE_Y_POS;//0.5f;

    public string[] ACTIONS;// = { "up", "left", "down", "right" };

    //Town Stage ���̽� �ǹ� ��ġ
    public Vector3 VILLAGE_BASE_START_POS;// = new Vector3(-200.0f, 6.0f, 0.0f);

    //InGame UI ǥ�� �Ÿ�
    public float INGAMEUI_VISIBLE_DISTANT;// = 15.0f;

    //Phase �ð�
    public float TOWNBUILD_PHASE_TIME;// = 10.0f;
    public float HUNT_PHASE_TIME;// = 10.0f;
    public float DEFENSE_PHASE_TIME;// = 10.0f;

    //Game Refresh �ð�
    public float GAME_REFRESH_TIME;// = 2.0f;

    //Commander Ability ����
    public int ABILITY_COUNT;//
    
    //Commander AutoAttack ����
    public int AUTOATTACK_TYPE_COUNT;// = 1;

    //���� Resource
    public int START_GOLD_AMOUNT;

    //�ִ� ������Ʈ ��(Pool�� ���)
    public int NORMALENEMY_MAX_AMOUNT;
    public int RANGEENEMY_MAX_AMOUNT;
    public int NORMALPROJECTILE_MAX_AMOUNT;
    public int GUIDEDPROJECTILE_MAX_AMOUNT;
    public int RANGEENEMYPROJECTILE_MAX_AMOUNT;
    public int ARROWPROJECTILE_MAX_AMOUNT;
    public int EXPORB_MAX_AMOUNT;
}

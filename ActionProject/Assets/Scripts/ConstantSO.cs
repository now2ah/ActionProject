using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Constant", menuName = "ScriptableObject/ConstantSO", order = 1)]
public class Constant : ScriptableObject
{
    //게임 카메라 높이
    public float GAMECAMERA_VERTICAL_DIST;// = 60.0f;
    //게임 카메라 거리
    public float GAMECAMERA_HORIZONTAL_DIST;// = -30.0f;
    //게임 카메라 fov
    public float GAMECAMERA_FOV;// = 60.0f;
    //지형 바닥 y 값
    public float GROUND_Y_POS;// = 6.0f;
    //투사체 y값
    public float DEFENSE_PROJECTILE_Y_POS;// = 7.0f;
    public float HUNT_PROJECTILE_Y_POS;//0.5f;

    public string[] ACTIONS;// = { "up", "left", "down", "right" };

    //Town Stage 베이스 건물 위치
    public Vector3 VILLAGE_BASE_START_POS;// = new Vector3(-200.0f, 6.0f, 0.0f);

    //InGame UI 표시 거리
    public float INGAMEUI_VISIBLE_DISTANT;// = 15.0f;

    //Phase 시간
    public float TOWNBUILD_PHASE_TIME;// = 10.0f;
    public float HUNT_PHASE_TIME;// = 10.0f;
    public float DEFENSE_PHASE_TIME;// = 10.0f;

    //Game Refresh 시간
    public float GAME_REFRESH_TIME;// = 2.0f;

    //Commander Ability 갯수
    public int ABILITY_COUNT;//
    
    //Commander AutoAttack 개수
    public int AUTOATTACK_TYPE_COUNT;// = 1;

    //시작 Resource
    public int START_GOLD_AMOUNT;

    //최대 오브젝트 수(Pool에 사용)
    public int NORMALENEMY_MAX_AMOUNT;
    public int RANGEENEMY_MAX_AMOUNT;
    public int NORMALPROJECTILE_MAX_AMOUNT;
    public int GUIDEDPROJECTILE_MAX_AMOUNT;
    public int RANGEENEMYPROJECTILE_MAX_AMOUNT;
    public int ARROWPROJECTILE_MAX_AMOUNT;
    public int EXPORB_MAX_AMOUNT;
}

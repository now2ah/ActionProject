using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.SO;

namespace Action
{
    public static class Enums
    {
        public enum eHitBoxType
        {
            ONLY_ENEMY,
            ONLY_PLAYER,
            BOTH
        }

        public enum eEnemyType
        {
            NORMAL,
            RANGE
        }
    }
}

//namespace Action
//{
//    public static class Constant
//    {
//        static ConstantSO _constantSO;

//        public static void GetScriptableObject()
//        {
//            _constantSO = Resources.Load("ScriptableObject/Constant") as ConstantSO;
//        }

//        //게임 카메라 높이
//        public static float GAMECAMERA_VERTICAL_DIST = _constantSO.GAMECAMERA_VERTICAL_DIST; //60.0f;
//        //게임 카메라 거리
//        public static float GAMECAMERA_HORIZONTAL_DIST = _constantSO.GAMECAMERA_HORIZONTAL_DIST; //-30.0f;
//        //게임 카메라 fov
//        public static float GAMECAMERA_FOV = _constantSO.GAMECAMERA_FOV; //30.0f;
//        //지형 바닥 y 값
//        public static float GROUND_Y_POS = _constantSO.GROUND_Y_POS; //6.0f;
//        //투사체 y값
//        public static float PROJECTILE_Y_POS = _constantSO.PROJECTILE_Y_POS; //7.0f;

//        public static string[] ACTIONS = _constantSO.ACTIONS; //{ "up", "left", "down", "right" };

//        //Town Stage 베이스 건물 위치
//        public static Vector3 VILLAGE_BASE_START_POS = _constantSO.VILLAGE_BASE_START_POS; //new Vector3(-200.0f, GROUND_Y_POS, 0.0f);

//        //InGame UI 표시 거리
//        public static float INGAMEUI_VISIBLE_DISTANT = _constantSO.INGAMEUI_VISIBLE_DISTANT; //15.0f;

//        //Phase 시간
//        public static float TOWNBUILD_PHASE_TIME = _constantSO.TOWNBUILD_PHASE_TIME; //10.0f;
//        public static float HUNT_PHASE_TIME = _constantSO.HUNT_PHASE_TIME; //10.0f;
//        public static float DEFENSE_PHASE_TIME = _constantSO.DEFENSE_PHASE_TIME; //10.0f;

//        //Game Refresh 시간 (몬스터 행동 갱신에 사용중)
//        public static float GAME_REFRESH_TIME = _constantSO.GAME_REFRESH_TIME; //1.0f;

//        //Commander AutoAttack 개수
//        public static int AUTOATTACK_TYPE_COUNT = _constantSO.AUTOATTACK_TYPE_COUNT; //1;

//        public enum eHitBoxType
//        {
//            ONLY_ENEMY,
//            ONLY_PLAYER,
//            BOTH
//        }

//        public enum eEnemyType
//        {
//            NORMAL,
//            RANGE
//        }
//    }
//}

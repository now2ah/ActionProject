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

//        //���� ī�޶� ����
//        public static float GAMECAMERA_VERTICAL_DIST = _constantSO.GAMECAMERA_VERTICAL_DIST; //60.0f;
//        //���� ī�޶� �Ÿ�
//        public static float GAMECAMERA_HORIZONTAL_DIST = _constantSO.GAMECAMERA_HORIZONTAL_DIST; //-30.0f;
//        //���� ī�޶� fov
//        public static float GAMECAMERA_FOV = _constantSO.GAMECAMERA_FOV; //30.0f;
//        //���� �ٴ� y ��
//        public static float GROUND_Y_POS = _constantSO.GROUND_Y_POS; //6.0f;
//        //����ü y��
//        public static float PROJECTILE_Y_POS = _constantSO.PROJECTILE_Y_POS; //7.0f;

//        public static string[] ACTIONS = _constantSO.ACTIONS; //{ "up", "left", "down", "right" };

//        //Town Stage ���̽� �ǹ� ��ġ
//        public static Vector3 VILLAGE_BASE_START_POS = _constantSO.VILLAGE_BASE_START_POS; //new Vector3(-200.0f, GROUND_Y_POS, 0.0f);

//        //InGame UI ǥ�� �Ÿ�
//        public static float INGAMEUI_VISIBLE_DISTANT = _constantSO.INGAMEUI_VISIBLE_DISTANT; //15.0f;

//        //Phase �ð�
//        public static float TOWNBUILD_PHASE_TIME = _constantSO.TOWNBUILD_PHASE_TIME; //10.0f;
//        public static float HUNT_PHASE_TIME = _constantSO.HUNT_PHASE_TIME; //10.0f;
//        public static float DEFENSE_PHASE_TIME = _constantSO.DEFENSE_PHASE_TIME; //10.0f;

//        //Game Refresh �ð� (���� �ൿ ���ſ� �����)
//        public static float GAME_REFRESH_TIME = _constantSO.GAME_REFRESH_TIME; //1.0f;

//        //Commander AutoAttack ����
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

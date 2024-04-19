using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Action
{
    public static class Constant
    {
        //���� ī�޶� ����
        public static float GAMECAMERA_VERTICAL_DIST = 60.0f;
        //���� ī�޶� �Ÿ�
        public static float GAMECAMERA_HORIZONTAL_DIST = -30.0f;
        //���� ī�޶� fov
        public static float GAMECAMERA_FOV = 30.0f;
        //���� �ٴ� y ��
        public static float GROUND_Y_POS = 6.0f;

        public static string[] ACTIONS = { "up", "left", "down", "right" };

        //Town Stage ���̽� �ǹ� ��ġ
        public static Vector3 VILLAGE_BASE_START_POS = new Vector3(-200.0f, GROUND_Y_POS, 0.0f);

        //InGame UI ǥ�� �Ÿ�
        public static float INGAMEUI_VISIBLE_DISTANT = 15.0f;

        //Phase �ð�
        public static float TOWNBUILD_PHASE_TIME = 10.0f;
        public static float HUNT_PHASE_TIME = 10.0f;
        public static float DEFENSE_PHASE_TIME = 10.0f;

        //Game Refresh �ð� (���� �ൿ ���ſ� �����)
        public static float GAME_REFRESH_TIME = 1.0f;
        
        public enum eCharacterState
        {
            IDLE,
            MOVING
        }
    }
}

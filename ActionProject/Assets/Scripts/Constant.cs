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
        public static float GAMECAMERA_HORIZONTAL_DIST = -40.0f;
        //���� ī�޶� fov
        public static float GAMECAMERA_FOV = 60.0f;
        //���� �ٴ� y ��
        public static float GROUND_Y_POS = 5.1f;

        public static string[] ACTIONS = { "up", "left", "down", "right" };

        public enum eCharacterState
        {
            IDLE,
            MOVING
        }
    }
}

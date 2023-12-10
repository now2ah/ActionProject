using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Action
{
    public static class Constant
    {
        //게임 카메라 높이
        public static float GAMECAMERA_VERTICAL_DIST = 60.0f;
        //게임 카메라 거리
        public static float GAMECAMERA_HORIZONTAL_DIST = -40.0f;
        //게임 카메라 fov
        public static float GAMECAMERA_FOV = 60.0f;
        //지형 바닥 y 값
        public static float GROUND_Y_POS = 5.1f;

        public static string[] ACTIONS = { "up", "left", "down", "right" };

        public enum eCharacterState
        {
            IDLE,
            MOVING
        }
    }
}

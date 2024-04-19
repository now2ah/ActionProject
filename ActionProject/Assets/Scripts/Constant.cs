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
        public static float GAMECAMERA_HORIZONTAL_DIST = -30.0f;
        //게임 카메라 fov
        public static float GAMECAMERA_FOV = 30.0f;
        //지형 바닥 y 값
        public static float GROUND_Y_POS = 6.0f;

        public static string[] ACTIONS = { "up", "left", "down", "right" };

        //Town Stage 베이스 건물 위치
        public static Vector3 VILLAGE_BASE_START_POS = new Vector3(-200.0f, GROUND_Y_POS, 0.0f);

        //InGame UI 표시 거리
        public static float INGAMEUI_VISIBLE_DISTANT = 15.0f;

        //Phase 시간
        public static float TOWNBUILD_PHASE_TIME = 10.0f;
        public static float HUNT_PHASE_TIME = 10.0f;
        public static float DEFENSE_PHASE_TIME = 10.0f;

        //Game Refresh 시간 (몬스터 행동 갱신에 사용중)
        public static float GAME_REFRESH_TIME = 1.0f;
        
        public enum eCharacterState
        {
            IDLE,
            MOVING
        }
    }
}

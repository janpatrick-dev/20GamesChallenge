using UnityEngine;

namespace DefaultNamespace
{
    public struct ScreenInfo
    {
        public static float GetHalfWidth()
        {
            return Camera.main.aspect * Camera.main.orthographicSize;
        }

        public static float GetHeight()
        {
            return Camera.main.orthographicSize;
        }
    }
}
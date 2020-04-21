﻿using UnityEngine;


namespace Snake_box
{
    public static partial class Vector2Extensions
    {
        public static float GetRandom(this Vector2 v)
        {
            return Random.Range(v.x, v.y);
        }
    }
}

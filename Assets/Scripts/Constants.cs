using System.Collections.Generic;
using UnityEngine;

namespace RandomDungeonWithBluePrint
{
    public static class Constants
    {
        public static class Direction
        {
            public const int Error = -1;
            public const int Unit = 45;
            public const int Right = 0;
            public const int UpRight = 45;
            public const int Up = 90;
            public const int UpLeft = 135;
            public const int Left = 180;
            public const int DownLeft = 225;
            public const int Down = 270;
            public const int DownRight = 315;

            public static readonly int[] FourDirections =
            {
                Right,
                Up,
                Left,
                Down
            };

            public static readonly Dictionary<int, Vector2Int> ToVector2Int = new Dictionary<int, Vector2Int>
            {
                {Left, new Vector2Int(-1, 0)},
                {Down, new Vector2Int(0, 1)},
                {Up, new Vector2Int(0, -1)},
                {Right, new Vector2Int(1, 0)},
                {UpLeft, new Vector2Int(-1, -1)},
                {UpRight, new Vector2Int(1, -1)},
                {DownLeft, new Vector2Int(-1, 1)},
                {DownRight, new Vector2Int(1, 1)},
            };

            public static readonly Dictionary<Vector2Int, int> ToInt = new Dictionary<Vector2Int, int>
            {
                {new Vector2Int(-1, 0), Left},
                {new Vector2Int(0, 1), Down},
                {new Vector2Int(0, -1), Up},
                {new Vector2Int(1, 0), Right},
                {new Vector2Int(-1, -1), UpLeft},
                {new Vector2Int(1, -1), UpRight},
                {new Vector2Int(-1, 1), DownLeft},
                {new Vector2Int(1, 1), DownRight},
            };

            public static int ToLeft(int current, int count = 1)
            {
                return (current + Unit * count) % 360;
            }

            public static Vector2Int ToLeftVector(int current, int count = 1)
            {
                return ToVector2Int[ToLeft(current, count)];
            }

            public static int ToRight(int current, int count = 1)
            {
                return (current + 360 - Unit * count) % 360;
            }

            public static Vector2Int ToRightVector(int current, int count = 1)
            {
                return ToVector2Int[ToRight(current, count)];
            }

            public static int Inverse(int current)
            {
                return current == Error ? Error : ToLeft(current, 4);
            }

            public static Vector2Int InverseVector(int current)
            {
                return ToVector2Int[Inverse(current)];
            }
        }

        public static class FieldBuild
        {
            public const int MinRectSize = 5;
            public const int MinDividableRectSize = 2 * MinRectSize;
        }

        public enum MapChipType
        {
            Floor,
            Wall
        }
    }
}

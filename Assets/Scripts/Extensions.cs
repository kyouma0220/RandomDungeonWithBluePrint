using System;
using System.Collections.Generic;
using UnityEngine;
// ReSharper disable MemberCanBePrivate.Global

namespace RandomDungeonWithBluePrint
{
    public static class RectIntExtensions
    {
        public static IEnumerable<Vector2Int> LineTo(this Vector2Int self, Vector2Int other)
        {
            return LineByBresenham(self.x, self.y, other.x, other.y);
        }

        public static IEnumerable<Vector2Int> LineByBresenham(int x0, int y0, int x1, int y1)
        {
            var dx = Mathf.Abs(x1 - x0);
            var dy = Mathf.Abs(y1 - y0);

            var sx = x0 < x1 ? 1 : -1;
            var sy = y0 < y1 ? 1 : -1;
            var err = dx - dy;

            var result = new List<Vector2Int>();
            while (true)
            {
                result.Add(new Vector2Int(x0, y0));
                if (x0 == x1 && y0 == y1)
                {
                    break;
                }

                var e2 = err * 2;
                if (e2 > -dy)
                {
                    err -= dy;
                    x0 += sx;
                }

                if (e2 < dx)
                {
                    err += dx;
                    y0 += sy;
                }
            }

            return result;
        }

        public static RectInt AddPadding(this RectInt self, int padding)
        {
            if (!self.AbleToPadding(padding))
            {
                throw new ArgumentOutOfRangeException();
            }

            var rect = self;
            rect.x += padding;
            rect.y += padding;
            rect.width -= padding * 2;
            rect.height -= padding * 2;
            return rect;
        }

        public static Vector2Int GetEdge(this RectInt self, int direction, Vector2Int initial = default)
        {
            switch (direction)
            {
                case Constants.Direction.Left:
                    return new Vector2Int(self.xMin, initial.y);
                case Constants.Direction.Down:
                    return new Vector2Int(initial.x, self.yMax);
                case Constants.Direction.Up:
                    return new Vector2Int(initial.x, self.yMin);
                case Constants.Direction.Right:
                    return new Vector2Int(self.xMax, initial.y);
                default:
                    return new Vector2Int(self.xMin, self.yMin);
            }
        }

        public static bool AbleToPadding(this RectInt self, int margin)
        {
            return 0 <= margin && margin * 2 < self.width && margin * 2 < self.height;
        }

        public static RectInt SafeAreaOfInclusion(this RectInt self, RectInt rect)
        {
            if (self.width < rect.width || self.height < rect.height)
            {
                throw new ArgumentOutOfRangeException();
            }

            return new RectInt(self.x, self.y, self.width - rect.width, self.height - rect.height);
        }

        public static int AdjoiningWithDirection(this RectInt self, RectInt other)
        {
            if (self.xMin < other.xMax && self.xMax > other.xMin)
            {
                if (self.yMax == other.yMin) { return Constants.Direction.Down; }
                if (self.yMin == other.yMax) { return Constants.Direction.Up; }
            }

            if (self.yMin < other.yMax && self.yMax > other.yMin)
            {
                if (self.xMax == other.xMin) { return Constants.Direction.Right; }
                if (self.xMin == other.xMax) { return Constants.Direction.Left; }
            }

            return Constants.Direction.Error;
        }
    }
}

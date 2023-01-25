using System;
using UnityEngine;

namespace Utility.Extensions{
    public enum Direction{
        Up,
        Left,
        Right,
        Down,
        Null
    }
    
    public static class DirectionExtension{
        public static Vector2Int ToVector2Int(this Direction dir){
            return dir switch{
                Direction.Up => Vector2Int.up,
                Direction.Left => Vector2Int.left,
                Direction.Right => Vector2Int.right,
                Direction.Down => Vector2Int.down,
                Direction.Null => Vector2Int.zero,
                _ => throw new ArgumentOutOfRangeException(nameof(dir), dir, null)
            };
        }

        public static Direction Opposite(this Direction dir){
            return dir switch{
                Direction.Up => Direction.Down,
                Direction.Left => Direction.Right,
                Direction.Right => Direction.Left,
                Direction.Down => Direction.Up,
                Direction.Null => Direction.Null,
                _ => throw new ArgumentOutOfRangeException(nameof(dir), dir, null)
            };
        }

        public static bool IsClockwiseLess(this Direction dir, Direction other){
            return dir.ToVector2Int().IsClockwiseLess(other.ToVector2Int());
        }
    }
    public static class Vector2IntExtension{

        public static Vector2 ToVector2(this Vector2Int vec) => new Vector2(vec.x, vec.y);
        
        public static Vector2 Aligned(this Vector2Int vec){
            return vec.ToVector2().Aligned();
        }

        public static Direction AlignedDirection(this Vector2Int vec){
            return vec.ToVector2().AlignedDirection();
        }

        public static Vector2Int Rotate90DegClockwise(this Vector2Int vec){
            return new(vec.y, -vec.x);
        }
        
        public static Vector2Int Rotate90DegAntiClockwise(this Vector2Int vec){
            return new(-vec.y, vec.x);
        }

        public static Vector2Int Rotate180Deg(this Vector2Int vec){
            return vec * (-1);
        }

        /// <summary>
        /// if on a clock, this vec pointing to a earlier time than the other one
        /// </summary>
        /// <param name="vec"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        public static bool IsClockwiseLess(this Vector2Int vec, Vector2Int other){
            if (vec.magnitude == 0 || other.magnitude == 0) return false;
            var z = vec.x * other.y - other.x * vec.y;
            return z < 0;
        }

        public static Vector3Int ToVector3Int(this Vector2Int vec){
            return new Vector3Int(vec.x, vec.y, 0);
        }
    }
}
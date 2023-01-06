using MVC;
using UnityEngine;

namespace Models.Items{
    public class Item: Model{
        public Vector2Int[] TakeUpRange;
        public Vector2Int BackpackPosition;
        public string IconPath;
        public Vector2Int RotateDirection = Vector2Int.up;
        
    }
}
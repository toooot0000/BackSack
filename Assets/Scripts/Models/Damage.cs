using Models.TileObjects;
using UnityEngine;

namespace Models{
    public struct Damage{
        public readonly int Amount;
        public readonly ElementType Type;
        public readonly TileObject Source;
        public readonly Vector2Int[] Coverage;
    }
}
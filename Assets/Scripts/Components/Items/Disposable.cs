using UnityEngine;

namespace Components.Items{
    public abstract class DisposableModel: ItemModel{
        public int Number = 0;
        public abstract Vector2Int[] Range{ get; }
    }
}
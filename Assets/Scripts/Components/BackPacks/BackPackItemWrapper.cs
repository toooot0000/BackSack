using System;
using System.Collections.Generic;
using Components.Effects;
using Components.Items;
using UnityEngine;
using Utility.Extensions;

namespace Components.BackPacks{
    public class BackPackItemWrapper{
        public readonly ItemModel Item;
        public readonly Direction PlaceDirection;
        public readonly Vector2Int PlacePosition;
        public BackPackItemWrapper(ItemModel item, Direction direction, Vector2Int position){
            Item = item;
            PlaceDirection = direction;
            PlacePosition = position;
        }
    }
}
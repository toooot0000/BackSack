using System;
using System.Collections.Generic;
using Components.Effects;
using Components.Items;
using UnityEngine;
using Utility.Extensions;

namespace Components.BackPacks{
    public class BackPackItemWrapper{
        public readonly ItemModel Item;
        public Direction PlaceDirection;
        public Vector2Int PlacePosition;
        public BackPackItemWrapper(ItemModel item, Direction direction, Vector2Int position){
            Item = item;
            PlaceDirection = direction;
            PlacePosition = position;
        }

        public override string ToString(){
            return
                $"Item: [{Item.ToString()}], Direction: [{PlaceDirection.GetDescription()}], Position: [{PlacePosition.ToString()}]";
        }
    }
}
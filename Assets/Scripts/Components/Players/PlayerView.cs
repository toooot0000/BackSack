using System;
using Components.Attacks;
using Components.TileObjects.BattleObjects;
using UnityEngine;

namespace Components.Players{

    public class PlayerView: BattleObjectView{

        public override void Die(){
            Debug.Log("Player died!");
        }
    }
}
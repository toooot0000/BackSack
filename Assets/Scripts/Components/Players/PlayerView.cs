using Components.Attacks;
using Components.TileObjects.BattleObjects;
using UnityEngine;

namespace Components.Players{

    public class PlayerView: BattleObjectView{
        public override void Attack(IAttack attack){
            Debug.Log("Player Attack!");
        }
    }
}
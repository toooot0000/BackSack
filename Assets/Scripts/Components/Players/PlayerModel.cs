using System.Collections.Generic;
using Components.TileObjects.BattleObjects;
using MVC;
using UnityEngine;
using Utility;
using Utility.Loader.Attributes;

namespace Components.Players{
    public class PlayerModel: Model{
        public Vector2Int CurrentStagePosition;
        [FromConfig("player_init_hp")]
        public int HealthLimit;
        [FromConfig("player_init_hp")]
        public int HealthPoint;
        public int ShieldPoint = 0;
        public int DefendPoint = 0;
        public int Weight = 1;

        public PlayerModel(){
            this.SetUp(typeof(PlayerModel));
        }
    }
}
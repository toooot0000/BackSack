using Components.Enemies.Intentions.Instances;
using Components.Players;
using UnityEngine;
using Utility.Extensions;

namespace Components.Enemies.Intentions{
    public class ActionPattern{
        public IEnemyIntention GetNextIntention(IIntentionContext context){
            // TODO
            return new MoveIntention(){
                Displacement = GetDirectionToPlayer(context.PlayerModel, context.EnemyModel)
            };
        }

        private Vector2Int GetDirectionToPlayer(PlayerModel playerModel, EnemyModel enemyModel){
            var t = playerModel.CurrentStagePosition - enemyModel.CurrentStagePosition;
            return t.Aligned();
        }
    }
}
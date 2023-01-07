using Components.Enemies.Intentions.Instances;
using Components.Players;
using UnityEngine;
using Utility.Extensions;

namespace Components.Enemies.Intentions{
    public class ActionPattern{
        public IEnemyIntention GetNextIntention(IIntentionContext context){
            // TODO
            return new MoveIntention(){
                Displacement = GetDirectionToPlayer(context.Player, context.Enemy)
            };
        }

        private Vector2Int GetDirectionToPlayer(Player player, Enemy enemy){
            var t = player.CurrentStagePosition - enemy.CurrentStagePosition;
            return t.Aligned();
        }
    }
}
using System.Collections.Generic;
using Components.Effects;
using Components.SelectMaps;
using UnityEngine;

namespace Components.Enemies.Intentions.Instances{
    public class AttackIntention : IEnemyIntention{
        public IEnumerable<Vector2Int> TargetPositionRange{ get; }
        public AttackIntention(IEnumerable<Vector2Int> targetPositionRange){
            TargetPositionRange = targetPositionRange;
        }

        public IEffect DoAction(){
            return null; //TODO
        }

        public void Label(SelectMap map){
            throw new System.NotImplementedException();
        }
    }
}
using Components.Effects;
using Components.SelectMaps;
using UnityEngine;
using Utility.Extensions;

namespace Components.Enemies.Intentions.Instances{
    public class MoveIntention: IEnemyIntention{
        public Direction Direction;
        private readonly Enemy _enemy;
        public MoveIntention(Enemy enemy){
            _enemy = enemy;
        }
        public IEffect DoAction(){
            return _enemy.Move(Direction);
        }

        public void Label(SelectMap map){
            map.AddNewTile(new SelectMapTileOptions(
                Direction.ToVector2Int() + _enemy.GetStagePosition(), 
                Color.cyan,
                SelectMapIcon.Move
            ));
        }
    }
}
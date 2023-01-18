using Components.Effects;
using Components.SelectMaps;
using UnityEngine;

namespace Components.Enemies.Intentions.Instances{
    public class MoveIntention: IEnemyIntention{
        public Vector2Int Direction;
        private readonly Enemy _enemy;
        public MoveIntention(Enemy enemy){
            _enemy = enemy;
        }
        public IEffect DoAction(){
            return _enemy.Move(Direction);
        }

        public void Label(SelectMap map){
            map.AddNewTile(new SelectMapTileOptions(
                Direction + _enemy.GetStagePosition(), 
                Color.cyan,
                SelectMapIcon.Move
            ));
        }
    }
}
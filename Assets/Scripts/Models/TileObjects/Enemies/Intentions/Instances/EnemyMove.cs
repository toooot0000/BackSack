using Models.EffectInfo;
using UnityEngine;

namespace Models.TileObjects.Enemies.Intentions.Instances{
    public class EnemyMove: IEnemyIntention{
        public Vector2Int Displacement;
        public IEffect Execute(Enemy enemy){
            throw new System.NotImplementedException();
        }
    }
}
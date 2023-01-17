using Components.Enemies.Intentions.Instances;
using Components.Players;
using Components.TileObjects;
using UnityEngine;
using Utility.Extensions;

namespace Components.Enemies.Intentions{

    public interface IActionPattern{
        IEnemyIntention GetIntention(Enemy enemy, IIntentionContext context);
    }
    
    public class ActionPattern: MonoBehaviour, IActionPattern{
        public virtual IEnemyIntention GetIntention(Enemy enemy, IIntentionContext context){
            return new MoveIntention(enemy){
                Direction = GetTileObjectDistance(context.Player, enemy).ToDirection()
            };
        }

        /// <summary>
        /// </summary>
        /// <param name="o1">TileObject1</param>
        /// <param name="o2">TileObject2</param>
        /// <returns>o1 -> o2</returns>
        protected static Vector2Int GetTileObjectDistance(ITileObject o1, ITileObject o2){
            return o2.GetStagePosition() - o1.GetStagePosition();
        }
        
    }
}
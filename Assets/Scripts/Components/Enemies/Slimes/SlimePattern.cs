using Components.Enemies.Intentions;
using Components.Enemies.Intentions.Instances;
using Utility.Extensions;

namespace Components.Enemies.Slimes{
    public class SlimePattern: ActionPattern{
        public override IEnemyIntention GetIntention(Enemy enemy, IIntentionContext context){
            var dis = GetTileObjectDistance(enemy, context.Player);
            if (dis.magnitude <= 1){
                return new Attack(enemy){
                    Direction = dis.ToDirection(),
                    AttackAnimator = enemy.ExtendedView as SlimeView 
                };
            }
            return new MoveIntention(enemy){
                Direction = dis.ToDirection()
            };
        }
    }
}
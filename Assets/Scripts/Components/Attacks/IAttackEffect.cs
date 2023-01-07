using Components.Effects;
using UnityEngine;

namespace Components.Attacks{

    public interface IAttack : IEffect{
        IAttacker Attacker{ get; }
        Vector2Int TargetRange{ get; }
        IAttackEffect[] Effects{ get; }
    }

    public interface IAttackEffect : IEffect{ }
    
    
    public class Attack : IAttack{
        public Attack(IAttacker attacker, Vector2Int targetRange, IAttackEffect[] effects){
            Attacker = attacker;
            TargetRange = targetRange;
            Effects = effects;
        }
        public IAttacker Attacker{ get; }
        public Vector2Int TargetRange{ get; }
        public IAttackEffect[] Effects{ get; }
    }
}
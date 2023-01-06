using UnityEngine;

namespace Models.Attacks{
    public class AttackAttempt : IAttackAttempt{
        public IAttacker Attacker{ get; set; }
        public Vector2Int TargetRange{ get; set; }
        public IAttackEffect[] Effects{ get; set; }
    }
}
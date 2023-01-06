using UnityEngine;

namespace Models.Attacks{
    public interface IAttackAttempt{
        IAttacker Attacker{ get; }
        Vector2Int TargetRange{ get; }
        IAttackEffect[] Effects{ get; }
    }
}
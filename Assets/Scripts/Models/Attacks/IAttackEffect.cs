using Models.Damages;
using Models.EffectInfo;
using Models.Ground;
using UnityEngine;

namespace Models.Attacks{
    
    public interface IAttackEffect: IEffect{ }

    public interface IForceMovement: IAttackEffect{ 
        int Force{ get; }
        Vector2Int Direction{ get; }
    }

    public interface ICreateGroundEffect : IAttackEffect{
        GroundType GroundToCreate{ get; }
    }
}
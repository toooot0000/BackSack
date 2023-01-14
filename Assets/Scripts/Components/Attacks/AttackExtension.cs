using System.Collections;
using System.Collections.Generic;
using Components.Effects;
using Components.Grounds.Effects;
using Components.TileObjects.Effects;
using Utility.Extensions;

namespace Components.Attacks{
    public static class AttackExtension{
        private class Wrapper{
            private readonly IAttack _attack;
            public Wrapper(IAttack attack){
                _attack = attack;
            }
            private IEnumerator SetCoEffResult(CoroutineEffect coEff){
                var anim = _attack.Attacker.GetAttackAnimator(_attack);
                yield return anim.Play(_attack);
                coEff.Result = anim.Result;
            }

            public CoroutineEffect Make(){
                return new CoroutineEffect(SetCoEffResult);
            }
        }
        public static CoroutineEffect ToCoroutineEffect(this IAttack attack){
            return new Wrapper(attack).Make();
        }
        
        public static IEffect PassEffects(this IAttack attack){
            var eff = attack.EffectTemplate.ToEffect();
            var forceMovement = eff as IForceMovement;
            var results = new List<IEffect>();
            foreach (var effectConsumer in attack.Targets){
                if(forceMovement != null){
                    if (forceMovement.Pulling){
                        forceMovement.Direction =
                            (attack.Attacker.GetStagePosition() - effectConsumer.GetStagePosition()).ToDirection();
                    }
                    else{
                        forceMovement.Direction =
                            (effectConsumer.GetStagePosition() - attack.Attacker.GetStagePosition()).ToDirection();
                    }
                }
                var side = effectConsumer.Consume(eff);
                if(side != null) results.Add(side);
            }
            if (eff is IGroundEffect groundEffect){
                foreach (var stagePos in attack.TargetPositions){
                    var side = attack.Attacker.GetStage().Consume(groundEffect, stagePos);
                    if(side != null) results.Add(side);
                }
            }
            return IEffect.MakeSideEffect(results);
        }
    }
}
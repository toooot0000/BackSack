using System.Collections;
using System.Collections.Generic;
using Components.Attacks;
using Components.Buffs;
using Components.Buffs.Effects;
using Components.Damages;
using Components.Effects;
using Components.Grounds.Effects;
using Components.TileObjects.Effects;
using Components.TileObjects.ForceMovables;
using Utility.Extensions;

namespace Components.TileObjects.BattleObjects{
    public abstract class BattleObject: ForceMovable, IBattleObject {
        public abstract int HealthLimit{ get; set; }
        public abstract int HealthPoint{ get; set; }
        public abstract int ShieldPoint{ get; set; }
        public abstract int DefendPoint{ get; set; }
        
        public virtual IEffect Consume(IDamage effect){
            HealthPoint -= effect.Point;
            m_GetView().TakeDamage(effect);
            if (HealthPoint <= 0){
                return Die();
            }
            return null;
        }


        public virtual IEffect Die(){
            return new CoroutineEffect(DieCoroutine);
        }

        private IEnumerator DieCoroutine(CoroutineEffect effect){
            yield return m_GetView().Die();
            Destroy(this);
            effect.Result = null;
        }

        public virtual IEffect Consume(IBuffEffect buffEffect){
            buffEffect.Apply();
            if (buffEffect.ChangeNumber > 0){
                m_GetView().AddBuff(buffEffect.GetDisplayName());
            }
            return null;
        }

        private readonly List<IEffect> _results = new();

        public override IEffect Consume(IEffect effect){
            CallConsumer<IEffect>(_results, effect, base.Consume);
            CallConsumer<IBuffEffect>(_results, effect, this);
            CallConsumer<IDamage>(_results, effect, this);
            return MakeSideEffect(_results);
        }

        public List<Buff> Buffs{ get; set; } = new();
        
        private IBattleObjectView m_GetView() => View as IBattleObjectView;
    }
}
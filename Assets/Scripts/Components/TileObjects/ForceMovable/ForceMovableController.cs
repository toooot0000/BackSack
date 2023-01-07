using System;
using System.Collections.Generic;
using Components.Effects;
using Components.Stages;
using Components.TileObjects.Effects;
using MVC;
using UnityEngine;
using Utility.Extensions;

namespace Components.TileObjects.ForceMovable{
    public abstract class ForceMovableController : TileObjectController, IForceMovable{

        protected override void AfterSetModel(){
            m_GetView().SetPosition(stageController.StagePositionToWorldPosition(m_GetModel().CurrentStagePosition));
        }

        public bool CanForcedIntoPosition(Vector2Int stagePosition){
            return false; // TODO
        }

        public int GetForceDistance(Vector2Int force){
            var weight = m_GetModel().Weight;
            return Math.Clamp((int)Math.Floor(force.Aligned().magnitude) - weight, 0, 5);
        }

        public virtual IEffectResult Consume(IForceMovement effect){
            // TODO
            return new EffectResult(effect, this);
        }

        private List<IEffectResult> _results;

        public override IEffectResult[] Consume(IEffect effect){
            _results.Clear();
            if (effect is IForceMovement damageEffect) _results.Add(Consume(damageEffect));
            return _results.ToArray();
        }

        private IForceMovableModel m_GetModel() => GetModel() as IForceMovableModel;
        private ForceMovableView m_GetView() => GetView() as ForceMovableView;
    }
}
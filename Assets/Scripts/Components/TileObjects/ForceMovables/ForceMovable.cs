using System;
using System.Collections.Generic;
using Components.Effects;
using Components.TileObjects.Effects;
using UnityEngine;
using Utility.Extensions;

namespace Components.TileObjects.ForceMovables{
    public abstract class ForceMovable : TileObject, IForceMovable{

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

        private readonly List<IEffectResult> _results = new();

        public override IEffectResult[] Consume(IEffect effect){
            _results.Clear();
            _results.AddRange(base.Consume(effect));
            if (effect is IForceMovement forceMovement) _results.Add(Consume(forceMovement));
            return _results.ToArray();
        }

        private IForceMovableModel m_GetModel() => Model as IForceMovableModel;
        private ForceMovableView m_GetView() => view as ForceMovableView;
    }
}
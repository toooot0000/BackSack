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

        public virtual IEffect Consume(IForceMovement effect){
            // TODO
            return null;
        }

        private readonly List<IEffect> _results = new();

        public override IEffect Consume(IEffect effect){
            _results.Clear();
            var baseRet = base.Consume(effect);
            if(baseRet != null) _results.Add(base.Consume(effect));
            if (effect is IForceMovement forceMovement) _results.Add(Consume(forceMovement));
            if (_results.Count == 0) return null;
            if (_results.Count == 1) return _results[0];
            return new MultiEffect(_results.ToArray());
        }

        private IForceMovableModel m_GetModel() => Model as IForceMovableModel;
        private ForceMovableView m_GetView() => view as ForceMovableView;
    }
}
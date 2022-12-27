using System;
using Models.TileObjects;
using UnityEngine;

namespace Models.GroundEffects{
    public interface IGroundEffect{
        public IGroundEffect TakeEffect<TIn>(TIn effect) where TIn : IGroundEffect;

        public const string GroundEffectsTypePrefix = "Asbl-Models.GroundEffects";
        public static IGroundEffect MakeGroundEffect(string name){
            if (!name.StartsWith(GroundEffectsTypePrefix)) name = $"{GroundEffectsTypePrefix}name";
            var type = Type.GetType(name);
            if (type == null){
                Debug.LogError($"Type not found! Type Name: {name}");
                return null;
            }
            return Activator.CreateInstance(type) as IGroundEffect;
        }
    }

    public interface IGroundEffectOnTileObjectEnter{
        public void OnTileObjectEnter(TileObject tileObject);
    }

    public interface IGroundEffectOnTileExit{
        public void OnTileObjectExit(TileObject tileObject);
    }
}
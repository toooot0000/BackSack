using System;
using System.Collections.Generic;
using System.Linq;
using Models.Damageable;
using MVC;
using UnityEngine;
using Utility.Loader;

namespace Models.Buffs{
    public interface IBuffHolder{
        List<Buff> Buffs{ set; get; }
    }

    public static class BuffHolderExtension{
        public static void AddBuffLayer<TBuff>(this IBuffHolder holder, int layer) where TBuff : Buff, new(){
            var buff = holder.GetBuffOfType<TBuff>();
            if (buff == null){
                buff = Buff.MakeBuff<TBuff>(layer);
                holder.Buffs.Add(buff);
            } else{
                buff.AddLayer(layer);
            }
        }

        public static void RemoveBuffLayer<TBuff>(this IBuffHolder holder, int layer) where TBuff : Buff, new(){
            var buff = holder.GetBuffOfType<TBuff>();
            if (buff == null) return;
            buff.RemoveLayer(layer);
            if (buff.Layer == 0) holder.Buffs.Remove(buff);
        }
        
        public static IEnumerable<T> GetBuffOfTrigger<T>(this IBuffHolder holder) where T : IBuffTrigger{
            var allBuffs = holder.Buffs;
            if(allBuffs == null) yield break;
            foreach (var buff in allBuffs){
                if(buff is not T typed) continue;
                yield return typed;
            }
        }

        public static T GetBuffOfType<T>(this IBuffHolder buffHolder) where T : Buff{
            try{
                return (T)buffHolder.Buffs.First(b => b is T);
            } catch{
                return null;
            }
        }
        
        public static string BuffsToString(this IBuffHolder buffHolder){
            var ret = buffHolder.Buffs.Aggregate("", (current, buff) => $"{current}, {buff}");
            return $"[{ret}]";
        }
    }

    public abstract class Buff: Model{
        public int Layer;
        public string IconPath;
        public string DisplayName;

        public event ModelDelegate<Buff> OnBuffLayerRemoved;
        public event ModelDelegate<Buff> OnBuffLayerAdded;
        public event ModelDelegate<Buff> OnBuffCanceled;
        
        
        /// <summary>
        /// USE FACTORY FUNCTIONS
        /// </summary>
        protected Buff(){ }
        
        public void RemoveLayer(int layerNum){
            Layer = Math.Max(0, Layer - layerNum);
            if (Layer == 0){
                OnBuffCanceled?.Invoke( this);
            } else{
                OnBuffLayerRemoved?.Invoke( this);
            }
        }

        public void AddLayer(int layerNum){
            Layer += layerNum;
            OnBuffLayerAdded?.Invoke( this);
        }
        
        public override string ToString(){
            return $"Buff: [name={Name}], [layer={Layer.ToString()}]";
        }

        public string ToDetailString(){
            return $"Name: {DisplayName}\nLayer: {Layer.ToString()}\nDetail: {Desc}";
        }

        public Sprite IconSprite => Resources.Load<Sprite>(IconPath);

        
        #region Factories
        
        public static T MakeBuff<T>(int layer) where T : Buff, new(){
            var ret = new T();
            SetUp(ret);
            ret.Layer = layer;
            return ret;
        }
        
        public static Buff MakeBuffByClassName(string className, int layer = 0){
            const string buffInstancePath = "Models.Buffs.BuffInstances";
            var ret = (Buff)Activator.CreateInstance(Type.GetType($"{buffInstancePath}.{className}", true));
            SetUp(ret);
            ret.Layer = layer;
            return ret;
        }
        
        #endregion
        
        
        #region Set up from the buff name

        private static Dictionary<string, int> _nameToId = null;
        public static Dictionary<string, int> NameToId{
            get{
                if (_nameToId == null){
                    MakeNameToIdDict();                    
                }
                return _nameToId;
            }
        }

        private static void MakeNameToIdDict(){
            _nameToId = new();
            var table = ConfigLoader.GetTable("Configs/buffs");
            foreach (var pair in table){
                _nameToId[(pair.Value["name"] as string)!] = pair.Key;
            }
        }
        
        
        protected abstract string GetBuffName();
        private static void SetUp(Buff buff){
            buff.ID = NameToId[buff.GetBuffName()];
            buff.StartFieldSetting("buffs");
            buff.Name = buff.GetField<string>("name");
            buff.Desc = buff.GetField<string>("desc");
            buff.IconPath = buff.GetField<string>("icon");
            buff.DisplayName = buff.GetField<string>("display_name");
            buff.EndFieldSetting();
        }

        #endregion
    }
}
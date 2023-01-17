using System;
using System.Collections.Generic;
using MVC;
using UnityEngine;
using Utility.Loader;
using Utility.Loader.Attributes;

namespace Components.Buffs{
    
    [Table("buffs")]
    public abstract class Buff: SelfSetUpModel{
        public int Layer;
        [Key("icon")]
        public string IconPath;
        [Key("display_name")]
        public string DisplayName;

        public event ModelDelegate<Buff> OnBuffLayerRemoved;
        public event ModelDelegate<Buff> OnBuffLayerAdded;
        public event ModelDelegate<Buff> OnBuffCanceled;

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
            ret.Layer = layer;
            return ret;
        }

        public static Buff MakeBuff(Type buffType, int layer){
            var ret = Activator.CreateInstance(buffType);
            if (ret is not Buff buff) return default;
            buff.Layer = layer;
            return buff;
        }
        
        public static Buff MakeBuffByClassName(string className, int layer = 0){
            const string buffInstancePath = "Components.Buffs.Instances";
            var ret = (Buff)Activator.CreateInstance(Type.GetType($"{buffInstancePath}.{className}", true));
            ret.Layer = layer;
            return ret;
        }
        
        #endregion

        protected Buff(int id){
            ID = id;
            this.SetUp();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Components.Buffs.Triggers;

namespace Components.Buffs{
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

        public static void AddBuffLayer(this IBuffHolder holder, Type buffType, int layer){
            var buff = holder.GetBuffOfType(buffType);
            if (buff == null){
                buff = Buff.MakeBuff(buffType, layer);
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

        public static void RemoveBuffLayer(this IBuffHolder holder, Type buffType, int layer){
            var buff = holder.GetBuffOfType(buffType);
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

        public static Buff GetBuffOfType(this IBuffHolder holder, Type type){
            return holder.Buffs.FirstOrDefault(b => b.GetType() == type);
        }
        
        public static string BuffsToString(this IBuffHolder buffHolder){
            var ret = buffHolder.Buffs.Aggregate("", (current, buff) => $"{current}, {buff}");
            return $"[{ret}]";
        }
    }
}
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Utility.Extensions{
    public static class ListExtension{
        public static T FirstNotActiveOrNew<T>(this List<T> list, Func<T> factory) where T : MonoBehaviour{
            var i = 0;
            for (; i < list.Count; i++){
                if(!list[i].gameObject.activeSelf) break;
            }
            if (i >= list.Count){
                var newBlock = factory();
                list.Add(newBlock);
            }
            else {
                list[i].gameObject.SetActive(true);
            }
            return list[i];
        }
        
        
        public static T FirstDisabledOrNew<T>(this List<T> list, Func<T> factory) where T : MonoBehaviour{
            var i = 0;
            for (; i < list.Count; i++){
                if(!list[i].isActiveAndEnabled) break;
            }
            if (i >= list.Count){
                var newBlock = factory();
                list.Add(newBlock);
            }
            else {
                list[i].gameObject.SetActive(true);
                list[i].enabled = true;
            }
            return list[i];
        }
        
        public static GameObject FirstNotActiveOrNewObject(this List<GameObject> list, Func<GameObject> factory){
            var i = 0;
            for (; i < list.Count; i++){
                if(!list[i].activeSelf) break;
            }
            if (i >= list.Count){
                var newBlock = factory();
                list.Add(newBlock);
            }
            else {
                list[i].gameObject.SetActive(true);
            }
            return list[i];
        }

        public static void AddIfNotNull<T>(this List<T> list, T nullable){
            if(nullable != null) list.Add(nullable);
        }
    }
}
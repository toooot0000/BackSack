using System.Collections.Generic;
using Components.Gizmos.ObjectToasts;
using UnityEngine;
using Utility.Extensions;

namespace Components.Buffs.BuffNames{
    public class BuffNameDisplay: MonoBehaviour{
        public ObjectToaster toaster;
        public void AddBuffNameDisplay(string buffName, bool add, Transform parent){
            toaster.AddToast(add ? buffName : $"<s>{buffName}</s>", parent);
        }
    }
}
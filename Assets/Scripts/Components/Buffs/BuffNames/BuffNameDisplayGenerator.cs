using System.Collections.Generic;
using UnityEngine;
using Utility.Extensions;

namespace Components.Buffs.BuffNames{
    public class BuffNameDisplayGenerator: MonoBehaviour{
        public GameObject buffNameDisplayPrefab;
        private readonly List<BuffNameDisplay> _buffNameDisplays = new();
        
        public void AddBuffNameDisplay(string buffName, bool add, Transform parent){
            var newNumber = _buffNameDisplays.FirstNotActiveOrNew(Make);
            var newTrans = newNumber.transform;
            newTrans.SetParent(parent, false);
            newNumber.ShowBuffName(buffName, add);
        }

        private BuffNameDisplay Make(){
            return Instantiate(buffNameDisplayPrefab, Vector3.zero, Quaternion.identity).GetComponent<BuffNameDisplay>();
        }
        
    }
}
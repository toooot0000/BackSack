using System.Collections.Generic;
using UnityEngine;
using Utility.Extensions;

namespace Components.Damages.DamageNumbers{
    public class DamageNumberGenerator: MonoBehaviour{
        public GameObject damageNumberPrefab;
        private readonly List<DamageNumber> _damageNumbers = new();

        public void AddDamageNumber(Damage number, Transform parent){
            var newNumber = _damageNumbers.FirstNotActiveOrNew(Make);
            var newTrans = newNumber.transform;
            newTrans.SetParent(parent, false);
            newNumber.Value = number;
        }

        private DamageNumber Make(){
            return Instantiate(damageNumberPrefab, Vector3.zero, Quaternion.identity).GetComponent<DamageNumber>();
        }
    }
}
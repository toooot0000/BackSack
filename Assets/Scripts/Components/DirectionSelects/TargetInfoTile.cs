using System;
using TMPro;
using UnityEngine;

namespace Components.DirectionSelects{
    [RequireComponent(typeof(SpriteMask))]
    public class TargetInfoTile: MonoBehaviour{
        public TextMeshPro tmp;
        private SpriteMask _mask;

        private void Start(){
            _mask = GetComponent<SpriteMask>();
        }

        private void OnEnable(){
            if (_mask == null) return; 
            _mask.enabled = true;
        }

        private void OnDisable(){
            if (_mask == null) return; 
            _mask.enabled = false;
        }
    }
}
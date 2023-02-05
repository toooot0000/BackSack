using System;
using UnityEngine;
using Utility.Extensions;

namespace Components.BackPacks.UI.Panels.BackupAreas{
    public class Detector: MonoBehaviour{
        public event Action EnteredArea;
        public event Action InsideArea;
        public event Action ExitArea;
        
        public float range = 20;
        public BackupArea area;

        private bool _isInside = false;

        private void Update(){
            if ((area.transform as RectTransform).GetWorldRect().Contains(Input.mousePosition)){
                if (_isInside){
                    InsideArea?.Invoke();
                } else{
                    _isInside = true;
                    EnteredArea?.Invoke();
                }
            } else{
                if (_isInside){
                    _isInside = false;
                    ExitArea?.Invoke();
                }
            }
        }
    }
}
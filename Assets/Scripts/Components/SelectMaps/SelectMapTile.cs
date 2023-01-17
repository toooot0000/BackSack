using System;
using Components.Stages;
using UnityEngine;

namespace Components.SelectMaps{
    public class SelectMapTile: MonoBehaviour{
        public SpriteRenderer icon;
        public SpriteRenderer outline;
        public Collider cld;
        
        [HideInInspector]
        public Stage stage;

        private Action _clickAction = null;
        
        public void SetUp(SelectMapTileOptions options){
            _clickAction = options.OnClick;
            cld.enabled = _clickAction != null;
            icon.sprite = options.Icon;
            outline.color = options.Color;
            transform.position = stage.StagePositionToWorldPosition(options.StagePosition);
        }

        private void OnMouseUpAsButton(){
            _clickAction?.Invoke();
        }
    }
}
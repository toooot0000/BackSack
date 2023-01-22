using System;
using Components.Stages;
using UnityEngine;

namespace Components.SelectMaps{
    public class SelectMapTile: MonoBehaviour{
        public SpriteRenderer icon;
        public SpriteRenderer outline;
        public Collider2D cld;
        
        [HideInInspector]
        public Stage stage;
        
        [HideInInspector]
        public SelectMap map;

        private Action _clickAction = null;
        
        public void SetUp(SelectMapTileOptions options){
            _clickAction = options.OnClick;
            cld.enabled = _clickAction != null;
            icon.sprite = map.Sprites[options.Icon];
            outline.color = options.Color;
            icon.color = options.Color;
            transform.position = stage.StageToWorldPosition(options.StagePosition);
        }

        private void OnMouseUpAsButton(){
            _clickAction?.Invoke();
        }
    }
}
using System;
using System.Linq;
using Components.Items;
using Components.Players;
using Components.Stages;
using UnityEngine;
using Utility.Extensions;

namespace Components.DirectionSelects{
    public class DirectionSelectManager: MonoBehaviour{
        public TargetInfoTileManager tileManager;
        public SpriteRenderer bg;
        public Stage Stage => GameManager.Shared.stage;
        public Player Player => GameManager.Shared.player;
        public PointerDetector detector;

        private ItemModel _item;

        private void Start(){
            detector.DirectionChanged += OnDirectionChanged;
            detector.DirectionConfirmed += Select;
            enabled = false;
            OnDisable();
        }

        public void ActiveWithItem(ItemModel itemModel){
            _item = itemModel;
            enabled = true;
        }

        private void Select(Direction direction){
            GameManager.Shared.PlayerUseItemWithDirection(_item, direction);
            enabled = false;
        }

        private void OnDirectionChanged(Direction direction){
            if (_item == null) return;
            tileManager.DisableAllTiles();
            foreach (var position in _item.Range.Rotate(direction)){
                var stagePosition = position + Player.GetStagePosition();
                tileManager.AddTargetInfoTile(new TargetInfoTileOption(){
                    StagePosition = stagePosition
                });
            }
        }
        
        
        public void OnEnable(){
            bg.color = Color.black.WithAlpha(0.5f);
            tileManager.enabled = true;
            detector.enabled = true;
        }

        public void OnDisable(){
            bg.color = Color.black.Transparent();
            tileManager.enabled = false;
            detector.enabled = false;
        }
    }
}
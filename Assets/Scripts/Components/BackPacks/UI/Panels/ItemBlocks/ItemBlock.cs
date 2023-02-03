using System;
using System.Collections.Generic;
using Components.BackPacks.UI.Panels.ItemBlocks.ShapeBlocks;
using Components.Items;
using UnityEngine;
using UnityEngine.UI;
using Utility.Extensions;

namespace Components.BackPacks.UI.Panels.ItemBlocks{
    [RequireComponent(typeof(DragController))]
    public class ItemBlock: ShapeBlock{
        private BackPackItemWrapper _itemWrapper;
        public Image icon;
        
        [NonSerialized] 
        public BackPackPanel BackPackPanel;
        private DragController _dragController;
        
        [Header("Shadow Block Configs")]
        public GameObject shadowPrefab;
        public Transform shadowRoot;
        private ShadowBlock _shadow;
        [NonSerialized]
        public RectInt ItemBound;

        public PositionTween positionTween;

        public BackPackItemWrapper ItemWrapper{
            set{
                _itemWrapper = value;
                Reload(_itemWrapper.Item.TakeUpRange);
                UpdateIcon();
                UpdateTransform();
                UpdateItemBound();
            }
            get => _itemWrapper;
        }

        private void UpdateTransform(){
            transform.position = grid.StageToWorldPosition(ItemWrapper.PlacePosition);
            transform.rotation = Quaternion.Euler(0, 0, Vector2.Angle(Const.DefaultDirection.ToVector2Int(), ItemWrapper.PlaceDirection.ToVector2Int()));
        }

        private void UpdateItemBound(){
            foreach (var pos in ItemWrapper.Item.TakeUpRange.Rotate(ItemWrapper.PlaceDirection)){
                ItemBound.min = Vector2Int.Min(ItemBound.min, pos);
                ItemBound.max = Vector2Int.Max(ItemBound.max, pos);
            }
        }

        private void UpdateIcon(){
            icon.sprite = Resources.Load<Sprite>(_itemWrapper.Item.IconPath);
            var size = grid.GetCellSize();
            var minLen = Mathf.Min(size.x, size.y) - padding * 2 - 10;
            ((RectTransform)icon.transform).sizeDelta = new Vector2(minLen, minLen);
        }

        protected override Tile MakeNewTile(){
            var ret = Instantiate(tilePrefab, tileRoot).GetComponent<ItemTile>();
            ret.block = this;
            return ret;
        }

        protected override Bridge MakeNewBridge(){
            var ret = Instantiate(bridgePrefab, bridgeRoot).GetComponent<ItemBridge>();
            ret.block = this;
            return ret;
        }

        public void OnLongTouched(){
            Debug.Log("Block Long Touched!");
            
        }

        public void OnClicked(){
            Debug.Log("Block Clicked!");
            if (BackPackPanel.IsRearranging) return;
            BackPackPanel.OnBlockClicked(this);
        }

        public void OnPointerDown(){
            Debug.Log("Block pointer down!");
            if (!BackPackPanel.IsRearranging) return;
            _dragController.StartDrag();
            EnableShadow();
            positionTween.enabled = false;
        }

        public void OnPointUp(){
            Debug.Log("Block Pointer Up!");
            if (!BackPackPanel.IsRearranging) return;
            _dragController.EndDrag();
            DisableShadow();
            positionTween.Target = GetClosestPossiblePosition();
        }

        private void Start(){
            _dragController = GetComponent<DragController>();
        }

        private void EnableShadow(){
            if (_shadow == null){
                _shadow = Instantiate(shadowPrefab, shadowRoot).GetComponent<ShadowBlock>();
                _shadow.Master = this;
            }
            _shadow.enabled = true;
        }

        private void DisableShadow(){
            _shadow.enabled = false;
        }

        public Vector3 GetClosestPossiblePosition(){
            var closest = grid.GetClosestGrid(transform.position);
            var gridBound = grid.GridBound;
            gridBound.min -= ItemBound.min;
            gridBound.max -= ItemBound.max;
            closest.Clamp(gridBound);
            return grid.GridToWorldPosition(closest);
        }
    }
}
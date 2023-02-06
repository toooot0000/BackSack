using System;
using System.Collections.Generic;
using Components.BackPacks.UI.Panels.BackupAreas;
using Components.BackPacks.UI.Panels.ItemBlocks.ShapeBlocks;
using Components.Items;
using UnityEngine;
using UnityEngine.UI;
using Utility.Extensions;

namespace Components.BackPacks.UI.Panels.ItemBlocks{
    [RequireComponent(typeof(DragController))]
    public class ItemBlock : ShapeBlock{
        private BackPackItemWrapper _itemWrapper;
        public Image icon;

        [NonSerialized] 
        public BackPackPanel BackPackPanel;
        private DragController _dragController;

        /// <summary>
        /// PositionRect after rotation
        /// </summary>
        [NonSerialized] 
        public RectInt ItemRect;

        public PositionTween positionTween;
        public Detector areaDetector;
        public Detector clockwiseDetector;
        public Detector counterclockwiseDetector;

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
            transform.position = grid.GridToWorldPosition(ItemWrapper.PlacePosition);
            transform.rotation = Quaternion.Euler(0, 0,
                Vector2.Angle(Const.DefaultDirection.ToVector2Int(), ItemWrapper.PlaceDirection.ToVector2Int()));
        }

        private void UpdateItemBound(){
            ItemRect = new RectInt();
            foreach (var pos in ItemWrapper.Item.TakeUpRange.Rotate(ItemWrapper.PlaceDirection)){
                ItemRect.min = Vector2Int.Min(ItemRect.min, pos);
                ItemRect.max = Vector2Int.Max(ItemRect.max,  pos + Vector2Int.one);
            }
        }

        private void UpdateIcon(){
            icon.sprite = Resources.Load<Sprite>(_itemWrapper.Item.IconPath);
            icon.transform.localPosition = Vector3.zero;
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

        public void OnClicked(){
            if (BackPackPanel.IsRearranging) return;
            Debug.Log("Block Clicked!");
            BackPackPanel.OnBlockClicked(this);
        }

        public void OnPointerDown(){
            Debug.Log("Block pointer down!");
            if (!BackPackPanel.IsRearranging) return;
            _dragController.StartDrag();
            positionTween.enabled = false;
            areaDetector.enabled = true;
            clockwiseDetector.enabled = true;
            counterclockwiseDetector.enabled = true;
            
            if (IsInBackup){
                BackPackPanel.PickUpItemFromBackup(this);
            } else{
                BackPackPanel.PickUpItemFromGrid(this);
            }
        }

        public void OnPointUp(){
            Debug.Log("Block Pointer Up!");
            if (!BackPackPanel.IsRearranging) return;
            _dragController.EndDrag();
            areaDetector.enabled = false;
            clockwiseDetector.enabled = false;
            counterclockwiseDetector.enabled = false;
            
            if(IsInBackup){
                BackPackPanel.PutSelectedInBackup();
            } else{
                BackPackPanel.PutSelectedInGrid();
            }
            
        }

        private void Awake(){
            _dragController = GetComponent<DragController>();
            areaDetector.EnteredArea += OnEnterBackupAreaWhileSelected;
            areaDetector.ExitArea += OnExitBackupAreaWhileSelected;
            clockwiseDetector.EnteredArea += RotateClockwise;
            counterclockwiseDetector.EnteredArea += RotateCounterclockwise;
        }
        [NonSerialized]
        public bool IsInBackup = false;

        private void OnEnterBackupAreaWhileSelected(){
            IsInBackup = true;
            BackPackPanel.SelectedEnterBackupArea();
        }

        private void OnExitBackupAreaWhileSelected(){
            IsInBackup = false;
            BackPackPanel.SelectedExitBackupArea();
        }
        
        public void RotateClockwise(){
            ItemWrapper.PlaceDirection.RotateClockWise();
            var cur = transform.rotation.eulerAngles.z;
            transform.rotation = Quaternion.Euler(0, 0, cur - 90);
            UpdateItemBound();
            BackPackPanel.SelectedRotated();
        }

        public void RotateCounterclockwise(){ 
            ItemWrapper.PlaceDirection.RotateCounterclockwise();
            var cur = transform.rotation.eulerAngles.z;
            transform.rotation = Quaternion.Euler(0, 0, cur + 90);
            UpdateItemBound();
            BackPackPanel.SelectedRotated();
        }
    }
}
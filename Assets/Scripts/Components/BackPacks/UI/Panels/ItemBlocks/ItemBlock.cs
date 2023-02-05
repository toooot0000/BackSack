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
    [RequireComponent(typeof(Detector))]
    public class ItemBlock : ShapeBlock{
        private BackPackItemWrapper _itemWrapper;
        public Image icon;

        [NonSerialized] 
        public BackPackPanel BackPackPanel;
        private DragController _dragController;

        [Header("Shadow Block Configs")] 
        public GameObject shadowPrefab;
        public Transform shadowRoot;
        private ShadowBlock _shadow;
        
        /// <summary>
        /// PositionRect after rotation
        /// </summary>
        [NonSerialized] 
        public RectInt ItemRect;

        public PositionTween positionTween;
        public BackupArea backUpArea;
        private Detector _detector;

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
            transform.rotation = Quaternion.Euler(0, 0,
                Vector2.Angle(Const.DefaultDirection.ToVector2Int(), ItemWrapper.PlaceDirection.ToVector2Int()));
        }

        private void UpdateItemBound(){
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

        public void OnLongTouched(){
            Debug.Log("Block Long Touched!");

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
            EnableShadow();
            positionTween.enabled = false;
            _detector.enabled = true;
            if (_isInBackup){
                backUpArea.RemoveBlock(this);
                backUpArea.MakeEmptyPosition(this);
            } else{
                BackPackPanel.PickUpItemFromGrid(this);
            }
        }

        public void OnPointUp(){
            Debug.Log("Block Pointer Up!");
            if (!BackPackPanel.IsRearranging) return;
            _dragController.EndDrag();
            DisableShadow();
            _detector.enabled = false;

            
            if(_isInBackup){
                ItemWrapper.PlacePosition = Vector2Int.zero;
                BackPackPanel.PutBlockInBackUp(this);
            } else{
                positionTween.Target = GetTargetPosition();
                ItemWrapper.PlacePosition = grid.GridToStagePosition(GetClosesGridPosition());
                BackPackPanel.PutBlockInGrid(this);
            }
            
        }

        private void Start(){
            _dragController = GetComponent<DragController>();
            _detector = GetComponent<Detector>();
            _detector.area = backUpArea;
            _detector.EnteredArea += OnEnterBackupArea;
            _detector.ExitArea += OnExitBackupArea;

        }

        private void Update(){
            if (!_isInBackup) return;
            backUpArea.UpdateEmptyPosition(this);
        }

        private bool _isInBackup = false;

        private void OnEnterBackupArea(){
            _isInBackup = true;
            backUpArea.MakeEmptyPosition(this);
        }

        private void OnExitBackupArea(){
            _isInBackup = false;
            backUpArea.RemoveEmptyPosition();
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

        private Vector2Int GetClosesGridPosition(){
            var closest = grid.GetClosestGrid(transform.position);
            var gridBound = grid.GridBound;
            gridBound.min -= ItemRect.min;
            gridBound.max -= (ItemRect.max - Vector2Int.one);
            closest.Clamp(gridBound.min, gridBound.max - Vector2Int.one);
            return closest;
        }
        
        public Vector3 GetTargetPosition() => !_isInBackup
            ? grid.GridToWorldPosition(GetClosesGridPosition())
            : backUpArea.GetClosestPosition(this);

        public void RotateClockwise(){
            ItemWrapper.PlaceDirection.RotateClockWise();
            var cur = transform.rotation.eulerAngles.z;
            transform.rotation = Quaternion.Euler(0, 0, cur + 90);
        }

        public void RotateCounterclockwise(){ 
            ItemWrapper.PlaceDirection.RotateCounterclockwise();
            var cur = transform.rotation.eulerAngles.z;
            transform.rotation = Quaternion.Euler(0, 0, cur + 90);
        }
    }
}
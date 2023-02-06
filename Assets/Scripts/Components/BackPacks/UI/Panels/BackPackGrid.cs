using System;
using Components.BackPacks.UI.Panels.ItemBlocks;
using UnityEngine;
using UnityEngine.UI;
using Utility;

namespace Components.BackPacks.UI.Panels{
    public class BackPackGrid: MonoBehaviour{
        public Grid grid;
        public Bindable<int> gridWidth;
        public Bindable<int> gridHeight;
        public Image bg; 
        private Vector3? _oriPosition;
        private Vector2 _originSize;

        private void Awake(){
            _originSize = (transform as RectTransform)!.rect.size;
            gridWidth.Bind(i => { _isGridBoundDirty = true;});
            gridHeight.Bind(j => { _isGridBoundDirty = true;});
        }

        private void Start(){
            Resize();
        }

        public void Resize(){
            ResizeGrid();
            AlignGrid();
            ResizeBg();
        }

        private void ResizeGrid(){
            var size = new Vector2(_originSize.x / gridWidth, _originSize.y / gridHeight);
            var minLen = Mathf.Min(size.x, size.y);
            var rectTrans = (grid.transform as RectTransform)!;
            rectTrans.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, minLen * gridWidth);
            rectTrans.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, minLen * gridHeight);
            grid.cellSize = new Vector3(minLen, minLen);
        }

        private void AlignGrid(){
            var rectTrans = (grid.transform as RectTransform)!;
            rectTrans.pivot = Vector2.zero;
            var cellSize = grid.cellSize;
            _oriPosition ??= rectTrans.localPosition;
            var localPosition = (Vector2)_oriPosition.Value;
            localPosition.x -= cellSize.x * gridWidth / 2;
            localPosition.y -= cellSize.y * gridHeight / 2;
            rectTrans.localPosition = localPosition;
        }

        private void ResizeBg(){
            var rectTrans = (grid.transform as RectTransform)!;
            var bgTrans = (bg.transform as RectTransform)!;
            bgTrans.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, rectTrans.rect.width);
            bgTrans.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, rectTrans.rect.height);
            bg.pixelsPerUnitMultiplier = (float)gridWidth / rectTrans.rect.width * 100f; // 100 is the size of the tile sprite width/height
        }
        
        public Vector3 GridToWorldPosition(Vector2Int gridPosition){
            return grid.GetCellCenterWorld(new Vector3Int(gridPosition.x, gridPosition.y, 0));
        }

        public Vector3 GridToLocalPosition(Vector2Int gridPosition){
            return grid.GetCellCenterLocal(new Vector3Int(gridPosition.x, gridPosition.y, 0));
        }

        // public Vector2Int GridToStagePosition(Vector2Int gridPosition){
        //     return new Vector2Int(gridPosition.x + gridWidth / 2, gridPosition.y + gridHeight / 2);
        // }

        public Vector2 GetCellSize() => grid.cellSize;
        
        
        private RectInt _gridBound;
        private bool _isGridBoundDirty = true;
        private void UpdateGridBound(){
            _gridBound = new RectInt(new Vector2Int(0, 0),
                new Vector2Int(gridWidth, gridHeight));
        }

        private RectInt GridBound{
            get{
                if (!_isGridBoundDirty) return _gridBound;
                UpdateGridBound();
                _isGridBoundDirty = false;
                return _gridBound;
            }
        }

        public Vector2Int GetClosestGrid(ItemBlock block){
            var ret = (Vector2Int)grid.WorldToCell(block.transform.position);
            var gridBound = GridBound;
            gridBound.min -= block.ItemRect.min;
            gridBound.max -= (block.ItemRect.max);
            ret.Clamp(gridBound.min, gridBound.max);
            return ret;
        }

        public Vector3 GetClosestWorld(ItemBlock block){
            return GridToWorldPosition(GetClosestGrid(block));
        }
    }
}
using System;
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
        }

        private void Start(){
            Resize();
        }

        public void Resize(){
            var size = new Vector2(_originSize.x / gridWidth, _originSize.y / gridHeight);
            var minLen = Mathf.Min(size.x, size.y);
            var rectTrans = (grid.transform as RectTransform)!;
            rectTrans.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, minLen * gridWidth);
            rectTrans.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, minLen * gridHeight);
            grid.cellSize = new Vector3(minLen, minLen);
            _oriPosition ??= rectTrans.localPosition;
            var localPosition = _oriPosition.Value;
            if ((gridWidth & 1) == 1) localPosition.x -= minLen / 2;
            if ((gridHeight & 1) == 1) localPosition.y -= minLen / 2;
            rectTrans.localPosition = localPosition;
            
            // bg
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

        public Vector2Int StageToGridPosition(Vector2Int stagePosition){
            return new Vector2Int(stagePosition.x - gridWidth / 2, stagePosition.y - gridHeight / 2);
        }

        public Vector2Int GridToStagePosition(Vector2Int gridPosition){
            return new Vector2Int(gridPosition.x + gridWidth / 2, gridPosition.y + gridHeight / 2);
        }

        public Vector3 StageToWorldPosition(Vector2Int stagePosition){
            return GridToWorldPosition(StageToGridPosition(stagePosition));
        }

        public Vector3 StageToLocalPosition(Vector2Int stagePosition){
            return GridToLocalPosition(StageToGridPosition(stagePosition));
        }

        public Vector2 GetCellSize() => grid.cellSize;


        public Vector2Int GetClosestGrid(Vector3 worldPosition){
            var ret = (Vector2Int)grid.WorldToCell(worldPosition);
            ret.Clamp(new Vector2Int(- gridWidth/2, - gridHeight/2), new Vector2Int(gridWidth/2, gridHeight/2));
            return ret;
        }

        public Vector3 GetClosestWorld(Vector3 worldPosition){
            var cell = GetClosestGrid(worldPosition);
            return GridToWorldPosition(cell);
        }

        public RectInt GridBound =>
            new RectInt(new Vector2Int(-gridWidth / 2, -gridHeight / 2),
                new Vector2Int(gridWidth, gridHeight));
    }
}
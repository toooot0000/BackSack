using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using Utility.Extensions;

namespace Components.UI.BackPacks{
    public class BackPackGrid: MonoBehaviour{
        public Grid grid;
        public int gridWidth;
        public int gridHeight;

        private void Start(){
            ResizeAll();
        }

        public void ResizeAll(){
            var oriSize = (transform as RectTransform)!.rect.size;
            var size = new Vector2(oriSize.x / gridWidth, oriSize.y / gridHeight);
            var minLen = Mathf.Min(size.x, size.y);
            var rectTrans = (grid.transform as RectTransform)!;
            rectTrans.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, minLen * gridWidth);
            rectTrans.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, minLen * gridHeight);
            grid.cellSize = new Vector3(minLen, minLen);
            var localPosition = rectTrans.localPosition;
            if ((gridWidth & 1) == 1) localPosition.x -= minLen / 2;
            if ((gridHeight & 1) == 1) localPosition.y -= minLen / 2;
            grid.transform.localPosition = localPosition;
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

        public Vector3 StageToWorldPosition(Vector2Int stagePosition){
            return GridToWorldPosition(StageToGridPosition(stagePosition));
        }

        public Vector3 StageToLocalPosition(Vector2Int stagePosition){
            return GridToLocalPosition(StageToGridPosition(stagePosition));
        }

        public Vector2 GetCellSize() => grid.cellSize;
    }
}
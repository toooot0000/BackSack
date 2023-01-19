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
            var size = (transform as RectTransform)!.rect.size;
            grid.cellSize = new Vector3(size.x / gridWidth, size.y / gridHeight);
            var localPosition = grid.transform.localPosition;
            if ((gridWidth & 1) == 1) localPosition.x -= grid.cellSize.x / 2;
            if ((gridHeight & 1) == 1) localPosition.y -= grid.cellSize.y / 2;
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
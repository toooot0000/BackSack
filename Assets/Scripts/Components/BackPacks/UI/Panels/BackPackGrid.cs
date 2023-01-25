using UnityEngine;
using Utility;

namespace Components.BackPacks.UI.Panels{
    public class BackPackGrid: MonoBehaviour{
        public Grid grid;
        public Bindable<int> gridWidth;
        public Bindable<int> gridHeight;
        private Vector3? _oriPosition;

        private void Start(){
            Resize();
        }

        public void Resize(){
            var oriSize = (transform as RectTransform)!.rect.size;
            var size = new Vector2(oriSize.x / gridWidth, oriSize.y / gridHeight);
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
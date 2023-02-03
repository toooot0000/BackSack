using System.Collections.Generic;
using UnityEngine;
using Utility.Extensions;

namespace Components.BackPacks.UI.Panels.ItemBlocks.ShapeBlocks{
    public class ShapeBlock: MonoBehaviour{
        [HideInInspector]
        public BackPackGrid grid;
        public float padding = 10f;
        public GameObject tilePrefab;
        public GameObject bridgePrefab;
        private readonly List<Tile> _tiles = new();
        public Transform tileRoot;
        public Transform bridgeRoot;

        protected virtual Tile MakeNewTile(){
            var ret = Instantiate(tilePrefab, tileRoot).GetComponent<Tile>();
            return ret;
        }

        protected virtual Bridge MakeNewBridge(){
            var ret = Instantiate(bridgePrefab, bridgeRoot).GetComponent<Bridge>();
            return ret;
        }


        public virtual void OnDisable(){
            foreach (var tile in _tiles){
                tile.gameObject.SetActive(false);
                if(tile.rightBridge != null) tile.rightBridge.gameObject.SetActive(false);
                if(tile.downBridge != null) tile.downBridge.gameObject.SetActive(false);
            }
        }

        public virtual void OnEnable(){
            foreach (var tile in _tiles){
                tile.gameObject.SetActive(true);
                if(tile.rightBridge != null) tile.rightBridge.gameObject.SetActive(true);
                if(tile.downBridge != null) tile.downBridge.gameObject.SetActive(true);
            }
        }

        public void Resize(){
            var cellSize = grid.GetCellSize();
            var tileSize = cellSize - new Vector2(padding * 2, padding * 2);
            foreach (var itemTile in _tiles){
                var tileTrans = (RectTransform)itemTile.transform;
                tileTrans.sizeDelta = tileSize;
                if (itemTile.rightBridge != null){
                    var trans = (RectTransform)itemTile.rightBridge.transform;
                    trans.sizeDelta = new Vector2(trans.sizeDelta.x, tileSize.y);
                }

                if (itemTile.downBridge != null){
                    var trans = (RectTransform)itemTile.downBridge.transform;
                    trans.sizeDelta = new Vector2(trans.sizeDelta.x, tileSize.y);
                }
            }
        }


        protected void Reload(Vector2Int[] takeUpRange){
            
            OnDisable();
            
            var cellSize = grid.GetCellSize();
            var positionSet = new HashSet<Vector2Int>(takeUpRange);
            var tileSize = cellSize - new Vector2(padding * 2, padding * 2);

            foreach (var position in takeUpRange){
                var tile = _tiles.FirstNotActiveOrNew(MakeNewTile);
                var tilePosition = new Vector3(position.x * cellSize.x, position.y * cellSize.y);
                var tileTransform = (RectTransform)tile.transform;
                tileTransform.localPosition = tilePosition;
                tileTransform.sizeDelta = tileSize;
                
                if (positionSet.Contains(position + Vector2Int.right)){
                    tile.rightBridge ??= MakeNewBridge();
                    tile.rightBridge.gameObject.SetActive(true);
                    var trans = (RectTransform)tile.rightBridge.transform;
                    trans.localPosition = tilePosition + new Vector3(cellSize.x / 2, 0, 0);
                    trans.sizeDelta = new Vector2(padding*2f + 13, tileSize.y);
                } else if(tile.rightBridge != null) 
                    tile.rightBridge.gameObject.SetActive(false);
                

                if (positionSet.Contains(position + Vector2Int.down)){
                    tile.downBridge ??= MakeNewBridge();
                    tile.downBridge.gameObject.SetActive(true);
                    var trans = (tile.downBridge.transform as RectTransform)!;
                    trans.localRotation = Quaternion.Euler(0, 0, 90);
                    trans.localPosition = tilePosition - new Vector3(0, cellSize.y/2, 0);
                    trans.sizeDelta = new Vector2(padding*2f + 13, tileSize.x);
                } else if(tile.downBridge != null) 
                    tile.downBridge.gameObject.SetActive(false);
            }
        }
    }
}
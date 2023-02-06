using System;
using System.Collections.Generic;
using UnityEngine;
using Utility;
using Utility.Extensions;

namespace Components.BackPacks.UI.Panels.ItemBlocks.ShapeBlocks{
    public class ShapeBlock: MonoBehaviour{
        [HideInInspector]
        public BackPackGrid grid;
        public float padding = 10f;
        public GameObject tilePrefab;
        public GameObject bridgePrefab;
        private readonly List<Tile> _tiles = new();
        public RectTransform tileRoot;
        public RectTransform bridgeRoot;

        private Vector2Int[] _takeUpRange;
        [NonSerialized]
        public RectInt PositionRect;

        private void UpdateSize(){
            var rectTrans = (RectTransform)transform;
            PositionRect = new RectInt();
            foreach (var pos in _takeUpRange){
                PositionRect.min = Vector2Int.Min(pos, PositionRect.min);
                PositionRect.max = Vector2Int.Max(pos + Vector2Int.one, PositionRect.max);
            }

            var cellSize = grid.GetCellSize();
            var sizeX = PositionRect.size.x * cellSize.x;
            var sizeY = PositionRect.size.y * cellSize.y;
            
            rectTrans.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, sizeX);
            rectTrans.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, sizeY);
            
            tileRoot.anchorMin = Vector2.zero;
            tileRoot.anchorMax = Vector2.one;
            tileRoot.sizeDelta = Vector2.zero;
            bridgeRoot.anchorMin = Vector2.zero;
            bridgeRoot.anchorMax = Vector2.one;
            bridgeRoot.sizeDelta = Vector2.zero;
        }

        protected void UpdatePivot(){
            var rectTrans = (RectTransform)transform;
            var pivotTile = (Vector2)_takeUpRange[0];
            pivotTile += new Vector2(0.5f, 0.5f) - PositionRect.min;
            var pivot =  new Vector2(pivotTile.x / PositionRect.size.x, pivotTile.y / PositionRect.size.y);
            rectTrans.pivot = pivot;
            tileRoot.pivot = pivot;
            bridgeRoot.pivot = pivot;
        }

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
            var tileSize = GetTileSize();
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
            UpdateSize();
        }

        public Vector2 GetTileSize() => grid.GetCellSize() - new Vector2(padding * 2, padding * 2);


        protected void Reload(Vector2Int[] takeUpRange){
            OnDisable();

            _takeUpRange = takeUpRange;
            
            UpdateSize();
            UpdatePivot();
            
            
            var cellSize = grid.GetCellSize();
            var positionSet = new HashSet<Vector2Int>(takeUpRange);
            var tileSize = GetTileSize();

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
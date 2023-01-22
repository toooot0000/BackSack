using System;
using System.Collections.Generic;
using Components.Items;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Utility.Extensions;

namespace Components.UI.BackPacks{
    public class ItemBlock: MonoBehaviour{
        private ItemModel _item;
        public Image icon;
        public BackPackGrid grid;
        public float padding = 10f;

        public GameObject tilePrefab;
        public GameObject bridgePrefab;

        private readonly List<ItemTile> _tiles = new();

        public Transform tileRoot;
        public Transform bridgeRoot;

        public BackPack backPack; 


        public ItemModel Item{
            set{
                _item = value;
                icon.sprite = Resources.Load<Sprite>(value.IconPath);
                var size = grid.GetCellSize();
                var minLen = Mathf.Min(size.x, size.y) - padding * 2 - 10;
                ((RectTransform)icon.transform).sizeDelta = new Vector2(minLen, minLen);
                Reload();
            }
            get => _item;
        }

        private ItemTile MakeNewTile(){
            var ret = Instantiate(tilePrefab, tileRoot).GetComponent<ItemTile>();
            ret.block = this;
            return ret;
        }

        private ItemBridge MakeNewBridge(){
            var ret = Instantiate(bridgePrefab, bridgeRoot).GetComponent<ItemBridge>();
            ret.block = this;
            return ret;
        }


        private void OnDisable(){
            foreach (var tile in _tiles){
                tile.gameObject.SetActive(false);
                if(tile.rightBridge != null) tile.rightBridge.gameObject.SetActive(false);
                if(tile.downBridge != null) tile.downBridge.gameObject.SetActive(false);
            }
        }

        public void OnEnable(){
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
        

        private void Reload(){
            
            OnDisable();
            
            var cellSize = grid.GetCellSize();
            var positionSet = new HashSet<Vector2Int>(_item.TakeUpRange);
            var tileSize = cellSize - new Vector2(padding * 2, padding * 2);

            foreach (var position in _item.TakeUpRange){
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
                    trans.sizeDelta = new Vector2(padding*2f + 15, tileSize.y);
                } else if(tile.rightBridge != null) 
                    tile.rightBridge.gameObject.SetActive(false);
                

                if (positionSet.Contains(position + Vector2Int.down)){
                    tile.downBridge ??= MakeNewBridge();
                    tile.downBridge.gameObject.SetActive(true);
                    var trans = (tile.downBridge.transform as RectTransform)!;
                    trans.rotation = Quaternion.Euler(0, 0, 90);
                    trans.localPosition = tilePosition - new Vector3(0, cellSize.y/2, 0);
                    trans.sizeDelta = new Vector2(padding*2f + 15, tileSize.x);
                } else if(tile.downBridge != null) 
                    tile.downBridge.gameObject.SetActive(false);
            }
        }

        public void OnLongTouched(){
            Debug.Log("Block Long Touched!");
            // backPack.OnBlockClicked(this);
        }

        public void OnClicked(){
            Debug.Log("Block Clicked!");
            backPack.OnBlockClicked(this);
        }
    }
}
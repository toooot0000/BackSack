using System;
using System.Collections.Generic;
using System.Linq;
using Components.Items;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Utility;
using Utility.Extensions;
using Direction = Utility.Extensions.Direction;

namespace Components.BackPacks{
    public class BackPack: MonoBehaviour{

        private BackPackItemWrapper[] _inventory;
        private readonly List<BackPackItemWrapper> _items = new();

        public event Action<BackPackItemWrapper> ItemAdded;
        public event Action<BackPackItemWrapper> ItemRemoved;
        public event Action<BackPackItemWrapper> ItemRotated;
        public event Action<BackPackItemWrapper> ItemPositionChanged;

        public Bindable<int> RowNum = 3;
        public Bindable<int> ColNum = 3;

        private void Awake(){
            _inventory = new BackPackItemWrapper[RowNum * ColNum];
        }

        private BackPackItemWrapper Get(int i, int j){
            return _inventory[i * ColNum + j];
        }

        private void Set(int i, int j,  BackPackItemWrapper item){
            _inventory[i * ColNum + j] = item;
        }

        public ItemModel GetItem(Vector2Int position){
            return Get(position.x, position.y).Item;
        }

        public IEnumerable<ItemModel> GetAllItem(){
            return _items.Select(s => s.Item);
        }

        public bool IsInsideBound(Vector2Int position){
            return position.x >= 0 && position.x < RowNum && position.y >= 0 && position.y < ColNum;
        }

        public bool IsOccupied(Vector2Int position){
            return Get(position.x, position.y) != null;
        }

        public bool CanPutIn(IEnumerable<Vector2Int> takeUpRange, Direction direction, Vector2Int position){
            foreach (var rotated in takeUpRange.Rotate(direction)){
                var cur = rotated + position;
                if (!IsInsideBound(cur) || IsOccupied(cur)) return false;
            }
            return true;
        }

        public BackPackItemWrapper AddItem(ItemModel item, Direction direction, Vector2Int position){
            if (!CanPutIn(item.TakeUpRange, direction, position)) return null;
            var ret = new BackPackItemWrapper(item, direction, position);
            _items.Add(ret);
            foreach (var pos in item.TakeUpRange.Rotate(direction)){
                var cur = pos + position;
                Set(cur.x, cur.y, ret);
            }
            ItemAdded?.Invoke(ret);
            return ret;
        }                                                                                                                                                                                                                                                                                

        public bool RemoveItem(ItemModel item){
            BackPackItemWrapper bpIt = null;
            foreach (var backpackItem in _items){
                if (backpackItem.Item == item){
                    bpIt = backpackItem;
                    break;
                }
            }
            if (bpIt == null) return false;
            return RemoveItem(bpIt);
        }

        public bool RemoveItem(BackPackItemWrapper item){
            if (!_items.Remove(item)) return false;
            foreach (var pos in item.Item.TakeUpRange.Rotate(item.PlaceDirection)){
                var cur = pos + item.PlacePosition;
                Set(cur.x, cur.y, null);
            }
            ItemRemoved?.Invoke(item);
            return true;
        }

        public bool RemoveItemAtPosition(Vector2Int position){
            var bpIt = Get(position.x, position.y);
            return bpIt != null && RemoveItem(bpIt);
        }

        public void Resize(int newRow, int newCol){
            var old = _inventory;
            _inventory = new BackPackItemWrapper[newRow * newCol];
            for (var i = 0; i < RowNum; i++){
                for (var j = 0; j < RowNum; j++){
                    _inventory[i * newCol + j] = old[i * ColNum + j];
                }
            }
            RowNum.Set(newRow);
            ColNum.Set(newCol);
        }

        public BackPackItemWrapper GetWrapper(ItemModel item) {
            return _items.FirstOrDefault(w => w.Item == item);
        }

        public void SetItemPosition(ItemModel item, Vector2Int position){
            
            // TODO
        }

        public void RotateItem(ItemModel item, Direction direction){
            // TODO
        }
    }
}
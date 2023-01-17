using System;
using System.Collections.Generic;
using Components.Stages;
using UnityEngine;
using Utility.Extensions;

namespace Components.SelectMaps{

    public class SelectMapTileOptions{
        public readonly Sprite Icon;
        public readonly Color Color;
        public readonly Action OnClick;
        public readonly Vector2Int StagePosition;

        public SelectMapTileOptions(
            Vector2Int stagePosition,
            Color color = default,
            Sprite icon = null,
            Action onClick = null
        ){
            StagePosition = stagePosition;
            Color = color;
            Icon = icon;
            OnClick = onClick;
        }
    }
    
    public class SelectMap: MonoBehaviour{
        public Stage stage;
        public GameObject tilePrefab;
        
        private readonly List<SelectMapTile> _tiles = new();
        private readonly Stack<List<SelectMapTileOptions>> _stack = new();

        public SelectMapTile AddNewTile(SelectMapTileOptions options){
            var newTile = _tiles.FirstNotActiveOrNew(Make);
            newTile.SetUp(options);
            if (_stack.Peek() == null){
                _stack.Push(new());
            }
            _stack.Peek().Add(options);
            return newTile;
        }

        private SelectMapTile Make(){
            var ret = Instantiate(tilePrefab, transform).GetComponent<SelectMapTile>();
            ret.stage = stage;
            return ret;
        }

        public void Stash(){
            if (_stack.Peek() == null || _stack.Peek().Count == 0) return;
            _stack.Push(new());
            foreach (var tile in _tiles){
                tile.gameObject.SetActive(false);
            }
        }

        public void Pop(){
            if (_stack.Count == 0) return;
            _stack.Pop();
            if (_stack.Peek() == null || _stack.Peek().Count <= 0) return;
            foreach (var tile in _tiles){
                tile.gameObject.SetActive(false);
            }
            var i = 0;
            foreach (var options in _stack.Peek()){
                if(i < _tiles.Count) _tiles.Add(Make());
                else _tiles[i].gameObject.SetActive(true);
                _tiles[i].SetUp(options);
                i++;
            }
        }
    }
}
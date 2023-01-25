using System;
using System.Collections.Generic;
using System.Linq;
using Components.Stages;
using UnityEngine;
using Utility.Extensions;

namespace Components.DirectionSelects{


    public class TargetInfoTileOption{
        public Vector2Int StagePosition;
        public string Message;
    }
    
    public class TargetInfoTileManager: MonoBehaviour{
        public GameObject targetInfoTile;
        public Stage stage;

        private readonly List<TargetInfoTile> _tiles = new();

        private TargetInfoTile MakeNew(){
            return Instantiate(targetInfoTile, transform).GetComponent<TargetInfoTile>();
        }

        public void AddTargetInfoTile(TargetInfoTileOption option){
            var newObj = _tiles.FirstDisabledOrNew(MakeNew);
            newObj.transform.position = stage.StageToWorldPosition(option.StagePosition);
            newObj.tmp.text = option.Message;
        }

        public void AddTargetInfoTiles(IEnumerable<TargetInfoTileOption> options){
            foreach (var option in options){
                AddTargetInfoTile(option);
            }
        }

        private void OnDisable(){
            DisableAllTiles();
        }

        public void DisableAllTiles(){
            foreach (var tile in _tiles){
                tile.enabled = false;
            }
        }
    }
}
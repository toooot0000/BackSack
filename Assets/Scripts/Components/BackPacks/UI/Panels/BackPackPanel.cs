using System;
using System.Collections.Generic;
using Components.DirectionSelects;
using Components.Items;
using Components.UI;
using Components.UI.Common;
using UnityEngine;
using Utility;
using Utility.Extensions;

namespace Components.BackPacks.UI.Panels{
    public class BackPackPanel: UIComponent{
        public EdgeHider hider;
        public GameObject blockPrefab;
        public BackPackGrid grid;
        public BackPack backPack;
        public DirectionSelectManager selectManager;

        private readonly List<ItemBlock> _blocks = new();


        public void Start(){
            grid.gridHeight = new Bindable<int>(backPack.RowNum);
            grid.gridWidth = new Bindable<int>(backPack.ColNum);
            grid.gridWidth.Bind((i) => Resize());
            grid.gridHeight.Bind((i)=> Resize());
            backPack.ItemAdded += AddBlock;
            backPack.ItemRemoved += RemoveBlock;
            Resize();
        }


        private ItemBlock MakeNew(){
            var ret = Instantiate(blockPrefab, grid.transform).GetComponent<ItemBlock>();
            ret.grid = grid;
            return ret;
        }

        private void AddBlock(BackPackItemWrapper item){
            AddBlock(item.Item, item.PlacePosition, item.PlaceDirection.ToVector2Int());
        }

        public void AddBlock(ItemModel item, Vector2Int stagePosition, Vector2Int? direction = null){
            if(direction == null) direction = Vector2Int.right;
            var block = _blocks.FirstNotActiveOrNew(MakeNew);
            block.Item = item;
            block.transform.position = grid.StageToWorldPosition(stagePosition);
            var angle = Vector2.Angle(direction.Value, Vector2.right);
            block.transform.rotation = Quaternion.Euler(0, 0, angle);
            block.backPackPanel = this;
        }

        private void RemoveBlock(BackPackItemWrapper item){
            foreach (var block in _blocks){
                if (!Equals(item, block.Item)) continue;
                block.enabled = false;
                block.gameObject.SetActive(false);
            }
        }

        private void Resize(){
            grid.Resize();
            foreach (var itemBlock in _blocks){
                itemBlock.Resize();
            }
        }

        public void OnBlockClicked(ItemBlock block){
            Debug.Log($"Use item: {block.Item}");
            // GameManager.Shared.StartSelectDirection(block.Item);
            selectManager.ActiveWithItem(block.Item);
        } 

        public override void Hide(){
            hider.Hide();
        }

        public override void Show(){
            hider.Show();
        }
    }
}
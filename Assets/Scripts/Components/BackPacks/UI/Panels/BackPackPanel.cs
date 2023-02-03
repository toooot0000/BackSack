using System;
using System.Collections.Generic;
using Components.BackPacks.UI.Panels.ItemBlocks;
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
        public PanelTween tween;

        private readonly List<ItemBlock> _blocks = new();
        
        [NonSerialized]
        public bool IsRearranging = false;


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
            var block = _blocks.FirstNotActiveOrNew(MakeNew);
            block.ItemWrapper = item;
            block.BackPackPanel = this;
        }

        // public void AddBlock(ItemModel item, Vector2Int stagePosition, Vector2Int? direction = null){
        //     if(direction == null) direction = Vector2Int.right;
        //     var block = _blocks.FirstNotActiveOrNew(MakeNew);
        //     block.Item = item;
        //     block.transform.position = grid.StageToWorldPosition(stagePosition);
        //     var angle = Vector2.Angle(direction.Value, Vector2.right);
        //     block.transform.rotation = Quaternion.Euler(0, 0, angle);
        //     block.BackPackPanel = this;
        // }

        private void RemoveBlock(BackPackItemWrapper item){
            foreach (var block in _blocks){
                if (item != block.ItemWrapper) continue;
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
            Debug.Log($"Use item: {block.ItemWrapper}");
            // GameManager.Shared.StartSelectDirection(block.Item);
            selectManager.ActiveWithItem(block.ItemWrapper.Item);
        } 

        public override void Hide(){
            hider.Hide();
        }

        public override void Show(){
            hider.Show();
        }

        public void StartRearrange(){
            tween.Reverse(false);
            tween.Play(() => IsRearranging = true);
        }

        public void CompleteRearrange(){
            IsRearranging = false;
            tween.Reverse(true);
            tween.Play();
        }
    }
}
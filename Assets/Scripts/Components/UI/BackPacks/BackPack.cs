using System.Collections.Generic;
using Components.Items;
using Components.UI.Common;
using UnityEngine;
using Utility.Extensions;

namespace Components.UI.BackPacks{
    public class BackPack: UIComponent{
        public EdgeHider hider;
        public GameObject blockPrefab;
        public BackPackGrid grid;

        private readonly List<ItemBlock> _blocks = new();


        public ItemBlock MakeNew(){
            var ret = Instantiate(blockPrefab, grid.transform).GetComponent<ItemBlock>();
            ret.grid = grid;
            return ret;
        }

        public void AddBlock(ItemModel item, Vector2Int stagePosition, Vector2Int? direction = null){
            if(direction == null) direction = Vector2Int.right;
            var block = _blocks.FirstNotActiveOrNew(MakeNew);
            block.Item = item;
            block.transform.position = grid.StageToWorldPosition(stagePosition);
            var angle = Vector2.Angle(direction.Value, Vector2.right);
            block.transform.rotation = Quaternion.Euler(0, 0, angle);
            block.backPack = this;
        }

        public void OnBlockClicked(ItemBlock block){
            Debug.Log($"Use item: {block.Item}");
            GameManager.Shared.StartSelectDirection(block.Item);
        } 

        public override void Hide(){
            hider.Hide();
        }

        public override void Show(){
            hider.Show();
        }
    }
}
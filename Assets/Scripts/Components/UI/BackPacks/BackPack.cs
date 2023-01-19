using System.Collections.Generic;
using Components.Items;
using Components.UI.Common;
using UI;
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

        public void AddBlock(ItemModel item, Vector2Int stagePosition){
            var block = _blocks.FirstNotActiveOrNew(MakeNew);
            block.Item = item;
            block.transform.position = grid.StageToWorldPosition(stagePosition);
        }

        public void OnBlockClicked(ItemBlock block){
            
        } 

        public override void Hide(){
            hider.Hide();
        }

        public override void Show(){
            hider.Show();
        }
    }
}
using System;
using System.Collections.Generic;
using Components.BackPacks.UI.Panels.BackupAreas;
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
        public BackupArea backupArea;
        public RectTransform clockwiseTransform;
        public RectTransform counterclockwiseTransform;
        
        [Header("Shadow Block")]
        public GameObject shadowPrefab;
        public Transform shadowRoot;
        private ShadowBlock _shadow;

        private readonly List<ItemBlock> _blocks = new();
        
        [NonSerialized]
        public bool IsRearranging = false;


        public void Start(){
            grid.gridHeight = new Bindable<int>(backPack.RowNum);
            grid.gridWidth = new Bindable<int>(backPack.ColNum);
            grid.gridWidth.Bind((i) => Resize());
            grid.gridHeight.Bind((i)=> Resize());
            backPack.ItemAdded += AddBlock;
            Resize();
        }


        private ItemBlock MakeNew(){
            var ret = Instantiate(blockPrefab, grid.transform).GetComponent<ItemBlock>();
            ret.grid = grid;
            return ret;
        }

        private void AddBlock(BackPackItemWrapper item){
            var block = _blocks.FirstNotActiveOrNew(MakeNew);
            block.BackPackPanel = this;
            block.ItemWrapper = item;
            block.areaDetector.area = backupArea.transform as RectTransform;
            block.clockwiseDetector.area = clockwiseTransform;
            block.counterclockwiseDetector.area = counterclockwiseTransform;
        }
        

        private void Resize(){
            grid.Resize();
            foreach (var itemBlock in _blocks){
                itemBlock.Resize();
            }
        }

        public void OnBlockClicked(ItemBlock block){
            Debug.Log($"Use item: {block.ItemWrapper}");
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

        private void PutBlockInGrid(ItemBlock block){
            var spared = backPack.PutItem(block.ItemWrapper);
            if (spared.Length == 0) return;
            var set = new HashSet<BackPackItemWrapper>(spared);
            foreach (var itemBlock in _blocks){
                if(!set.Contains(itemBlock.ItemWrapper)) continue;
                set.Remove(itemBlock.ItemWrapper);
                backupArea.AddBlock(itemBlock);
                itemBlock.IsInBackup = true;
            }
        }

        private void PutBlockInBackUp(ItemBlock block){
            backPack.RemoveItem(block.ItemWrapper);
            backupArea.ReplacePlaceholder(block);
        }

        private ItemBlock _selected;


        public void PickUpItemFromGrid(ItemBlock block){
            _selected = block;
            backPack.RemoveItem(block.ItemWrapper);
            EnableShadow();
        }

        public void PickUpItemFromBackup(ItemBlock block){
            _selected = block;
            backupArea.RemoveBlock(block);
            backupArea.MakePlaceholder(block);
            EnableShadow();
        }

        public void SelectedEnterBackupArea(){
            backupArea.MakePlaceholder(_selected);
        }

        public void SelectedExitBackupArea(){
            backupArea.RemovePlaceholder();
        }

        public void PutSelectedInBackup(){
            _selected.ItemWrapper.PlacePosition = Vector2Int.zero;
            PutBlockInBackUp(_selected);
            _selected = null;
            DisableShadow();
        }

        public void PutSelectedInGrid(){
            var gridPosition = grid.GetClosestGrid(_selected);
            _selected.ItemWrapper.PlacePosition = gridPosition;
            _selected.positionTween.Target = grid.GridToWorldPosition(gridPosition);
            PutBlockInGrid(_selected);
            _selected = null;
            DisableShadow();
        }

        public Vector3 GetSelectedClosestPosition(){
            return _selected.IsInBackup ? backupArea.GetClosestWorld(_selected) : grid.GetClosestWorld(_selected);
        }

        private void Update(){
            if (_selected is not{ IsInBackup: true }) return;
            backupArea.UpdatePlaceholder(_selected);
        }

        private void EnableShadow(){
            if (_shadow == null){
                _shadow = Instantiate(shadowPrefab, shadowRoot).GetComponent<ShadowBlock>();
                _shadow.Panel = this;
            }
            _shadow.enabled = true;
            _shadow.Master = _selected;
        }

        private void DisableShadow(){
            _shadow.enabled = false;
        }

        public void SelectedRotated(){
            _shadow.SyncRotation();
        }
    }
}
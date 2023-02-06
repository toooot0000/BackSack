using System;
using System.Collections.Generic;
using Components.BackPacks.UI.Panels.BackupAreas.Containers;
using Components.BackPacks.UI.Panels.ItemBlocks;
using UnityEngine;
using Utility.Extensions;

namespace Components.BackPacks.UI.Panels.BackupAreas{
    public class BackupArea : MonoBehaviour{
        public CanvasGroup canvasGroup;
        public BackupContainer container;
        public RectTransform placeHolder;
        private readonly Dictionary<ItemBlock, ItemBlockManageable> _dictionary = new();

        public Vector3 GetClosestWorld(ItemBlock block){
            return placeHolder.position;
        }

        public void AddBlock(ItemBlock itemBlock){
            container.Add(GetManageable(itemBlock));
        }

        public void RemoveBlock(ItemBlock itemBlock){
            if (!_dictionary.ContainsKey(itemBlock)) return;
            container.Remove(_dictionary[itemBlock]);
        }

        private int _prevInd;

        public void MakePlaceholder(ItemBlock itemBlock){
            var rectTrans = (RectTransform)itemBlock.transform;
            placeHolder.sizeDelta = rectTrans.rect.size;
            placeHolder.pivot = rectTrans.pivot;
            placeHolder.rotation = rectTrans.rotation;
            container.InsertPlaceHolder(placeHolder, Input.mousePosition.y);
        }

        public void UpdatePlaceholder(ItemBlock itemBlock){
            var mousePosition = Input.mousePosition;
            container.UpdatePlaceholder(mousePosition.y);
        }

        public void RemovePlaceholder(){
            container.RemovePlaceholder();
        }

        public void ReplacePlaceholder(ItemBlock itemBlock){
            container.ReplacePlaceholder(GetManageable(itemBlock));
        }

        private ItemBlockManageable GetManageable(ItemBlock block){
            var manageable = _dictionary.ContainsKey(block) ? _dictionary[block] : new ItemBlockManageable(block);
            _dictionary[block] = manageable;
            return manageable;
        }
    }
}
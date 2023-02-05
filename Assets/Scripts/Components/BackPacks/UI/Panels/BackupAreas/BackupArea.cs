using System;
using System.Collections.Generic;
using Components.BackPacks.UI.Panels.ItemBlocks;
using UnityEngine;
using Utility.Extensions;

namespace Components.BackPacks.UI.Panels.BackupAreas{
    public class BackupArea: MonoBehaviour{
        public CanvasGroup canvasGroup;
        public BackupContainer container;
        public RectTransform placeHolder;
        public Camera cmr;

        public Vector3 GetClosestPosition(ItemBlock block){
            return placeHolder.position;
        }
        
        public void AddBlock(ItemBlock itemBlock){
            var rectTrans = (RectTransform)itemBlock.transform;
            var index = container.GetInsertIndex(rectTrans);
            container.Insert(index, rectTrans);
        }

        public void RemoveBlock(ItemBlock itemBlock){
            container.Remove((RectTransform)itemBlock.transform);
        }

        private int _prevInd;

        public void MakeEmptyPosition(ItemBlock itemBlock){
            var rectTrans = (RectTransform)itemBlock.transform;
            var index = container.GetInsertIndex(rectTrans);
            placeHolder.sizeDelta = rectTrans.rect.size;
            container.Insert(index, placeHolder);
            _prevInd = index;
        }

        public void UpdateEmptyPosition(ItemBlock itemBlock){
            var mousePosition = cmr.ScreenToWorldPoint(Input.mousePosition);
            var ctnRectTrans = ((RectTransform)container.transform).GetWorldRect();
            if (mousePosition.y < ctnRectTrans.yMin || mousePosition.y > ctnRectTrans.yMax) return;
            var worldRect =  placeHolder.GetWorldRect();
            if (worldRect.yMin < mousePosition.y && worldRect.yMax > mousePosition.y) return;
            var rectTrans = (RectTransform)itemBlock.transform;
            var newInd = container.GetInsertIndex(rectTrans);
            container.Move(_prevInd, newInd);
            _prevInd = newInd;
        }

        public void RemoveEmptyPosition(){
            container.Remove(placeHolder);
        }
    }
}
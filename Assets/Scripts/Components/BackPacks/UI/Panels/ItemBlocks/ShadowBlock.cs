using System;
using Components.BackPacks.UI.Panels.ItemBlocks.ShapeBlocks;
using Components.Items;
using UnityEditor;
using UnityEngine;
using Utility.Extensions;

namespace Components.BackPacks.UI.Panels.ItemBlocks{
    public class ShadowBlock: ShapeBlock{
        public CanvasGroup canvasGroup;
        [NonSerialized] public ItemBlock Master;

        public override void OnEnable(){
            base.OnEnable();
            if (Master == null) return;
            var transform1 = Master.transform;
            var transform2 = transform;
            transform2.position = transform1.position;
            transform2.rotation = transform1.rotation;
        }


        public void Start(){
            grid = Master.grid;
            Reload(Master.ItemWrapper.Item.TakeUpRange);
            var transform1 = Master.transform;
            var transform2 = transform;
            transform2.position = transform1.position;
            transform2.rotation = transform1.rotation;
        }

        private void Update(){
            transform.position = Master.GetClosestPossiblePosition();
        }
    }
}
using System;
using Components.BackPacks.UI.Panels.ItemBlocks.ShapeBlocks;
using Components.Items;
using UnityEditor;
using UnityEngine;
using Utility.Extensions;

namespace Components.BackPacks.UI.Panels.ItemBlocks{
    public class ShadowBlock: ShapeBlock{
        private ItemBlock _master;
        public ItemBlock Master{
            get => _master;
            set{
                _master = value;
                grid = value.grid;
                SyncTransform();
                Reload(value.ItemWrapper.Item.TakeUpRange);
                
            }
        }

        [NonSerialized] public BackPackPanel Panel;
        

        private void SyncTransform(){
            var transform1 = Master.transform;
            var transform2 = transform;
            transform2.position = transform1.position;
            transform2.rotation = transform1.rotation;
        }

        public void SyncRotation(){
            transform.rotation = Master.transform.rotation;
        }

        private void Update(){
            transform.position = Panel.GetSelectedClosestPosition();
        }
    }
}
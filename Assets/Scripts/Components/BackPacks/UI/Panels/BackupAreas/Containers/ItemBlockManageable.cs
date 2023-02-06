using Components.BackPacks.UI.Panels.ItemBlocks;
using UnityEngine;
using Utility.Extensions;

namespace Components.BackPacks.UI.Panels.BackupAreas.Containers{
    public class ItemBlockManageable: IBackupManageable{

        private readonly ItemBlock _block;

        public ItemBlockManageable(ItemBlock block){
            _block = block;
        }
        
        public RectTransform Transform => _block.transform as RectTransform;
        public Vector2 GetSize() => Transform.rect.size;

        private Vector3[] _corners = new Vector3[4];
        public void SetCenterPosition(Vector2 targetPosition){
            _block.positionTween.Target = Transform.TransformCenterToWorld(targetPosition, _corners);
        }
    }
}
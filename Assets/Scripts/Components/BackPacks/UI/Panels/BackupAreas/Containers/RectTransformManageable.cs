using UnityEngine;
using Utility.Extensions;

namespace Components.BackPacks.UI.Panels.BackupAreas.Containers{
    public class RectTransformManageable : IBackupManageable{
        public RectTransformManageable(RectTransform transform){
            Transform = transform;
        }
        public RectTransform Transform{ get; }
        public Vector2 GetSize(){
            return Transform.rect.size;
        }

        public void SetCenterPosition(Vector2 targetPosition){
            Transform.SetCenterPosition(targetPosition);
        }
    }
}
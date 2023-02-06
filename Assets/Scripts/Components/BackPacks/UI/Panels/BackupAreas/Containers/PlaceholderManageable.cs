using UnityEngine;
using Utility.Extensions;

namespace Components.BackPacks.UI.Panels.BackupAreas.Containers{
    public class PlaceholderManageable: IBackupManageable{

        public int CurIndex;

        public PlaceholderManageable(RectTransform transform){
            Transform = transform;
        }

        public RectTransform Transform{ get; }

        public Vector2 GetSize() => Transform.rect.size;

        public void SetCenterPosition(Vector2 targetPosition){
            Transform.SetCenterPosition(targetPosition);
        }
    }
}
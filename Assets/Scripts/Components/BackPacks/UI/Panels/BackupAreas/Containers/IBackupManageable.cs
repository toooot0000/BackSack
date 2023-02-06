using UnityEngine;

namespace Components.BackPacks.UI.Panels.BackupAreas.Containers{
    public interface IBackupManageable{
        RectTransform Transform{ get; }
        Vector2 GetSize();
        void SetCenterPosition(Vector2 targetPosition);
    }
}
using UnityEngine;

namespace Components.Attacks{
    public interface IAttacker{
        Vector2Int CurrentStagePosition{ set; get; }
        
    }
}
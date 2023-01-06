using UnityEngine;

namespace Models.Attacks{
    public interface IAttacker{
        Vector2Int CurrentStagePosition{ set; get; }
        
    }
}
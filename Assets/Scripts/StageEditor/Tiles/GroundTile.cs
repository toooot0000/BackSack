using Components.Grounds;
using UnityEditor;

namespace StageEditor.Tiles{
    public class GroundTile: StageTile{
        public GroundType type;
        
#if UNITY_EDITOR
        [MenuItem("Assets/Create/Tile/GroundTile")]
        public static void CreateObjectTile(){
            CreateTile<GroundTile>();
        }
#endif
    }
}
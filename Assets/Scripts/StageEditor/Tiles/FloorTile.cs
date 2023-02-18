using Components.Stages;
using Components.Stages.Floors;
using UnityEditor;

namespace StageEditor.Tiles{
    public class FloorTile: StageTile{
        public FloorType type;
#if UNITY_EDITOR
        [MenuItem("Assets/Create/Tile/FloorTile")]
        public static void CreateObjectTile(){
            CreateTile<FloorTile>();
        }
#endif
    }
}
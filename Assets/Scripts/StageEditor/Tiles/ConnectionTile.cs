using UnityEditor;
using Utility.Extensions;

namespace StageEditor.Tiles{
    public class ConnectionTile: StageTile{
        public Direction direction;
        
#if UNITY_EDITOR
        [MenuItem("Assets/Create/Tile/ConnectionTile")]
        public static void CreateConnectionTile(){
            CreateTile<ConnectionTile>();
        }
#endif
    }
}
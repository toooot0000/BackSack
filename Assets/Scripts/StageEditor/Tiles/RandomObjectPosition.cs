using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace StageEditor.Tiles{
    public class RandomObjectPosition: StageTile{

#if UNITY_EDITOR
        [MenuItem("Assets/Create/Tile/RandomObject")]
        public static void CreateObjectTile(){
            CreateTile<RandomObjectPosition>();
        }
#endif
    }
}
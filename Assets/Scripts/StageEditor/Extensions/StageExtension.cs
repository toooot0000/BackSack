using Components.Stages;
using UnityEngine;

namespace StageEditor{
    internal static class StageExtension{
        public static Vector3Int GetGridPosition(this StageModel stageModel, int row, int col){
            return new Vector3Int(row - stageModel.Width/2, col - stageModel.Height/2);
        }
    }
}
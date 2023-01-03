using System.Collections;
using Models.TileObjects;
using MVC;
using UnityEngine;

namespace Entities.Players{
    public class PlayerView: MonoBehaviour, IViewWithType<Player>{
        public void MoveToPosition(Vector3 targetPosition){
            
        }

        public IEnumerator MoveToPositionRoutine(Vector3 targetPosition){
            yield break;
        }

        public void SetPosition(Vector3 targetPosition){
            transform.position = targetPosition;
        }

        public void BumpToWall(Vector2Int direction){
            throw new System.NotImplementedException();
        }
    }
}
using Models.TileObjects;
using MVC;
using UnityEngine;

namespace Entities.Players{
    public class PlayerView: MonoBehaviour, IViewWithType<Player>{
        public void MoveToPosition(Vector3 targetPosition){
            
        }

        public void SetPosition(Vector3 targetPosition){
            transform.position = targetPosition;
        }

        public void BumpToWall(Vector2Int direction){
            throw new System.NotImplementedException();
        }
    }
}
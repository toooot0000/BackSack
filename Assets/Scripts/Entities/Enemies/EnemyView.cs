using System.Collections;
using Models.TileObjects;
using MVC;
using UnityEngine;

namespace Entities.Enemies{
    public class EnemyView: MonoBehaviour, IViewWithType<Enemy>{
        public SpriteRenderer spriteRenderer;

        public void SetSprite(Sprite sprite){
            spriteRenderer.sprite = sprite;
        }

        public IEnumerator AttackRoutine(){
            yield return null;
        }

        public IEnumerator BeingAttackedRoutine(){
            yield return null;
        }

        public void SetPosition(Vector3 position){
            transform.position = position;
        }
    }
}
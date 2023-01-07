using Components.TileObjects;
using Components.TileObjects.ForceMovable;
using MVC;
using UnityEngine;

namespace Components.Enemies{

    public enum EnemyAnimation{ }
    
    public class EnemyView: ForceMovableView{
        public SpriteRenderer spriteRenderer;

        public void SetSprite(Sprite sprite){
            spriteRenderer.sprite = sprite;
        }
    }
}
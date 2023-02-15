using Components.TileObjects.BattleObjects;
using UnityEngine;

namespace Components.Enemies{

    public enum EnemyAnimation{ }

    public class EnemyView: BattleObjectView{
        public SpriteRenderer spriteRenderer;

        public void SetSprite(Sprite sprite){
            spriteRenderer.sprite = sprite;
        }
    }
}
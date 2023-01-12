using Components.Attacks;
using Components.TileObjects;
using Components.TileObjects.BattleObjects;
using Components.TileObjects.ForceMovables;
using MVC;
using UnityEngine;

namespace Components.Enemies{

    public enum EnemyAnimation{ }
    
    public class EnemyView: BattleObjectView{
        public SpriteRenderer spriteRenderer;

        public void SetSprite(Sprite sprite){
            spriteRenderer.sprite = sprite;
        }

        public override void Attack(IAttack attack){
            throw new System.NotImplementedException();
        }
    }
}
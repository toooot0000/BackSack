using System.Collections;
using Entities.Players;
using Entities.TileObjects;
using Entities.TileObjects.Tweens;
using Models.TileObjects;
using Models.TileObjects.Enemies;
using MVC;
using UnityEngine;
using Utility.Animation;

namespace Entities.Enemies{

    public enum EnemyAnimation{
        Move,
        Bump,
        Attack,
        BeingAttacked
    }
    
    public class EnemyView: TileObjectAnimationController, IViewWithType<Enemy>{
        public SpriteRenderer spriteRenderer;

        public void SetSprite(Sprite sprite){
            spriteRenderer.sprite = sprite;
        }
    }
}
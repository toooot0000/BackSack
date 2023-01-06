using System.Collections;
using Entities.Players.Animtors;
using Entities.TileObjects;
using Entities.TileObjects.Tweens;
using Models.TileObjects;
using MVC;
using UnityEngine;

namespace Entities.Players{

    public class PlayerView: TileObjectAnimationController, IViewWithType<Player>{
        public void Jump(){
            Play<PlayerAnimation, PlayerAnimationController>(PlayerAnimation.Jump, new Jump.Argument(0));
        }
    }
}
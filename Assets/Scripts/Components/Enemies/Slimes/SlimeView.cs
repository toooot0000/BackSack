﻿using System.Collections;
using Components.Attacks;
using Components.Effects;
using Components.TileObjects.ForceMovables;
using Components.TileObjects.Tweens;
using UnityEngine;

namespace Components.Enemies.Slimes{
    public class SlimeView: MonoBehaviour, IEnemyExtendedView, IAttackAnimator{

        public IEffect Result{ get; private set; }
        public IEnumerator Play(IAttack attack){
            if (attack is not Attack slimeAtt) yield break;
            yield return View.PlayAndWaitUntilComplete(TileObjectAnimation.Bump, new Bump.Argument(){
                Direction = slimeAtt.Direction
            });
            Result = attack.PassEffects();
        }

        public EnemyView View{ get; set; }
    }
}
using System;
using System.Collections.Generic;
using Components.Buffs;
using Components.Buffs.Effects;
using Components.Damages;
using Components.Effects;
using Components.TileObjects;
using Components.TileObjects.BattleObjects;
using Components.TileObjects.ForceMovables;
using UnityEngine;

namespace Components.Enemies{
    public class Enemy: BattleObject{
        public new EnemyView view;
        public new EnemyModel Model{
            set => SetModel(value);
            get => base.Model as EnemyModel;
        }
        
        protected override void Awake(){
            base.Awake();
            base.view = view;
        }

        protected override void AfterSetModel(){
            base.AfterSetModel();
            var spr = Resources.Load<Sprite>(Model.GetSpriteResourcePath());
            view.SetSprite(spr);
        }

        public IEffect DoNextAction(){
            throw new NotImplementedException();
        }
    }
}
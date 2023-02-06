using System;
using Components.Attacks;
using Components.Effects;
using Components.Enemies.Intentions;
using Components.Players;
using Components.SelectMaps;
using Components.TileObjects;
using Components.TileObjects.Automate;
using Components.TileObjects.BattleObjects;
using MVC;
using UnityEngine;
using Utility.Extensions;

namespace Components.Enemies{
    public class Enemy: BattleObject, IAttacker, IAutomate{
        public EnemyView view;
        
        private IIntentionContext _context = null;
        private IIntentionContext Context{
            get{
                _context ??= new IntentionContext(stage, IController.GetController<Player>());
                return _context;
            }
        }

        private IActionPattern _pattern;
        private GameObject _core;

        private GameObject Core{
            get{
                if (_core != null) return _core;
                var prefab = Resources.Load<GameObject>(Model.CorePath);
                if (prefab == null) return null;
                _core = Instantiate(prefab, transform);
                return _core;
            }
        }

        private EnemyModel _model;
        public EnemyModel Model{
            set{
                _model = value;
                var spr = Resources.Load<Sprite>(Model.SprPath);
                view.SetSprite(spr);
                SetStagePosition(_model.CurrentStagePosition);
            }
            get => _model;
        }

        public IEffect DoAction(){
            var intention = ActionPattern.GetIntention(this, Context);
            return intention?.DoAction();
        }

        public void ShowIntention(SelectMap selectMap){
            ActionPattern.GetIntention(this, Context).Label(selectMap);
        }

        public bool IsActive => gameObject.activeSelf;

        private IActionPattern ActionPattern{
            get{
                if (_pattern != null) return _pattern;
                _pattern = Core.GetComponent<IActionPattern>();
                return _pattern;
            }
        }

        private IEnemyExtendedView _extendedView = null;
        public IEnemyExtendedView ExtendedView{
            get{
                _extendedView ??= Core.GetComponent<IEnemyExtendedView>();
                _extendedView.View = view;
                return _extendedView;
            }
        }

        public IAttackAnimator GetAttackAnimator(IAttack attack){
            if (attack is not IEnemyAttack enemyAttack) return null;
            return enemyAttack.GetAnimator();
        }

        public override ITileObjectView View => view;
        
        public override Vector2Int CurrentStagePosition{
            set => Model.CurrentStagePosition = value;
            get => Model.CurrentStagePosition;
        }
        
        public override int Weight{ 
            get => Model.Weight;
            set => Model.Weight = value;
        }

        public override int HealthLimit{
            get => Model.HealthLimit;
            set => Model.HealthLimit = value;
        }
        public override int HealthPoint{
            get => Model.HealthPoint;
            set => Model.HealthPoint = value;
        }
        public override int ShieldPoint{ 
            get => Model.ShieldPoint;
            set => Model.ShieldPoint = value;
        }
        public override int DefendPoint{
            get => Model.DefendPoint;
            set => Model.DefendPoint = value;
        }
    }
}
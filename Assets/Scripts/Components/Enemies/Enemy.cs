using Components.Attacks;
using Components.Effects;
using Components.Enemies.Intentions;
using Components.Players;
using Components.SelectMaps;
using Components.TileObjects.Automate;
using Components.TileObjects.BattleObjects;
using MVC;
using UnityEngine;
using Utility.Extensions;

namespace Components.Enemies{
    public class Enemy: BattleObject, IAttacker, IAutomate{
        public new EnemyView view;
        public string enemyFolder = "";
        [HideInInspector]
        public EnemyManager Manager;
        
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
                var id = Model.ID!.Value.ToString();
                var prefab = Resources.Load<GameObject>(Model.CorePath);
                if (prefab == null) return null;
                _core = Instantiate(prefab, transform);
                return _core;
            }
        }

        public new EnemyModel Model{
            set => SetModel(value);
            get => base.Model as EnemyModel;
        }

        protected override void AfterSetModel(){
            base.AfterSetModel();
            var spr = Resources.Load<Sprite>(Model.SprPath);
            view.SetSprite(spr);
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

        public void LabelIntention(SelectMap map){
            ActionPattern.GetIntention(this, Context).Label(map);
        }

        public IAttackAnimator GetAttackAnimator(IAttack attack){
            if (attack is not IEnemyAttack enemyAttack) return null;
            return enemyAttack.GetAnimator();
        }

        protected override void Awake(){
            base.Awake();
            base.view = view;
        }
    }
}
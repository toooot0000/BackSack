using System;
using System.Collections;
using System.Collections.Generic;
using Components.BackPacks.UI.Panels;
using Components.Buffs;
using Components.Buffs.Triggers;
using Components.DirectionSelects;
using Components.Effects;
using Components.Enemies;
using Components.Items;
using Components.Items.Instances;
using Components.Players;
using Components.SelectMaps;
using Components.Stages;
using Components.TileObjects;
using Components.TileObjects.Automate;
using Components.TileObjects.Movables;
using Components.UI;
using MVC;
using UnityEngine;
using Utility.Extensions;

namespace Components{
    /// <summary>
    /// PreStart => Exploring: Before game start, target state is based on save file 
    /// InDialogue => Exploring: dialogue finishes
    /// Any => Pausing: Open UI
    /// </summary>


    public class GameManager : MonoBehaviour{
        public readonly Game Model = new Game();

        private static GameManager _shared;
        public static GameManager Shared => _shared;
        public GameState State => Model.State;
    
    
        public Player player;
        public Stage stage;
        public SelectMap selectMap;
        public BackPackPanel backPackPanel;
        public DirectionSelectManager directionSelectManager;


        private bool _isEnd = false;
        public bool IsEnd{
            set{
                _isEnd = value;
                if (_isEnd) GameEnd();
            }
            get => _isEnd;
        }

        private Coroutine _gameLoop = null;

        private Enemy _enemy;

        private void Awake(){
            if (_shared == null) _shared = this;
            else Destroy(this);
        }

        private void Start(){
            PreGameSetUp();
            LoadGame();
        
            stage = IController.GetController<Stage>();
            stage.SetModel(MVC.Model.FromJsonString<StageModel>(Resources.Load<TextAsset>("Stages/stage-test-1").text));
            player = IController.GetController<Player>();
            player.SetModel(new PlayerModel());
            _enemy = IController.GetController<Enemy>();

            _gameLoop = StartCoroutine(GameLoop());
        }


        private void Update(){
            if (Input.GetKeyUp(KeyCode.UpArrow)){
                MovePlayer(Vector2Int.up);
            } else if (Input.GetKeyUp((KeyCode.LeftArrow))){
                MovePlayer(Vector2Int.left);
            } else if (Input.GetKeyUp(KeyCode.DownArrow)){
                MovePlayer(Vector2Int.down);
            } else if (Input.GetKeyUp(KeyCode.RightArrow)){
                MovePlayer(Vector2Int.right);
            } else if (Input.GetKeyUp(KeyCode.Space)){
                _allActionFinished = true;
            } else if (Input.GetKeyUp(KeyCode.Z)){
                StartCoroutine(PropagateEffect(player.UseItemWithDirection(new Sword(), Direction.Right)));
            } else if (Input.GetKeyUp(KeyCode.X)){
                StartCoroutine(PropagateEffect(player.UseItemWithDirection(new Hooklock(), Direction.Left)));
            } else if (Input.GetKeyUp(KeyCode.C)){
                StartCoroutine(EnemiesActs());
            } else if (Input.GetKeyUp(KeyCode.V)){
                foreach (var enemy in IController.GetControllers<IAutomate>()){
                    if(!enemy.IsActive) continue;
                    enemy.ShowIntention(selectMap);
                }
            } else if (Input.GetKeyUp(KeyCode.B)){
                selectMap.Stash();
            } else if (Input.GetKeyUp(KeyCode.N)){
                selectMap.Pop();
            } else if (Input.GetKeyUp(KeyCode.M)){
                backPackPanel.AddBlock(new Sword(), Vector2Int.one, Vector2Int.left);
            } else if (Input.GetKeyUp(KeyCode.Q)){
                player.backPack.AddItem(new Sword(), Direction.Left, Vector2Int.one);
            }
        }

        private void MovePlayer(Vector2Int direction){
            MoveTileObject(player, direction.AlignedDirection());
        }

        private void MoveTileObject(IMovable tileObject, Direction direction){
            StartCoroutine(PropagateEffect(tileObject.Move(direction)));
        }

        /// <summary>
        /// System setting;
        /// 
        /// </summary>
        private void PreGameSetUp(){
        
        }
    
        /// <summary>
        /// 1. Load stage;
        /// 2. Set player;
        /// </summary>
        private void LoadGame(){
        
        }

        private IEnumerator GameLoop(){
            while (!IsEnd){
                yield return _GameLoop();
            }
        
        }

        private bool _allActionFinished = false;
        private bool _AdvancePhase(){
            if (!_allActionFinished) return false;
            _allActionFinished = false;
            return true;
        }

        private IEnumerator _GameLoop(){
        
            OtherTurnStarts();
            yield return new WaitUntil(_AdvancePhase);
        
            // Player turn starts
            PlayerTurnStarts();
            yield return new WaitUntil(_AdvancePhase);
        
            // Player input instruction 
            yield return WaitForPlayerInput();
        
            // Process Player action
            PlayerActs();
            yield return new WaitUntil(_AdvancePhase);
        
            // Player turn ends
            PlayerTurnEnds();
            yield return new WaitUntil(_AdvancePhase);
        
            // Enemies' turns execute sequentially on the model layer, but performed in parallel on the view layer
            EnemiesTurnStarts();
            yield return new WaitUntil(_AdvancePhase);
        
            // Perform actions;
            yield return EnemiesActs();
            yield return new WaitUntil(_AdvancePhase);
        
            // Enemies' turns ends?
            EnemiesTurnEnds();
            yield return new WaitUntil(_AdvancePhase);
        
            OtherTurnEnds();
            yield return new WaitUntil(_AdvancePhase);
        }
    
        private IEnumerator _BattleLoop(){
            yield return null;
        }


        private void GameEnd(){
            StopAllCoroutines();
            // blah blah TODO
        }

        private void PlayerTurnStarts(){
            Debug.Log("Game Phase: Player turn starts!");
        }

        private void PlayerActs(){
            Debug.Log("Game Phase: Acting player action");
        }

        private void PlayerTurnEnds(){
            Debug.Log("Game Phase: Player turn ends");
            foreach (var playerBuff in player.GetBuffOfTrigger<IOnTurnEnd>()){
                var effect = playerBuff.OnTurnEnd(player);
                player.Consume(effect);
            }
        }

        private IEnumerator WaitForPlayerInput(){
            Debug.Log("Game Phase: Wait for player instruction");
            yield return new WaitUntil(_AdvancePhase);
        }

        private void EnemiesTurnStarts(){
            Debug.Log("Game Phase: Other objects' turn start & calculate actions");
        }

        private IEnumerator EnemiesActs(){
            Debug.Log("Game Phase: Other objects' actions");
            foreach (var enemy in IController.GetControllers<IAutomate>()){
                yield return PropagateEffect(enemy.DoAction());
            }
        }

        private void EnemiesTurnEnds(){
            Debug.Log("Game Phase: Other objects' actions");
        }

        private void OtherTurnStarts(){
            Debug.Log("Game Phase: Other Turn Starts");
        }

        private void OtherTurnEnds(){
            Debug.Log("Game Phase: Other Turn Ends");
        
        }

        private IEnumerator WaitForAllAnimationComplete(){
            yield return null;
        }

        private static IEnumerator PropagateEffect(IEffect effect){
            while (effect != null){
                switch (effect){
                    case CoroutineEffect coEff:
                        yield return coEff.Coroutine(coEff);
                        effect = coEff.Result;
                        break;
                    case MultiEffect multi:
                        foreach (var sub in multi.Effects){
                            yield return PropagateEffect(sub);
                        }
                        effect = null;
                        break;
                    default:
                        effect = effect.Target?.Consume(effect);
                        break;
                }
            }
        }

        private IEnumerator PlayerUseItemWithDirectionCoroutine(ItemModel item, Direction direction){
            yield return PropagateEffect(player.UseItemWithDirection(item, direction));
            // Perhaps advance phase;
        }

        public void PlayerUseItemWithDirection(ItemModel weapon, Direction direction){
            UIManager.Shared.ShowAllComponents();
            StartCoroutine(PlayerUseItemWithDirectionCoroutine(weapon, direction));
        }

        public void StartSelectDirection(ItemModel item){
            UIManager.Shared.HideAllComponents();
            directionSelectManager.ActiveWithItem(item);
        }
    }
}

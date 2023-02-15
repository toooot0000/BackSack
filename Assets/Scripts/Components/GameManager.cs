using System;
using System.Collections;
using System.Collections.Generic;
using Components.BackPacks;
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

        [NonSerialized] 
        public bool AllowPlayerInput = false;
    
    
        public Player player;
        public Stage stage;
        public SelectMap selectMap;
        public BackPackPanel backPackPanel;


        private bool _isEnd = false;
        public bool IsEnd{
            set{
                _isEnd = value;
                if (_isEnd) GameEnd();
            }
            get => _isEnd;
        }

        private Coroutine _gameLoop = null;

        private void Awake(){
            if (_shared == null) _shared = this;
            else Destroy(this);
        }

        private void Start(){
            PreGameSetUp();
            LoadGame();
        
            stage = IController.GetController<Stage>();
            stage.Model = MVC.Model.FromJsonString<StageModel>(Resources.Load<TextAsset>("Stages/stage-test-1").text);
            player = IController.GetController<Player>();
            player.Model = new PlayerModel();

            _gameLoop = StartCoroutine(GameLoop());
        }


        private void Update(){
            TestInput();
            GetInput();
        }

        private BackPackItemWrapper _wrapper;

        private void TestInput(){
            if (Input.GetKeyUp(KeyCode.Q)){
                _wrapper = player.backPack.AddItem(new Sword(), Direction.Up, Vector2Int.one);
            } else if (Input.GetKeyUp(KeyCode.Space)){
                Debug.Log(_wrapper.ToString());
            } else if (Input.GetKeyUp(KeyCode.W)){
                var sword = new Sword();
                sword.TakeUpRange = new Vector2Int[1]{ Vector2Int.zero };
                var pos = player.backPack.FindSuitablePosition(sword, Direction.Down);
                if (pos != new Vector2Int(-1, -1)){
                    player.backPack.AddItem(sword, Direction.Down, pos);
                }
            }
        }

        private void GetInput(){
            if (!AllowPlayerInput) return;
            if (Input.GetKeyUp(KeyCode.UpArrow)){
                MovePlayer(Vector2Int.up);
                AllowPlayerInput = false;
            } else if (Input.GetKeyUp((KeyCode.LeftArrow))){
                MovePlayer(Vector2Int.left);
                AllowPlayerInput = false;
            } else if (Input.GetKeyUp(KeyCode.DownArrow)){
                MovePlayer(Vector2Int.down);
                AllowPlayerInput = false;
            } else if (Input.GetKeyUp(KeyCode.RightArrow)){
                MovePlayer(Vector2Int.right);
                AllowPlayerInput = false;
            }
            if(!AllowPlayerInput) _allActionFinished = true;
        }

        private void MovePlayer(Vector2Int direction){
            MoveTileObject(player, direction.AlignedDirection());
        }

        private void MoveTileObject(IMovable tileObject, Direction direction){
            StartCoroutine(PropagateEffect(tileObject.TryMove(direction)));
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
        
            yield return OtherTurnStarts();
            yield return new WaitUntil(_AdvancePhase);
        
            // Player turn starts
            yield return PlayerTurnStarts();
            yield return new WaitUntil(_AdvancePhase);
        
            // Player input instruction 
            yield return WaitForPlayerAction();

            // Player turn ends
            yield return PlayerTurnEnds();
            yield return new WaitUntil(_AdvancePhase);
        
            // Enemies' turns execute sequentially on the model layer, but performed in parallel on the view layer
            yield return EnemiesTurnStarts();
            yield return new WaitUntil(_AdvancePhase);
        
            // Perform actions;
            yield return EnemiesActs();
            yield return new WaitUntil(_AdvancePhase);
        
            // Enemies' turns ends?
            yield return EnemiesTurnEnds();
            yield return new WaitUntil(_AdvancePhase);
        
            yield return OtherTurnEnds();
            yield return new WaitUntil(_AdvancePhase);
        }
    
        private IEnumerator _BattleLoop(){
            yield return null;
        }


        private void GameEnd(){
            StopAllCoroutines();
            // blah blah TODO
        }

        private IEnumerator PlayerTurnStarts(){
            Debug.Log("Game Phase: Player turn starts!");
            foreach (var buff in player.Buffs){
                if (buff is IOnTurnBegin begin) yield return PropagateEffect(begin.OnTurnBegin(player));
            }
            _allActionFinished = true;
        }

        private IEnumerator PlayerTurnEnds(){
            Debug.Log("Game Phase: Player turn ends");
            foreach (var playerBuff in player.GetBuffOfTrigger<IOnTurnEnd>()){
                yield return PropagateEffect(playerBuff.OnTurnEnd(player));
            }
            selectMap.Pop();
            _allActionFinished = true;
        }

        private IEnumerator WaitForPlayerAction(){
            Debug.Log("Game Phase: Wait for player instruction");
            AllowPlayerInput = true;
            _allActionFinished = false;
            yield return new WaitUntil(_AdvancePhase);
        }

        private IEnumerator EnemiesTurnStarts(){
            Debug.Log("Game Phase: Other objects' turn start & calculate actions");
            selectMap.HideAll();
            selectMap.Pop();
            foreach (var floor in stage.GetFloors()){
                if (floor.TileObject is not Enemy enemy) continue;
                foreach (var buff in enemy.Buffs){
                    if (buff is not IOnTurnBegin onTurnBegin) continue;
                    yield return PropagateEffect(onTurnBegin.OnTurnBegin(enemy));
                }
            }
            _allActionFinished = true;
        }

        private IEnumerator EnemiesActs(){
            Debug.Log("Game Phase: Other objects' actions");
            foreach (var enemy in stage.enemyManager.GetSortedActiveEnemies()){
                yield return PropagateEffect(enemy.DoAction());
            }

            _allActionFinished = true;
        }

        private IEnumerator EnemiesTurnEnds(){
            Debug.Log("Game Phase: Other objects' actions");
            foreach (var enemy in stage.enemyManager.GetSortedActiveEnemies()){
                foreach (var buff in enemy.Buffs){
                    if (buff is not IOnTurnEnd onTurnEnd) continue;
                    yield return PropagateEffect(onTurnEnd.OnTurnEnd(enemy));
                }
                if(enemy.gameObject.activeSelf) enemy.ShowIntention(selectMap);
            }
            _allActionFinished = true;
        }

        private IEnumerator OtherTurnStarts(){
            Debug.Log("Game Phase: Other Turn Starts");
            _allActionFinished = true;
            yield return null;
        }

        private IEnumerator OtherTurnEnds(){
            Debug.Log("Game Phase: Other Turn Ends");
            foreach (var floor in stage.GetFloors()){
                if(floor.Ground) 
                    yield return PropagateEffect(floor.Ground.OnTurnEnd(floor.TileObject));
            }
            _allActionFinished = true;
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
            _allActionFinished = true;
        }

        public void PlayerUseItemWithDirection(ItemModel weapon, Direction direction){
            if (!AllowPlayerInput) return;
            StartCoroutine(PlayerUseItemWithDirectionCoroutine(weapon, direction));
        }
    }
}

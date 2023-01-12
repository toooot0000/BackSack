using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Components;
using Components.Attacks;
using Components.Buffs;
using Components.Buffs.Triggers;
using Components.Effects;
using Components.Items.Instances;
using Components.Players;
using Components.Stages;
using Components.TileObjects;
using Components.TileObjects.BattleObjects;
using Coroutines;
using MVC;
using UnityEditor.VersionControl;
using UnityEngine;


/// <summary>
/// PreStart => Exploring: Before game start, target state is based on save file 
/// InDialogue => Exploring: dialogue finishes
/// Any => Pausing: Open UI
/// </summary>


public class GameManager : MonoBehaviour{
    private static GameManager _shared = null;
    public static GameManager Shared => _shared;
    public CoroutineManager coroutineManager;
    public readonly Game Model = new Game();
    public GameState State => Model.State;
    
    
    public Player player;
    public Stage stage;


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
        if(_shared != null) Destroy(this);
        _shared = this;
    }

    private void Start(){
        PreGameSetUp();
        LoadGame();
        
        stage = IController.GetController<Stage>();
        stage.SetModel(MVC.Model.FromJsonString<StageModel>(Resources.Load<TextAsset>("Stages/stage-test-1").text));
        player = IController.GetController<Player>();
        player.SetModel(new PlayerModel());
        
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
            PropagateEffect(player.UseWeapon(new Sword(), Vector2Int.right));
        }
    }

    private void MovePlayer(Vector2Int direction){
        MoveTileObject(player, direction);
    }

    private void MoveTileObject(ITileObject tileObject, Vector2Int direction){
        PropagateEffect(tileObject.Move(direction));
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
        EnemiesActs();
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

    private void EnemiesActs(){
        Debug.Log("Game Phase: Other objects' actions");
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

    private void PropagateEffect(IEffect effect){
        while (effect != null) effect = effect.Target.Consume(effect);
    }
}

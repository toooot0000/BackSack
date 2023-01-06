using System;
using System.Collections;
using System.Collections.Generic;
using Coroutines;
using Entities.Players;
using Entities.Stages;
using Models;
using Models.Stages;
using Models.TileObjects;
using MVC;
using UnityEngine;


/// <summary>
/// PreStart => Exploring: Before game start, target state is based on save file 
/// InDialogue => Exploring: dialogue finishes
/// Any => Pausing: Open UI
/// </summary>
public enum GameState{
    Exploring,
    Pausing, // => When UI Opened
}

public class GameManager : MonoBehaviour{
    private static GameManager _shared = null;
    public static GameManager Shared => _shared;
    public CoroutineManager coroutineManager;
    public GameState state = GameState.Pausing;
    [HideInInspector]
    public PlayerController player;
    [HideInInspector]
    public StageController stage;


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
        
        stage = IController.GetController<StageController>();
        stage.SetModel(Model.FromJsonString<Stage>(Resources.Load<TextAsset>("Stages/stage-test-blank").text));
        player = IController.GetController<PlayerController>();
        player.SetModel(new Player());
        
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
            player.Jump();
        }
    }

    private void MovePlayer(Vector2Int direction){
        player.Move(direction);
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

    private IEnumerator _GameLoop(){
        // Player turn starts
        Debug.Log("Game Phase: Player turn starts!");
        yield return new WaitUntil(() => false);
        // Player input instruction 
        Debug.Log("Game Phase: Wait for player instruction");
        yield return new WaitUntil(() => false);
        // Process Player action
        Debug.Log("Game Phase: Acting player action");
        yield return new WaitUntil(() => false);
        // Player turn ends
        Debug.Log("Game Phase: Player turn ends"); 
        yield return new WaitUntil(() => false);
        
        // Enemies' turns execute sequentially on the model layer, but performed in parallel on the view layer
        // Calculate Enemies' turns;
        Debug.Log("Game Phase: Other objects' turn start & calculate actions");
        yield return new WaitUntil(() => false);
        // Perform actions;
        Debug.Log("Game Phase: Other objects' actions");
        yield return new WaitUntil(() => false);
        // Enemies' turns ends?
        Debug.Log("Game Phase: Other objects' turn ends");
        yield return new WaitUntil(() => false);
    }
    
    private IEnumerator _BattleLoop(){
        yield return null;
    }


    private void GameEnd(){
        StopAllCoroutines();
        // blah blah TODO
    }

    private void PlayerTurnStarts(){
        
    }
}

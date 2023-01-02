using System;
using System.Collections;
using System.Collections.Generic;
using Coroutines;
using UnityEngine;

public class GameManager : MonoBehaviour{
    private static GameManager _shared = null;
    public static GameManager Shared => _shared;

    public CoroutineManager coroutineManager;

    private void Awake(){
        if(_shared != null) Destroy(this);
        _shared = this;
    }

    private void Start(){
        PreGameSet();
    }

    private void PreGameSet(){
        
    }

    private void GameLoop(){
        
    }
}

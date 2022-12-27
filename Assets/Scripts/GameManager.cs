using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour{
    private static GameManager _shared = null;
    public static GameManager Shared => _shared;

    private void Awake(){
        if(_shared != null) Destroy(this);
        _shared = this;
    }
}

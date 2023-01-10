using System;
using Components.Grounds.Reducer;
using MVC;
using UnityEngine;
using Utility;

namespace Components.Grounds{
    public class GroundModel: Model{
        public event Action AfterTypeChanged;
        public int LastTurnNum = 0;
        public Vector2Int Position;

        private GroundType _type;
        public GroundType Type{
            set{
                _type = value;
                AfterTypeChanged?.Invoke();
            }
            get => _type;
        }
    }
}
using System;
using UnityEngine;

namespace Components.UI.Containers{
    public interface IContainer{
        public event Action SizeChanged;
        public event Action PositionChanged;

        Vector2 GetSize();
        void SetSize(Vector2 size);

        
        
        Vector2 GetPosition();
        void SetPosition(Vector2 position);
        
        
        Vector2 GetLocalPosition();
        void SetLocalPosition(Vector2 localPosition);

    }
}
using UnityEngine;

namespace Components.UI.Containers{
    [RequireComponent(typeof(RectTransform))]
    public class StackContainer: MonoBehaviour{
        public enum Axis{
            Vertical,
            Horizontal
        }

        public Axis axis;
        
        
    }
}
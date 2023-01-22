using UnityEngine;

namespace Components.UI{
    public abstract class UIComponent: MonoBehaviour{

        protected virtual void Awake(){
            UIManager.Shared.RegisterComponent(this);
        }
        public abstract void Hide();
        public abstract void Show();
    }
}
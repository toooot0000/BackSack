using UnityEngine;

namespace UI{
    public abstract class UIComponent: MonoBehaviour{
        private void Awake(){
            UIManager.Shared.RegisterComponent(this);
        }
        public abstract void Hide();
        public abstract void Show();
    }
}
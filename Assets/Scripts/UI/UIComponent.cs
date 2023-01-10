using UnityEngine;

namespace UI{
    public abstract class UIComponent: MonoBehaviour{
        private void Start(){
            UIManager.Shared.RegisterComponent(this);
        }
        public abstract void Hide();
        public abstract void Show();
    }
}
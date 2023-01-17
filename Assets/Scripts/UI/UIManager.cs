using System.Collections.Generic;
using UnityEngine;
using Utility;

namespace UI{
    public delegate void UINormalEvent(UIWindow uiWindow);

    public class UIManager : MonoBehaviour{

        public const float UITransitionTime = 0.2f;
        private const string ResourcesFolder = "Prefabs/UIs/";
        
        public static UIManager Shared;
        private readonly List<UIWindow> _uiList = new();
        private readonly List<UIComponent> _uiComponents = new();
        public GameObject uiContainer;

        public event UINormalEvent OnLoadUI; // right after load a ui;
        public event UINormalEvent OnOpenUI; // Right after ui is opened;
        public event UINormalEvent OnCloseUI; // Right after ui is closed;

        private void Awake(){
            if (Shared) Destroy(this);
            Shared = this;
        }

        private UIWindow OpenUI(string uiPrefabName){
            
            // Find the ui already opened
            var cur = _uiList.Find(uiBase => uiBase.name == uiPrefabName);
            if (cur != null){
                _uiList.Remove(cur);
                _uiList.Add(cur);
                return cur;
            }

            // Loading Prefab
            var ui = Resources.Load<GameObject>($"{ResourcesFolder}{uiPrefabName}");
            if (ui == null){
                Debug.LogError($"Unable to find UI resource: {uiPrefabName}");
                return null;
            }
            ui = Instantiate(ui, uiContainer.transform);
            cur = ui.GetComponent<UIWindow>();
            if (cur == null){
                Debug.LogError($"UI prefab doesn't have UIBase component! PrefabName = {uiPrefabName}");
                return null;
            }
            OnLoadUI?.Invoke(cur);

            _uiList.Add(cur);
            cur.OnClose += RemoveUI;
            StartCoroutine(CoroutineUtility.Delayed(() => {
                cur.Open();
                OnOpenUI?.Invoke(cur);
            }));
            return cur;
        }

        public T OpenUI<T>(string uiPrefabName, IUISetUpOptions<T> arg = null) where T : UIWindow{
            var ret = OpenUI(uiPrefabName) as T;
            if (ret == null) return null;
            arg?.ApplyOptions(ret);
            return ret;
        }

        private void RemoveUI(UIWindow uiWindow){
            OnCloseUI?.Invoke(uiWindow);
            _uiList.Remove(uiWindow);
        }
        
        public void RegisterComponent(UIComponent component){
            _uiComponents.Add(component);
        }

        public void HideAllComponents(){
            foreach (var comp in _uiComponents){
                comp.Hide();
            }
        }

        public void ShowAllComponents(){
            foreach (var comp in _uiComponents){
                comp.Show();
            }
        }

        public T GetUI<T>() where T : UIWindow{
            return _uiList.Find(b => b.GetType() == typeof(T)) as T;
        }

        public T GetUIComponent<T>() where T : UIComponent{
            return _uiComponents.Find(b => b.GetType() == typeof(T)) as T;
        }
    }
}
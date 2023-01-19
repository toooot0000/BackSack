using System.Collections.Generic;
using UnityEngine;
using Utility;

namespace UI{
    public delegate void UIWindowDelegate(UIWindow uiWindow);

    public class UIManager : MonoBehaviour{

        public const float UITransitionTime = 0.2f;
        private const string ResourcesFolder = "Prefabs/UIs/";
        
        public static UIManager Shared;
        private readonly List<UIWindow> _windows = new();
        private readonly List<UIComponent> _components = new();
        public GameObject windowContainer;

        public event UIWindowDelegate OnLoad; // right after load a ui;
        public event UIWindowDelegate OnOpen; // Right after ui is opened;
        public event UIWindowDelegate OnClose; // Right after ui is closed;

        private void Awake(){
            if (Shared) Destroy(this);
            Shared = this;
        }

        private UIWindow OpenUI(string uiPrefabName){
            
            // Find the ui already opened
            var cur = _windows.Find(uiBase => uiBase.name == uiPrefabName);
            if (cur != null){
                _windows.Remove(cur);
                _windows.Add(cur);
                return cur;
            }

            // Loading Prefab
            var ui = Resources.Load<GameObject>($"{ResourcesFolder}{uiPrefabName}");
            if (ui == null){
                Debug.LogError($"Unable to find UI resource: {uiPrefabName}");
                return null;
            }
            ui = Instantiate(ui, windowContainer.transform);
            cur = ui.GetComponent<UIWindow>();
            if (cur == null){
                Debug.LogError($"UI prefab doesn't have UIBase component! PrefabName = {uiPrefabName}");
                return null;
            }
            OnLoad?.Invoke(cur);

            _windows.Add(cur);
            cur.OnClose += RemoveUI;
            StartCoroutine(CoroutineUtility.Delayed(() => {
                cur.Open();
                OnOpen?.Invoke(cur);
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
            OnClose?.Invoke(uiWindow);
            _windows.Remove(uiWindow);
        }
        
        public void RegisterComponent(UIComponent component){
            _components.Add(component);
        }

        public void HideAllComponents(){
            foreach (var comp in _components){
                comp.Hide();
            }
        }

        public void ShowAllComponents(){
            foreach (var comp in _components){
                comp.Show();
            }
        }

        public T GetUI<T>() where T : UIWindow{
            return _windows.Find(b => b.GetType() == typeof(T)) as T;
        }

        public T GetUIComponent<T>() where T : UIComponent{
            return _components.Find(b => b.GetType() == typeof(T)) as T;
        }
    }
}
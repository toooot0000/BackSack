using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Codice.Utils;
using Components.UI.Attributes;
using UnityEngine;
using Utility;

namespace Components.UI{
    public delegate void UIWindowDelegate(UIWindow uiWindow);

    public class UIManager : MonoBehaviour{

        public const float UITransitionTime = 0.2f;
        private const string ResourcesFolder = "Prefabs/UIs/";
        
        public static UIManager Shared;
        private readonly List<UIWindow> _windows = new();
        private readonly List<UIComponent> _components = new();
        public GameObject windowContainer;

        public event UIWindowDelegate WindowLoaded; // right after load a ui;

        private void Awake(){
            if (Shared) Destroy(this);
            Shared = this;
        }

        private UIWindow LoadUI(string uiPrefabName){
            // Loading Prefab
            var ui = Resources.Load<GameObject>($"{ResourcesFolder}{uiPrefabName}");
            if (ui == null){
                Debug.LogError($"Unable to find UI resource: {uiPrefabName}");
                return null;
            }
            ui = Instantiate(ui, windowContainer.transform);
            var ret = ui.GetComponent<UIWindow>();
            if (ret == null){
                Debug.LogError($"UI prefab doesn't have UIBase component! PrefabName = {uiPrefabName}");
                return null;
            }
            return ret;
        }

        public T Open<T>(UIOpenOption<T> arg = null) where T : UIWindow{
            if (arg != null && arg.ReuseExisted) {
                var reused = TryReuseWindow(arg);
                if (reused != null) return reused;
            }
            var ret = LoadUI(GetPrefabName(typeof(T))) as T;
            if (ret == null) return null;
            _windows.Add(ret);
            ret.Closed += OnWindowClosed;
            OpenWindow(ret, arg?.BeforeOpen, arg);
            return ret;
        }

        private void OnWindowClosed<T>(T window) where T: UIWindow {
            window.gameObject.SetActive(false);
            foreach (var w in _windows) {
                if (w.gameObject.activeSelf) return;
            }
            ShowAllComponents();
        }

        public void CloseActive() {
            // TODO
        }

        private T TryReuseWindow<T>(UIOpenOption<T> arg) where T : UIWindow {
            var ret = _windows.OfType<T>().FirstOrDefault();
            if (ret == null) return null;
            ret.gameObject.SetActive(true);
            ret.enabled = true;
            ret.Closed += OnWindowClosed;
            OpenWindow(ret, arg?.BeforeOpen, arg, arg?.Reopen ?? true);
            return ret;
        }

        private void OpenWindow<T>(
            T window, 
            Action<T> openCallback, 
            IUIWindowOptions<T> options, 
            bool open = true
        ) where T: UIWindow{
            HideAllComponents();
            Elevate(window);
            options?.ApplyOptions(window);
            openCallback?.Invoke(window);
            if(open) window.Open();
        }

        private static string GetPrefabName(ICustomAttributeProvider uiWindowType) {
            var attr = (Prefab[])uiWindowType.GetCustomAttributes(typeof(Prefab), false);
            if (attr.Length == 0) {
                throw new Exception($"UIWindow does not provide a prefab name! Type: {uiWindowType}");
            }

            return attr.FirstOrDefault()?.Name;
        }

        private void Elevate(Component window){
            window.transform.SetAsLastSibling();
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

        public T Open<T>(UIOpenOption<T> arg) where T : UIWindow{
            var ret = _windows.OfType<T>().FirstOrDefault();
            if (ret != null){
                Elevate(ret);
                if (ret.gameObject.activeSelf) return ret;
                ret.gameObject.SetActive(true);
                ret.Open();
                return ret;
            }
            ret = LoadUI(GetPrefabName(typeof(T))) as T;
            if (ret == null) return null;
            _windows.Add(ret);

            if (arg != null){
                arg.WindowOptions?.ApplyOptions(ret);
                if(arg.HideComponents) HideAllComponents();
            }
            
            WindowLoaded?.Invoke(ret);
            ret.Closed += RemoveUI;
            ret.Open();
            return ret;
        }


        public T Open<T>(bool hideAllComps = true) where T : UIWindow{
            return Open<T>(new UIOpenOption<T>(null){
                HideComponents = true
            });
        }

        private static string GetPrefabName(ICustomAttributeProvider uiWindowType){
            var attr = (UIPrefab[])uiWindowType.GetCustomAttributes(typeof(UIPrefab), false);
            if (attr.Length == 0){
                throw new Exception($"UIWindow does not provide a prefab name! Type: {uiWindowType}");
            }
            return attr.FirstOrDefault()?.Name;
        }
        
        private static void RemoveUI(UIWindow uiWindow){
            uiWindow.gameObject.SetActive(false);
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
using System;

namespace Components.UI{
    public class UIOpenOption<T> where T: UIWindow{
        public readonly IUIWindowOptions<T> WindowOptions;
        public bool HideComponents = true;
        // public Action<>
        public UIOpenOption(IUIWindowOptions<T> windowOptions){
            WindowOptions = windowOptions;
        }
    }
}
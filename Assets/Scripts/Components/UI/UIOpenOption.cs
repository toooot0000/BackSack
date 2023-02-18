using System;

namespace Components.UI{
    public abstract class UIOpenOption<T>: IUIWindowOptions<T> where T: UIWindow{
        public bool ReuseExisted = false;
        public bool Reopen = false;
        public Action<T> BeforeOpen = null;
        public abstract void ApplyOptions(T window);
    }
}
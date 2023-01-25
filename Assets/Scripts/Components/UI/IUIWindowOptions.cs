namespace Components.UI{
    public interface IUIWindowOptions<in T> where T: UIWindow{
        void ApplyOptions(T window);
    }
}
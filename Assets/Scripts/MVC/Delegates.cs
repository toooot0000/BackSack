namespace MVC{
    
    public delegate void ModelDelegate<in T>(T model) where T : Model;
    public delegate void ManagerDelegate<TModel, TView>(Manager<TModel, TView> manager) where TModel : Model where TView: IViewWithType<TModel>;
}
namespace MVC{
    
    public delegate void ModelDelegate<in T>(T model) where T : Model;
    public delegate void ManagerDelegate<TModel, TView>(Controller<TModel, TView> controller) where TModel : Model where TView: IViewWithType<TModel>;
}
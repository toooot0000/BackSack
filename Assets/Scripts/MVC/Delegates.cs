namespace MVC{
    
    public delegate void ModelDelegate<in T>(T model) where T : Model;
}
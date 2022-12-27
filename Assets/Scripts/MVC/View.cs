using UnityEngine;

namespace MVC{
    public interface IView{ }

    public interface IViewWithType<TModel> : IView where TModel : Model{ }
}


using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MVC;
using UnityEngine;

namespace MVC{

    public interface IController{
        public static readonly List<IController> Controllers = new();
        public static void RegisterManager(IController controller){
            Controllers.Add(controller);
        }

        public static T GetController<T>() where T : IController{
            return Controllers.Capacity == 0 ? default : (T)Controllers.First(m => m is T);
        }
    }

    public abstract class Controller<TModel, TView>: MonoBehaviour, IController where TModel : IModel where TView: IViewWithType<TModel>{
        private TModel _model;
        protected TModel Model{
            private set{
                if(this is IBeforeSetModel before) before.BeforeSetModel();
                _model = value;
                if(this is IAfterSetMode after) after.AfterSetModel();
            }
            get => _model;
        }
        public TView view;

        protected virtual void Awake(){
            IController.RegisterManager(this);
        }

        public void SetModel(TModel model) => Model = model;
        public virtual TModel GetModel() => Model;
        public virtual TView GetView() => view;
    }
    
    
    public interface IBeforeSetModel{
        void BeforeSetModel();
    }

    public interface IAfterSetMode{
        void AfterSetModel();
    }
}
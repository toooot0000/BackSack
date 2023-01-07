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

    public interface IControllerOfView<out T> : IController where T : IView{
        public T GetView();
    }

    public abstract class Controller: MonoBehaviour, IController{
        private IModel _model;
        protected IModel Model{
            private set{
                BeforeSetModel();
                _model = value;
                AfterSetModel();
            }
            get => _model;
        }

        protected virtual void Awake(){
            IController.RegisterManager(this);
        }
        
        public void SetModel(IModel model) => Model = model;
        public T GetModel<T>() where T: IModel => (T)Model;
        protected virtual void BeforeSetModel(){ }
        protected virtual void AfterSetModel(){ }
    }
}
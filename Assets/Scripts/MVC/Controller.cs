using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MVC;
using UnityEngine;

namespace MVC{

    public interface IController{

        // public T GetModel<T>() where T: IModel;
        
        private static readonly List<IController> Controllers = new();
        internal static void RegisterManager(IController controller){
            Controllers.Add(controller);
        }

        public static T GetController<T>() where T : IController{
            return Controllers.Capacity == 0 ? default : (T)Controllers.FirstOrDefault(m => m is T);
        }

        public static IEnumerable<T> GetControllers<T>() where T : IController{
            return Controllers.Capacity == 0 ? default : Controllers.OfType<T>();
        }
    }
    
    public abstract class Controller: MonoBehaviour, IController{
        // private IModel _model;
        // protected IModel Model{
        //     private set{
        //         BeforeSetModel();
        //         _model = value;
        //         AfterSetModel();
        //     }
        //     get => _model;
        // }

        // public IView view;

        protected virtual void Awake(){
            IController.RegisterManager(this);
        }
        
        // public void SetModel(IModel model) => Model = model;
        // public T GetModel<T>() where T: IModel => (T)Model;
        // protected virtual void BeforeSetModel(){ }
        // protected virtual void AfterSetModel(){ }
    }
}
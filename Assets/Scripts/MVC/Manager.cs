using System;
using System.Collections.Generic;
using System.Linq;
using MVC;
using UnityEngine;

namespace MVC{

    public interface IManager{
        public static readonly List<IManager> Managers = new();
        public static void RegisterManager(IManager manager){
            Managers.Add(manager);
        }

        public static T GetManager<T>() where T : IManager{
            return Managers.Capacity == 0 ? default : (T)Managers.First(m => m is T);
        }
    }

    public abstract class Manager<TModel, TView>: MonoBehaviour, IManager where TModel : Model where TView: IViewWithType<TModel>{
        public TModel model;
        public TView view;

        protected virtual void Awake(){
            IManager.RegisterManager(this);
        }
    }
    
    public interface IBeforeSetModel{
        void BeforeSetModel();
    }

    public interface IAfterSetMode{
        void AfterSetModel();
    }
}

public static class ManagerExtension{
    public static void Bind<T>(this Manager<T,IViewWithType<T>> manager, T model) where T : Model{
        if(manager is IBeforeSetModel before) before.BeforeSetModel();
        manager.model = model;
        if(manager is IAfterSetMode after) after.AfterSetModel();
    }
}
using System;

namespace Components.UI.Attributes{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class Prefab: Attribute{
        public readonly string Name;

        public Prefab(string name){
            Name = name;
        }
    }
}
using System;

namespace Components.UI.Attributes{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class UIPrefab: Attribute{
        public readonly string Name;

        public UIPrefab(string name){
            Name = name;
        }
    }
}
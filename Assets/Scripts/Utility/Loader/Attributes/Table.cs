using System;

namespace Utility.Loader.Attributes{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
    public class Table: Attribute{
        public string Name;

        public Table(string name){
            Name = name;
        }
    }
}
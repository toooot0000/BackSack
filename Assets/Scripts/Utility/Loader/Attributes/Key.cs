using System;

namespace Utility.Loader.Attributes{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class Key: Attribute{
        public string Name;

        public Key(string name){
            Name = name;
        }
    }
}
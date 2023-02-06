using System;

namespace Utility.Loader.Attributes{
    public class FromConfig: Attribute{
        public string Key;

        public FromConfig(string key){
            Key = key;
        }
    }
}
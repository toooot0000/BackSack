using System;

namespace Utility.Loader.Attributes{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class TableAndKey: Attribute{
        public string TableName;
        public string Key;

        public TableAndKey(string tableName, string key){
            TableName = tableName;
            Key = key;
        }
    }
}
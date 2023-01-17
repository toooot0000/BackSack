using System;
using System.Linq;
using System.Reflection;
using MVC;

namespace Utility.Loader.Attributes{
    public static class Utility{
        public static void SetUp<T>(this T model) where T: Model{
            var type = typeof(T);
            SetUp(model, type);
        }
        
        public static void SetUp(this Model model, Type type){
            var tableNameAttr = (Table)type.GetCustomAttribute(typeof(Table));
            var tableName = tableNameAttr?.Name;
            if (model.ID is not { } id) return;
            foreach (var propertyInfo in type.GetProperties()){
                var val = GetValue(propertyInfo, id, tableName);
                if(val == null) continue;
                propertyInfo.SetValue(model, val);
            }

            foreach (var fieldInfo in type.GetFields()){
                var val = GetValue(fieldInfo, id, tableName);
                if(val == null) continue;
                fieldInfo.SetValue(model, val);
            }
        }

        private static object GetValue(MemberInfo info, int id, string globalTableName){
            var configAttr = (FromConfig)info.GetCustomAttribute(typeof(FromConfig));
            if (configAttr != null) return ConfigLoader.GetConfig(configAttr.Key);
            
            var keyNameAttr = (Key)info.GetCustomAttribute(typeof(Key));
            var tableKeyAttr = (TableAndKey)info.GetCustomAttribute(typeof(TableAndKey));
            var curTableName = tableKeyAttr?.TableName ?? globalTableName;
            if (curTableName == null) return null;
            var keyName = keyNameAttr?.Name ?? tableKeyAttr?.Key;
            if (string.IsNullOrEmpty(keyName)) return null;
            return ConfigLoader.TryGetValue(curTableName, id, keyName);
        }
    }
}
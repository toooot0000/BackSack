using System;
using MVC;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using UnityEngine;


namespace MVC{

    public interface IModel{ }
    
    public class IdNotSetException: Exception{}
    
    [Serializable]
    public abstract class Model: IModel{
        public int? ID = null;
        public string Name;
        public string Desc;

        private static JsonSerializerSettings _jsonSerializerSetting = new JsonSerializerSettings{
            MissingMemberHandling = MissingMemberHandling.Ignore,
            ObjectCreationHandling = ObjectCreationHandling.Replace,
        };

        public virtual string ToJson(Formatting formatting = Formatting.None){
            return JsonConvert.SerializeObject(this, formatting, _jsonSerializerSetting);
        }

        public static T ToModel<T>(string json){
            return JsonConvert.DeserializeObject<T>(json, _jsonSerializerSetting);
        }
    }
}
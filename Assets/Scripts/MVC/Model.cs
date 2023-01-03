using System;
using MVC;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using UnityEngine;


namespace MVC{

    public interface IModel{ }
    
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

        public static T ToModel<T>(string jsonText) where T: Model{
            return JsonConvert.DeserializeObject<T>(jsonText, _jsonSerializerSetting);
        }
    }
}
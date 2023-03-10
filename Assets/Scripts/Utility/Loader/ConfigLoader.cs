using System;
using System.Collections.Generic;
using System.Linq;
using MVC;
using UnityEngine;
using UnityEngine.UIElements;

namespace Utility.Loader{
    using TableEntry = Dictionary<string, object>;
    using Table = Dictionary<int, Dictionary<string, object>>;

    public static class ConfigLoader{
        private const string EmptyString = "__empty";
        private static readonly Dictionary<string, Table> PrevLoaded = new();
        private static readonly Dictionary<string, object> ConfigTable = new();

        public static string ConfigFolderPath = "Configs/";

        public static Table GetTable(string filename){
            if (!filename.StartsWith(ConfigFolderPath)) filename = $"{ConfigFolderPath}{filename}";
            if (PrevLoaded.ContainsKey(filename)) return PrevLoaded[filename];
            var file = Resources.Load<TextAsset>(filename);
            if (file == null){
                Debug.LogError($"Can't find resources with name {filename}");
                return null;
            }

            var lines = ParseLine(file.text, '\n');
            if (lines.Length < 3){
                Debug.LogError("Table contents error, less than 3 lines!");
                return null;
            }

            Dictionary<int, Dictionary<string, object>> ret = new();
            var keys = ParseLine(lines[0], ',');
            keys = keys.Select(str => str.ToLower()).ToArray();
            var idInd = 0;
            while (idInd < keys.Length && !keys[idInd].ToLower().Equals("id")) idInd++;
            if (idInd >= keys.Length){
                Debug.LogError("Table doesn't have field 'id'!");
                return null;
            }

            var types = ParseLine(lines[2], ',');
            for (var i = 3; i < lines.Length; i++){
                var line = ParseLine(lines[i], ',');
                var id = (int)Math.Round(float.Parse(line[idInd]));
                ret[id] = new TableEntry();
                for (var j = 0; j < line.Length; j++){
                    var key = keys[j];
                    var type = types[j];
                    ret[id][key] = ParseTypedValue(type, line[j]);
                }
            }

            PrevLoaded[filename] = ret;
            return ret;
        }

        public static object ParseTypedValue(string typename, string val){
            return typename.ToLower() switch{
                "int" => IntUtility.ParseString(val),
                "string" => val.Equals(EmptyString) ? "" : val,
                "float" => float.Parse(val),
                _ => val
            };
        }

        private static string[] ParseLine(string lines, char deli){
            lines = lines.Replace("\r", "");
            if (!lines.EndsWith(deli)) lines += deli;
            List<string> ret = new();
            var inQuote = false;
            var lastInd = 0;
            for (var i = 0; i < lines.Length; i++)
                switch (lines[i]){
                    case var x when x == deli && !inQuote:
                        ret.Add(lines.Substring(lastInd, i - lastInd));
                        lastInd = i + 1;
                        break;
                    case '\'':
                    case '\"':
                        inQuote = !inQuote;
                        break;
                }

            return ret.ToArray();
        }

        public static TableEntry TryGetEntry(string filename, int id){
            var table = GetTable(filename);
            if (table == null) return null;
            if (!table.ContainsKey(id)){
                Debug.LogError($"Wrong id in table '{filename}', id = {id}");
                return null;
            }
            return table[id];
        }

        public static object TryGetValue(string filename, int id, string key){
            return TryGetEntry(filename, id)?[key];
        }

        public static object GetConfig(string configKey){
            if (ConfigTable.Count != 0){
                if (ConfigTable.Keys.Contains(configKey)) return ConfigTable[configKey];

                Debug.LogError($"No config key: {configKey}!");
                return null;
            }

            var config = GetTable("Configs/configs");
            if (config == null){
                Debug.LogError("No configs.csv! Did you forget to download config tables?");
                return null;
            }

            foreach (var pair in config){
                var key = pair.Value["key"] as string;
                var type = pair.Value["type"] as string;
                var val = ParseTypedValue(type, pair.Value["value"] as string);
                ConfigTable[key!] = val!;
            }

            if (ConfigTable.Keys.Contains(configKey)){
                return ConfigTable[configKey];
            }

            Debug.LogError($"No config key: {configKey}!");
            return null;
        }
    }

    public static class ModelExtension{
        private static Table _currenTable = null;
        public static T GetField<T, TSrc>(this Model model, string key, Func<TSrc, T> converter){
            if (model.ID == null) return default(T);
            var entry = _currenTable[model.ID.Value];
            if (!entry.ContainsKey(key)) return default(T);
            return converter((TSrc)entry[key]);
        }

        public static T GetField<T>(this Model model, string key, Func<T, T> converter = null){
            return GetField<T, T>(model, key, converter ?? (arg => arg) );
        }

        public static T GetField<TSrc, T>(this Model model, string tableName, string key, Func<TSrc, T> converter){
            if (model.ID == null) return default;
            var ret = (TSrc)ConfigLoader.TryGetEntry(tableName, model.ID.Value)[key];
            return converter.Invoke(ret);
        }

        public static T GetField<T>(this Model model, string tableName, string key, Func<T, T> converter = null){
            return GetField<T, T>(model, tableName, key, converter ?? (x => x));
        }

        public static bool StartFieldSetting(this Model model, string tableName){
            if (model.ID == null) return false;
            _currenTable = ConfigLoader.GetTable(tableName);
            if (_currenTable.ContainsKey(model.ID.Value)) return true;
            _currenTable = null;
            return false;
        }

        public static void EndFieldSetting(this Model model){
            _currenTable = null;
        }
        
    }
}
using Components.Stages.Templates;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace StageEditor.MapConverters{
    public abstract class MapConverter: MonoBehaviour{
        public Tilemap map;
        public abstract void ReadFromTemplate(StageTemplate template);
        public abstract void WriteToTemplate(StageTemplate template);

        public void ClearAll(){
            map.ClearAllTiles();
        }

        public abstract void Clear();
        public virtual void Reload(){ }
    }
}
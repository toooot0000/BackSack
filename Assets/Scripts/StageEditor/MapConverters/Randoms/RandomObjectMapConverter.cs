using System.Collections.Generic;
using System.Linq;
using Components.Stages.Templates;
using StageEditor.Extensions;
using StageEditor.Tiles;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace StageEditor.MapConverters.Randoms{
    public class RandomObjectMapConverter: MapConverter{

        public string containerTag = "random-container";
        public GameObject containerPrefab;
        
        private readonly List<RandomObjectContainer> _containers = new();

        public RandomObjectPosition randomTile;

        public override void ReadFromTemplate(StageTemplate template){
            ClearAllContainers();
            // generate RandomPosition tiles
            foreach (var group in template.randomGroups){
                foreach (var info in group.infos){
                    map.SetTile(info.position.ToVector3Int(), randomTile);
                    var ctn = Instantiate(containerPrefab, transform, false);
                    ctn.transform.localPosition = map.GetCellCenterLocal(info.position.ToVector3Int());
                    ctn.name = $"[{info.position.x.ToString()}, {info.position.y.ToString()}]";
                    ctn.tag = containerTag;
                    var comp = ctn.GetComponent<RandomObjectContainer>();
                    comp.info = info.objectInfo;
                    comp.weight = info.weight;
                    comp.groupId = group.groupId;
                    comp.position = info.position;
                    _containers.Add(comp);
                }
            }
        }

        public override void WriteToTemplate(StageTemplate template){
            if (_containers.Count == 0){
                Debug.LogError("No containers! Generate containers and input the weights there!");
                return;
            }
            var groupById = new Dictionary<string, List<RandomObjectContainer>>();
            foreach (var con in _containers){
                if (!groupById.ContainsKey(con.groupId)) groupById[con.groupId] = new();
                groupById[con.groupId].Add(con);
            }

            var ret = new List<RandomObjectGroup>();
            foreach (var pair in groupById){
                var groupItem = new RandomObjectGroup{
                    groupId = pair.Key,
                    infos = pair.Value.Select(ctn => ctn.ToInfo()).ToArray()
                };
                ret.Add(groupItem);
            }
            template.randomGroups = ret.ToArray();
        }

        public override void Clear(){
            ClearAllContainers();
            map.ClearAllTiles();
            Debug.Log("Random object map cleared!");
        }

        public void CreateAllContainers(){
            if (_containers.Count > 0){
                Debug.LogError("Containers already exist! If you want to generate new containers, first clear all containers!");
                return;
            }
            map.ProcessTile<RandomObjectPosition>((i, j, tile) => {
                var ctn = Instantiate(containerPrefab, transform, false);
                var component = ctn.GetComponent<RandomObjectContainer>();
                if (component == null){
                    DestroyImmediate(ctn);
                    return;
                }
                ctn.name = $"[{i.ToString()}, {j.ToString()}]";
                ctn.transform.localPosition = map.GetCellCenterLocal(new(i, j));
                ctn.tag = containerTag;
                component.position = new(i, j);
                _containers.Add(component);
            });
        }

        public void ClearAllContainers(){
            foreach (var container in _containers){
                if(!container.gameObject.IsDestroyed()) DestroyImmediate(container.gameObject);
            }
            _containers.Clear();

            foreach (var o in GameObject.FindGameObjectsWithTag(containerTag)){
                DestroyImmediate(o);
            }
        }

        public void ClearAllTiles(){
            map.ProcessTile<RandomObjectPosition>((i, j, tile) => {
                map.SetTile(new(i, j), null);
            });
        }

        public void RelinkContainers(){
            var count = 0;
            foreach (var randomObjectContainer in GetComponentsInChildren<RandomObjectContainer>()){
                if(_containers.Contains(randomObjectContainer)) continue;
                _containers.Add(randomObjectContainer);
                count++;
            }

            Debug.Log($"Relinked {count} containers!");
        }
    }
}
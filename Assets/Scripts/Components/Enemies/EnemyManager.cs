using System.Collections.Generic;
using System.Linq;
using Components.Stages;
using MVC;
using UnityEngine;
using Utility.Extensions;

namespace Components.Enemies{
    public class EnemyManager : MonoBehaviour{
        public Stage stage;
        public GameObject enemyPrefab;
        public string enemyPrefabResPath;
        [HideInInspector] public List<Enemy> enemyControllers = new();
        private bool isSorted = false;
        
        private Enemy CreateInstance(){
            var ret =  Instantiate(enemyPrefab, transform).GetComponent<Enemy>();
            ret.stage = stage;
            return ret;
        }

        public Enemy AddEnemy(EnemyModel enemyModel){
            if (enemyModel == null) return null;
            var newEnemy = enemyControllers.FirstNotActiveOrNew(CreateInstance);
            newEnemy.stage = stage;
            newEnemy.Model = enemyModel;
            newEnemy.Manager = this;
            newEnemy.StagePositionUpdated += () => { isSorted = true; };
            isSorted = false;
            return newEnemy;
        }

        public IEnumerable<Enemy> GetAllActiveEnemies(){
            foreach (var enemy in enemyControllers){
                if (enemy.gameObject.activeSelf && enemy.enabled){
                    yield return enemy;
                }
            }
        }

        public IEnumerable<Enemy> GetSortedActiveEnemies(){
            if (!isSorted){
                enemyControllers.Sort((l, r) => {
                    var lPos = l.GetStagePosition();
                    var rPos = r.GetStagePosition();
                    return lPos.x * lPos.y - rPos.x * rPos.y;
                });
                isSorted = true;
            }
            return GetAllActiveEnemies();
        }
    }
}
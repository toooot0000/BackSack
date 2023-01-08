using System.Collections.Generic;
using Components.Stages;
using UnityEngine;
using Utility.Extensions;

namespace Components.Enemies{
    public class EnemyManager : MonoBehaviour{
        public StageController stageController;
        public GameObject enemyPrefab;
        [HideInInspector] public List<EnemyController> enemyControllers = new();  
        
        private EnemyController CreateInstance(){
            var ret =  Instantiate(enemyPrefab, transform).GetComponent<EnemyController>();
            ret.stageController = stageController;
            return ret;
        }
        public EnemyController AddEnemy(Enemy enemy){
            if (enemy == null) return null;
            var newEnemy = enemyControllers.FirstNotActiveOrNew(CreateInstance);
            newEnemy.stageController = stageController;
            newEnemy.SetModel(enemy);
            return newEnemy;
        }
    }
}
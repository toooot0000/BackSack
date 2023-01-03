using System.Collections.Generic;
using Entities.Stages;
using Models.TileObjects;
using UnityEngine;
using Utility.Extensions;

namespace Entities.Enemies{
    public class EnemyManager : MonoBehaviour{
        public StageController stageController;
        public GameObject enemyPrefab;
        [HideInInspector] public List<EnemyController> enemyControllers = new();  
        
        private EnemyController CreateInstance(){
            return Instantiate(enemyPrefab, transform).GetComponent<EnemyController>();
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
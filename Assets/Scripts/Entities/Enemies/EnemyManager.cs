using System.Collections.Generic;
using Models.TileObjects;
using UnityEngine;
using Utility.Extensions;

namespace Entities.Enemies{
    public class EnemyManager : MonoBehaviour{
        public GameObject enemyPrefab;
        [HideInInspector] public List<EnemyController> enemyControllers = new();  
        
        private EnemyController CreateInstance(){
            return Instantiate(enemyPrefab, transform).GetComponent<EnemyController>();
        }


        public void AddEnemy(Enemy enemy){
            if (enemy == null) return;
            var newEnemy = enemyControllers.FirstNotActiveOrNew(CreateInstance);
            newEnemy.SetModel(enemy);
        }
    }
}
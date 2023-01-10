using System.Collections.Generic;
using Components.Stages;
using UnityEngine;
using Utility.Extensions;

namespace Components.Enemies{
    public class EnemyManager : MonoBehaviour{
        public Stage stage;
        public GameObject enemyPrefab;
        [HideInInspector] public List<Enemy> enemyControllers = new();  
        
        private Enemy CreateInstance(){
            var ret =  Instantiate(enemyPrefab, transform).GetComponent<Enemy>();
            ret.stage = stage;
            return ret;
        }
        public Enemy AddEnemy(EnemyModel enemyModel){
            if (enemyModel == null) return null;
            var newEnemy = enemyControllers.FirstNotActiveOrNew(CreateInstance);
            newEnemy.stage = stage;
            newEnemy.SetModel(enemyModel);
            return newEnemy;
        }
    }
}
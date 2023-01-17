using System.Collections.Generic;
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
        
        private Enemy CreateInstance(){
            var ret =  Instantiate(enemyPrefab, transform).GetComponent<Enemy>();
            ret.stage = stage;
            return ret;
        }

        private Enemy CreateInstance(int id){
            var prefab = Resources.Load<GameObject>($"{enemyPrefabResPath}/{id.ToString()}");
            if (prefab == null) return null;
            var ret = Instantiate(prefab, transform).GetComponent<Enemy>();
            ret.stage = stage;
            return ret;
        }
        
        public Enemy AddEnemy(EnemyModel enemyModel){
            if (enemyModel == null) return null;
            var newEnemy = enemyControllers.FirstNotActiveOrNew(CreateInstance);
            newEnemy.stage = stage;
            newEnemy.Model = enemyModel;
            return newEnemy;
        }
    }
}
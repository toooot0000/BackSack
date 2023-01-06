using System;
using System.Collections;
using Entities.Enemies;
using Models.TileObjects;
using Models.TileObjects.Enemies;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.PlayerTests.Test_EnemyManager{
    public class EnemyManagerTest{

        [UnityTest]
        public IEnumerator Test_AddEnemy_1(){
            yield return new MonoBehaviourTest<Test_AddEnemy_1>();
        }
        
        [UnityTest]
        public IEnumerator Test_AddEnemy_null(){
            yield return new MonoBehaviourTest<Test_AddEnemy_null>();
        }
    }

    internal class Test_AddEnemy_1 : MonoBehaviour, IMonoBehaviourTest{
        public bool IsTestFinished{ private set; get; } = false;
        private void Start(){
            var prefab = Resources.Load<GameObject>("Test_enemyManager");
            Assert.NotNull(prefab);
            var enemyManager = Instantiate(prefab, transform).GetComponent<EnemyManager>();
            Assert.NotNull(enemyManager);
            Assert.NotNull(enemyManager.enemyPrefab);
            enemyManager.AddEnemy(Enemy.MakeEnemy(0));
            Assert.AreEqual(1, enemyManager.enemyControllers.Count, "Controller count should be 1");
            IsTestFinished = true;
        }
    }

    internal class Test_AddEnemy_null : MonoBehaviour, IMonoBehaviourTest{
        public bool IsTestFinished{ set; get; } = false;

        private void Start(){
            var prefab = Resources.Load<GameObject>("Test_enemyManager");
            Assert.NotNull(prefab);
            var enemyManager = Instantiate(prefab, transform).GetComponent<EnemyManager>();
            Assert.NotNull(enemyManager);
            Assert.NotNull(enemyManager.enemyPrefab);
            enemyManager.AddEnemy(null);
            Assert.AreEqual(0, enemyManager.enemyControllers.Count, "Controller count should be 0 if enemy is null");
            IsTestFinished = true;
        }
    }
}
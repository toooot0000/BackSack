using System.Collections;
using System.Linq;
using Entities.Enemies;
using Entities.Stages;
using Models;
using Models.TileObjects;
using MVC;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Tests.PlayerTests.Test_Stages{
    public class Test_StageController{
        
        [SetUp]
        public void SetUp(){
            SceneManager.LoadScene("Main", LoadSceneMode.Single);
        }
        
        [UnityTest]
        public IEnumerator Test_AfterSetModel_Normal(){
            yield return new WaitForSeconds(0.1f);
            var grid = GameObject.Find("Grid");
            Assert.That(grid != null);
            var stageManager = grid.GetComponent<StageController>();
            Assert.That(stageManager != null);
            var textAsset = Resources.Load<TextAsset>("Stages/stage-test-0");
            var stage = Model.ToModel<Stage>(textAsset.text);
            Assert.That(stage != null);
            var enemyManager = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
            Assert.That(enemyManager!=null);

            stageManager.SetModel(stage);
            
            Assert.That(stage.Floors[1,1].TileObject != null);
            var enemy = stage.Floors[1, 1].TileObject;
            Assert.That(enemy, Is.InstanceOf<Enemy>());
            var expectedPosition = stageManager.StagePositionToWorldPosition(new(1, 1));
            Assert.That(expectedPosition == enemyManager.enemyControllers.First().transform.position);
            Assert.That(enemyManager.transform.childCount > 0);
        }
    }
}
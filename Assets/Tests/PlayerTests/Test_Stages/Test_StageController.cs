using System.Collections;
using System.Linq;
using Components.Enemies;
using Components.Stages;
using Components.TileObjects;
using Components.TreasureBoxes;
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
            var stageManager = grid.GetComponent<Stage>();
            Assert.That(stageManager != null);
            var textAsset = Resources.Load<TextAsset>("Stages/stage-test-0");
            var stage = Model.FromJsonString<StageModel>(textAsset.text);
            Assert.That(stage != null);
            var enemyManager = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
            Assert.That(enemyManager!=null);

            stageManager.SetModel(stage);
            
            Assert.That(stage.Floors[1,1].TileObject != null);
            var enemy = stage.Floors[1, 1].TileObject;
            Assert.That(enemy, Is.InstanceOf<EnemyModel>());
            var expectedPosition = stageManager.StagePositionToWorldPosition(new(1, 1));
            Assert.That(expectedPosition == enemyManager.enemyControllers.First().transform.position);
            Assert.That(enemyManager.transform.childCount > 0);
        }
        
        [UnityTest]
        public IEnumerator Test_AfterSetModel_NormalBox(){
            yield return new WaitForSeconds(0.1f);
            var grid = GameObject.Find("Grid");
            Assert.That(grid != null);
            var stageManager = grid.GetComponent<Stage>();
            Assert.That(stageManager != null);
            var textAsset = Resources.Load<TextAsset>("Stages/stage-test-1");
            var stage = Model.FromJsonString<StageModel>(textAsset.text);
            Assert.That(stage != null);
            var enemyManager = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
            Assert.That(enemyManager!=null);
            
            
            stageManager.SetModel(stage);
            var treasureBox = grid.GetComponentInChildren<TreasureBox>();
            Assert.That(treasureBox != null);
            Assert.That(stage.Floors[3,2].TileObject != null);
            Assert.That(stage.Floors[4,1].TileObject != null);
        }
    }
}
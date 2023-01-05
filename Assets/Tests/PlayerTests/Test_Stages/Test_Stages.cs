using Models;
using Models.Stage;
using MVC;
using NUnit.Framework;
using UnityEngine;

namespace Tests.PlayerTests.Test_Stages{
    public class Test_Stages{
        [Test]
        public void Test_Normal(){
            var textAsset = Resources.Load<TextAsset>("Stages/stage-test-0");
            var stage = Model.FromJsonString<Stage>(textAsset.text);
            Assert.That(stage != null);
            Assert.That(stage.Width == 3);
            Assert.That(stage.Height == 3);
            Assert.That(stage.Floors[1,1].TileObjectType == TileObjectType.Enemy);
        }
    }
}
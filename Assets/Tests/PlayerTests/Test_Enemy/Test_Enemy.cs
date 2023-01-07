using Components.Enemies;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.PlayerTests.Test_Enemy{
    public class Test_Enemy{
        [Test]
        public void Test_MakeEnemy_normal(){
            var enemy = Enemy.MakeEnemy(0);
            Assert.NotNull(enemy);
            Assert.NotNull(enemy.ID);
            Assert.AreEqual(0, enemy.ID.Value);
            Assert.AreEqual("Slime", enemy.Name, "Enemy whose id is 0 has name \'Slime\'");
            Assert.IsNotNull("Squishy~", enemy.Desc, "Enemy whose id is 0 has name \'Squishy~\'");
        }
        
        
        [Test]
        public void Test_MakeEnemy_WrongId(){
            var enemy = Enemy.MakeEnemy(-1);
            Assert.IsNull(enemy);
            LogAssert.Expect(LogType.Error,"Invalid Id -1!");
        }
    }
}
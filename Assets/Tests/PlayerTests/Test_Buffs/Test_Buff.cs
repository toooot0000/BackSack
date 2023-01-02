using System.Collections.Generic;
using Models;
using Models.Buffs;
using Models.Damageable;
using NUnit.Framework;

namespace Tests.PlayerTests.Test_Buff{
    public class Test_Buff{
        private class TestBuffHolder: IBuffHolder{
            public int HealthPoint{ get; set; }
            public int ShieldPoint{ get; set; }
            public int DefendPoint{ get; set; }
            public Dictionary<ElementType, int> Resistances{ get; set; }
            public void TakeDamage(Damage damage){ }

            public List<Buff> Buffs{ get; set; } = new();
        }

        class TestBuff : Buff{
            protected override string GetBuffName() => "test";
        }

        private TestBuffHolder _holder;

        [SetUp]
        public void SetUp(){
            _holder = new TestBuffHolder();
        }
        
        [Test]
        public void Test_Buff_normal(){
            _holder.AddBuffLayer<TestBuff>(1);
            Assert.AreEqual(1, _holder.Buffs.Count);
            
            var buff = _holder.GetBuffOfType<TestBuff>();
            Assert.NotNull(buff);
            Assert.AreEqual(1, buff.Layer);
            Assert.That(buff.Name, Is.EqualTo("test"));
            Assert.That(buff.DisplayName, Is.EqualTo("Test"));

            _holder.RemoveBuffLayer<TestBuff>(1);
            Assert.AreEqual(0, buff.Layer);
            Assert.IsEmpty(_holder.Buffs);
        }
    }
}
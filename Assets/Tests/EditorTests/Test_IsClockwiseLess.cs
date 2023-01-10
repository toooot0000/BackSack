using NUnit.Framework;
using UnityEngine;
using Utility.Extensions;

namespace Tests.EditorTests{
    public class Test_IsClockwiseLess{
        
        [Test]
        public void Test_IsClockWiseLess(){
            Assert.That(Vector2Int.up.IsClockwiseLess(Vector2Int.right));
            Assert.That(Vector2Int.left.IsClockwiseLess(Vector2Int.up));
        }
        
    }
}
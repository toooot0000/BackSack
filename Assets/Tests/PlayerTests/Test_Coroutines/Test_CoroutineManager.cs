using System.Collections;
using Coroutines;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.PlayerTests.Test_Coroutines{
    public class Test_CoroutineManager{

        public int I;
        public int J;

        [UnityTest]
        public IEnumerator Test_MultipleNormal(){
            I = 0;
            J = 0;
            var manager = new GameObject().AddComponent<CoroutineManager>();
            var cd = new CoroutineData(UpdateI);
            var iHandler = manager.SubmitAnimation(cd);

            cd = new CoroutineData(UpdateJ);
            var jHandler = manager.SubmitAnimation(cd);
            
            Assert.That(iHandler, Is.Not.Null);
            Assert.That(jHandler, Is.Not.Null);
            Assert.That(iHandler.IsFinished == false);
            Assert.That(iHandler.IsStarted == false);
            Assert.That(jHandler.IsFinished == false);
            Assert.That(jHandler.IsStarted == false);

            yield return null;
            Assert.That(iHandler.IsFinished == false);
            Assert.That(iHandler.IsStarted);
            Assert.That(jHandler.IsFinished == false);
            Assert.That(jHandler.IsStarted);
            
            yield return null;
            Assert.That(iHandler.IsFinished == false);
            Assert.That(iHandler.IsStarted);
            Assert.That(jHandler.IsFinished == false);
            Assert.That(jHandler.IsStarted);
            
            yield return null;
            Assert.That(iHandler.IsFinished == false);
            Assert.That(iHandler.IsStarted);
            Assert.That(jHandler.IsFinished);
            Assert.That(jHandler.IsStarted);
            
            yield return null;
            Assert.That(iHandler.IsFinished);
            Assert.That(iHandler.IsStarted);
            Assert.That(jHandler.IsFinished);
            Assert.That(jHandler.IsStarted);
            
        }
        
        [UnityTest]
        public IEnumerator Test_DelayedJ(){
            I = 0;
            J = 0;
            var manager = new GameObject().AddComponent<CoroutineManager>();
            var cd = new CoroutineData(UpdateI);
            var iHandler = manager.SubmitAnimation(cd);

            cd = new CoroutineData(() => I == 1 , UpdateJ);
            var jHandler = manager.SubmitAnimation(cd);
            
            Assert.That(iHandler, Is.Not.Null);
            Assert.That(jHandler, Is.Not.Null);
            Assert.That(iHandler.IsFinished == false);
            Assert.That(iHandler.IsStarted == false);
            Assert.That(jHandler.IsFinished == false);
            Assert.That(jHandler.IsStarted == false);

            yield return null;
            Assert.That(iHandler.IsFinished == false);
            Assert.That(iHandler.IsStarted);
            Assert.That(jHandler.IsFinished == false);
            Assert.That(jHandler.IsStarted == false);
            
            yield return null;
            Assert.That(iHandler.IsFinished == false);
            Assert.That(iHandler.IsStarted);
            Assert.That(jHandler.IsFinished == false);
            Assert.That(jHandler.IsStarted);
            
            yield return null;
            Assert.That(iHandler.IsFinished == false);
            Assert.That(iHandler.IsStarted);
            Assert.That(jHandler.IsFinished == false);
            Assert.That(jHandler.IsStarted);
            
            yield return null;
            Assert.That(iHandler.IsFinished);
            Assert.That(iHandler.IsStarted);
            Assert.That(jHandler.IsFinished);
            Assert.That(jHandler.IsStarted);
        }
        
        [UnityTest]
        public IEnumerator Test_Cancel(){
            I = 0;
            var manager = new GameObject().AddComponent<CoroutineManager>();
            var cd = new CoroutineData(UpdateI);
            var handler = manager.SubmitAnimation(cd);
            Assert.That(handler, Is.Not.Null);
            Assert.That(handler.IsFinished, Is.False);
            Assert.That(handler.IsStarted, Is.False);
            Assert.That(I == 0);
            
            yield return null;
            Assert.That(handler.IsFinished, Is.False);
            Assert.That(handler.IsStarted, Is.True);
            
            yield return null;
            Assert.That(handler.IsFinished, Is.False);
            Assert.That(handler.IsStarted, Is.True);
            Assert.That(!handler.IsCanceled);
            Assert.That(manager.CancelAnimation(handler));
            Assert.That(handler.IsCanceled);
            Assert.That(manager.CancelAnimation(handler) == false);
            Assert.That(I != 3);

            yield return null;
            Assert.That(I != 3);
            
        }

        private IEnumerator UpdateI(){
            I++;
            yield return null;
            I++;
            yield return null;
            I++;
            yield return null;
        }

        private IEnumerator UpdateJ(){
            J++;
            yield return null;
            J++;
            yield return null;
        }
    }

    public class Test_CoroutineManager_Single{
        private int I = 0;
        
        [UnityTest]
        public IEnumerator Test_Single(){
            I = 0;
            var manager = new GameObject().AddComponent<CoroutineManager>();
            var cd = new CoroutineData(UpdateI);
            var handler = manager.SubmitAnimation(cd);
            Assert.That(handler, Is.Not.Null);
            Assert.That(handler.IsFinished, Is.False);
            Assert.That(handler.IsStarted, Is.False);
            Assert.That(I == 0);
            
            yield return null;
            Assert.That(handler.IsFinished, Is.False);
            Assert.That(handler.IsStarted, Is.True);
            
            yield return null;
            Assert.That(handler.IsFinished, Is.False);
            Assert.That(handler.IsStarted, Is.True);
            
            yield return null;
            Assert.That(handler.IsFinished, Is.False);
            Assert.That(handler.IsStarted, Is.True);
            
            yield return null;
            Assert.That(handler.IsFinished, Is.True);
            Assert.That(handler.IsStarted, Is.True);
            Assert.That(I == 3);
        }
        
        private IEnumerator UpdateI(){
            I++;
            yield return null;
            I++;
            yield return null;
            I++;
            yield return null;
        }
    }

    public class Test_CoroutineManage_MultiDispatch{
        private int I;
        private int J;
        private IEnumerator UpdateI(){
            I++;
            yield return null;
            I++;
            yield return null;
            I++;
            yield return null;
        }

        private IEnumerator UpdateJ(){
            J++;
            yield return null;
            J++;
            yield return null;
        }
        
        [UnityTest]
        public IEnumerator Test_MultiDispatch(){
            I = 0;
            J = 0;
            var manager = new GameObject().AddComponent<CoroutineManager>();
            var cd = new CoroutineData(UpdateI);
            var iHandler = manager.SubmitAnimation(cd);

            cd = new CoroutineData(UpdateJ);
            Assert.That(iHandler, Is.Not.Null);
            Assert.That(iHandler.IsFinished == false);
            Assert.That(iHandler.IsStarted == false);
            
            yield return null;
            var jHandler = manager.SubmitAnimation(cd);
            Assert.That(jHandler, Is.Not.Null);
            Assert.That(jHandler.IsFinished == false);
            Assert.That(jHandler.IsStarted == false);
            Assert.That(iHandler.IsFinished == false);
            Assert.That(iHandler.IsStarted);


            yield return null;
            Assert.That(iHandler.IsFinished == false);
            Assert.That(iHandler.IsStarted);
            Assert.That(jHandler.IsFinished == false);
            Assert.That(jHandler.IsStarted == false);
            
            yield return null;
            Assert.That(iHandler.IsFinished == false);
            Assert.That(iHandler.IsStarted);
            Assert.That(jHandler.IsFinished == false);
            Assert.That(jHandler.IsStarted == false);
            
            
            yield return null;
            Assert.That(iHandler.IsFinished, Is.True);
            Assert.That(iHandler.IsStarted, Is.True);
            Assert.That(I == 3);
            Assert.That(jHandler.IsFinished == false);
            Assert.That(jHandler.IsStarted == false);

            yield return null;
            Assert.That(jHandler.IsFinished == false);
            Assert.That(jHandler.IsStarted);
            
            yield return null;
            Assert.That(jHandler.IsFinished == false);
            Assert.That(jHandler.IsStarted);
            
            yield return null;
            Assert.That(jHandler.IsFinished);
            Assert.That(jHandler.IsStarted);
            Assert.That(J == 2);
        }
    }
}
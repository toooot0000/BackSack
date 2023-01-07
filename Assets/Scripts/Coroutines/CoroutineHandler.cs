namespace Coroutines{
    public class CoroutineHandler{
        public bool IsStarted{ internal set; get; } = false;
        public bool IsFinished{ internal set; get; } = false;
        public bool IsCanceled{ internal set; get; } = false;
        internal CoroutineData Data = null;
    }
}
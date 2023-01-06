namespace Utility.Animation{
    public interface IAnimatorArgument{ };

    public interface IAnimatorArgumentNonTyped{
        bool SetUp(IAnimator animator);
    }

    public interface IAnimatorArgumentTyped<in T>: IAnimatorArgument where T : IAnimator{
        void SetUp(T animator);
    }
}
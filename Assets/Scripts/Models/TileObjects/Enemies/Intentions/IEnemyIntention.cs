using Models.EffectInfo;

namespace Models.TileObjects.Enemies.Intentions{
    public interface IEnemyIntention{
        IEffect Execute(Enemy enemy);
    }
}
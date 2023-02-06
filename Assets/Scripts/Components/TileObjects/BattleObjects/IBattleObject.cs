using Components.Buffs;
using Components.Damages;
using Components.TileObjects.ForceMovables;

namespace Components.TileObjects.BattleObjects{
    public interface IBattleObject: IForceMovable, IDamageable, IBuffHolder{}
}
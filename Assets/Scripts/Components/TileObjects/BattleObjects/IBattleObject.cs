using Components.Buffs;
using Components.Damages;
using Components.TileObjects.Movables;

namespace Components.TileObjects.BattleObjects{
    public interface IBattleObject: IMovable, IDamageable, IBuffHolder{}
}
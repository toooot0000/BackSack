using System.Collections;
using Components.Damages;
using Components.TileObjects.ForceMovables;

namespace Components.TileObjects.BattleObjects{
    public interface IBattleObjectView: IForceMovableView{
        void TakeDamage(IDamage damage);
        void AddBuff(string buffName);
        IEnumerator Die();
    }
}
using System.Collections;
using Components.Damages;
using Components.TileObjects.Movables;

namespace Components.TileObjects.BattleObjects{
    public interface IBattleObjectView: IMovableView{
        void TakeDamage(IDamage damage);
        void AddBuff(string buffName);
        IEnumerator Die();
    }
}
using System.Collections.Generic;
using Components.Effects;
using Components.SelectMaps;
using UnityEngine;

namespace Components.Enemies.Intentions{
    public interface IEnemyIntention{
        IEffect DoAction();
        void Label(SelectMap map);
    }
}
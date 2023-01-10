using Components.Effects;
using MVC;

namespace Components.Buffs.Effects{
    public interface IChangeBuff<TBuff> : IBuffEffect where TBuff : Buff{ }
    
    public class ChangeBuffEffect<TBuff>: IChangeBuff<TBuff> where TBuff: Buff, new(){
        public ChangeBuffEffect(IBuffHolder target, int changeNumber){
            ChangeNumber = changeNumber;
            _target = target;
        }
        public int ChangeNumber{  get; }

        private IBuffHolder _target;

        public IController Target{
            set{
                if (value is not IBuffHolder holder) return;
                _target = holder;
            }
            get => _target;
        }
        public IController Source{ set; get; } = null;

        public void Apply(){
            if(ChangeNumber>0)
                _target.AddBuffLayer<TBuff>(ChangeNumber);
            else 
                _target.RemoveBuffLayer<TBuff>(ChangeNumber);
        }

        public string GetDisplayName() => _target.GetBuffOfType<TBuff>().DisplayName;

        public ElementType Element => ElementType.Physic;
    }
}
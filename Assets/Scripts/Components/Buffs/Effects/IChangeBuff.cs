namespace Components.Buffs.Effects{
    public interface IChangeBuff<TBuff>: IBuffEffect where TBuff: Buff{ }
    
    public class ChangeBuffEffect<TBuff>: IChangeBuff<TBuff> where TBuff: Buff, new(){
        public ChangeBuffEffect(IBuffHolderModel target, int changeNumber){
            ChangeNumber = changeNumber;
            Target = target;
        }
        public int ChangeNumber{  get; }
        public IBuffHolderModel Target{  get; }

        public void Apply(){
            if(ChangeNumber>0)
                Target.AddBuffLayer<TBuff>(ChangeNumber);
            else 
                Target.RemoveBuffLayer<TBuff>(ChangeNumber);
        }

        public string GetDisplayName() => Target.GetBuffOfType<TBuff>().DisplayName;
    }
}
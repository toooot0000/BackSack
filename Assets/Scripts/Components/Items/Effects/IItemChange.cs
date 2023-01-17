using Components.Effects;
using MVC;

namespace Components.Items.Effects{
    public interface IItemChange: IEffect{
        ItemModel Item{ get; }
        int ChangeNum{ get; }
    }

    public class ItemChange : IItemChange, IEffectTemplate{
        public ItemChange(ItemModel item, int changeNum){
            Item = item;
            ChangeNum = changeNum;
        }
        public IEffectConsumer Target{ get; set; }
        public IController Source{ get; set; }
        public IEffect ToEffect() => this;
        public ItemModel Item{ get; }
        public int ChangeNum{ get; }
    } 
}
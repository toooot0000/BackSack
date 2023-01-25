using System.Collections.Generic;
using System.Linq;
using Components.Effects;
using Components.Items.Effects;
using MVC;

namespace Components.Items{
    public interface IItemHolder : IController, ICanConsume<IItemChange>{
        IEnumerable<ItemModel> GetAllItems();
    }

    public static class ItemHolderExtension{
        public static T GetItem<T>(this IItemHolder holder) where T : ItemModel{
            return holder.GetAllItems().OfType<T>().FirstOrDefault();
        }
        
        
    }
}
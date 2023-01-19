using System.Collections.Generic;
using Components.Effects;
using Components.Items.Effects;
using MVC;

namespace Components.Items{
    public interface IItemHolder : IController, ICanConsume<IItemChange>{
        IEnumerator<ItemModel> GetAllItems();
    }

    // public static class ItemHolderExtension{
    //     public static T GetItem<T>(this IItemHolder holder) where T: ItemModel{
    //         
    //     } 
    // }
}
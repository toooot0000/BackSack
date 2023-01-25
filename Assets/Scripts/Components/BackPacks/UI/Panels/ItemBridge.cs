using UnityEngine;
using UnityEngine.EventSystems;

namespace Components.BackPacks.UI.Panels{
    public class ItemBridge: MonoBehaviour, IPointerClickHandler{
        public ItemBlock block;
        // protected override void OnLongTouch(){
        //     block.OnLongTouched();
        // }

        public void OnPointerClick(PointerEventData eventData){
            block.OnClicked();
        }
    }
}
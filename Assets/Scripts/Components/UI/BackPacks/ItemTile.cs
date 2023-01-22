using Components.UI.Common;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Components.UI.BackPacks{
    public class ItemTile: MonoBehaviour, IPointerClickHandler{
        public ItemBridge rightBridge;
        public ItemBridge downBridge;
        public ItemBlock block;
        // protected override void OnLongTouch(){
        //     block.OnLongTouched();
        // }

        public void OnPointerClick(PointerEventData eventData){
            block.OnClicked();
        }
    }
}
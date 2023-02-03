using Components.BackPacks.UI.Panels.ItemBlocks.ShapeBlocks;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Components.BackPacks.UI.Panels.ItemBlocks{
    public class ItemTile: Tile, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler{
        public ItemBlock block;

        public void OnPointerClick(PointerEventData eventData){
            if (block == null) return;
            block.OnClicked();
        }

        public void OnPointerDown(PointerEventData eventData){
            if (block == null) return;
            block.OnPointerDown();
        }

        public void OnPointerUp(PointerEventData eventData){
            if (block == null) return;
            block.OnPointUp();
        }
    }
}
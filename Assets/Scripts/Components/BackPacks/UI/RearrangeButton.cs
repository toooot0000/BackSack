using Components.BackPacks.UI.Panels;
using UnityEngine;

namespace Components.BackPacks.UI{
    public class RearrangeButton: MonoBehaviour{
        public BackPackPanel panel;
        
        public void OnClicked(){
            if (panel.IsRearranging){
                panel.CompleteRearrange();
            } else{
                panel.StartRearrange();   
            }
        }
    }
}
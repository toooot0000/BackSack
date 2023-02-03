using Components.BackPacks.UI.Panels;
using Components.BackPacks.UI.Windows;
using Components.UI;
using Components.UI.Common;
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
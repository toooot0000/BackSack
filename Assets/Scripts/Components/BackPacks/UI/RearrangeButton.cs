using Components.BackPacks.UI.Windows;
using Components.UI;
using Components.UI.Common;
using UnityEngine;

namespace Components.BackPacks.UI{
    public class RearrangeButton: UIComponent{
        public EdgeHider hider;
        
        public void OpenRearrangeWindow(){
            UIManager.Shared.Open<RearrangeWindow>();
        }

        public override void Hide(){
            hider.Hide();
        }

        public override void Show(){
            hider.Show();
        }
    }
}
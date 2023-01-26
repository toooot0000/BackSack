using Components.BackPacks.UI.Panels;
using Components.BackPacks.UI.RearrangeWindows;
using Components.UI;
using Components.UI.Common;
using UnityEngine;

namespace Components.BackPacks.UI{
    public class RearrangeButton: UIComponent{
        public EdgeHider hider;
        public BackPackPanel panel;
        public BackPack backPack;
        public void OpenRearrangeWindow(){
            UIManager.Shared.Open(new RearrangeWindow.Option(backPack));
        }

        public override void Hide(){
            hider.Hide();
        }

        public override void Show(){
            hider.Show();
        }
    }
}
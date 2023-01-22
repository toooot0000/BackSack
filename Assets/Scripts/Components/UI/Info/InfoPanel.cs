using Components.UI.Common;

namespace Components.UI.Info{
    public class InfoPanel: UIComponent{
        public EdgeHider hider;
        public override void Hide(){
            hider.Hide();
        }

        public override void Show(){
            hider.Show();
        }
    }
}
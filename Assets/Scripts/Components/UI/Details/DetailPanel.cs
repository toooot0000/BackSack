using Components.UI.Common;

namespace Components.UI.Details{
    public class DetailPanel: UIComponent{
        public EdgeHider hider;

        public override void Hide(){
            hider.Hide();
        }

        public override void Show(){
            hider.Show();
        }
    }
}
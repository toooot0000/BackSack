using UI.Common;

namespace UI.BackPacks{
    public class CompBackPack: UIComponent{
        public EdgeHider hider;

        public override void Hide(){
            hider.Hide();
        }

        public override void Show(){
            hider.Show();
        }
    }
}
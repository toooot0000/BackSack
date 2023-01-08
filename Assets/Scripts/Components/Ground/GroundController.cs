using MVC;

namespace Components.Ground{
    public class GroundController: Controller, IView{
        public new Ground Model;

        protected override void AfterSetModel(){
            
        }
    }
}
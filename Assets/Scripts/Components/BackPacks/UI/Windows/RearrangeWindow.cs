using Components.BackPacks.UI.Panels;
using Components.UI;
using Components.UI.Attributes;
using UnityEngine;
using Utility;

namespace Components.BackPacks.UI.Windows{
    [Prefab("UIRearrangeWindow")]
    
    public class RearrangeWindow: UIWindow{
        
        public class Option: IUIWindowOptions<RearrangeWindow>{
            private readonly BackPack _backPack;

            public Option(BackPack backPack){
                _backPack = backPack;
            }
            public void ApplyOptions(RearrangeWindow window){
                window.backPack = _backPack;
            }
        }
        [HideInInspector]
        public BackPack backPack;

        public BackPackGrid backPackGrid;
        private ItemBlock[] _blocks;

        private Bindable<int> _width;
        private Bindable<int> _height;

        private void SyncBackpack(){
            
        }

        protected override void OnOpen(){
            SyncBackpack();
        }

        protected override void OnClose(){
            throw new System.NotImplementedException();
        }
    }
}
using Components.BackPacks.UI.Panels;
using Components.UI;
using Components.UI.Attributes;
using UnityEngine;

namespace Components.BackPacks.UI.RearrangeWindows{
    [Prefab("UIRearrangeWindow")]
    
    public class RearrangeWindow: UIWindow{
        
        public class Option: UIOpenOption<RearrangeWindow>{
            private readonly BackPack _backPack;

            public Option(BackPack backPack){
                _backPack = backPack;
            }
            public override void ApplyOptions(RearrangeWindow window){
                window.backPack = _backPack;
            }
        }
        
        [HideInInspector]
        public BackPack backPack;

        public BackPackGrid backPackGrid;
        public CanvasGroup canvasGroup;
        
        private Panels.ItemBlock[] _blocks;

        private void SyncBackpack(){
            backPackGrid.gridWidth.BindSource(backPack.ColNum);
            backPackGrid.gridHeight.BindSource(backPack.RowNum);
            backPackGrid.Resize();
            
        }

        private void Start() {
            canvasGroup.alpha = 0;
        }

        protected override void OnOpen(){
            SyncBackpack();
            StartCoroutine(FadeIn(canvasGroup));
        }

        protected override void OnClose() {
            StartCoroutine(FadeOut(canvasGroup));
        }
    }
}
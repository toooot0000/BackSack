using System.Linq;
using UnityEngine;
using Utility.Animation.Tweens;

namespace Components.TileObjects.Tweens{
    public class Die: Tween{
        public GameObject root;
        private SpriteRenderer[] _renderers;
        
        protected override void Start(){
            base.Start();
            _renderers = root.GetComponentsInChildren<SpriteRenderer>().ToArray();
        }

        protected override void OnTimerUpdate(float i){
            var alpha = 1 - i;
            foreach (var spriteRenderer in _renderers){
                var c = spriteRenderer.color;
                c.a = alpha;
                spriteRenderer.color = c;
            }
        }
    }
}
using System.Collections;
using Components.Attacks;
using Components.Players;
using UnityEngine;
using Utility.Extensions;

namespace Components.Items.Animations{
    public class SwordAnim : ItemAnimator{
        public Transform pivot;
        public float totalTime = .2f;
        public AnimationCurve alphaCurve;
        public SpriteRenderer sprRenderer;
        

        public override IEnumerator Play(IAttack attack){
            gameObject.SetActive(true);
            if (attack is not PlayerAttack playerAttack) yield break;
            var start = Vector2.Angle(playerAttack.Direction.ToVector2Int(), Vector2.right) + 45;
            var end = start - 90;
            var isPassed = false;
            pivot.rotation = Quaternion.Euler(0, 0, start);
            for (var curTime = 0f; curTime < totalTime; curTime += Time.deltaTime){
                var i = curTime / totalTime;
                if(i>0.5 && !isPassed){
                    Result = attack.PassEffects();
                    isPassed = true;
                }
                sprRenderer.color = new Color(1, 1, 1, alphaCurve.Evaluate(i));
                pivot.rotation = Quaternion.Euler(0, 0, Mathf.Lerp(start, end, i));
                yield return null;
            }
            if (!isPassed) Result = attack.PassEffects();
            gameObject.SetActive(false);
        }
    }
}
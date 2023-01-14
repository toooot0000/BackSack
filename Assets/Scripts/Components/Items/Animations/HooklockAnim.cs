using System.Collections;
using System.Linq;
using Components.Attacks;
using Components.Effects;
using Components.Players;
using Components.TileObjects;
using UnityEngine;

namespace Components.Items.Animations{
    public class HooklockAnim:ItemAnimator{

        public LineRenderer lineRenderer;
        public Transform endPoint;
        public SpriteRenderer arrow;
        public float preTime = 0.2f;
        public float postTime = 0.2f;

        private float _curTime;
        private ITileObject _target;
        private IAttacker _source;
        private Vector3 _relativePosition;

        public override IEnumerator Play(IAttack attack){
            if (attack is not PlayerAttack pAtt) yield break;
            gameObject.SetActive(true);
            yield return MoveEndPointToTarget(attack);
            Result = pAtt.PassEffects();
            yield return MoveEndPointToDest(attack);
            ResetState();
            gameObject.SetActive(false);
        }

        private void ResetState(){
            arrow.transform.rotation = Quaternion.Euler(0, 0, 0);
            lineRenderer.startColor = Color.black;
            lineRenderer.endColor = Color.black;
            arrow.color = Color.white;
        }

        private IEnumerator MoveEndPointToTarget(IAttack attack){
            _curTime = 0f;
            _target = attack.Targets.First();
            _source = attack.Attacker;
            _relativePosition = _target.GetWorldPosition() - _source.GetWorldPosition();
            lineRenderer.SetPosition(0, _source.GetWorldPosition());
            arrow.transform.Rotate(axis: new Vector3(0, 0, 1), Vector3.Angle(Vector3.right, _relativePosition));
            while (_curTime < preTime){
                _curTime += Time.deltaTime;
                endPoint.localPosition = Vector3.Lerp(Vector3.zero, _relativePosition, _curTime/preTime);
                lineRenderer.SetPosition(1, endPoint.position);
                yield return null;
            }
        }

        private IEnumerator MoveEndPointToDest(IAttack attack){
            _curTime = 0f;
            while (_curTime < postTime){
                _curTime += Time.deltaTime;
                var i = _curTime / postTime;
                endPoint.localPosition = Vector3.Lerp(_relativePosition, Vector3.zero, i);
                lineRenderer.SetPosition(1, endPoint.position);
                lineRenderer.startColor = new Color(0, 0, 0, 1 - i);
                lineRenderer.endColor = new Color(0, 0, 0, 1 - i);
                arrow.color = new Color(1, 1, 1, 1 - i);
                yield return null;
            }
            
        }

    }
}
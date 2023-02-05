using System;
using System.Collections.Generic;
using Components.BackPacks.UI.Panels.ItemBlocks;
using Components.UI.Containers;
using UnityEngine;
using Utility.Extensions;

namespace Components.BackPacks.UI.Panels.BackupAreas{

    public enum AlignType{
        Start,
        Center,
        End
    }
    
    public class BackupContainer: MonoBehaviour{

        private readonly Dictionary<RectTransform, Transform> _parents = new();
        private readonly List<RectTransform> _children = new();

        public float spacing = 20f;
        /// <summary>
        /// Up, Right, Down, Left
        /// </summary>
        public float[] paddings = new float[]{ 0, 0, 0, 0 };

        public Vector3 GetClosestWorldPosition(RectTransform trans){
            var index = GetInsertIndex(trans);
            if (index == _children.Count){
                var worldRect = ((RectTransform)transform).GetWorldRect();
                return worldRect.position - trans.rect.position;
            }
            var curPos = new Vector2(0, paddings[2]) + ((RectTransform)transform).GetWorldRect().position;
            for (var i = _children.Count - 1; i > index; i--){
                curPos.y += _children[i].rect.height + spacing;
            }
            return curPos - trans.rect.position;
        }

        public int GetInsertIndex(RectTransform trans){
            var mTrans = (RectTransform)transform;
            var mRect = mTrans.rect;
            var transRect = trans.rect;
            var targetPos = new Vector2((mTrans.position.x + mRect.position.x + ((RectTransform)mTrans).rect.size.x/2), trans.position.y + transRect.size.y + transRect.position.y);
            var ret = _children.Count - 1;
            for (; ret >= 0; ret--){
                var child = _children[ret];
                var rect = child.GetWorldRect();
                if (targetPos.y < rect.yMin){
                    return ret + 1;
                }
            }
            return 0;
        }

        public void Insert(int index, RectTransform newChild){
            _children.Insert(index, newChild);
            _parents[newChild] = newChild.parent;
            newChild.SetParent(transform, true);
            SetLayoutDirty();
        }

        public void Remove(RectTransform child){
            if (!_children.Contains(child)) return;
            _children.Remove(child);
            child.SetParent(_parents[child]);
            _parents.Remove(child);
            SetLayoutDirty();
        }

        public void Move(int from, int to){
            (_children[from], _children[to]) = (_children[to], _children[from]);
            SetLayoutDirty();
        }

        public Vector2 GetSize(){
            var ret = Vector2.zero;
            foreach (var child in _children){
                var rect = child.rect;
                ret.y += rect.size.y;
            }

            if (_children.Count > 0){
                ret.y += spacing * (_children.Count - 1);
            }

            ret.y += paddings[0] + paddings[2];
            ret.x = ((RectTransform)transform).rect.size.x;
            return ret;
        }

        private bool _isLayoutDirty = false;
        private void SetLayoutDirty(){
            _isLayoutDirty = true;
            enabled = true;
        }

        private void Update(){
            if (!_isLayoutDirty) return;
            UpdateLayout();
            _isLayoutDirty = false;
            enabled = false;
        }

        private void UpdateLayout(){
            var finalSize = GetSize();
            var trans = (RectTransform)transform;
            trans.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, finalSize.y);
            var corners = new Vector3[4];
            trans.GetWorldCorners(corners);
            // var corners = trans.GetWorldRect();

            var curPos = new Vector2(finalSize.x * 0.5f, paddings[2]) + (Vector2)corners[0];
            for (var i = _children.Count - 1; i >= 0; i--){
                var child = _children[i];
                var size = child.rect.size;
                // child.position = curPos + new Vector2(0, size.y / 2);
                child.SetCenterPosition(new Vector2(curPos.x, curPos.y + size. y * .5f));
                curPos.y += size.y + spacing;
            }

        }
    }
}
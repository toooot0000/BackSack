using System;
using System.Collections.Generic;
using UnityEngine;
using Utility.Extensions;

namespace Components.BackPacks.UI.Panels.BackupAreas.Containers{

    public enum AlignType{
        Start,
        Center,
        End
    }

    public class BackupContainer: MonoBehaviour{

        private readonly Dictionary<RectTransform, Transform> _parents = new();

        private readonly List<IBackupManageable> _manageables = new();
        /// <summary>
        /// [y0, y1, y2 ...]
        /// [y0, y1] -> child[0]'s y range supposedly
        /// [y1, y2] -> child[1]'s .....
        /// </summary>
        private readonly List<float> _idealYPositions = new();

        public float spacing = 20f;
        /// <summary>
        /// Up, Right, Down, Left
        /// </summary>
        public float[] paddings = new float[]{ 0, 0, 0, 0 };

        private RectTransformManageable Insert(int index, RectTransform newChild){
            var ret = new RectTransformManageable(newChild);
            _manageables.Insert(index, ret);
            _parents[newChild] = newChild.parent;
            newChild.SetParent(transform, true);
            SetLayoutDirty();
            UpdateIdeaYPositions();
            return ret;
        }

        public void Remove(RectTransform child){
            var i = 0;
            while (i < _manageables.Count && _manageables[i].Transform != child){
                i++;
            }
            if (i == _manageables.Count) return;
            Remove(_manageables[i]);
        }

        public void Remove(IBackupManageable manageable){
            _manageables.Remove(manageable);
            SetLayoutDirty();
            if (manageable is PlaceholderManageable) return;
            UpdateIdeaYPositions();
            var rectTrans = manageable.Transform;
            rectTrans.SetParent(_parents[rectTrans]);
            _parents.Remove(rectTrans);
        }


        private Vector2 GetSize(){
            var ret = Vector2.zero;
            foreach (var child in _manageables){
                var rect = child.GetSize();
                ret.y += rect.y;
            }

            if (_manageables.Count > 0){
                ret.y += spacing * (_manageables.Count - 1);
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

        private bool _isIdealYPositionDirty = false;
        private void SetIdealYPositionsDirty(){
            _isIdealYPositionDirty = true;
            enabled = true;
        }

        private void Update(){
            if (_isLayoutDirty){
                UpdateLayout();
                _isLayoutDirty = false;
            }

            if (_isIdealYPositionDirty){
                UpdateIdeaYPositions();
                _isIdealYPositionDirty = false;
            }
            enabled = false;
        }

        private void UpdateLayout(){
            var finalSize = GetSize();
            var trans = (RectTransform)transform;
            trans.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, finalSize.y);
            var corners = new Vector3[4];
            trans.GetWorldCorners(corners);
            var curPos = new Vector2(finalSize.x * 0.5f, paddings[2]) + (Vector2)corners[0];
            for (var i = _manageables.Count - 1; i >= 0; i--){
                var child = _manageables[i];
                var size = child.GetSize();
                child.SetCenterPosition(new Vector2(curPos.x, curPos.y + size. y * .5f));
                curPos.y += size.y + spacing;
            }
        }

        private void UpdateIdeaYPositions(){
            _idealYPositions.Clear();
            var trans = (RectTransform)transform;
            var corners = new Vector3[4];
            trans.GetWorldCorners(corners);
            var curPos = corners[1].y - paddings[0];
            _idealYPositions.Add(curPos);
            foreach (var manageable in _manageables){
                curPos -= (manageable.GetSize().y + spacing);
                _idealYPositions.Add(curPos);
            }
        }

        private int GetIndex(float yPosition){
            var ret = 0;
            for (; ret + 1 < _idealYPositions.Count; ret++){
                var pivot = (_idealYPositions[ret] + _idealYPositions[ret + 1]) / 2;
                if (yPosition > pivot) return ret;
            }

            return ret;
        }

        private PlaceholderManageable _placeholder;

        public void InsertPlaceHolder(RectTransform rectTransform, float mouseY){
            if (_placeholder != null){
                UpdatePlaceholder(mouseY);
                return;
            }
            var index = GetIndex(mouseY);
            _placeholder = new PlaceholderManageable(rectTransform){
                CurIndex = index
            };
            
            _manageables.Insert(index, _placeholder);
            SetLayoutDirty();
        }

        public void UpdatePlaceholder(float mouseY){
            var newIndex = GetIndex(mouseY);
            if (_placeholder.CurIndex == newIndex) return;
            _manageables.Remove(_placeholder);
            _manageables.Insert(newIndex, _placeholder);
            _placeholder.CurIndex = newIndex;
            SetLayoutDirty();
        }

        public void ReplacePlaceholder(IBackupManageable manageable){
            var trans = manageable.Transform;
            _parents[trans] = trans.parent;
            trans.SetParent(transform, true);
            _manageables[_placeholder.CurIndex] = manageable;
            _placeholder = null;
            SetLayoutDirty();
            UpdateIdeaYPositions();
        }

        public void RemovePlaceholder(){
            Remove(_placeholder);
            _placeholder = null;
        }

        public void Add(IBackupManageable manageable){
            var trans = manageable.Transform;
            _manageables.Add(manageable);
            _parents[trans] = trans.parent;
            trans.SetParent(transform, true);
            SetLayoutDirty();
            UpdateIdeaYPositions();
        }

        private void Start(){
            UpdateIdeaYPositions();
        }
    }
}
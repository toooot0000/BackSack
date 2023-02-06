using System;
using UnityEngine;

namespace Utility.Extensions{
    public static class RectTransformExtension{
        public static Rect GetWorldRect(this RectTransform trans){
            var corners = new Vector3[4];
            trans.GetWorldCorners(corners);
            return new Rect(corners[0], corners[2] - corners[0]);
        }

        public static Rect GetWorldRect(this RectTransform trans, Vector3[] corners){
            if (corners.Length != 4) throw new Exception("Corners' length is not 4!");
            trans.GetWorldCorners(corners);
            return new Rect(corners[0], corners[2] - corners[0]);
        }

        public static void SetWorldRect(this RectTransform trans, Rect rect){
            trans.position = rect.position - trans.rect.position;
            trans.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, rect.width);
            trans.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, rect.height);
        }

        public static void SetCenterPosition(this RectTransform trans, Vector2 pos){
            var corners = new Vector3[4];
            trans.GetWorldCorners(corners);
            var curCenter = (Vector2)(corners[0] + corners[2]) * 0.5f;
            trans.position += (Vector3)(pos - curCenter);
        }
        
        public static void SetCenterPosition(this RectTransform trans, Vector2 pos, Vector3[] corners){
            if (corners.Length != 4) throw new Exception("Corners' length is not 4!");
            trans.GetWorldCorners(corners);
            var curCenter = (Vector2)(corners[0] + corners[2]) * 0.5f;
            trans.position += (Vector3)(pos - curCenter);
        }

        public static Vector3 TransformCenterToWorld(this RectTransform trans, Vector2 pos, Vector3[] corners){
            if (corners.Length != 4) throw new Exception("Corners' length is not 4!");
            trans.GetWorldCorners(corners);
            var curCenter = (Vector2)(corners[0] + corners[2]) * 0.5f;
            return trans.position + (Vector3)(pos - curCenter);
        }
    }
}
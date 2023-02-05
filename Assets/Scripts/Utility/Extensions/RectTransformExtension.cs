using UnityEngine;

namespace Utility.Extensions{
    public static class RectTransformExtension{
        public static Rect GetWorldRect(this RectTransform trans){
            var ret = trans.rect;
            ret.position = (Vector3)ret.position + trans.position;
            return ret;
        }

        public static void SetWorldRect(this RectTransform trans, Rect rect){
            trans.position = rect.position - trans.rect.position;
            trans.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, rect.width);
            trans.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, rect.height);
        }
    }
}
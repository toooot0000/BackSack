using UnityEngine;

namespace Utility{
    public static class InputUtility{
        private static Camera _main;
        public static Vector3 GetMouseWorldPosition(Camera camera){
            return camera.ScreenToWorldPoint(Input.mousePosition);
        }
    }
}
using UnityEngine;

namespace CarParking.Game.Custom
{
    [System.Serializable]
    public class RaycastDetector
    {
        private LayerMask whatIsMask;
        public struct RaycastInfor
        {
            public bool isContact;
            public Vector3 point;
            public Transform transform;
            public Collider colider;
        }

        public RaycastDetector(LayerMask _mask)
        {
            this.whatIsMask = _mask;
        }

        public RaycastInfor GetInforRaycast()
        {
            Ray ray = new Ray();

            if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
            {
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            }
            else if (Application.platform == RuntimePlatform.Android)
            {
                Touch touch = Input.GetTouch(0);
                ray = Camera.main.ScreenPointToRay(touch.position);
            }



            bool hitPoint = Physics.Raycast(ray, out RaycastHit hitInfor, float.PositiveInfinity, whatIsMask);

            return new RaycastInfor
            {
                isContact = hitPoint,
                point = hitInfor.point,
                transform = hitInfor.transform,
                colider = hitInfor.collider
            };
        }
    }
}

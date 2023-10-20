using UnityEngine;

namespace _Scripts.Data.Colliders
{
    public class CapsuleColliderData
    {
        public CapsuleCollider Collider;
        public Vector3 ColliderCenterInLocalSpace;

        public void Initialize(CapsuleCollider collider)
        {
            if (Collider != null) return;
                
            Collider = collider;
            UpdateColliderData();
        }

        public void UpdateColliderData()
        {
            ColliderCenterInLocalSpace = Collider.center;
        }
    }
}
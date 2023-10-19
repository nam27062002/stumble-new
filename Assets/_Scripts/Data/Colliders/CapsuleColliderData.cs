using UnityEngine;

namespace _Scripts.Data.Colliders
{
    public class CapsuleColliderData
    {
        public CapsuleCollider Collider;
        public Vector3 ColliderCenterInLocalSpace;

        public void Initialize(GameObject gameObject)
        {
            if (Collider != null)
            {
                return;
            }

            Collider = gameObject.GetComponent<CapsuleCollider>();
            
            UpdateColliderData();
        }

        private void UpdateColliderData()
        {
            ColliderCenterInLocalSpace = Collider.center;
        }
    }
}
using UnityEngine;

namespace _Scripts.Data.Colliders
{
    public class DefaultColliderData
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
            ColliderCenterInLocalSpace = Collider.center;
        }
    }
}
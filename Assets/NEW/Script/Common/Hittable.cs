using UnityEngine;

namespace ScriptGG
{
    public interface IHittable
    {
        void OnHit(float damage);
    }

    // TODO Might use this to wrap the hit in an event like object to enhance parameters without refactoring everything
    public struct HitEvent
    {
        public float damage;
        private Vector3 hitPosition;
    }
}

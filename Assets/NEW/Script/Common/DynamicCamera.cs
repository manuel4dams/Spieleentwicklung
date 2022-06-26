using UnityEngine;

namespace ScriptGG
{
    public class DynamicCamera : MonoBehaviour
    {
        public Transform target;
        public float lerp;

        private Vector3 offset;

        void Start()
        {
            offset = transform.position - target.position;
        }

        void FixedUpdate()
        {
            if (!target)
                return;

            var targetCameraPosition = target.position + offset;
            transform.position = Vector3.Lerp(transform.position, targetCameraPosition, lerp * Time.fixedDeltaTime);
        }
    }
}

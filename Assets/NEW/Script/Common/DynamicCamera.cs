using UnityEngine;

namespace ScriptGG
{
    public class DynamicCamera : MonoBehaviour
    {
        public Transform target;
        public Vector3 offset;
        public bool calculateOffsetOnStart;
        public float lerp;

        void Start()
        {
            if (calculateOffsetOnStart)
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

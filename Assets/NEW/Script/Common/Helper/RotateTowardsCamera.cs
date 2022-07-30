using MyBox;
using UnityEngine;

namespace ScriptGG
{
    public class RotateTowardsCamera : MonoBehaviour
    {
        [Header("References")] //
        public Transform target;
        public bool autoAssignSelfTransform = true;
        public Transform source;
        public bool autoAssignMainCameraTransform = true;

        [Header("Settings")] //
        public bool useX;
        public bool useY = true;
        public bool useZ;

        void Start()
        {
            if (autoAssignSelfTransform)
                target = transform;
            if (autoAssignMainCameraTransform)
                source = Camera.main!.transform;
        }

        void LateUpdate()
        {
            Handle();
        }

        private void Handle()
        {
            var sourceAngles = source.eulerAngles;
            var angles = target.eulerAngles;

            if (useX) angles = angles.SetX(sourceAngles.x);
            if (useY) angles = angles.SetY(sourceAngles.y);
            if (useZ) angles = angles.SetZ(sourceAngles.z);

            target.rotation = Quaternion.Euler(angles);
        }
    }
}
